using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class LogoutController : Controller
    {
        [HttpGet]
        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
