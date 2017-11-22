using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;

namespace BarneyGo.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            return new ChallengeResult("Google",
                Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            return new RedirectResult(returnUrl);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}