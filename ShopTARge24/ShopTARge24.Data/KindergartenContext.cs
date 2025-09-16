using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge24.Data
{
    public class KindergartenContext
    {
        public KindergartenContext(DbContextOptions<KindergartenContext> options)
        : base(options) { }

        public DbSet<Kindergarten> Kindergarten { get; set; }
    }
}
