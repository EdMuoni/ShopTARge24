using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace ShopTARge24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopTARge24Context _context;
        private readonly IWebHostEnvironment _webHost;

        public FileServices(
            ShopTARge24Context context,
            IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // EXISTING METHODS - Keep these for backward compatibility
        public void FilesToApi(SpaceshipDto dto, Spaceships spaceship)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    FilesToApi(file, spaceship.Id, null);
                }
            }

            if (dto.FileToApiDtos != null && dto.FileToApiDtos.Any())
            {
                foreach (var fileDto in dto.FileToApiDtos)
                {
                    fileDto.SpaceshipId = spaceship.Id;
                }
            }
        }

        public void FilesToApi(KindergartenDto dto, Kindergartens kindergarten)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    FilesToApi(file, null, kindergarten.Id);
                }
            }

            if (dto.FileToApiDtos != null && dto.FileToApiDtos.Any())
            {
                foreach (var fileDto in dto.FileToApiDtos)
                {
                    fileDto.KindergartenId = kindergarten.Id;
                }
            }
        }

        private void FilesToApi(IFormFile file, Guid? spaceshipId, Guid? kindergartenId)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "multipleFileUpload");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            FileToApi path = new FileToApi
            {
                Id = Guid.NewGuid(),
                ExistingFilePath = $"multipleFileUpload/{uniqueFileName}", // Updated to store relative path
                SpaceshipId = spaceshipId,
                KindergartenId = kindergartenId
            };

            _context.FileToApis.Add(path);
            _context.SaveChanges();
        }

        public async Task<FileToApiDto> RemoveImageFromApi(FileToApiDto dto)
        {
            var fileToRemove = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (fileToRemove != null)
            {
                // Delete physical file
                string filePath = Path.Combine(_webHost.WebRootPath, "multipleFileUpload", fileToRemove.ExistingFilePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _context.FileToApis.Remove(fileToRemove);
                await _context.SaveChangesAsync();

                return new FileToApiDto
                {
                    Id = fileToRemove.Id,
                    ExistingFilePath = fileToRemove.ExistingFilePath,
                    SpaceshipId = fileToRemove.SpaceshipId,
                    KindergartenId = fileToRemove.KindergartenId
                };
            }

            return null;
        }  

        public async Task<bool> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                await RemoveImageFromApi(dto);
            }
            return true;
        }

        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate realEstate)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var image in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = image.FileName,
                            RealEstateId = realEstate.Id
                        };

                        image.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabases.Add(files);
                        _context.SaveChanges();
                    }
                }
            }
        }

        // NEW METHODS for dual storage - Fixed to accept nullable Guid
        public async Task<List<FileToApi>> SaveToFileSystem(
            List<IFormFile> files, Guid? kindergartenId)
        {
            if (!kindergartenId.HasValue)
                throw new ArgumentNullException(nameof(kindergartenId));

            var uploadPath = Path.Combine(_webHost.WebRootPath, "uploads", "kindergarten");
            Directory.CreateDirectory(uploadPath);

            var fileEntities = new List<FileToApi>();

            foreach (var file in files)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileEntity = new FileToApi
                {
                    Id = Guid.NewGuid(),
                    ExistingFilePath = $"uploads/kindergarten/{fileName}",
                    KindergartenId = kindergartenId.Value
                };

                fileEntities.Add(fileEntity);
                await _context.FileToApis.AddAsync(fileEntity);
            }

            return fileEntities;
        }

        public async Task<List<FileToDatabase>> SaveToDatabase(
            List<IFormFile> files, Guid? kindergartenId)
        {
            if (!kindergartenId.HasValue)
                throw new ArgumentNullException(nameof(kindergartenId));

            var dbEntities = new List<FileToDatabase>();

            foreach (var file in files)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                var dbEntity = new FileToDatabase
                {
                    Id = Guid.NewGuid(),
                    ImageTitle = file.FileName,
                    ImageData = memoryStream.ToArray(),
                    KindergartenId = kindergartenId.Value
                };

                dbEntities.Add(dbEntity);
                await _context.FileToDatabases.AddAsync(dbEntity);
            }

            return dbEntities;
        }

        public async Task<bool> DeleteFileFromFileSystem(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return false;

                var fullPath = Path.Combine(_webHost.WebRootPath, filePath);

                if (File.Exists(fullPath))
                {
                    await Task.Run(() => File.Delete(fullPath));
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log exception
                return false;
            }
        }

        public async Task<FileToDatabaseDto> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            try
            {
                var fileToDatabase = await _context.FileToDatabases
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (fileToDatabase == null)
                {
                    return null;
                }

                _context.FileToDatabases.Remove(fileToDatabase);
                await _context.SaveChangesAsync();

                return dto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                await RemoveImageFromDatabase(dto);
            }
            return true;
        }
    }
}