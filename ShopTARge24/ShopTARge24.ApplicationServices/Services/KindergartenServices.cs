using Microsoft.EntityFrameworkCore;
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
            Kindergartens kindergartens = new Kindergartens();

            kindergartens.Id = Guid.NewGuid();
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = DateTime.Now;
            kindergartens.UpdatedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, kindergartens);

            await _context.Kindergartens.AddAsync(kindergartens);
            await _context.SaveChangesAsync();

            return kindergartens;
        }

        public async Task<Kindergartens> Update(KindergartenDto dto)
        {
            //vaja leida doamini objekt, mida saaks mappida dto-ga
            Kindergartens kindergartens = new Kindergartens();

            kindergartens.Id = Guid.NewGuid();
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = DateTime.Now;
            kindergartens.UpdatedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, kindergartens);

            //tuleb db-s teha andmete uuendamine jauue oleku salvestamine
            _context.Kindergartens.Update(kindergartens);
            await _context.SaveChangesAsync();

            return kindergartens;
        }

        public async Task<Kindergartens> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergartens> Delete(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    KindergartenId = y.KindergartenId,
                    ExistingFilePath = y.ExistingFilePath,
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromApi(images);
            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}

