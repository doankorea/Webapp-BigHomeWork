using System;
using System.Collections.Generic;

namespace NewBTL.Models;

public partial class Donhang
{

    public int Madon { get; set; }

    public DateTime? Ngaydat { get; set; }

    public int? Tinhtrang { get; set; }

    public int? MaNguoidung { get; set; }
    public int? Thanhtoan { get; set; }

    public string? Diachinhanhang { get; set; }

    public decimal? Tongtien { get; set; }

    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

    public virtual Nguoidung? MaNguoidungNavigation { get; set; }
}
