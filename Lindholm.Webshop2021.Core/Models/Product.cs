namespace Lindholm.Webshop2021.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int  OwnerId { get; set; }
        public User Owner { get; set; }
    }
}