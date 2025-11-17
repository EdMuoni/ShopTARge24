namespace ShopTARge24.Models.Kindergartens
{
    public class ImageViewModel
    {
        public Guid Id { get; set; }
        public string? Filepath { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public string? Image { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}
