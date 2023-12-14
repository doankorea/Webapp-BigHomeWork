using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewBTL.Models;

public partial class Nguoidung
{
	public int MaNguoiDung { get; set; }

	[Required(ErrorMessage = "Họ tên không được để trống.")]
	[StringLength(100, ErrorMessage = "Họ tên không vượt quá 100 ký tự.")]
	public string Hoten { get; set; }

	[Required(ErrorMessage = "Email không được để trống.")]
	[EmailAddress(ErrorMessage = "Email không hợp lệ.")]
	public string Email { get; set; }
	[Required(ErrorMessage = "SĐT không được để trống.")]
	[StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự.")]
	public string Dienthoai { get; set; }

	[Required(ErrorMessage = "Mật khẩu không được để trống.")]
	[MinLength(1, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
	public string Matkhau { get; set; }

	public int? Idquyen { get; set; }

    public string? Diachi { get; set; }

    public string? Anhdaidien { get; set; }

    public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

    public virtual PhanQuyen? IdquyenNavigation { get; set; }
}
