namespace WebApiArchutecture.Domain
{

    public enum Color {black , white , multicolored }
    public class Tattoo
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public Color Color { get; set; }
        public string Style { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfCreating {  get; set; } = DateTime.UtcNow;
        public byte[]? RowVersion { get; set; }
    }
}
