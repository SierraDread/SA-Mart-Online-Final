using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version2SAMart.Data;
using Version2SAMart.Models;

namespace Version2SAMart.Pages
{
    public class UserDashboardModel : PageModel
    {
        private readonly Version2SAMartContext _context;

        public UserDashboardModel(Version2SAMartContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<OrderItem> OrderItems { get; set; }

        [BindProperty]
        public Order NewOrder { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
            Orders = await _context.Orders.ToListAsync();
            OrderItems = await _context.OrderItems.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateOrderAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Orders.Add(NewOrder);
            await _context.SaveChangesAsync();

            // Create Order Items here if needed

            return RedirectToPage();
        }
    }
}

