using QLSX.Shared.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SaleAPI.Services
{

    public interface IImageUtilsServices
    {
        Task<ImageQRCode> CreateImageBarcode(int mdvid, int userid, string loai, int id, double sotien, string ghichu);
        Task<ImageQRCode> SaveImageBarcode(ImageQRCode item);
        Task<ImageQRCode> GetImageBarcode(string loai, int idPhieu);



    }
}
