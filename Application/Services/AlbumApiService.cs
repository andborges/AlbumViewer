using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AlbumViewer.Application.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AlbumViewer.Application.Services
{
    public class AlbumApiService : IAlbumApiService
    {
        private readonly AppSettings _appSettings;

        private readonly IMemoryCache _cache;

        public AlbumApiService(AppSettings appSettings, IMemoryCache cache)
        {
            _appSettings = appSettings;
            _cache = cache;
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbunsAsync()
        {
            List<AlbumDto> albums;
            
            if(!_cache.TryGetValue("albums", out albums))
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{_appSettings.AlbumApiUrl}/albums"))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("Cannot retrieve albums from API");
                        }

                        var content = await response.Content.ReadAsStringAsync();
                        
                        albums = JsonConvert.DeserializeObject<List<AlbumDto>>(content);

                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
                        _cache.Set("albums", albums, cacheEntryOptions);
                    }
                }
            }

            return albums;
        }

        public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(int albumId)
        {
            List<PhotoDto> photos;
            
            if(!_cache.TryGetValue("photos", out photos))
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{_appSettings.AlbumApiUrl}/photos"))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("Cannot retrieve photos from API");
                        }

                        var content = await response.Content.ReadAsStringAsync();
                        
                        photos = JsonConvert.DeserializeObject<List<PhotoDto>>(content);

                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
                        _cache.Set("photos", photos, cacheEntryOptions);
                    }
                }
            }

            return photos?.Where(p => p.AlbumId == albumId);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAsync(int photoId)
        {
            List<CommentDto> comments;
            
            if(!_cache.TryGetValue("comments", out comments))
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{_appSettings.AlbumApiUrl}/comments"))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("Cannot retrieve comments from API");
                        }

                        var content = await response.Content.ReadAsStringAsync();
                        
                        comments = JsonConvert.DeserializeObject<List<CommentDto>>(content);

                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
                        _cache.Set("comments", comments, cacheEntryOptions);
                    }
                }
            }
            
            return comments?.Where(c => c.PostId == photoId);
        }
    }
}