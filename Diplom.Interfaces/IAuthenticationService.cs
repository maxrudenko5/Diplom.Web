using Diplom.Domain;
using Diplom.ViewModels.Main.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Diplom.Interfaces
{
    public interface IAuthenticationService
    {
        HttpCookie SignIn(LoginViewModel view);
        HttpCookie SignUp(RegisterView view);
        HttpCookie SignUpIndirect(User user, Profile profile, LoginType type);
        void SignOut();
    }
}
