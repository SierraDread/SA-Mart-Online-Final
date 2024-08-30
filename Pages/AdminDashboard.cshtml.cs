using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Version2SAMart.Models;
using Version2SAMart.Data;
using System.Threading.Tasks;

namespace Version2SAMart.Pages
{
    public class AdminDashboardModel : PageModel
    {
        private readonly Version2SAMartContext _context;

        public AdminDashboardModel(Version2SAMartContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostCreateUser(string Username, string Password, string Email)
        {
            var user = new User { Username = Username, Password = Password, Email = Email };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditUser(int Id, string Username, string Password, string Email)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(Username)) user.Username = Username;
            if (!string.IsNullOrEmpty(Password)) user.Password = Password;
            if (!string.IsNullOrEmpty(Email)) user.Email = Email;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteUser(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateProduct(string Name, string Description, decimal Price, int Stock, string ImageUrl)
        {
            var product = new Product { Name = Name, Description = Description, Price = Price, Stock = Stock, ImageUrl = ImageUrl };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditProduct(int Id, string Name, string Description, decimal Price, int Stock, string ImageUrl)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null) return NotFound();

            if (!string.IsNullOrEmpty(Name)) product.Name = Name;
            if (!string.IsNullOrEmpty(Description)) product.Description = Description;
            if (Price > 0) product.Price = Price;
            if (Stock > 0) product.Stock = Stock;
            if (!string.IsNullOrEmpty(ImageUrl)) product.ImageUrl = ImageUrl;

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteProduct(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateOrder(int UserId, string OrderDate, decimal TotalAmount)
        {
            var order = new Order { UserId = UserId, OrderDate = DateTime.Parse(OrderDate), TotalAmount = TotalAmount };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteOrder(int Id)
        {
            var order = await _context.Orders.FindAsync(Id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateOrderItem(int ProductId, int OrderId, int Quantity, decimal Price)
        {
            var orderItem = new OrderItem { ProductId = ProductId, OrderId = OrderId, Quantity = Quantity, Price = Price };
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteOrderItem(int Id)
        {
            var orderItem = await _context.OrderItems.FindAsync(Id);
            if (orderItem == null) return NotFound();

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}


