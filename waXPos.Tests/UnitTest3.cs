using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using XPos.ViewModel;
using XPos.DataAccess;

namespace waXPos.Tests
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestGetProduct()
        {
            Trace.WriteLine("--- Start Read Product ---");
            List<ProductViewModel> respon = ProductRepo.All();
            foreach (var item in respon)
            {
                Trace.WriteLine(String.Format("Id: {0}, VariantId: {1}, Initial: {2}, Name: {3}", item.Id, item.VariantId, item.Initial, item.Name));
            }
            Trace.WriteLine("--- End Read Product ---");
        }

        [TestMethod]
        public void TestUpdateProduct()
        {
            Trace.WriteLine("--- Start Update Product ---");
            ProductViewModel product = new ProductViewModel();
            product.VariantId = 1;
            product.Initial = "PZ";
            product.Name = "Pizza";
            product.Description = "Desert lezat";
            product.Price = 10000;
            product.Active = true;

            ResponResult respon = ProductRepo.Update(product);
            if (respon.Success == true)
            {
                Trace.WriteLine("--- Save Successfull ---");
                ProductViewModel prodRespon = (ProductViewModel)respon.Entity;
                Trace.WriteLine(string.Format("Initial: {0}, Name: {1}, Description: {2}, Price: {3}", prodRespon.Initial, prodRespon.Name, prodRespon.Description, prodRespon.Price));
            }
            else
            {
                Trace.WriteLine("--- Save Filed ---");
            }
            Trace.WriteLine("--- Start Update Product ---");
        }

        [TestMethod]
        public void TestGetProductById()
        {
            Trace.WriteLine("--- Start Read Product By Id ---");
            ProductViewModel respon = ProductRepo.ById(1);
            Trace.WriteLine(String.Format("VariantId: {0}, Initial: {1}, Name: {2}, Description: {3}, Price: {4}", respon.VariantId, respon.Initial, respon.Name, respon.Description, respon.Price));
            Trace.WriteLine("--- End Read Product By Id ---");
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            Trace.WriteLine("--- Start Delete Product ---");
            //ProductViewModel product = ProductRepo.ById(2);
            ResponResult respon = ProductRepo.Delete(2);
            if (respon.Success == true)
            {
                Trace.WriteLine("--- Delete Successfull ---");
                ProductViewModel prodRespon = (ProductViewModel)respon.Entity;
                Trace.WriteLine(String.Format("VariantId: {0}, Initial: {1}, Name: {2}, Description: {3}, Price: {4}", prodRespon.VariantId, prodRespon.Initial, prodRespon.Name, prodRespon.Description, prodRespon.Price));
            }
            else
            {
                Trace.WriteLine("--- Delete Failed ---");
            }
            Trace.WriteLine("--- Start Delete Product ---");
        }
    }
}
