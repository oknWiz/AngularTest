using Core.Utils;
using DataModel.Models;
using Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(MyFirstAngularContext db) : base(db)
        {
        }
        public int InsertNewProduct(Product newProduct)
        {
            try
            {
                var searchCategory = SelectProductByTitle(newProduct.Title);
                if (searchCategory != null) return -1;
                Product product = new Product()
                {
                    Title = newProduct.Title,
                    Price = newProduct.Price,
                    Description = newProduct.Description,
                    Category = newProduct.Category,
                    Image = newProduct.Image,
                    IsDelete = newProduct.IsDelete,
                    CreatedDate = newProduct.CreatedDate,
                    CreatedUser = newProduct.CreatedUser
                };
                dbContext.TblProducts.Add(product);
                var count = dbContext.SaveChanges();
                if (count == 1)
                {
                    LogUtilities.LogInfoMessage("Insert new product success.", ConstantValues.GetCurrentMethod());
                    return count;
                }
                else
                {
                    LogUtilities.LogInfoMessage("Insert new product fails!", ConstantValues.GetCurrentMethod());
                    return 0;
                }
            }
            catch (Exception ex)
            {
                LogUtilities.LogErrorMessage(ex.Message, ex);
                throw;
            }
        }

        public int UpdateProduct(Product updateProduct)
        {
            var count = 0;
            using (var context = dbContext)
            {
                try
                {
                    var searchProduct = SelectProductById(updateProduct.ProductId);
                    if (searchProduct != null)
                    {
                        searchProduct.Title = updateProduct.Title;
                        searchProduct.Price = updateProduct.Price;
                        searchProduct.Description = updateProduct.Description;
                        searchProduct.Category = updateProduct.Category;
                        searchProduct.IsDelete = false;
                        searchProduct.UpdatedUser = updateProduct.UpdatedUser;
                        searchProduct.UpdatedDate = updateProduct.UpdatedDate;
                    }
                    //Update User Table
                    count += dbContext.SaveChanges();
                    if (count == 1)
                        LogUtilities.LogInfoMessage("UPDATE product success in DB...",
                            ConstantValues.GetCurrentMethod());
                }
                catch (Exception e)
                {
                    LogUtilities.LogErrorMessage(e.Message, e);
                    throw;
                }
            }
            return count;
        }

        public int DeleteProduct(int productId, string updatedUser)
        {
            var i = new int();
            try
            {
                var searchProduct =
                    dbContext.TblProducts.FirstOrDefault(w => w.ProductId == productId && w.IsDelete == false);
                if (searchProduct == null) return 0;
                searchProduct.IsDelete = true;
                searchProduct.UpdatedDate = ConstantValues.GetDateTime(ConstantValues.SG_Timezone);
                searchProduct.UpdatedUser = updatedUser;
                i += dbContext.SaveChanges();
                if (i == 1)
                    LogUtilities.LogInfoMessage("DELETE product success in DB...", ConstantValues.GetCurrentMethod());
            }
            catch (Exception e)
            {
                LogUtilities.LogErrorMessage(e.Message, e);
                throw;
            }
            return i;
        }

        public IEnumerable<object> SelectProductList(int startIndex, int pageSize, DTOrder[] orders, DTColumn[] columns, DTSearch searchExp)
        {
            var query = this.dbContext.TblProducts.Where(w => w.IsDelete == false).Select(s => s);

            if (!string.IsNullOrEmpty(searchExp.Value))
                query = query.Where(w => w.Category.ToLower().Contains(searchExp.Value)
                                         || w.Description.ToLower().Contains(searchExp.Value)
                                         );

            var totalCount = query.Count();

            for (var i = 0; i < orders.Length; i++)
                query = query.OrderBy(columns[orders[i].Column].Data + " " + orders[i].Dir);

            var list = query.Skip(pageSize * (startIndex - 1))
                .Take(pageSize)
                .ToList();

            IEnumerable<object> resultList = list.Select(s => new
            {
                s.ProductId,
                s.Title,
                s.Price,
                s.Description,
                s.Category,
                s.Image,
                s.IsDelete,
                s.CreatedDate,
                s.CreatedUser,
                TotalCount = totalCount
            }).ToList();
            return resultList;
        }

        public IEnumerable<Product> SelectAllProducts()
        {
            var result = this.dbContext.TblProducts.Where(w => w.IsDelete == false)
                .OrderBy(o => o.Category).ToList();
            return result;
        }

        public Product SelectProductById(int id)
        {
            return this.dbContext.TblProducts.FirstOrDefault(w => w.IsDelete == false && w.ProductId == id);
        }

        public Product SelectProductByTitle(string name)
        {
            return this.dbContext.TblProducts.FirstOrDefault(w => w.IsDelete == false && w.Title == name);
        }
    }
}
