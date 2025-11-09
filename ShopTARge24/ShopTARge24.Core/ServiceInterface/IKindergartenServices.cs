using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IKindergartenServices
    {
        Task<Kindergartens> Create(KindergartenDto dto);
        Task<Kindergartens> DetailAsync(Guid id);
        Task<Kindergartens> Delete(Guid id);
        Task<Kindergartens> Update(KindergartenDto dto);
    }
}
