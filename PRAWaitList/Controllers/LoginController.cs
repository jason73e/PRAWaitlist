
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using PRAWaitList.Models;
using Microsoft.Owin.Security;
using PRAWaitList;


namespace PRAWaitList.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public virtual ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // usually this will be injected via DI. but creating this manually now for brevity
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.Username, model.Password);

            if (authenticationResult.IsSuccess)
            {
                // we are in!
                return RedirectToLocal(ReturnUrl);
            }

            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);

            return RedirectToAction("Index");
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "IntentToEnroll");
        }
    }


    public class LoginViewModel
    {
        [Required, AllowHtml]
        public string Username { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}