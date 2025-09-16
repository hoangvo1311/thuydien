using IndusG.Models.Setting;
using IndusG.Service;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(IndusG.Web.Startup))]

namespace IndusG.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            // Start background task
            Task.Run(() => OperationPLCTask());
            Task.Run(() => SendLatestMeasurementToDaksrongTask());
        }

        private async Task OperationPLCTask()
        {
            while (true)
            {
                try
                {
                    if (PlcService.IsWritingToPLC)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(500));
                    }
                    else
                    {

                        var plcMonitoringModel = new PLCMonitoringModel
                        {
                            DrainLevel1 = PlcService.ReadReal(AddressConstant.DrainLevel1),
                            DrainLevel2 = PlcService.ReadReal(AddressConstant.DrainLevel2),

                            CV1Bottom = PlcService.ReadBool(AddressConstant.CV1Bottom),
                            CV1Overload = PlcService.ReadBool(AddressConstant.CV1Overload),
                            CV1Running = PlcService.ReadBool(AddressConstant.CV1Running),
                            CV2Bottom = PlcService.ReadBool(AddressConstant.CV2Bottom),
                            CV2Overload = PlcService.ReadBool(AddressConstant.CV2Overload),
                            CV2Running = PlcService.ReadBool(AddressConstant.CV2Running),

                            ManBit = PlcService.ReadBool(AddressConstant.Man_ON),
                            RemoteBit = PlcService.ReadBool(AddressConstant.Remote_ON),
                            SimulationBit = PlcService.ReadBool(AddressConstant.Simulation),
                            ScheduleBit = PlcService.ReadBool(AddressConstant.ScheduleBit),

                            KU1 = PlcService.ReadBool(AddressConstant.KU1),
                            KD1 = PlcService.ReadBool(AddressConstant.KD1),
                            KU2 = PlcService.ReadBool(AddressConstant.KU2),
                            KD2 = PlcService.ReadBool(AddressConstant.KD2),

                            KU1_SIM = PlcService.ReadBool(AddressConstant.KU1_SIM),
                            KD1_SIM = PlcService.ReadBool(AddressConstant.KD1_SIM),
                            KU2_SIM = PlcService.ReadBool(AddressConstant.KU2_SIM),
                            KD2_SIM = PlcService.ReadBool(AddressConstant.KD2_SIM),
                        };

                        var plcService = new PlcService();
                        plcMonitoringModel.PLCLiveBit = true;
                        plcService.UpdatePLCMonitoring(plcMonitoringModel);

                        var settingSerivice = new SettingService();
                        var currentSetting = settingSerivice.GetSettingModel();

                        var now = DateTime.Now;
                        int today = (int)now.DayOfWeek;

                        var isSchduleBitUpdated = false;

                        if (currentSetting.PLCSetting.ScheduleSwitch && currentSetting.PLCSetting.ScheduleDayOn > -1 && currentSetting.PLCSetting.ScheduleDayOff > -1)
                        {
                            bool shouldBeOn = false;

                            if (currentSetting.PLCSetting.ScheduleDayOn == currentSetting.PLCSetting.ScheduleDayOff)
                            {
                                // Cùng 1 ngày
                                if (today == currentSetting.PLCSetting.ScheduleDayOn &&
                                    now.TimeOfDay >= currentSetting.PLCSetting.ScheduleTimeOn &&
                                    now.TimeOfDay < currentSetting.PLCSetting.ScheduleTimeOff)
                                {
                                    shouldBeOn = true;
                                }
                            }
                            else
                            {
                                // Trường hợp khác ngày (ví dụ ON thứ 6, OFF chủ nhật)
                                if (today == currentSetting.PLCSetting.ScheduleDayOn && now.TimeOfDay >= currentSetting.PLCSetting.ScheduleTimeOn)
                                {
                                    shouldBeOn = true;
                                }
                                else if (today == currentSetting.PLCSetting.ScheduleDayOff && now.TimeOfDay < currentSetting.PLCSetting.ScheduleTimeOff)
                                {
                                    shouldBeOn = true;
                                }
                                else if (today > currentSetting.PLCSetting.ScheduleDayOn && today < currentSetting.PLCSetting.ScheduleDayOff)
                                {
                                    shouldBeOn = true;
                                }
                                // Nếu lịch chạy qua tuần (vd: ON Thứ 6, OFF Thứ 2)
                                else if (currentSetting.PLCSetting.ScheduleDayOn > currentSetting.PLCSetting.ScheduleDayOff)
                                {
                                    if (today > currentSetting.PLCSetting.ScheduleDayOn || today < currentSetting.PLCSetting.ScheduleDayOff)
                                        shouldBeOn = true;
                                    else if (today == currentSetting.PLCSetting.ScheduleDayOn && now.TimeOfDay >= currentSetting.PLCSetting.ScheduleTimeOn)
                                        shouldBeOn = true;
                                    else if (today == currentSetting.PLCSetting.ScheduleDayOff && now.TimeOfDay < currentSetting.PLCSetting.ScheduleTimeOff)
                                        shouldBeOn = true;
                                }
                            }

                            // Chỉ update nếu trạng thái khác
                            if (plcMonitoringModel.ScheduleBit != shouldBeOn)
                            {
                                PlcService.WriteToPLC(AddressConstant.ScheduleBit, shouldBeOn);
                                isSchduleBitUpdated = true;
                                LoggerHelper.Info($"Bit turned {(shouldBeOn ? "ON" : "OFF")} by schedule.");
                            }
                        }


                        if (!isSchduleBitUpdated)
                            await Task.Delay(TimeSpan.FromMilliseconds(1));
                    }
                }
                catch (Exception ex)
                {
                    LoggerHelper.Error("Error in PLC background task: " + ex.Message);
                    await Task.Delay(TimeSpan.FromSeconds(30));
                }
            }
        }

        private async Task SendLatestMeasurementToDaksrongTask()
        {
            // Force TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var handler = new HttpClientHandler()
            {
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12
            };

            var httpClient = new HttpClient(handler);

            var measurementService = new MeasurementService();
            while (true)
            {
                try
                {
                    var latestMeasurement = measurementService.GetLatestMeasurement();
                    if (latestMeasurement != null && latestMeasurement.Date > new DateTime(2025, 01, 01))
                    {
                        // Prepare the JSON payload
                        var payload = new
                        {
                            upstream_waterlevel_m = latestMeasurement.UpstreamWaterLevel_m,
                            downstream_waterlevel_m = latestMeasurement.DownstreamWaterLevel_m,
                            qve_ho = latestMeasurement.Qve_Ho,
                            qoverflow = latestMeasurement.Qoverflow,
                            qcmh1h2h3 = latestMeasurement.QcmH1H2H3,
                            qminflow = latestMeasurement.Qminflow,
                            qve_hadu = latestMeasurement.Qve_HaDu,
                            drain_level1 = latestMeasurement.Drain_Level1,
                            drain_level2 = latestMeasurement.Drain_Level2,
                            qve_hodb = latestMeasurement.Qve_HoDB,
                            reserve_water = latestMeasurement.Reserve_Water,
                            qminflow_tt = latestMeasurement.Qminflow_TT,
                            h1_mw = latestMeasurement.H1_MW,
                            h2_mw = latestMeasurement.H2_MW,
                            h3_mw = latestMeasurement.H3_MW,
                            qcmh1 = latestMeasurement.QcmH1,
                            qcmh2 = latestMeasurement.QcmH2,
                            qcmh3 = latestMeasurement.QcmH3,
                            deltaqsb = latestMeasurement.DeltaQsb,
                            qve_tt = latestMeasurement.Qve_TT,
                            qve_tb = latestMeasurement.Qve_TB
                        };

                        // Convert to JSON
                        var jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                        // POST to WordPress API
                        var response = await httpClient.PostAsync(
                            "https://daksrong.vn/wp-json/custom-api/v1/insert-measurement/",
                            jsonContent);

                        if (!response.IsSuccessStatusCode)
                        {
                            LoggerHelper.Error($"Failed to send measurement. Status: {response.StatusCode}");
                        }
                    }
                    await Task.Delay(TimeSpan.FromMilliseconds(3));
                }
                catch (Exception ex)
                {
                    LoggerHelper.Error("Error in PLC background task: " + ex.Message);
                    await Task.Delay(TimeSpan.FromSeconds(30));
                }
            }
        }
    }
}
