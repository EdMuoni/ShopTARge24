using Microsoft.AspNetCore.Http;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceships domain);
        void FilesToDatabase(KindergartenDto dto, Kindergartens domain);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabase dto);
        Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabase[] dtos);
        void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);
        void UploadFilesToDatabase(KindergartenDto dto, Kindergartens domain);

    }
}
