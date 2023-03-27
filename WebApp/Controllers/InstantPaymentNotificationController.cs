
using Microsoft.AspNetCore.Mvc;
using MomoSdk.Models.InstantPaymentNotification;
using MomoSdk.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstantPaymentNotificationController : ControllerBase
    {
        private readonly IMomoService _momoService;
        
        
        // POST: api/Callback
        public InstantPaymentNotificationController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost]
        public StatusCodeResult Post([FromBody] IPNModel model)
        {
            if (_momoService.CheckIPN(model))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
