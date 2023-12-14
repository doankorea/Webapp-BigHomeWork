using System.ComponentModel.DataAnnotations;

namespace NewBTL.Models
{
    public class Thongke
    {
        [Key]
        public int Manguoidung { get; set; }
        public string Tennguoidung { get; set; }
        public string Dienthoai { get; set; }
        public decimal? Tongtien { get; set; }
    }
}
