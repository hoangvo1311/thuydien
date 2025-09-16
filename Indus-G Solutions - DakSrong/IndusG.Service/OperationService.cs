using IndusG.DataAccess;
using IndusG.Models;
using IndusG.Models.Setting;
using System;
using System.Linq;

namespace IndusG.Service
{
    public class OperationService
    {

        private bool isTesting = false;
        private int UPDOWNDELAY = 800;
        public PLCMonitoringModel GetOperationModel()
        {
            using (var context = new QuantracEntities())
            {
                var settingModel = new PLCMonitoringModel();

                var plcMonitoring = context.DB_SesanPLCConfiguration
                            .AsNoTracking()
                            .FirstOrDefault();
                settingModel.PLCLiveBit = plcMonitoring?.Status ?? false;
                settingModel.SimulationBit = plcMonitoring?.SimulationBit ?? false;
                settingModel.RemoteBit = plcMonitoring?.RemoteBit ?? false;
                settingModel.ManBit = plcMonitoring?.ManBit ?? false;
                settingModel.CV1Bottom = plcMonitoring?.CV1Bottom ?? false;
                settingModel.CV1Overload = plcMonitoring?.CV1Overload ?? false;
                settingModel.CV1Running = plcMonitoring?.CV1Running ?? false;
                settingModel.CV2Bottom = plcMonitoring?.CV2Bottom ?? false;
                settingModel.CV2Overload = plcMonitoring?.CV2Overload ?? false;
                settingModel.CV2Running = plcMonitoring?.CV2Running ?? false;
                settingModel.KU1 = plcMonitoring?.KU1 ?? false;
                settingModel.KD1 = plcMonitoring?.KD1 ?? false;
                settingModel.KU2 = plcMonitoring?.KU2 ?? false;
                settingModel.KD2 = plcMonitoring?.KD2 ?? false;

                settingModel.KU1_SIM = plcMonitoring?.KU1_SIM ?? false;
                settingModel.KD1_SIM = plcMonitoring?.KD1_SIM ?? false;
                settingModel.KU2_SIM = plcMonitoring?.KU2_SIM ?? false;
                settingModel.KD2_SIM = plcMonitoring?.KD2_SIM ?? false;

                settingModel.DrainLevel1 = (plcMonitoring?.DrainLevel1 ?? 0) * 100.0;
                settingModel.DrainLevel2 = (plcMonitoring?.DrainLevel2 ?? 0) * 100.0;

                return settingModel;
            }
        }

        public ResponseData SaveDrainLevel1(double drainLevel)
        {
            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(AddressConstant.DrainLevel1_Write, drainLevel);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.DrainLevel1 = drainLevel;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "Đã cập nhật giá trị Cửa xả 1"
            };
        }

        public ResponseData SaveRemoteMode(string mode)
        {
            try
            {
                ResponseData plcResult;
                if (!isTesting)
                {
                    switch (mode)
                    {
                        case "OFF":
                            plcResult = PlcService.WriteToPLC(AddressConstant.Remote_ON, false);
                            if (!plcResult.Result)
                            {
                                return plcResult;
                            }

                            plcResult = PlcService.WriteToPLC(AddressConstant.Man_ON, false);
                            if (!plcResult.Result)
                            {
                                return plcResult;
                            }

                            break;
                        case "MAN":
                            plcResult = PlcService.WriteToPLC(AddressConstant.Man_ON, true);
                            if (!plcResult.Result)
                            {
                                return plcResult;
                            }

                            plcResult = PlcService.WriteToPLC(AddressConstant.Remote_ON, false);
                            if (!plcResult.Result)
                            {
                                return plcResult;
                            }

                            break;
                        case "REMOTE":
                            var currentDrainLevel1 = PlcService.ReadReal(AddressConstant.DrainLevel1);
                            PlcService.WriteToPLC(AddressConstant.DrainLevel1_Write, currentDrainLevel1 * 100);
                            var currentDrainLevel2 = PlcService.ReadReal(AddressConstant.DrainLevel2);
                            PlcService.WriteToPLC(AddressConstant.DrainLevel2_Write, currentDrainLevel2 * 100);
                            plcResult = PlcService.WriteToPLC(AddressConstant.Remote_ON, true);
                            if (!plcResult.Result)
                            {
                                return plcResult;
                            }

                            plcResult = PlcService.WriteToPLC(AddressConstant.Man_ON, false);
                            if (!plcResult.Result)
                            {
                                return plcResult;
                            }
                            break;
                        default:
                            break;
                    }

                    if (mode == "OFF")
                    {
                        PlcService.WriteToPLC(AddressConstant.Simulation, false);
                        PlcService.WriteToPLC(AddressConstant.KU1, false);
                        PlcService.WriteToPLC(AddressConstant.KD1, false);
                        PlcService.WriteToPLC(AddressConstant.KU2, false);
                        PlcService.WriteToPLC(AddressConstant.KD2, false);

                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.RemoteBit = mode == "REMOTE";
                    plcConfig.ManBit = mode == "MAN";

                    if (mode == "OFF")
                    {
                        plcConfig.SimulationBit = false;
                        plcConfig.KU1 = false;
                        plcConfig.KD1 = false;
                        plcConfig.KU2 = false;
                        plcConfig.KD2 = false;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = $"{mode} mode"
            };
        }

        public ResponseData SaveSimulationBit(bool simulationBit)
        {
            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(AddressConstant.Simulation, simulationBit);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }

                    if (!simulationBit)
                    {
                        PlcService.WriteToPLC(AddressConstant.KU1_SIM, false);
                        PlcService.WriteToPLC(AddressConstant.KD1_SIM, false);
                        PlcService.WriteToPLC(AddressConstant.KU2_SIM, false);
                        PlcService.WriteToPLC(AddressConstant.KD2_SIM, false);
                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.SimulationBit = simulationBit;

                    if (!simulationBit)
                    {
                        plcConfig.KU1_SIM = false;
                        plcConfig.KD1_SIM = false;
                        plcConfig.KU2_SIM = false;
                        plcConfig.KD2_SIM = false;
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "Simulation " + (simulationBit ? "ON" : "OFF")
            };
        }

        public ResponseData SaveKU1(bool ku1Bit)
        {
            try
            {
                if (!isTesting)
                {
                    var plcResultTest = PlcService.WriteToPLC(AddressConstant.KU1_SIM, ku1Bit);
                    if (!plcResultTest.Result)
                    {
                        return plcResultTest;
                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.KU1_SIM = ku1Bit;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "KU 1 " + (ku1Bit ? "ON" : "OFF")
            };
        }

        public ResponseData SaveKU2(bool ku2Bit)
        {
            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(AddressConstant.KU2_SIM, ku2Bit);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.KU2_SIM = ku2Bit;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "KU 2 " + (ku2Bit ? "ON" : "OFF")
            };
        }

        public ResponseData SaveKD1(bool kd1Bit)
        {
            try
            {
                if (!isTesting)
                {
                    var plcResultTest = PlcService.WriteToPLC(AddressConstant.KD1_SIM, kd1Bit);
                    if (!plcResultTest.Result)
                    {
                        return plcResultTest;
                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.KD1_SIM = kd1Bit;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "KD 1 " + (kd1Bit ? "ON" : "OFF")
            };
        }

        public ResponseData SaveKD2(bool kd2Bit)
        {
            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(AddressConstant.KD2_SIM, kd2Bit);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.KD2_SIM = kd2Bit;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "KD 2 " + (kd2Bit ? "ON" : "OFF")
            };
        }

        public ResponseData SaveDrainLevel2(double drainLevel)
        {
            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(AddressConstant.DrainLevel2_Write, drainLevel);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }
                }

                using (var context = new QuantracEntities())
                {
                    var plcConfig = context.DB_SesanPLCConfiguration.First();
                    plcConfig.DrainLevel2 = drainLevel;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = "Đã cập nhật giá trị Cửa xả 2"
            };
        }


        public ResponseData SetK1Up()
        {
            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(AddressConstant.K1Up, true);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }

                    System.Threading.Thread.Sleep(UPDOWNDELAY);
                    PlcService.WriteToPLC(AddressConstant.K1Up, false);
                }

            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = ""
            };
        }

        public ResponseData SetK1Down()
        {
            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(AddressConstant.K1Down, true);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }

                    System.Threading.Thread.Sleep(UPDOWNDELAY);
                    PlcService.WriteToPLC(AddressConstant.K1Down, false);
                }

            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = ""
            };
        }

        public ResponseData SetK2Up()
        {
            var address = AddressConstant.K2Up;

            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(address, true);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }

                    System.Threading.Thread.Sleep(UPDOWNDELAY);
                    PlcService.WriteToPLC(address, false);
                }

            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = $"{address}"
            };
        }

        public ResponseData SetK2Down()
        {
            var address = AddressConstant.K2Down;

            try
            {
                if (!isTesting)
                {
                    var plcResult = PlcService.WriteToPLC(address, true);
                    if (!plcResult.Result)
                    {
                        return plcResult;
                    }

                    System.Threading.Thread.Sleep(UPDOWNDELAY);
                    PlcService.WriteToPLC(address, false);
                }

            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            return new ResponseData
            {
                Result = true,
                Message = $"{address}"
            };
        }

    }
}
