namespace Lindholm.Webshop2021.EntityFramework.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public UserEntity Owner { get; set; }
    }
}