using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewBTL.Infrastructure;
using NewBTL.Models;
using NewBTL.Models.Authentication;
using NuGet.Protocol.Core.Types;

namespace NewBTL.Controllers
{
    public class CartController : Controller
    {
        private readonly QldienthoaiContext  _context;

        public CartController(QldienthoaiContext context)
        {
            _context = context;
        }
        public Cart? Cart { get; set; }
        public IActionResult Index()
        {
           return View("AddToCart", HttpContext.Session.GetJson<Cart>("cart"));
        }
        [Authentication]
        public IActionResult AddToCart(int maSP)
        {
            var userName = HttpContext.Session.GetString("Ten");
            var userId = HttpContext.Session.GetString("ID");
            var dienthoai = HttpContext.Session.GetString("Dienthoai");
            ViewBag.UserName = userName;
            ViewBag.UserId = userId;
            ViewBag.dienthoai = dienthoai;
            Sanpham? product = _context.Sanphams.SingleOrDefault(x => x.Masp == maSP);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return View("AddToCart", Cart);
        }
        public IActionResult UpdateCart(int maSP)
        {
            Sanpham? product = _context.Sanphams.SingleOrDefault(x => x.Masp == maSP);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("AddToCart", Cart);
        }
        public IActionResult RemoveFromCart(int maSP)
        {
            Sanpham? product = _context.Sanphams.SingleOrDefault(x => x.Masp == maSP);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("AddToCart", Cart);
        }
        public IActionResult Checkout(IFormCollection form)
        {

            // Lấy thông tin người mua từ session hoặc từ đăng nhập
            var userId = HttpContext.Session.GetString("ID");
            var userName = HttpContext.Session.GetString("Ten");
            int tinhTrangThanhToan = int.Parse(form["tinhTrangThanhToan"]);
            string diaChiNhanHang = form["diachi"];
            if (int.TryParse(userId, out int UI))
            {
                var cart = HttpContext.Session.GetJson<Cart>("cart");

                // Tạo một đơn hàng mới
                var order = new Donhang
                {
                    MaNguoidung = UI,
                    Ngaydat = DateTime.Now,
                    Tinhtrang = null, // Tùy thuộc vào tình trạng đơn hàng, bạn có thể điều chỉnh giá trị này.
                    Thanhtoan = tinhTrangThanhToan, // Tùy thuộc vào phương thức thanh toán, bạn có thể điều chỉnh giá trị này.
                    Diachinhanhang = diaChiNhanHang, // Điền thông tin địa chỉ giao hàng.
                    Tongtien = cart.ComputeTotalValues(),
                };

                // Lưu đơn hàng vào cơ sở dữ liệu
                _context.Donhangs.Add(order);
                _context.SaveChanges();
                // Lưu chi tiết đơn hàng (các sản phẩm trong đơn hàng)
                foreach (var line in cart.Lines)
                {
                    var chitiet = new Chitietdonhang
                    {
                        Madon = order.Madon,
                        Masp = line.Product.Masp,
                        Soluong = line.Quantity,
                        // Đơn giá sản phẩm (số tiền cho mỗi sản phẩm)
                        Dongia = line.Product.Giatien,

                        // Tổng số tiền cho sản phẩm này (số lượng * đơn giá)
                        Thanhtien = line.Product.Giatien * line.Quantity,
                        Phuongthucthanhtoan = tinhTrangThanhToan
                    };
                    _context.Chitietdonhangs.Add(chitiet);
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                // Xóa giỏ hàng
                HttpContext.Session.Remove("cart");

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
