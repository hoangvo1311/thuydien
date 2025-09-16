using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IndusG.Models.Setting
{
    public class IAMOParameterModel
    {
        public int Id { get; set; }
        [DisplayName("Hiệu Suất Máy Phát (%)")]
        public decimal HieuSuatMayPhat { get; set; }
        [DisplayName("Hiệu Suất Cơ Khí (%)")]
        public decimal HieuSuatCoKhi { get; set; }
        [DisplayName("Hiệu Suất Turbine (%)")]
        public decimal HieuSuatTurbine { get; set; }
        [DisplayName("Cột Áp")]
        public decimal CotAp { get; set; }
        [DisplayName("Tính Theo Tỉ Số")]
        public bool BasedOnTiSo { get; set; }
        [DisplayName("Port")]
        public string Portname { get; set; }
        [DisplayName("Baudrate")]
        public int Baudrate { get; set; }
    }
}
