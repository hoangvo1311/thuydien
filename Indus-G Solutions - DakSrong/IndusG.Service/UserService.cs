using IndusG.Models.User;
using System;

namespace IndusG.Service
{
    public class UserService
    {
        public ResponseData Login(UserLoginModel model)
        {
            var loginSuccess = false;

            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
            {
                if ((model.UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                    && model.Password.Equals("sesandata")))
                {
                    WorkContext.CurrentUser = new Models.UserSessionModel
                    {
                        Username = "Admin",
                        UserType = Models.Enums.UserType.Admin
                    };
                    loginSuccess = true;
                }
                else if ((model.UserName.Equals("Vanhanh", StringComparison.OrdinalIgnoreCase)
                  && model.Password.Equals("vanhanh123")))
                {
                    WorkContext.CurrentUser = new Models.UserSessionModel
                    {
                        Username = "VanHanh",
                        UserType = Models.Enums.UserType.Operator
                    };
                    loginSuccess = true;
                }
                else if ((model.UserName.Equals("Truongca", StringComparison.OrdinalIgnoreCase)
                 && model.Password.Equals("truongca@123")))
                {
                    WorkContext.CurrentUser = new Models.UserSessionModel
                    {
                        Username = "TruongCa",
                        UserType = Models.Enums.UserType.ShiftLeader
                    };
                    loginSuccess = true;
                }
                //else if ((model.UserName.Equals("duybt", StringComparison.OrdinalIgnoreCase)
                // && model.Password.Equals("duybt123")))
                //{
                //    WorkContext.CurrentUser = new Models.UserSessionModel
                //    {
                //        Username = "duybt",
                //        UserType = Models.Enums.UserType.Operator
                //    };
                //    loginSuccess = true;
                //}
                //else if ((model.UserName.Equals("tuandd", StringComparison.OrdinalIgnoreCase)
                // && model.Password.Equals("tuandd123")))
                //{
                //    WorkContext.CurrentUser = new Models.UserSessionModel
                //    {
                //        Username = "tuandd",
                //        UserType = Models.Enums.UserType.Operator
                //    };
                //    loginSuccess = true;
                //}
                //else if ((model.UserName.Equals("vanhanh", StringComparison.OrdinalIgnoreCase)
                // && model.Password.Equals("vanhanh123")))
                //{
                //    WorkContext.CurrentUser = new Models.UserSessionModel
                //    {
                //        Username = "vanhanh",
                //        UserType = Models.Enums.UserType.Viewer
                //    };
                //    loginSuccess = true;
                //}
                //else if ((model.UserName.Equals("VH_IAMO", StringComparison.OrdinalIgnoreCase)
                //    && model.Password.Equals("vhiamo123")))
                //{
                //    WorkContext.CurrentUser = new Models.UserSessionModel
                //    {
                //        Username = "VH_IAMO",
                //        UserType = Models.Enums.UserType.ModbusManager
                //    };
                //    loginSuccess = true;
                //}
                //else if ((model.UserName.Equals("GS_IAMO", StringComparison.OrdinalIgnoreCase)
                //    && model.Password.Equals("gsiamo123")))
                //{
                //    WorkContext.CurrentUser = new Models.UserSessionModel
                //    {
                //        Username = "GS_IAMO",
                //        UserType = Models.Enums.UserType.ModbusViewer
                //    };
                //    loginSuccess = true;
                //}
            }

            if (loginSuccess)
            {
                return new ResponseData
                {
                    Result = true,
                    Message = "Đăng nhập thành công!"
                };
            }

            return new ResponseData
            {
                Result = false,
                Message = "Tên đăng nhập hoặc mật khẩu không đúng!"
            };
        }

        public ResponseData Logout()
        {
            WorkContext.CurrentUser = null;
            return new ResponseData
            {
                Result = true,
                Message = "Đã đăng xuất!"
            };
        }

    }
}
