namespace ShopTARge24.Core.Dto
{
    public class ChuckNorrisResultDto
    {
        public string? Id { get; set; }
        public string? IconUrl { get; set; }
        public string? Url { get; set; }
        public string? Value { get; set; }
        public List<string> Categories { get; set; } = new();
    }

    //public class ChuckNorrisResultDto
    //{
    //    public string[] Categories { get; set; } = Array.Empty<string>();
    //    public DateTime CreatedAt { get; set; }
    //    public string IconUrl { get; set; } = string.Empty;
    //    public string Id { get; set; } = string.Empty;
    //    public DateTime UpdatedAt { get; set; }
    //    public string Url { get; set; } = string.Empty;
    //    public string Value { get; set; } = string.Empty;
    //}
}
