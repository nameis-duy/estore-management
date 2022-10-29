using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class LoginController : Controller
    {
        //GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            Member member = new Member();
            return View(member);
        }
        //Post: Login
        [HttpPost]
        public ActionResult Login(Member member)
        {
            using var myFstoreContext = new MyFStoreContext();
            var memberDetail = myFstoreContext.Members.Where(mem => (mem.Email == member.Email && mem.Password == mem.Password)).SingleOrDefault();
            if (memberDetail == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else if (member.Email.Contains("admin"))
            {
                TempData["Role"] = "Admin";
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["Role"] = "User";
                return RedirectToAction("Details", "Member", new {@id=memberDetail.MemberId});
            }
            return View(member);
        }
    }
}
