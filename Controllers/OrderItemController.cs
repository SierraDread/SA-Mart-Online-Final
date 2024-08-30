using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Version2SAMart.Data;
using Version2SAMart.Models;

namespace Version2SAMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly Version2SAMartContext Context;

        public OrderItemController(Version2SAMartContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems() => await Context.OrderItems.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await Context.OrderItems.FindAsync(id);
            if (orderItem == null) {return NotFound();}

            return orderItem;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItemDto orderItemDto)
        {
            if (id != orderItemDto.Id) { return BadRequest();}

            var orderItem = new OrderItem
            {
                Id = id,
                ProductId = orderItemDto.ProductId,
                OrderId = orderItemDto.OrderId,
                Quantity = orderItemDto.Quantity,
                Price = orderItemDto.Price
            };

            Context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id)) {return NotFound();} else throw;
            }
            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItemDto orderItemDto)
        {
            var orderItem = new OrderItem
            {
                ProductId = orderItemDto.ProductId,
                OrderId = orderItemDto.OrderId,
                Quantity = orderItemDto.Quantity,
                Price = orderItemDto.Price
            };

            Context.OrderItems.Add(orderItem);
            await Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItem);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await Context.OrderItems.FindAsync(id);
            if (orderItem == null) {return NotFound();}

            Context.OrderItems.Remove(orderItem);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderItemExists(int id) => Context.OrderItems.Any(e => e.Id == id);
    }
}
