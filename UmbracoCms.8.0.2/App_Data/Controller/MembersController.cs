using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace Umbraco.Web.Controller{
    public class MembersController : SurfaceController{
        public ActionResult RenderLogin(){
            return PartialView("_Login", new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLogin(LoginModel model, string returnUrl){
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    UrlHelper myHelper = new UrlHelper(HttpContext.Request.RequestContext);
                    if (myHelper.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/login/");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The username or password provided is incorrect.");
                }
            }
            return CurrentUmbracoPage();
        }

         public ActionResult RenderLogout()
        {
            return PartialView("_Logout", null);
        }

        public ActionResult SubmitLogout()
        {
            TempData.Clear();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToCurrentUmbracoPage();
        }

    }
}