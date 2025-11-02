using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface ITheCocktailServices // make it public
    {
        Task<List<TheCocktailDto>> SearchByNameAsync(string name);
        Task<TheCocktailDto?> LookupByIdAsync(string idDrink);
    }
}
