using Microsoft.AspNetCore.Mvc;
using MoMoSdk.Models.Redirect;
using MoMoSdk.Services;

namespace WebApp.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IMomoService _momoService;
        // GET: Redirect
        public RedirectController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        public ActionResult Index([FromQuery] RedirectQuery query)
        {
            if (_momoService.CheckRedirectUrl(query))
            {
                return View(); 
            }

            throw new Exception("Wrong Signature from MoMo Server");
        }
    }
}