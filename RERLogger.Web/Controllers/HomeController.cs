using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using RERLogger.Web.Filters;

namespace RERLogger.Web.Controllers
{
    public class HomeController : Controller
    {
        private SharePointContext _spContext;
        private  ClientContext _hostWebContext;


        [SharePointContextFilter]
        public ActionResult Index()
        {
            _spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            _hostWebContext = _spContext.CreateUserClientContextForSPHost();

            using (var clientContext = _hostWebContext)
            {
                if (clientContext != null)
                {
                    User spUser = clientContext.Web.CurrentUser;

                    clientContext.Load(spUser, user => user.Title);

                    clientContext.ExecuteQuery();

                    ViewBag.UserName = spUser.Title;



                }
            }

            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
