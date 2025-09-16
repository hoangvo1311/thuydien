using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TNMTModel
    {
        public string MaTinh { get { return "GL"; } }
        public string KyHieuCongTrinh { get; set; }
        public string ThoiGianGui { get; set; }
        public List<NoiDung> NoiDung { get; set; }
    }

    public class NoiDung
    {
        public NoiDung(string kyHieuTram, string thongsoDo,
            double giaTriDo, string donviTinh, string thoigianDo)
        {
            ThongSoDo = thongsoDo;
            GiaTriDo = giaTriDo;
            DonViTinh = donviTinh;
            ThoiGianDo = thoigianDo;
            KyHieuTram = kyHieuTram;
        }

        public string KyHieuTram { get; set; }
        public string ThongSoDo { get; set; }
        public double GiaTriDo { get; set; }
        public string DonViTinh { get; set; }
        public string ThoiGianDo { get; set; }
        public string TrangThaiDo { get { return "00"; } }
    }
}
