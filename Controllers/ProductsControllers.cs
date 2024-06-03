using Microsoft.AspNetCore.Mvc;
using EFCoreExample.Data;
using EFCoreExample.DTOs;
using EFCoreExample.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        //Thiss appdbcontext is used to interact with our database


        private readonly AppDbContext _context;


        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


         [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name
                }).ToList();

            return products;
        }



        
        //added
         [HttpGet("{ProductId}")]
        public ActionResult<ProductDTO> GetProductById(int ProductId)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId==ProductId);
            var productDto = new ProductDTO
            {
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name
            };
            return productDto;
        }

        

        


         [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            var category = await _context.Categories.FirstAsync(c => c.Name == productDto.CategoryName);

            
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryId = category.CategoryId,
                Category = category
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProducts), new { UserId = product.ProductId }, productDto);
        }





        
        //added
        [HttpPut("{ProductId}")]
        public ActionResult<ProductDTO> UpdateProduct(int ProductId, ProductDTO UpdatedProduct)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);

            product.Name = UpdatedProduct.Name;

            _context.Products.Update(product);
            _context.SaveChanges();

            return Ok(UpdatedProduct);
        }

        





        
        //added
        [HttpDelete("{ProductId}")]
        public IActionResult DeleteProduct(int ProductId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok();
        }

        
    }

}