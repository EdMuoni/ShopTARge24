namespace ShopTARge24.Models.Kindergartens
{
    public class KindergartenIndexViewModel
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.MinValue;
    }
}
