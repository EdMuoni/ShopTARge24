
namespace ShopTARge24.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisViewModel> GetRandomAsync(string? category = null);
        Task<List<string>> GetCategoriesAsync();
    }
}
