using System.Threading.Tasks;
using System.Web.Mvc;
using Twitter.Extentions;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string track)
        {
            if (!string.IsNullOrEmpty(track))
            {
                var task = Task.Factory.StartNew(() => new TwitterStream(track));
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