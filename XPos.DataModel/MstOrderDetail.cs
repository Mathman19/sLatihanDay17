namespace XPos.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MstOrderDetail")]
    public partial class MstOrderDetail
    {
        public long Id { get; set; }

        public long HeaderId { get; set; }

        public long ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual MstOrderHeader MstOrderHeader { get; set; }

        public virtual MstProduct MstProduct { get; set; }
    }
}
