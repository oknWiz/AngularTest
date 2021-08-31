using System;
using DataModel.Models;
using Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService { get; }

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpPost("AddProduct")]
        public int AddProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.CreatedUser = "admin";
            return _productService.InsertNewProduct(product);
        }

        [HttpPost("UpdateProduct")]
        public int UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }

        [Route("DeleteProduct/{productId}")]
        [HttpGet]
        public int DeleteProduct(int productId)
        {
            string updatedUser = "admin";
            return _productService.DeleteProduct(productId, updatedUser);
        }


        [HttpGet]
        [Route("GetProducts")]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.SelectAllProducts();
        }
    }
}
