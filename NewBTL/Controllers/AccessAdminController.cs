using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBTL.Models;
using System.Net;

namespace NewBTL.Controllers
{
    public class AccessAdminController : Controller
    {
        QldienthoaiContext db = new QldienthoaiContext();
        [HttpGet]
		
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(Nguoidung user)
        {
            ViewBag.User = HttpContext.Session.GetString("UserName");
            TempData["error"] = "";

            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.Nguoidungs.Where(x => x.Email.Equals(user.Email) && x.Matkhau.Equals(user.Matkhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Email.ToString());
                    HttpContext.Session.SetString("ID", u.MaNguoiDung.ToString());
                    HttpContext.Session.SetString("Ten", u.Hoten.ToString());
                    HttpContext.Session.SetString("Dienthoai", u.Dienthoai.ToString());

                    if (u.Idquyen == 2) // Kiểm tra IDquyen của người dùng
                    {
                        return RedirectToAction("DanhMucSanPham", "HomeAdmin", new { area = "admin" }); // Chuyển hướng đến trang admin
                    }
                    else if (u.Idquyen == 1) // Kiểm tra IDquyen cho người dùng thông thường
                    {
                        return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chính
                    }
                    else
                    {
                        TempData["error"] = "ID quyền không hợp lệ";
                    }
                }
                else
                {
                    TempData["error"] = "Tài khoản đăng nhập không đúng";
                }
            }
            return View();
        }

        public IActionResult Signup()
        {
			return View();
        }
		[HttpPost]
		[HttpPost]
		public IActionResult Signup(Nguoidung newUser)
		{
			if (ModelState.IsValid)
			{
				// Đặt giá trị mặc định cho các trường không cho nhập
				newUser.Idquyen = 1; // Đặt Idquyen mặc định
				newUser.Diachi = null; // Đặt Diachi mặc định
				newUser.Anhdaidien = null;

				// Thêm người dùng mới vào cơ sở dữ liệu
				db.Nguoidungs.Add(newUser);
				db.SaveChanges();

				// Redirect đến trang đăng nhập hoặc trang chính
				return RedirectToAction("Login"); // Thay "Login" bằng tên action hoặc trang chính của bạn
			}

			//TempData["error"] = "Đã xảy ra lỗi. Vui lòng thử lại.";
			return View(newUser);
		}
		public IActionResult Logout()
		{
            HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "AccessAdmin");
		}
		// Action xử lý đăng ký
		
	}
}
