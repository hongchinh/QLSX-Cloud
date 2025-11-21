using QLSX.Shared.Models;
using System.Threading.Tasks;

namespace SaleAPI.Services;

public interface INhatKyService
{
    Task<bool> LogCreate(string TableName);
    Task<bool> LogUpdate(string TableName);
    Task<bool> LogDelete(string TableName);
    Task<bool> LogExportExcel(string TableName);
    Task<bool> LogError(string TableName, string Error);


    Task<bool> LogUpdateNX(NhapXuatModel itemCu, NhapXuatModel itemMoi);
    Task<bool> LogCreateNX(NhapXuatModel item);
    Task<bool> LogDeleteNX(NhapXuatModel item);
}
