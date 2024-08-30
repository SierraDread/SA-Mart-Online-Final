using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Version2SAMart.Data;
using Version2SAMart.Models;

namespace Version2SAMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Version2SAMartContext Context;

        public OrdersController(Version2SAMartContext context)
        {
            Context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await Context.Orders.FindAsync(id);
            if (order == null) {return NotFound();}
            return order;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders() => await Context.Orders.ToListAsync();
        
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDto orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount
            };

            Context.Orders.Add(order);
            await Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.UserId) {return BadRequest();}

            var order = new Order
            {
                Id = id,
                UserId = orderDto.UserId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount
            };

            Context.Entry(order).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id)) {return NotFound();} else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await Context.Orders.FindAsync(id);
            if (order == null) {return NotFound();}

            Context.Orders.Remove(order);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return Context.Orders.Any(e => e.Id == id);
        }
    }
}
