using System.Collections.Generic;
using System.Threading.Tasks;
using AlbumViewer.Application.Dtos;

namespace AlbumViewer.Application.Services
{
    public interface IAlbumApiService
    {
        Task<IEnumerable<AlbumDto>> GetAlbunsAsync();

        Task<IEnumerable<PhotoDto>> GetPhotosAsync(int albumId);

        Task<IEnumerable<CommentDto>> GetCommentsAsync(int photoId);
    }
}