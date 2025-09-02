namespace ShopTARge24.Models.Spaceships
{
    public class SpaceshipIndexViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Classification { get; set; }
        public string? Size { get; set; }
        public DateTime? BuiltDate { get; set; } = default(DateTime?);
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }
    }
}
