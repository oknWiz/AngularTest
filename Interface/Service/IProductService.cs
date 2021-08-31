using Core.Utils;
using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface.Service
{
    public interface IProductService : IDisposable
    {
        int InsertNewProduct(Product newProduct);

        int UpdateProduct(Product updateProduct);

        int DeleteProduct(int productId, string updatedUser);

        IEnumerable<object> SelectProductList(int startIndex, int pageSize, DTOrder[] orders, DTColumn[] columns,
            DTSearch searchExp);

        IEnumerable<Product> SelectAllProducts();

        Product SelectProductById(int id);

        Product SelectProductByTitle(string name);
    }
}
