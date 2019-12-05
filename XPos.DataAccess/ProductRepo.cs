using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPos.DataModel;
using XPos.ViewModel;

namespace XPos.DataAccess
{
    public class ProductRepo
    {
        // Retrieve/Select/Get
        public static List<ProductViewModel> All()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            using (var db = new XPosContext())
            {
                result = db.MstProducts.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    CategoryName = p.MstVariant.MstCategory.Name,
                    VariantId = p.VariantId,
                    VariantName = p.MstVariant.Name,
                    Initial = p.Initial,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    Image = p.Image,
                    Active = p.Active
                }).ToList();
            }
            return result;
        }

        public static List<ProductViewModel> ByFilter(string search)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            using (var db = new XPosContext())
            {
                result = db.MstProducts.Where(p => p.Stock> 0 &&( p.Initial.Contains(search) || p.Name.Contains(search) ||p.Description.Contains(search))).Take(10).Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    CategoryName = p.MstVariant.MstCategory.Name,
                    VariantId = p.VariantId,
                    VariantName = p.MstVariant.Name,
                    Initial = p.Initial,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    Image = p.Image,
                    Active = p.Active
                }).ToList();
            }
            return result;
        }

        // Retrieve/Select/Get By Id
        public static ProductViewModel ById(long Id)
        {
            ProductViewModel result = new ProductViewModel();
            using (var db = new XPosContext())
            {
                result = db.MstProducts.Where(p => p.Id == Id).Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    CategoryId = p.MstVariant.CategoryId,
                    CategoryName = p.MstVariant.MstCategory.Name,
                    VariantId = p.VariantId,
                    VariantName = p.MstVariant.Name,
                    Initial = p.Initial,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    Image = p.Image,
                    Active = p.Active
                }).FirstOrDefault();
                if (result == null)
                {
                    result = new ProductViewModel();
                }
            }

            return result;
        }

        // Update -> Insert & Edit
        public static ResponResult Update(ProductViewModel entity)
        {
            ResponResult result = new ResponResult();
            try
            {
                using (var db = new XPosContext())
                {
                    // Insert Id = 0
                    if (entity.Id == 0)
                    {
                        MstProduct prod = new MstProduct();
                        prod.VariantId = entity.VariantId;
                        prod.Initial = entity.Initial;
                        prod.Name = entity.Name;
                        prod.Description = entity.Description;
                        prod.Price = entity.Price;
                        prod.Stock = entity.Stock;
                        prod.Image = entity.Image;
                        prod.Active = entity.Active;

                        prod.CreatedBy = "Akbar";
                        prod.CreatedDate = DateTime.Now;

                        db.MstProducts.Add(prod);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    // Edit Id != 0
                    else
                    {
                        MstProduct prod = db.MstProducts.Where(p => p.Id == entity.Id).FirstOrDefault();
                        if (prod != null)
                        {
                            prod.VariantId = entity.VariantId;
                            prod.Initial = entity.Initial;
                            prod.Name = entity.Name;
                            prod.Description = entity.Description;
                            prod.Price = entity.Price;
                            prod.Stock = entity.Stock;
                            prod.Image = entity.Image;
                            prod.Active = entity.Active;

                            prod.CreatedBy = "Akbar";
                            prod.CreatedDate = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        // Delete
        public static ResponResult Delete(long id)
        {
            ResponResult result = new ResponResult();
            try
            {
                using (var db = new XPosContext())
                {
                    MstProduct product = db.MstProducts.Where(p => p.Id == id).FirstOrDefault();
                    if (product != null)
                    {
                        ProductViewModel entity = new ProductViewModel();
                        entity.Id = product.Id;
                        entity.Initial = product.Initial;
                        entity.Name = product.Name;
                        entity.CategoryName = product.MstVariant.MstCategory.Name;
                        entity.VariantId = product.VariantId;
                        entity.VariantName = product.MstVariant.Name;
                        entity.Description = product.Description;
                        entity.Price = product.Price;
                        entity.Stock = product.Stock;
                        entity.Image = product.Image;
                        entity.Active = product.Active;

                        db.MstProducts.Remove(product);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
