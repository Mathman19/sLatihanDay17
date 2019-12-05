using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using XPos.ViewModel;
using System.Collections.Generic;
using XPos.DataAccess;

namespace waXPos.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInsertCategory()
        {
            Trace.WriteLine("--- Start Testing Insert Category Repo ---");
            CategoryViewModel category = new CategoryViewModel();
            category.Initial = "AKB";
            category.Name = "Akbar";
            category.Active = true;

            ResponResult respon = CategoryRepo.Update(category);

            if (respon.Success == true)
            {
                Trace.WriteLine("--- Save Successful ---");

                CategoryViewModel catRespon = (CategoryViewModel)respon.Entity;

                Trace.WriteLine(String.Format("Initial : {0}, Name : {1} ", catRespon.Initial, catRespon.Name));
            }
            else
            {
                Trace.WriteLine("--- Save Failed ---");
                Trace.WriteLine("Fail Message : " + respon.Message);
            }
            Trace.WriteLine("--- End Testing Insert Category Repo ---");
        }

        [TestMethod]
        public void TestCategoryRepo()
        {
            Trace.WriteLine("--- Start Testing Category Repo ---");
            List<CategoryViewModel> result = CategoryRepo.All();
            foreach (var item in result)
            {
                Trace.WriteLine(String.Format("Initial : {0}, Name : {1}", item.Initial, item.Name));
            }
            Trace.WriteLine("--- End Testing Category Repo ---");
        }

        [TestMethod]
        public void TestGettAllCategory()
        {
            Trace.WriteLine("--- Start Read Category ---");
            List<CategoryViewModel> cats = CategoryRepo.All();
            foreach (var item in cats)
            {
                Trace.WriteLine(String.Format("Id :{0}, Initial:{1}, Name:{2}", item.Id, item.Initial, item.Name));
            }
            Trace.WriteLine("--- End Read Category ---");
        }

        [TestMethod]
        public void TestGetByIdCategory()
        {
            Trace.WriteLine("--- Start Read Category By Id ---");
            CategoryViewModel cat = CategoryRepo.ById(10);

            Trace.WriteLine(String.Format("Id : {0}, Initial: {1}, Name: {2}", cat.Id, cat.Initial, cat.Name));

            Trace.WriteLine("--- End Read Category By Id ---");
        }

        [TestMethod]
        public void TestDeleteById()
        {
            Trace.WriteLine("--- Start Delete Id Category ---");
            CategoryViewModel cat = CategoryRepo.ById(3);

            ResponResult respon = CategoryRepo.Delete(3);

            if (respon.Success == true)
            {
                Trace.WriteLine("--- Delete Successful ---");
                CategoryViewModel catRespon = (CategoryViewModel)respon.Entity;
                Trace.WriteLine(String.Format("Id: {0}, Initial: {1}, Name: {2}", cat.Id, cat.Initial, cat.Name));
            }
            else
            {
                Trace.WriteLine("--- Delete Failed ---");
                Trace.WriteLine("Fail Message : " + respon.Message);
            }

            Trace.WriteLine("--- End Delete Id Category ---");
        }
    }
}
