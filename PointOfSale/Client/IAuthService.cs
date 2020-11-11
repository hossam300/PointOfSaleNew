using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Services.ISevices
{
    public interface IAuthService
    {
        public Task<RegisterResult> Register(RegisterModel registerModel);
        public Task<LoginResult> Login(LoginModel loginModel);
        public Task Logout();
    }
}
