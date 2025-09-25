using System.Collections.Generic;

namespace ShopTARge24.Models.Kindergartens
{
    public class KindergartenIndexViewModel
    {
        public IEnumerable<Kindergarten> Items { get; set; } = new List<Kindergarten>();
        public string? Search { get; set; }
    }
}
