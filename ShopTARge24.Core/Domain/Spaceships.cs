using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge24.Core.Domain
{
    public class Spaceships
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Classification { get; set; }
        public string? Size { get; set; }
        public DateTime? BuiltDate { get; set; } = default(DateTime?);
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
