using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace ShopTARge24.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileServices _fileServices;

        public RealEstateServices
            (
                ShopTARge24Context context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            RealEstate domain = new RealEstate
            {
                Id = Guid.NewGuid(),
                Area = dto.Area,
                Location = dto.Location,
                RoomNumber = dto.RoomNumber,
                BuildingType = dto.BuildingType,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            await _context.RealEstates.AddAsync(domain);
            await _context.SaveChangesAsync(); // Save RealEstate first

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            await _context.SaveChangesAsync(); // Save FileToDatabase entities

            return domain;
        }

        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            RealEstate domain = new RealEstate();

            domain.Id = dto.Id;
            domain.Area = dto.Area;
            domain.Location = dto.Location;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<RealEstate> DetailAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<RealEstate> Delete(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageTitle = y.ImageTitle,
                    RealEstateId = y.RealEstateId,
                })
                .ToArrayAsync();

            await RemoveImagesFromDatabase(images);
            _context.RealEstates.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var imageId = await _context.FileToDatabases
                 .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

                _context.FileToDatabases.Remove(imageId);
                await _context.SaveChangesAsync();
            }

            return null;
        }
    }
}
