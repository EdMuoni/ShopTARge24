using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileServices _fileServices;

        public KindergartenServices(
            ShopTARge24Context context,
            IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        // ADD THIS MISSING METHOD
        public async Task<Kindergartens> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergartens> Create(KindergartenDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var kindergarten = new Kindergartens
                {
                    Id = Guid.NewGuid(),
                    GroupName = dto.GroupName,
                    ChildrenCount = dto.ChildrenCount ?? 0,
                    KindergartenName = dto.KindergartenName,
                    TeacherName = dto.TeacherName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _context.Kindergartens.AddAsync(kindergarten);
                await _context.SaveChangesAsync();

                // Process uploaded files for BOTH storage methods
                if (dto.Files != null && dto.Files.Any())
                {
                    // Store in file system
                    await _fileServices.SaveToFileSystem(dto.Files, kindergarten.Id);

                    // Store in database
                    await _fileServices.SaveToDatabase(dto.Files, kindergarten.Id);

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return kindergarten;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Kindergartens> Update(KindergartenDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var kindergarten = await _context.Kindergartens
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (kindergarten == null)
                    return null;

                kindergarten.GroupName = dto.GroupName;
                kindergarten.ChildrenCount = dto.ChildrenCount ?? 0;
                kindergarten.KindergartenName = dto.KindergartenName;
                kindergarten.TeacherName = dto.TeacherName;
                kindergarten.UpdatedAt = DateTime.UtcNow;

                if (dto.Files != null && dto.Files.Any())
                {
                    await _fileServices.SaveToFileSystem(dto.Files, kindergarten.Id);
                    await _fileServices.SaveToDatabase(dto.Files, kindergarten.Id);
                }

                _context.Kindergartens.Update(kindergarten);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return kindergarten;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Kindergartens> Delete(Guid id)
        {
            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            if (kindergarten == null)
                return null;

            // Delete from file system
            var fileApiImages = await _context.FileToApis
                .Where(x => x.KindergartenId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    KindergartenId = y.KindergartenId,
                    ExistingFilePath = y.ExistingFilePath,
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromApi(fileApiImages);

            // Delete from database blob storage
            var databaseImages = await _context.FileToDatabases
                .Where(x => x.KindergartenId == id)
                .ToListAsync();

            if (databaseImages.Any())
            {
                _context.FileToDatabases.RemoveRange(databaseImages);
            }

            _context.Kindergartens.Remove(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }
    }
}