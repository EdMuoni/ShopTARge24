using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ShopTARge24.Core.Dto
{
    public class KindergartenDto
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string GroupName { get; set; } = string.Empty;

        [Range(0, 60)]
        public int ChildrenCount { get; set; }

        [Required, StringLength(120)]
        public string KindergartenName { get; set; } = string.Empty;

        [Required, StringLength(80)]
        public string TeacherName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
