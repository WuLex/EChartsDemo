using EChartsProject.Dtos;
using EChartsProject.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EChartsProject.Controllers
{
    // 更新控制器
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly BcSwapDbContext _context;

        public ProductController(BcSwapDbContext context)
        {
            _context = context;
        }


        // 获取所有商品
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var productWithTags = await _context.Products
                    .Include(p => p.DataTags)
                    .ThenInclude(dt => dt.Tag)
                    .ToListAsync();
                if (productWithTags != null)
                {
                    var productDto = productWithTags.Adapt<List<ProductDto>>();
                    // 现在你可以使用productDto来访问映射后的数据
                    return productDto;
                }
                return null;
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        // 根据ID获取单个商品
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // 创建新商品
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        // 更新商品信息
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // 删除商品
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

        // 获取与商品关联的标签
        [HttpGet("{productID}/datatags")]
        public async Task<ActionResult<IEnumerable<DataTag>>> GetDataTagsForProduct(int productID)
        {
            var product = await _context.Products
                .Include(p => p.DataTags)
                .FirstOrDefaultAsync(p => p.ProductID == productID);

            if (product == null)
            {
                return NotFound();
            }

            return product.DataTags;
        }



    }
}
