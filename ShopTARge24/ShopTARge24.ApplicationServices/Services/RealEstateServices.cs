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
            RealEstate realestate = new RealEstate();

            realestate.Id = Guid.NewGuid();
            realestate.Area = dto.Area;
            realestate.Location = dto.Location;
            realestate.RoomNumber = dto.RoomNumber;
            realestate.BuildingType = dto.BuildingType;
            realestate.CreatedAt = DateTime.Now;
            realestate.ModifiedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, realestate);

            await _context.RealEstates.AddAsync(realestate);
            await _context.SaveChangesAsync();

            return realestate;
        }

        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            RealEstate realestate = new RealEstate();

            realestate.Id = dto.Id;
            realestate.Area = dto.Area;
            realestate.Location = dto.Location;
            realestate.RoomNumber = dto.RoomNumber;
            realestate.BuildingType = dto.BuildingType;
            realestate.CreatedAt = dto.CreatedAt;
            realestate.ModifiedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, realestate);

            _context.RealEstates.Update(realestate);
            await _context.SaveChangesAsync();

            return realestate;
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

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    RealEstateId = y.RealEstateId,
                    ExistingFilePath = y.ExistingFilePath,
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromApi(images);
            _context.RealEstates.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
