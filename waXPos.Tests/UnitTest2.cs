using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using XPos.ViewModel;
using XPos.DataAccess;

namespace waXPos.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestGetAllVariant()
        {
            Trace.WriteLine("--- Start Read Variant ---");
            List<VariantViewModel> vars = VariantRepo.All();

            foreach (var item in vars)
            {
                Trace.WriteLine(String.Format("Id: {0}, CategoryId: {1}, Initial: {2}, Name: {3}", item.Id, item.CategoryId, item.Initial, item.Name));
            }
            Trace.WriteLine("--- End Read Variant ---");
        }

        [TestMethod]
        public void TestGetByIdVariant()
        {
            Trace.WriteLine("--- Start Read Variant By Id ---");
            VariantViewModel vars = VariantRepo.ById(2);

            Trace.WriteLine(String.Format("Id: {0}, CategoryId: {1}, Initial: {2}, Name: {3}", vars.Id, vars.CategoryId, vars.Initial, vars.Name));

            Trace.WriteLine("--- End Read Variant By Id ---");
        }

        [TestMethod]
        public void TestUpdateVariant()
        {
            Trace.WriteLine("--- Start Updating Variant ---");
            VariantViewModel var = new VariantViewModel();
            //var.Id = 2;
            var.CategoryId = 1;
            var.Initial = "PCL";
            var.Name = "Pecel";
            var.Active = true;

            ResponResult result = VariantRepo.Update(var);
            if (result.Success==true)
            {
                Trace.WriteLine("--- Saved Successfully ---");
                VariantViewModel varRespon = (VariantViewModel)result.Entity;
                Trace.WriteLine(String.Format("CategoryId: {0}, Initial: {1}, Name: {2}", varRespon.CategoryId, varRespon.Initial, varRespon.Name));
            }
            else
            {
                Trace.WriteLine("--- Save Failed ---");
            }
            Trace.WriteLine("--- End Updating Variant ---");
        }

        [TestMethod]
        public void TestDeleteVariant()
        {
            Trace.WriteLine("--- Start Deleting Variant ---");
            //VariantViewModel var = VariantRepo.ById(2);
            ResponResult respon = VariantRepo.Delete(2);

            if (respon.Success == true)
            {
                Trace.WriteLine("--- Delete Successfull ---");
                VariantViewModel varRespon = (VariantViewModel)respon.Entity;
                Trace.WriteLine(String.Format("Id: {0}, CategoryId: {1}, Initial: {2}, Name: {3}", varRespon.Id, varRespon.CategoryId, varRespon.Initial, varRespon.Name));
            }
            else
            {
                Trace.WriteLine("--- Save Failed ---");
            }
            Trace.WriteLine("--- End Deleting Variant ---");
        }
    }
}
