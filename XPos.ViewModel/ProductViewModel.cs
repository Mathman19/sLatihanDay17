using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XPos.ViewModel
{
    public class ProductViewModel
    {
        [Key]
        public long Id { get; set; }

        [Display(Name ="Category")]
        public long CategoryId { get; set; }

        [Display(Name ="Category")]
        public string CategoryName { get; set; }

        [Display(Name ="Variant")]
        public long VariantId { get; set; }

        [Display(Name="Variant")]
        public string VariantName { get; set; }

        [Required, StringLength(10)]
        public string Initial { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Stock { get; set; }

        public string Image { get; set; }
        public HttpPostedFileBase File { get; set; }

        public bool Active { get; set; }

    }
}
