using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
//using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
using Owin;
using TiendaVirtual.API.Models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using MohammadYounes.Owin.Security.MixedAuth;
//using System.DirectoryServices.AccountManagement;
using TiendaVirtual.Core.Interface;
using TiendaVirtual.Entities.Models;
using System.Threading.Tasks;
using TiendaVirtual.Core.Services;
using System.Web;
using System.Text;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using Microsoft.Owin.Security.Cookies;

namespace TiendaVirtual.API
{    
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
