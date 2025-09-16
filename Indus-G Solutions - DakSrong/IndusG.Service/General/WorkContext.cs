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

        public static bool CanChangeSimulation()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin || CurrentUser.UserType == Enums.UserType.ShiftLeader);
        }

        public static bool IsOperator()
        {
            return CurrentUser != null && CurrentUser.UserType == Enums.UserType.Operator;
        }

        public static bool IsShiftLeader()
        {
            return CurrentUser != null && CurrentUser.UserType == Enums.UserType.ShiftLeader;
        }

        public static bool IsDakhnolUser()
        {
            return CurrentUser != null && (CurrentUser.UserType == Enums.UserType.Admin
                || CurrentUser.UserType == Enums.UserType.Operator || CurrentUser.UserType == Enums.UserType.ShiftLeader

                );

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
                HttpContext.Current.Session.Timeout = 30;
            }
        }

        #endregion
    }
}
