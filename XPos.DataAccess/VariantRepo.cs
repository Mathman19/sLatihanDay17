using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPos.DataModel;
using XPos.ViewModel;

namespace XPos.DataAccess
{
    public class VariantRepo
    {
        // Retrieve/Select/Get
        public static List<VariantViewModel> All()
        {
            List<VariantViewModel> result = new List<VariantViewModel>();
            using (var db = new XPosContext())
            {
                result = db.MstVariants.Select(v => new VariantViewModel
                {
                    Id = v.id,
                    CategoryId = v.CategoryId,
                    CategoryName = v.MstCategory.Name,
                    Initial = v.Initial,
                    Name = v.Name,
                    Active = v.Active
                }).ToList();
            }
            return result;
        }

        public static List<VariantViewModel> ByCategory(long Id)
        {
            // id => Category Id
            List<VariantViewModel> result = new List<VariantViewModel>();
            using (var db = new XPosContext())
            {
                result = db.MstVariants.Where(v => v.CategoryId == Id).Select(v => new VariantViewModel
                {
                    Id = v.id,
                    CategoryId = v.CategoryId,
                    CategoryName = v.MstCategory.Name,
                    Initial = v.Initial,
                    Name = v.Name,
                    Active = v.Active
                }).ToList();
            }
            return result;
        }

        // Retrive/Select/Get By Id
        public static VariantViewModel ById(long Id)
        {
            VariantViewModel result = new VariantViewModel();
            using (var db = new XPosContext())
            {
                result = db.MstVariants.Where(v => v.id == Id).Select(v => new VariantViewModel
                {
                    Id = v.id,
                    CategoryId = v.CategoryId,
                    CategoryName = v.MstCategory.Name,
                    Initial = v.Initial,
                    Name = v.Name,
                    Active = v.Active
                }).FirstOrDefault();
                if (result == null)
                {
                    result = new VariantViewModel();
                }
                return result;
            }
        }

        // Update -> Insert & Edit
        public static ResponResult Update(VariantViewModel entity)
        {
            ResponResult result = new ResponResult();
            try
            {
                // Insert Id = 0
                using (var db = new XPosContext())
                {
                    if (entity.Id == 0)
                    {
                        MstVariant var = new MstVariant();
                        var.CategoryId = entity.CategoryId;
                        var.Initial = entity.Initial;
                        var.Name = entity.Name;
                        var.Active = entity.Active;

                        var.CreatedBy = "Akbar";
                        var.CreatedDate = DateTime.Now;

                        db.MstVariants.Add(var);
                        db.SaveChanges();

                        result.Entity = var;
                    }
                    // Edit -> Id != 0
                    else
                    {
                        MstVariant var = db.MstVariants.Where(v => v.id == entity.Id).FirstOrDefault();
                        if (var != null)
                        {
                            var.CategoryId = entity.CategoryId;
                            var.Initial = entity.Initial;
                            var.Name = entity.Name;
                            var.Active = entity.Active;

                            var.CreatedBy = "Akbar";
                            var.CreatedDate = DateTime.Now;

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
                result.Entity = ex.Message;
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
                    MstVariant var = db.MstVariants.Where(v => v.id == id).FirstOrDefault();
                    if (var != null)
                    {
                        VariantViewModel entity = new VariantViewModel();
                        entity.Id = var.id;
                        entity.Initial = var.Initial;
                        entity.CategoryName = var.MstCategory.Name;
                        entity.Name = var.Name;
                        entity.Active = var.Active;

                        db.MstVariants.Remove(var);
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
