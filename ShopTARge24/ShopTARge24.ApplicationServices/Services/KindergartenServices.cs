using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileServices _fileServices;

        public KindergartenServices
            (
            ShopTARge24Context context,
            IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Kindergartens> Create(KindergartenDto dto)
        {

            Kindergartens domain = new Kindergartens();
            
                domain.Id = Guid.NewGuid();
                domain.GroupName = dto.GroupName;
                domain.ChildrenCount = dto.ChildrenCount;
                domain.KindergartenName = dto.KindergartenName;
                domain.TeacherName = dto.TeacherName;
                domain.CreatedAt = DateTime.UtcNow;
                domain.UpdatedAt = DateTime.UtcNow;
            

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            await _context.Kindergartens.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Kindergartens> Update(KindergartenDto dto)
        {
            Kindergartens domain = new Kindergartens();

            domain.Id = dto.Id;
            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.KindergartenName = dto.KindergartenName;
            domain.TeacherName = dto.TeacherName;
            domain.CreatedAt = DateTime.UtcNow;
            domain.UpdatedAt = DateTime.UtcNow;
            _fileServices.FilesToDatabase(dto, domain);

            // Save changes to the database
            _context.Kindergartens.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Kindergartens> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result!;
        }

        public async Task<Kindergartens> Delete(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            var images = await _context.FileToDatabases
                .Where(x => x.KindergartenId == id)
                .Select(y => new FileToDatabase
                {
                    Id = y.Id,
                    KindergartenId = y.KindergartenId,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                }).ToArrayAsync();

            _context.Kindergartens.Remove(result);
            await _fileServices.RemoveImagesFromDatabase(images);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}