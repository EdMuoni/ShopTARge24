namespace ShopTARge24.Models.Kindergartens
{
    public class KindergartenDeleteViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;

        public string Title => $"Kas kustutada rühm \"{GroupName}\"?";
    }
}
