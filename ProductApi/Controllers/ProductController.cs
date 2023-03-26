using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.Domain;
using ProductApi.Repositories;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository,IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper= mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productRepository.GetAllProducts();

            var productsDTO=mapper.Map<List<Models.DTOs.ProductDTO>>(products);
            return Ok(productsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product=await productRepository.GetById(id);

            var productDTO=mapper.Map<Models.DTOs.ProductDTO>(product);

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Models.DTOs.UploadProductDTO productDTO)
        {
            var newProduct = new Models.Domain.Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price
            };

            await productRepository.AddProduct(newProduct);

            var newProductDTO = new Models.DTOs.ProductDTO()
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price

            };
            return Ok(newProductDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProductById(Guid id)
        {
            var productToDelete = await productRepository.DeleteById(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            var productDTO = new Models.DTOs.ProductDTO
            {
                Name= productToDelete.Name,
                Description= productToDelete.Description,
                Price = productToDelete.Price

            };

            return Ok(productDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] Models.DTOs.UploadProductDTO updateProductRequest)
        {
            var product = new Models.Domain.Product()
            {
                Name= updateProductRequest.Name,
                Description= updateProductRequest.Description,
                Price = updateProductRequest.Price
            };

            product = await productRepository.UpdateProduct(id, product);

            if (product == null) { return NotFound(); }
 
            var productDTO = new Models.DTOs.ProductDTO
            {
                Name= product.Name,
                Description= product.Description,
                Price = product.Price
            };

            return Ok(productDTO);
        }
    }
}
