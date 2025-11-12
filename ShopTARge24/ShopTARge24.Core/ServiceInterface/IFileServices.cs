using Microsoft.AspNetCore.Http;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceships domain);
        void FilesToApi(KindergartenDto dto, Kindergartens kindergartens);
        Task<FileToApiDto> RemoveImageFromApi(FileToApiDto dto);  
        Task<bool> RemoveImagesFromApi(FileToApiDto[] dtos);
        void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);
        Task<FileToDatabaseDto> RemoveImageFromDatabase(FileToDatabaseDto dto);

        // New methods for dual storage - fixed to accept nullable Guid
        Task<List<FileToApi>> SaveToFileSystem(List<IFormFile> files, Guid? kindergartenId);
        Task<List<FileToDatabase>> SaveToDatabase(List<IFormFile> files, Guid? kindergartenId);
        Task<bool> DeleteFileFromFileSystem(string filePath);
    }
}
