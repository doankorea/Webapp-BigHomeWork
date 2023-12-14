using NewBTL.Models;
namespace WebAppBigHomeWork.Repository
{
    public interface ILoaiSPRepository
    {
        Hangsanxuat Add(Hangsanxuat Tenhang);
        Hangsanxuat Update(Hangsanxuat Tenhang);
        Hangsanxuat Delete(int Mahang);
        Hangsanxuat GetLoaiSp(int Mahang);
        IEnumerable<Hangsanxuat> GetAllLoaiSp();

        
    }
}
