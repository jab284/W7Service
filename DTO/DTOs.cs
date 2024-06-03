namespace EFCoreExample.DTOs
{
    public class UserDTO
    {
        public int UserId {get;set;}
        public string Name { get; set; }

    }

    public class ProfileDTO
    {
        public string Bio { get; set; }
        public int UserId {get;set;}
    }

    public class CategoryDTO
    {
        public string Name { get; set; }
    }

    public class ProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
    }
}