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
}
