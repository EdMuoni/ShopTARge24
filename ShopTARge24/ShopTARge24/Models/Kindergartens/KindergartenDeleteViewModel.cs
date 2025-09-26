using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Models.Kindergartens
{
    public class KindergartenDeleteViewModel
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; } = string.Empty;
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; } = string.Empty;
        public string? TeacherName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string Title => $"Kas kustutada rühm \"{GroupName}\"?";
    }
}
