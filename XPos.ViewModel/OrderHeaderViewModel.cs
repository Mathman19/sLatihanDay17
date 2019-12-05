using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPos.ViewModel
{
    public class OrderHeaderViewModel
    {
        [Key]
        public long Id { get; set; }

        public string Reference { get; set; }

        // Total Quantity
        public decimal Quantity { get; set; }

        // Total Amount
        public decimal Amount { get; set; }

        // Tax
        public decimal Tax { get; set; }

        // Total Payment
        public decimal Payment { get; set; }

        public List<OrderDetailViewModel> Details { get; set; }
    }

    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {
            Quantity = 1;
        }
        public long ProductId { get; set; }

        [Display(Name ="Product")]
        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Amount {
            get {
                return Quantity * Price;
            }
            set { }
        }
    }
}
