using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TestLogin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> TryLogin(string userName, string password)
        {
            ICUTechClient client = new ICUTechClient();
            LoginResponse response = await client.LoginAsync(userName, password, "");
            return convertMessage(response.@return);
        }

        private string convertMessage(string message)
        {
            Dictionary<string, object> messageToArray = JsonSerializer.Deserialize<Dictionary<string, object>>(message);
            string color = messageToArray.Values.First().ToString() == "-1" ? "red" : "green";
            string convertedMessage = $"<i style=\"color: {color}\">";
            foreach (var atribute in messageToArray)
            {
                convertedMessage += atribute.Key + ": " + atribute.Value.ToString() + "<br>";
            }
            convertedMessage += "</i>";
            return convertedMessage;
        }
    }
}