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
                else if ((model.UserName.Equals("Operator", StringComparison.OrdinalIgnoreCase)
                  && model.Password.Equals("sesanoperator")))
                {
                    WorkContext.CurrentUser = new Models.UserSessionModel
                    {
                        Username = "Operator",
                        UserType = Models.Enums.UserType.Operator
                    };
                    loginSuccess = true;
                }
                else if ((model.UserName.Equals("quandoc", StringComparison.OrdinalIgnoreCase)
                 && model.Password.Equals("quandoc123")))
                {
                    WorkContext.CurrentUser = new Models.UserSessionModel
                    {
                        Username = "Quandoc",
                        UserType = Models.Enums.UserType.Quandoc
                    };
                    loginSuccess = true;
                }
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
