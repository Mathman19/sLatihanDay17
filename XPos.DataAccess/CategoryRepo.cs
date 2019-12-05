using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPos.DataModel;
using XPos.ViewModel;

namespace XPos.DataAccess
{
    public class CategoryRepo
    {
        // Retrieve/Select/Get
        public static List<CategoryViewModel> All()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            using (var db = new XPosContext())
            {
                result = db.MstCategories.Select(c => new CategoryViewModel{ 
                        Id = c.Id,
                        Initial = c.Initial,
                        Name = c.Name,
                        Active = c.Active
                    }).ToList();
            }
            return result;
        }

        // Retrieve/Select/Get by Id
        public static CategoryViewModel ById(long id)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var db = new XPosContext())
            {
                result = db.MstCategories.Where(c => c.Id == id).Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Initial = c.Initial,
                    Name = c.Name,
                    Active = c.Active
                })
                .FirstOrDefault();
                if (result == null)
                {
                    result = new CategoryViewModel();
                }
            }
            return result;
        }

        // Update => Insert & Edit
        public static ResponResult  Update(CategoryViewModel entity)
        {
            ResponResult result = new ResponResult();
            try
            {
                using (var db = new XPosContext())
                {
                    // Insert Id = 0
                    if (entity.Id == 0)
                    {
                        MstCategory cat = new MstCategory();
                        cat.Initial = entity.Initial;
                        cat.Name = entity.Name;
                        cat.Active = entity.Active;

                        cat.CreatedBy = "Atur";
                        cat.CreatedDate = DateTime.Now;

                        db.MstCategories.Add(cat);
                        db.SaveChanges();

                        result.Entity = cat;
                    }
                    //Edit => Id != 0
                    else
                    {
                        MstCategory cat = db.MstCategories.Where(c => c.Id == entity.Id).FirstOrDefault();
                        if (cat != null)
                        {
                            cat.Initial = entity.Initial;
                            cat.Name = entity.Name;
                            cat.Active = entity.Active;

                            cat.ModifiedBy = "Atur";
                            cat.ModifiedDate = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Category Not Found";
                        }
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

    // Delete By Id
    public static ResponResult Delete(long id)
        {
            ResponResult result = new ResponResult();
            try
            {
                using (var db = new XPosContext())
                {
                    MstCategory cat = db.MstCategories
                        .Where(c => c.Id == id)
                        .FirstOrDefault();
                    if (cat != null)
                    {
                        CategoryViewModel entity = new CategoryViewModel();
                        entity.Id = cat.Id;
                        entity.Initial = cat.Initial;
                        entity.Name = cat.Name;
                        entity.Active = cat.Active;

                        db.MstCategories.Remove(cat);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Category not found";
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
