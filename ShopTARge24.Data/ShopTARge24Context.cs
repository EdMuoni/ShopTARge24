using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;

namespace ShopTARge24.Data
{
    public class ShopTARge24Context : DbContext
    {

        public DbSet<Spaceships> Spaceships { get; set; } 
    }
}
