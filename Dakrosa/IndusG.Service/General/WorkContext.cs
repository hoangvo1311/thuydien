using IndusG.Models;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Service
{
    public class WorkContext
    {
        const string CurrentUserKey = "CurrentUser";
        public static UserSessionModel CurrentUser
        {
            get
            {
                return GetSession<UserSessionModel>(CurrentUserKey);
            }
            set
            {
                SetSession(CurrentUserKey, value);
            }
        }

        public static bool IsAdmin()
        {
            return CurrentUser != null && CurrentUser.UserType == Enums.UserType.Admin;
        }

        public static bool IsTruongCa()
        {
            return CurrentUser != null && CurrentUser.UserType == Enums.UserType.TruongCa;
        }

        public static bool CanUpdateCaoTrinhNguongTran()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateK_ChuaCoHep()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateChieuDaiDapTran()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateK_CoHepNgang()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateDungTichHoMNC()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateK_CoHepDung()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateChieuRongKenhXa()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateK_LuuLuong()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateH_Turbine()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateH_MayPhat()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateMucNuocChet()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateH_CoKhi()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateDungTichHuuIch1()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateDungTichHuuIch2()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateDungTichHuuIch3()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateDungTichHuuIch4()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateDCTT_QuyDinh()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin);
        }

        public static bool CanUpdateCaoTrinhNguongKenhXa()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin || CurrentUser.UserType == Enums.UserType.TruongCa
                || CurrentUser.UserType == Enums.UserType.GiamDoc);
        }

        public static bool CanUpdateK_DCTT()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin || CurrentUser.UserType == Enums.UserType.TruongCa
                || CurrentUser.UserType == Enums.UserType.GiamDoc);
        }

        public static bool CanUpdateSampleSize()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin || CurrentUser.UserType == Enums.UserType.VanHanh
                || CurrentUser.UserType == Enums.UserType.TruongCa || CurrentUser.UserType == Enums.UserType.GiamDoc);
        }

        public static bool CanUpdateDCTT_Toggle()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin || CurrentUser.UserType == Enums.UserType.TruongCa
                || CurrentUser.UserType == Enums.UserType.GiamDoc);
        }

        public static bool CanUpdateUpstream_Cal_Toggle()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin || CurrentUser.UserType == Enums.UserType.GiamDoc);
        }

        #region Session

        /// <summary>
        /// Get session by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="setState"></param>
        public static T GetSession<T>(string name, T defaultValue = default(T), bool setState = false)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session[name] != null)
                {
                    return (T)HttpContext.Current.Session[name];
                }
            }

            //Set default value to state
            if (setState)
            {
                SetSession(name, defaultValue);
            }

            return defaultValue;
        }

        /// <summary>
        /// Set session
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetSession<T>(string name, T value)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[name] = value;
                HttpContext.Current.Session.Timeout = 2;
            }
        }

        #endregion
    }
}
