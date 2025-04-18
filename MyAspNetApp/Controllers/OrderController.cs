using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly Logger _logger;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            _logger = Logger.GetInstance();
        }


        // API: GET api/order
        [HttpGet]
        public async Task<ActionResult> GetAllOrder()
        {
            try{
                var orders = await _orderService.GetAllOrder(HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All order Success",
                    metadata = orders
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderDto orderDto)
        {
            try
            {
                var order = await _orderService.CreateOrder(orderDto, HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Create Order Success",
                    metadata = order
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

        [HttpPost("addOrderItem")]
        public async Task<ActionResult> AddOrderItem(AddOrderItemDto orderItemDto)
        {
            try
            {
                var orderItem = await _orderService.AddOrderItem(orderItemDto);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Add OrderItem Success",
                    metadata = orderItem
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

        [HttpPost("getOrderById")]
        public async Task<ActionResult> GetOrderById(GetOrderDto getOrderDto)
        {
            try
            {
                var order = await _orderService.GetOrderById(getOrderDto.OrderId);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get Order Success",
                    metadata = order
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

    }
}