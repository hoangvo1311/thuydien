using IndusG.DataAccess;
using IndusG.Models.User;
using System;
using System.Linq;

namespace IndusG.Service
{
    public class UserService
    {
        public ResponseData Login(UserLoginModel model)
        {
            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
            {
                using (var context = new QuantracEntities())
                {
                    if (model.UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        var user = context.DB_User.FirstOrDefault(x => x.Username.ToLower() == "admin");

                        if (user != null && model.Password.Equals(user.Password))
                        {
                            WorkContext.CurrentUser = new Models.UserSessionModel
                            {
                                Username = "Admin",
                                UserType = Models.Enums.UserType.Admin
                            };
                            return new ResponseData
                            {
                                Result = true,
                                Message = "Đăng nhập thành công!"
                            };
                        }
                    }

                    if (model.UserName.Equals("giamdoc", StringComparison.OrdinalIgnoreCase))
                    {
                        var user = context.DB_User.FirstOrDefault(x => x.Username.ToLower() == "giamdoc");

                        if (user != null && model.Password.Equals(user.Password))
                        {
                            WorkContext.CurrentUser = new Models.UserSessionModel
                            {
                                Username = "Giamdoc",
                                UserType = Models.Enums.UserType.GiamDoc
                            };
                            return new ResponseData
                            {
                                Result = true,
                                Message = "Đăng nhập thành công!"
                            };
                        }
                    }

                    if (model.UserName.Equals("ptgiamdoc", StringComparison.OrdinalIgnoreCase))
                    {
                        var user = context.DB_User.FirstOrDefault(x => x.Username.ToLower() == "ptgiamdoc");

                        if (user != null && model.Password.Equals(user.Password))
                        {

                            WorkContext.CurrentUser = new Models.UserSessionModel
                            {
                                Username = "Ptgiamdoc",
                                UserType = Models.Enums.UserType.Admin
                            };
                            return new ResponseData
                            {
                                Result = true,
                                Message = "Đăng nhập thành công!"
                            };
                        }
                    }


                    if (model.UserName.Equals("Vanhanh", StringComparison.OrdinalIgnoreCase))
                    {
                        var user = context.DB_User.FirstOrDefault(x => x.Username.ToLower() == "vanhanh");

                        if (user != null && model.Password.Equals(user.Password))
                        {
                            WorkContext.CurrentUser = new Models.UserSessionModel
                            {
                                Username = "Vanhanh",
                                UserType = Models.Enums.UserType.VanHanh
                            };
                            return new ResponseData
                            {
                                Result = true,
                                Message = "Đăng nhập thành công!"
                            };
                        }
                    }

                    if (model.UserName.Equals("truongca", StringComparison.OrdinalIgnoreCase))
                    {
                        var user = context.DB_User.FirstOrDefault(x => x.Username.ToLower() == "truongca");

                        if (user != null && model.Password.Equals(user.Password))
                        {

                            WorkContext.CurrentUser = new Models.UserSessionModel
                            {
                                Username = "Truongca",
                                UserType = Models.Enums.UserType.TruongCa
                            };
                            return new ResponseData
                            {
                                Result = true,
                                Message = "Đăng nhập thành công!"
                            };
                        }
                    }
                }
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
