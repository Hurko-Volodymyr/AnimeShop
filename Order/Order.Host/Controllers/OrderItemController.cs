﻿using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dtos;

namespace Order.Host.Controllers
{
    [ApiController]
    [Scope("order.orderitem")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderItemController : ControllerBase
    {
        private readonly ILogger<OrderItemController> _logger;
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(ILogger<OrderItemController> logger, IOrderItemService orderItemService)
        {
            _logger = logger;
            _orderItemService = orderItemService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddOrderRequest request)
        {
            var result = await _orderItemService.AddAsync(
                request.UserId,
                request.GameAccountId,
                request.Name,
                request.LastName,
                request.Email,
                request.BasketItems,
                request.TotalSum);

            return Ok(result);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById(int id)
        {
            var result = await _orderItemService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(typeof(DeleteOrderItemResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderItemService.DeleteOrderAsync(id);
            return Ok(new DeleteOrderItemResponse<bool>() { IsDeleted = result });
        }
    }
}