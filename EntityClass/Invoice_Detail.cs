namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice_Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(10)]
        [DisplayName("Financial Year")]
        public string Financial_Yr { get; set; }
        [DisplayName("Invoice ID")]
        public int? Invoice_Id { get; set; }
        [DisplayName("Item Id")]
        public int? Item_id { get; set; }
        [DisplayName("No of Package")]
        public int? No_of_pkg { get; set; }
        [DisplayName("Quantity")]
        public decimal? Qty { get; set; }
        [DisplayName("Rate")]
        public decimal? Rate { get; set; }
        [DisplayName("Total Amount")]
        public double? Total_amt { get; set; }

        public int? sr_flag { get; set; }

        [StringLength(255)]
        public string TYPE { get; set; }

        [StringLength(255)]
        [DisplayName("Descriptoin")]
        public string DEC { get; set; }
    }
}
