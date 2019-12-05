using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPos.ViewModel
{
    public class VariantViewModel
    {
        [Key]
        public long Id { get; set; }

        [Display(Name="Category")]
        public long CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(10)]
        public string Initial { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
