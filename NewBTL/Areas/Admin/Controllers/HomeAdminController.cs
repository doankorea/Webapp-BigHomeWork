using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewBTL.Models;
using NewBTL.Models.Authentication;
using System.Net;

namespace NewBTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class HomeAdminController : Controller
    {
        QldienthoaiContext db = new QldienthoaiContext();
        [Route("")]
        [Route("index")]
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham()
        {
            var lstSanPham = db.Sanphams.ToList();
            return View(lstSanPham);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.Mahang = new SelectList(db.Hangsanxuats.ToList(), "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs.ToList(), "Mahdh", "Tenhdh");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(Sanpham sanPham, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Save the image file to a directory within wwwroot
                    var uniqueFileName = imageFile.FileName;
                    var imagePath = Path.Combine("wwwroot/Images/files", uniqueFileName); // Đường dẫn tới thư mục trong wwwroot
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // Update the ImageFileName property with the unique file name and desired format
                    sanPham.Anhbia = "/Images/files/" + uniqueFileName;
                }
                db.Sanphams.Add(sanPham);
                        db.SaveChanges();
                        return RedirectToAction("DanhMucSanPham");
                    }
            ViewBag.Mahang = new SelectList(db.Hangsanxuats.ToList(), "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs.ToList(), "Mahdh", "Tenhdh");
            return View(sanPham);
        }
        




        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSP)
        {
            if (int.TryParse(maSP, out int masp))
            {
                ViewBag.Mahang = new SelectList(db.Hangsanxuats.ToList(), "Mahang", "Tenhang");
                ViewBag.Mahdh = new SelectList(db.Hedieuhanhs.ToList(), "Mahdh", "Tenhdh");
                var sanPham = db.Sanphams.Find(masp);
                if (sanPham != null)
                {
                    return View(sanPham);
                }
            }
            return NotFound();
        }

        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Sanpham sanPham, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var existingSanPham = db.Sanphams.FirstOrDefault(s => s.Masp == sanPham.Masp);
                if (existingSanPham != null)
                {
                    // Handle image upload
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Save the image file to a directory within wwwroot
                        var uniqueFileName = imageFile.FileName;
                        var imagePath = Path.Combine("wwwroot/Images/files", uniqueFileName); // Đường dẫn tới thư mục trong wwwroot
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        // Update the ImageFileName property with the unique file name and desired format
                        sanPham.Anhbia = "/Images/files/" + uniqueFileName;
                    }

                    // Update other properties of the product

                    // Save the changes to the database
                    db.Entry(existingSanPham).CurrentValues.SetValues(sanPham);
                    db.SaveChanges();

                    return RedirectToAction("DanhMucSanPham", "HomeAdmin");
                }
                else
                {
                    return NotFound();
                }

            }

            // If ModelState is not valid, return to the form with validation errors
            ViewBag.Mahang = new SelectList(db.Hangsanxuats.ToList(), "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs.ToList(), "Mahdh", "Tenhdh");
            return View(sanPham);
        }


        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSP)
        {
            TempData["Message"] = "";

            if (int.TryParse(maSP, out int masp))
            {
                var sanPham = db.Sanphams.Find(masp);

                if (sanPham != null)
                {
                    // Kiểm tra và xóa chi tiết đơn hàng liên quan
                    var chiTietSanPhams = db.Chitietdonhangs.Where(ct => ct.Masp == masp).ToList();
                    if (chiTietSanPhams.Any()) db.RemoveRange(chiTietSanPhams);

                    // Xóa sản phẩm
                    db.Remove(sanPham);
                    db.SaveChanges();

                    TempData["Message"] = "Sản phẩm đã được xóa!";
                    return RedirectToAction("DanhMucSanPham", "HomeAdmin");
                }
            }

            return NotFound();
        }
        [Route("danhmuchangsanxuat")]
        public IActionResult DanhMucHangSanXuat()
        {
            var lstHangSanXuats = db.Hangsanxuats.ToList();
            return View(lstHangSanXuats);
        }
        [Route("ThemHangSanXuatMoi")]
        [HttpGet]
        public IActionResult ThemHangSanXuatMoi()
        {
            return View();
        }
        [Route("ThemHangSanXuatMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHangSanXuatMoi(Hangsanxuat hangSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Hangsanxuats.Add(hangSanXuat);
                db.SaveChanges();
                return RedirectToAction("DanhMucHangSanXuat");
            }
            return View(hangSanXuat);
        }
        [Route("SuaHangSanXuat")]
        [HttpGet]
        public IActionResult SuaHangSanXuat(string maHang)
        {
            if (int.TryParse(maHang, out int mahang))
            {
                var hangSanXuat = db.Hangsanxuats.Find(mahang);
                if (hangSanXuat != null)
                {
                    return View(hangSanXuat);
                }
            }
            return NotFound();
        }

        [Route("SuaHangSanXuat")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHangSanXuat(Hangsanxuat hangSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangSanXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucHangSanXuat", "HomeAdmin");
            }
            return View(hangSanXuat);
        }
        [Route("XoaHangSanXuat")]
        [HttpGet]
        public IActionResult XoaHangSanXuat(string maHang)
        {
            TempData["Message"] = "";

            if (int.TryParse(maHang, out int mahang))
            {
                var hangSanXuat = db.Hangsanxuats.Find(mahang);

                if (hangSanXuat != null)
                {
                    // Kiểm tra và xóa chi tiết đơn hàng liên quan
                    var SanPhams = db.Sanphams.Where(sp => sp.Mahang == mahang).ToList();
                    if (SanPhams.Count > 0)
                    {
                        TempData["Message"] = "Không xóa được mã hãng này!";
                    }
                    // Xóa sản phẩm
                    db.Remove(hangSanXuat);
                    db.SaveChanges();

                    TempData["Message"] = "Hãng đã được xóa!";
                    return RedirectToAction("DanhMucHangSanXuat", "HomeAdmin");
                }
            }

            return NotFound();
        }
        [Route("danhmuchedieuhanh")]
        public IActionResult DanhMucHeDieuHanh()
        {
            var lstHeDieuHanhs = db.Hedieuhanhs.ToList();
            return View(lstHeDieuHanhs);
        }
        [Route("ThemHeDieuHanhMoi")]
        [HttpGet]
        public IActionResult ThemHeDieuHanhMoi()
        {
            return View();
        }
        [Route("ThemHeDieuHanhMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHeDieuHanhMoi(Hedieuhanh heDieuHanh)
        {
            if (ModelState.IsValid)
            {
                db.Hedieuhanhs.Add(heDieuHanh);
                db.SaveChanges();
                return RedirectToAction("DanhMucHeDieuHanh");
            }
            return View(heDieuHanh);
        }
        [Route("SuaHeDieuHanh")]
        [HttpGet]
        public IActionResult SuaHeDieuHanh(string maHDH)
        {
            if (int.TryParse(maHDH, out int mahdh))
            {
                var heDieuHanh = db.Hedieuhanhs.Find(mahdh);
                if (heDieuHanh != null)
                {
                    return View(heDieuHanh);
                }
            }
            return NotFound();
        }

        [Route("SuaHeDieuHanh")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHeDieuHanh(Hedieuhanh heDieuHanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(heDieuHanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucHeDieuHanh", "HomeAdmin");
            }
            return View(heDieuHanh);
        }
        [Route("XoaHeDieuHanh")]
        [HttpGet]
        public IActionResult XoaHeDieuHanh(string maHDH)
        {
            TempData["Message"] = "";

            if (int.TryParse(maHDH, out int mahdh))
            {
                var heDieuHanh = db.Hedieuhanhs.Find(mahdh);

                if (heDieuHanh != null)
                {
                    // Kiểm tra và xóa chi tiết đơn hàng liên quan
                    var SanPhams = db.Sanphams.Where(sp => sp.Mahang == mahdh).ToList();
                    if (SanPhams.Count > 0)
                    {
                        TempData["Message"] = "Không xóa được mã hãng này!";
                    }
                    // Xóa sản phẩm
                    db.Remove(heDieuHanh);
                    db.SaveChanges();

                    TempData["Message"] = "Hãng đã được xóa!";
                    return RedirectToAction("DanhMucHeDieuHanh", "HomeAdmin");
                }
            }

            return NotFound();
        }
        [Route("danhmucnguoidung")]
        public IActionResult DanhMucNguoiDung()
        {
            var lstNguoiDungs = db.Nguoidungs.ToList();
            return View(lstNguoiDungs);
        }
        [Route("ThemNguoiDungMoi")]
        [HttpGet]
        public IActionResult ThemNguoiDungMoi()
        {
            return View();
        }
        [Route("ThemNguoiDungMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNguoiDungMoi(Nguoidung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Nguoidungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("DanhMucNguoiDung");
            }
            return View(nguoiDung);
        }
        [Route("SuaNguoiDung")]
        [HttpGet]
        public IActionResult SuaNguoiDung(string maND)
        {
            if (int.TryParse(maND, out int mand))
            {
                var nguoiDung = db.Nguoidungs.Find(mand);
                if (nguoiDung != null)
                {
                    return View(nguoiDung);
                }
            }
            return NotFound();
        }

        [Route("SuaNguoiDung")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNguoiDung(Nguoidung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucNguoiDung", "HomeAdmin");
            }
            return View(nguoiDung);
        }
        [Route("XoaNguoiDung")]
        [HttpGet]
        public IActionResult XoaNguoiDung(string maND)
        {
            TempData["Message"] = "";

            if (int.TryParse(maND, out int mand))
            {
                var nguoiDung = db.Nguoidungs.Find(mand);

                if (nguoiDung != null)
                {
                    var DongHangs = db.Donhangs.Where(dh => dh.MaNguoidung == mand).ToList();
                    if (DongHangs.Count > 0)
                    {
                        TempData["Message"] = "Không xóa được người dùng này!";
                    }
                    // Xóa sản phẩm
                    db.Remove(nguoiDung);
                    db.SaveChanges();

                    TempData["Message"] = "Người dùng đã được xóa!";
                    return RedirectToAction("DanhMucNguoiDung", "HomeAdmin");
                }
            }

            return NotFound();
        }
        [Route("danhmucdonhang")]
        public IActionResult DanhMucDonHang()
        {
            var lstDonhangs = db.Donhangs.Include(d => d.MaNguoidungNavigation).ToList();
            return View(lstDonhangs);
        }
        [Route("ChiTietDonHang")]
        [HttpGet]
        public ActionResult ChiTietDonHang(string maDon)
        {
            if (int.TryParse(maDon, out int madon))
            {
                var DonHangs = db.Donhangs.Find(madon);
                DonHangs = db.Donhangs
                .Include(d => d.MaNguoidungNavigation)
                .FirstOrDefault(d => d.Madon == madon);
                if (DonHangs != null)
                {
                    // Kiểm tra xem DonHangs có quan hệ với Nguoidung không
                    if (DonHangs.MaNguoidungNavigation != null)
                    {
                        // Lấy tên của người dùng
                        string tenKhachHang = DonHangs.MaNguoidungNavigation.Hoten;
                        ViewData["TenKhachHang"] = tenKhachHang;
                    }

                    return View(DonHangs);
                }
            }
            return NotFound();
        }
        [Route("Xacnhan")]
        [HttpGet]
        public ActionResult Xacnhan(int id)
        {
            var DonHangs = db.Donhangs.Find(id);

            if (DonHangs != null)
            {
                // Kiểm tra xem trạng thái đơn hàng có phù hợp cho việc xác nhận hay không.
                // Ở đây, tôi kiểm tra xem trạng thái là 0 (Đang chờ xác nhận) mới có thể xác nhận.

                    DonHangs.Tinhtrang = 1;
                     // Đã xác nhận
                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            return RedirectToAction("DanhMucDonHang");
        }
        [Route("ThongKe")]
        [HttpGet]
        public ActionResult ThongKe()
        {
            var thongKeData = (
                from d in db.Donhangs
                join nd in db.Nguoidungs on d.MaNguoidung equals nd.MaNguoiDung
                group new { d, nd } by new { nd.MaNguoiDung, nd.Hoten, nd.Dienthoai } into grouped
                select new Thongke
                {
                    Manguoidung = grouped.Key.MaNguoiDung,
                    Tennguoidung = grouped.Key.Hoten,
                    Dienthoai = grouped.Key.Dienthoai,
                    Tongtien = grouped.Sum(x => x.d.Tongtien),
                }
            )
            .OrderByDescending(thongKe => thongKe.Tongtien)
            .Take(5)
            .ToList();

            return View(thongKeData);
        }


    }
}

