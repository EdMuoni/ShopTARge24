using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly KindergartenContext _context;

        public KindergartenServices
            (
                KindergartenContext context
            )
        {
            _context = context;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergartens = new Kindergarten();

            kindergartens.Id = Guid.NewGuid();
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = DateTime.Now;
            kindergartens.UpdateAt = DateTime.Now;

            await _context.Kindergartens.AddAsync(kindergartens);
            await _context.SaveChangesAsync();

            return kindergartens;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            //vaja leida doamini objekt, mida saaks mappida dto-ga
            Kindergarten kindergartens = new Kindergarten();

            kindergartens.Id = Guid.NewGuid();
            kindergartens.GroupName = dto.GroupName;
            kindergartens.ChildrenCount = dto.ChildrenCount;
            kindergartens.KindergartenName = dto.KindergartenName;
            kindergartens.TeacherName = dto.TeacherName;
            kindergartens.CreatedAt = DateTime.Now;
            kindergartens.UpdateAt = DateTime.Now;

            //tuleb db-s teha andmete uuendamine jauue oleku salvestamine
            _context.Kindergartens.Update(kindergartens);
            await _context.SaveChangesAsync();

            return kindergartens;
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            //leida ülesse konkreetne soovitud rida, mida soovite kustutada
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);


            //kui rida on leitud, siis eemaldage andmebaasist
            _context.Kindergartens.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
