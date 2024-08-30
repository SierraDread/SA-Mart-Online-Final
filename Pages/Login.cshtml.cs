using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;

namespace Version2SAMart.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            if (username == "Admin")
            {
                var client = _httpClientFactory.CreateClient();
                var content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)});

                var response = await client.PostAsync("https://localhost:7069/api/Users/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // If Admin credentials are correct, redirect to the Admin Dashboard
                    return RedirectToPage("AdminDashboard");
                }
                else
                {
                    ErrorMessage = "Invalid Admin credentials.";
                    return Page();
                }
            }
            else
            {
                var client = _httpClientFactory.CreateClient();
                var content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)});

                var response = await client.PostAsync("https://localhost:7069/api/Users/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // If regular user credentials are correct, redirect to the User Dashboard
                    return RedirectToPage("UserDashboard");
                }
                else
                {
                    ErrorMessage = "Invalid username or password.";
                    return Page();
                }
            }
        }

    }
}



