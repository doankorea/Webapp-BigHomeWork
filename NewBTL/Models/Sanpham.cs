using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBTL.Models
{
    public partial class Sanpham
    {
        public int Masp { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là trường bắt buộc.")]
        public string Tensp { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn 0.")]
        public decimal? Giatien { get; set; }

        public string Mota { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 1.")]
        public int? Soluong { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Thẻ SIM phải lớn hơn hoặc bằng 1.")]
        public int? Thesim { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Bộ nhớ trong phải lớn hơn hoặc bằng 1.")]
        public int? Bonhotrong { get; set; }

        public bool? Sanphammoi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "RAM phải lớn hơn hoặc bằng 1.")]
        public int? Ram { get; set; }

        [Display(Name = "Ảnh bìa")]
        public string? Anhbia { get; set; }

        public int? Mahang { get; set; }

        public int? Mahdh { get; set; }

        public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

        public virtual Hangsanxuat? MahangNavigation { get; set; }

        public virtual Hedieuhanh? MahdhNavigation { get; set; }
    }
}