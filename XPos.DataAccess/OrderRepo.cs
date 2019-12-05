using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPos.DataModel;
using XPos.ViewModel;

namespace XPos.DataAccess
{
    public class OrderRepo
    {
        public static ResultOrder Post(OrderHeaderViewModel entity)
        {
            ResultOrder result = new ResultOrder();
            try
            {
                using (var db = new XPosContext())
                {
                    string newRef = NewReference();
                    MstOrderHeader oh = new MstOrderHeader();
                    oh.Reference = newRef;
                    oh.Amount = entity.Amount;
                    oh.Tax = entity.Tax;
                    oh.Active = true;

                    oh.CreatedBy = "Admin Create";
                    oh.CreatedDate = DateTime.Now;

                    db.MstOrderHeaders.Add(oh);

                    foreach (var item in entity.Details)
                    {
                        MstOrderDetail od = new MstOrderDetail();
                        MstProduct prod = db.MstProducts.Where(p => p.Id == item.ProductId).FirstOrDefault();
                        od.HeaderId = oh.Id;
                        od.ProductId = item.ProductId;
                        od.Price = item.Price;
                        od.Quantity = item.Quantity;
                        od.Active = true;
                        prod.Stock = (prod.Stock - item.Quantity);
                        od.CreatedBy = "Admin Create";
                        od.CreatedDate = DateTime.Now;

                        db.MstOrderDetails.Add(od);
                    }

                    db.SaveChanges();

                    result.Reference = newRef;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static string NewReference()
        {
            //Suf-YYMM-Incr
            //SLS-1910-0123
            string yearMonth = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("D2");
            string result = "SLS-" + yearMonth + "-";
            using (var db = new XPosContext())
            {
                var maxRef = db.MstOrderHeaders
                    .Where(oh => oh.Reference.Contains(result))
                    .Select(oh => new { reference = oh.Reference })
                    .OrderByDescending(oh => oh.reference)
                    .FirstOrDefault();
                if (maxRef != null)
                {
                    string[] oldRef = maxRef.reference.Split('-');
                    int newInc = int.Parse(oldRef[2]) + 1;
                    result += newInc.ToString("D4");
                }
                else
                {
                    result += "0001";
                }
            }
            return result;
        }
    }
}
