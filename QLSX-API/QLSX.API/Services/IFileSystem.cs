using System.Threading;
using System.Threading.Tasks;

namespace SaleAPI.Services
{

    public interface IFileSystem
    {
        Task<bool> SavePicture(string pictureName, string pictureBase64, CancellationToken cancellationToken);
    }
}
