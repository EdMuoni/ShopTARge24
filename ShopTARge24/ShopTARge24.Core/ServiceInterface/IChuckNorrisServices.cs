using ShopTARge24.Core.Dto;   // <- not Web models

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisResultDto> GetRandomAsync(string? category = null);
        Task<List<string>> GetCategoriesAsync();
    }
}