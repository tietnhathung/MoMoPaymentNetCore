using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MoMoSdk.Enums;
using MoMoSdk.Models;
using MoMoSdk.Models.Query;
using MoMoSdk.Services;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMoMoService _momoService;

        public HomeController(ILogger<HomeController> logger,IMoMoService momoService)
        {
            _logger = logger;
            _momoService = momoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrder()
        {
            var orderGuiId = Guid.NewGuid();
            var rnd = new Random(DateTime.Now.Millisecond);
            int amount = rnd.Next(1000 , 50000000);
            var viewModel = new CreateOrderViewModel
            {
                OrderId = orderGuiId.ToString(),
                OrderInfo = "Mua tivi",
                Amount = amount,
                RequestType = MoMoRequestType.captureWallet
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] CreateOrderViewModel model)
        {
            
            PaymentResponse paymentResponse = await _momoService.CreatePayment( model.RequestType,model.Amount,model.OrderId,model.OrderInfo,null,null,model.DeliveryInfo,model.UserInfo);
            if ( paymentResponse.ResultCode == MoMoResultCode.Successful)
            {
                return Redirect(paymentResponse.PayUrl);
            }
            return View(model);
        }

        public async Task<IActionResult> QueryOrder(string? orderId)
        {
            var queryResponse = new QueryResponse();
            if (orderId != null)
            {
                queryResponse = await _momoService.QueryPayment(orderId);
            }
            return View(queryResponse);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}