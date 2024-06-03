using System.Net.Http.Headers;

namespace EFCoreExample.Models
{
    public class Category
    {
        public int CategoryId {get; set;}
        public string Name {get; set;}

        //This establishes the 1-many relationship for the category class
        //A category can be associated with many products
        //We use the ICollection<> interface because EF manages this relationship for us
        public ICollection<Product> Products {get; set;}

    }
    public class Product
    {
        public int ProductId {get;set;}
        public string Name {get; set;}
        public decimal Price {get; set;}


        //Products can only have a single category
        //Products will also hold the foreign key associated with the category
        public int CategoryId {get;set;}
        public Category Category {get;set;}
    }
}