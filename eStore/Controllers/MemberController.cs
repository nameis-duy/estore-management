using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        IMemberRepository? memberRepository = null;
        public MemberController() => memberRepository = new MemberRepository();
        // GET: MemberController
        public ActionResult Index()
        {
            if (memberRepository != null)
            {
                var members = memberRepository.GetMemberList();
                return View(members);
            }
            return View(null);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (memberRepository == null)
            {
                ViewBag.Message = "Member Repository is null.";
                return View();
            }
            var member = memberRepository.GetMemberById(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: MemberController/Create
        public ActionResult Create() => View();

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (memberRepository != null)
                    {
                        memberRepository.AddMember(member);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message(ex.Message);
                return View(member);
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (memberRepository == null)
            {
                ViewBag.Message = "Member Repository is null.";
                return View();
            }
            var member = memberRepository.GetMemberById(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (memberRepository != null)
                    {
                        memberRepository.UpdateMember(member);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(member);
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (memberRepository == null)
            {
                ViewBag.Message = "Member Repository is null.";
                return View();
            }
            var member = memberRepository.GetMemberById(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                if (memberRepository != null)
                {
                    memberRepository.RemoveMember(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
