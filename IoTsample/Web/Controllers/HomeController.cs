
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("{Web_Api_Url}");
                var model = JsonConvert.DeserializeObject<Entity.Models.Switch>(response.Content.ReadAsStringAsync().Result);

                ViewBag.State = (bool)model.IsOpen;
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(bool state)
        //{
        //    return View();
        //}
    }
}