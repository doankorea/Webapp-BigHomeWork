using Microsoft.EntityFrameworkCore;
using NewBTL.Models;
using WebAppBigHomeWork.Repository;

namespace WebAppBigHomeWork.Repository
{
    public class LoaiSpRepository : ILoaiSPRepository
    {
        private readonly QldienthoaiContext _context;
        public LoaiSpRepository(QldienthoaiContext context)
        {
            _context = context;
        }
        public Hangsanxuat Add(Hangsanxuat Tenhang)
        {
            _context.Hangsanxuats.Add(Tenhang);
            _context.SaveChanges();
            return Tenhang;
        }

        public Hangsanxuat Delete(int Mahang)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hangsanxuat> GetAllLoaiSp()
        {
            return _context.Hangsanxuats;
        }

        public Hangsanxuat GetLoaiSp(int Mahang)
        {
            return _context.Hangsanxuats.Find(Mahang);
        }

        public Hangsanxuat Update(Hangsanxuat Tenhang)
        {
            _context.Update(Tenhang);
            _context.SaveChanges(true);
            return Tenhang;
        }
    }
}