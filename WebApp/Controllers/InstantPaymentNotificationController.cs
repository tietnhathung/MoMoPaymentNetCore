
using Microsoft.AspNetCore.Mvc;
using MoMoSdk.Models.InstantPaymentNotification;
using MoMoSdk.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstantPaymentNotificationController : ControllerBase
    {
        private readonly IMoMoService _momoService;
        
        
        // POST: api/Callback
        public InstantPaymentNotificationController(IMoMoService momoService)
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
