
using Microsoft.EntityFrameworkCore;
using SaleAPI.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SaleAPI.Services
{
    public class QuyenSuDungService : IQuyenSuDungSerVice
    {
        private readonly CRMDBContext _context;
        public QuyenSuDungService(CRMDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckEnable(int UserId, string quyen)
        {
            var item = await _context.QuyenSuDungRepository.Where(x => x.UserId == UserId && x.MaSo == quyen && x.Selectted == true).AnyAsync();

            return item;

        }

        public async Task<bool> CheckSua(int UserId, string quyen)
        {
            var item = await _context.QuyenSuDungRepository.Where(x => x.UserId == UserId && x.MaSo == quyen && x.Sua == true).AnyAsync();

            return item;
        }

        public async Task<bool> CheckThem(int UserId, string quyen)
        {
            var item = await _context.QuyenSuDungRepository.Where(x => x.UserId == UserId && x.MaSo == quyen && x.Them == true).AnyAsync();

            return item;
        }

        public async Task<bool> CheckXemIn(int UserId, string quyen)
        {
            var item = await _context.QuyenSuDungRepository.Where(x => x.UserId == UserId && x.MaSo == quyen && x.XemIn == true).AnyAsync();

            return item;
        }

        public async Task<bool> CheckXoa(int UserId, string quyen)
        {
            var item = await _context.QuyenSuDungRepository.Where(x => x.UserId == UserId && x.MaSo == quyen && x.Xoa == true).AnyAsync();

            return item;
        }



    }
}
