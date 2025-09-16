using IndusG.Models.Setting;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static IndusG.Models.Enums;

namespace IndusG.Models
{
    public class IAMOConfigurationModel
    {
        [DisplayName("Slave ID")]
        public short SlaveID1 { get; set; }

        [DisplayName("Address P")]
        public int AddressP1 { get; set; }

        [DisplayName("Address Q")]
        public int AddressQ1 { get; set; }

        [DisplayName("Tỉ Số P")]
        public decimal TiSoP1 { get; set; }

        [DisplayName("Tỉ Số Q")]
        public decimal TiSoQ1 { get; set; }

        [DisplayName("Hiệu Suất Máy Phát (%)")]
        public decimal HieuSuatMayPhat { get; set; }

        [DisplayName("Hiệu Suất Cơ Khí (%)")]
        public decimal HieuSuatCoKhi { get; set; }

        [DisplayName("Hiệu Suất Turbine (%)")]
        public decimal HieuSuatTurbine { get; set; }

        [DisplayName("Cột Áp")]
        public decimal CotAp { get; set; }

        [DisplayName("Công Thức Theo Tỉ Số")]
        public bool BasedOnTiSo { get; set; }

        [DisplayName("Port")]
        public string Portname { get; set; }

        [DisplayName("Baudrate")]
        public int Baudrate { get; set; }

        [DisplayName("Slave ID")]
        public short SlaveID2 { get; set; }

        [DisplayName("Address P")]
        public int AddressP2 { get; set; }

        [DisplayName("Address Q")]
        public int AddressQ2 { get; set; }

        [DisplayName("Tỉ Số P")]
        public decimal TiSoP2 { get; set; }

        [DisplayName("Tỉ Số Q")]
        public decimal TiSoQ2 { get; set; }
        public byte[] Timestamp { get; set; }
    }
}