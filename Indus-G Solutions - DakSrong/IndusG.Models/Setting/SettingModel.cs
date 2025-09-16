using IndusG.Models.Setting;
using System;
using System.ComponentModel;
using static IndusG.Models.Enums;

namespace IndusG.Models
{
    public class SettingModel
    {
        public PLCSettingModel PLCSetting { get; set; }

        [DisplayName("Hệ số chưa co hẹp (DT)")]
        public double K_ChuaCoHep { get; set; }

        [DisplayName("Hệ số co hẹp ngang (DT)")]
        public double K_CoHepNgang { get; set; }

        [DisplayName("Hệ số co hẹp đứng (KX)")]
        public double K_CoHepDung { get; set; }


        [DisplayName("Hệ số lưu lượng (KX)")]
        public double K_LuuLuong { get; set; }


        [DisplayName("Hiệu suất máy phát 1 (%)")]
        public double H_MayPhat { get; set; }

        [DisplayName("Hiệu suất máy phát 2 (%)")]
        public double H_MayPhat2 { get; set; }

        [DisplayName("Hiệu suất máy phát 3 (%)")]
        public double H_MayPhat3 { get; set; }

        [DisplayName("Hiệu suất cơ khí (%)")]
        public double H_CoKhi { get; set; }

        [DisplayName("Hiệu suất turbine (%)")]
        public double H_Turbine { get; set; }


        [DisplayName("Cao trình ngưỡng tràn (m)")]
        public double CaoTrinhNguongTran { get; set; }


        [DisplayName("Chiều dài (rộng) đập tràn (m)")]
        public double ChieuDaiDapTran { get; set; }


        [DisplayName("Cao trình ngưỡng kênh xả (m)")]
        public double CaoTrinhNguongKenhXa { get; set; }


        [DisplayName("Chiều rộng kênh xả (m)")]
        public double ChieuRongKenhXa { get; set; }

        [DisplayName("Dung tích hữu ích (m3)")]
        public Nullable<double> DungTichHuuIch { get; set; }


        [DisplayName("Mực nước chết (m)")]
        public double MucNuocChet { get; set; }

        [DisplayName("Dung tích hồ ở MNC (m3)")]
        public double DungTichHoMNC { get; set; }

        [DisplayName("Chu kì lấy mẫu")]
        public double K_DCTT { get; set; }

        [DisplayName("DCTT theo quy định")]
        public double DCTT_QuyDinh { get; set; }

        [DisplayName("Độ mở cửa xả (DCTT)")]
        public bool DCTT_Toggle { get; set; }

        [DisplayName("PLC Live Bit")]
        public bool PLCLiveBit { get; set; }

        [DisplayName("Lưu lượng không tải H1")]
        public double LuuLuongKhongTaiH1 { get; set; }
        [DisplayName("Lưu lượng không tải H2")]
        public double LuuLuongKhongTaiH2 { get; set; }
        [DisplayName("Lưu lượng không tải H3")]
        public double LuuLuongKhongTaiH3 { get; set; }
        [DisplayName("DCTT Trung Gian")]
        public double DCTT_TrungGian { get; set; }


        public string Nhamay { get; set; }

    }
}