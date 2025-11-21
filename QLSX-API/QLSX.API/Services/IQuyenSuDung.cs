using System.Threading;
using System.Threading.Tasks;

namespace SaleAPI.Services
{

    public interface IQuyenSuDungSerVice
    {
        Task<bool> CheckEnable(int UserId, string quyen);
        Task<bool> CheckThem(int UserId, string quyen);
        Task<bool> CheckSua(int UserId, string quyen);
        Task<bool> CheckXoa(int UserId, string quyen);
        Task<bool> CheckXemIn(int UserId, string quyen);
    }
}
