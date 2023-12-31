﻿using System;
using System.Collections.Generic;

namespace NewBTL.Models;

public partial class Chitietdonhang
{
    public int Madon { get; set; }

    public int Masp { get; set; }

    public int? Soluong { get; set; }

    public decimal? Dongia { get; set; }

    public decimal? Thanhtien { get; set; }

    public int? Phuongthucthanhtoan { get; set; }

    public virtual Donhang MadonNavigation { get; set; } = null!;

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}
