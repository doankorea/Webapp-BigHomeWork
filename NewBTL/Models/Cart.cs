using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NewBTL.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public void AddItem(Sanpham product, int quantity)
        {
            CartLine? line = Lines
            .Where(p => p.Product.Masp == product.Masp)
            .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Sanpham product) =>
        Lines.RemoveAll(l => l.Product.Masp == product.Masp);
        public decimal ComputeTotalValues()
        {
            if (Lines == null)
            {
                return 0; // Hoặc giá trị mặc định khác mà bạn muốn trả về khi Lines là null
            }

            return (decimal)Lines.Sum(e => e.Product.Giatien * e.Quantity);
        }
        public void Clear() => Lines.Clear();

    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Sanpham Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
