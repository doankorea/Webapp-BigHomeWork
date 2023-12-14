using NewBTL.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppBigHomeWork.Repository;

namespace NewBTL.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSPRepository _loaiSp;
        public LoaiSpMenuViewComponent(ILoaiSPRepository loaiSPRepository)
        {
            _loaiSp = loaiSPRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSp.GetAllLoaiSp().OrderBy(x => x.Tenhang);
            return View(loaisp) ;
        }
    }
}
