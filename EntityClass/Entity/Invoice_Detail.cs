namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice_Detail
    {
        public int ID { get; set; }

        [StringLength(10)]
        public string Financial_Yr { get; set; }

        public int? Invoice_Id { get; set; }

        public int? Item_id { get; set; }

        public int? No_of_pkg { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Rate { get; set; }

        public double? Total_amt { get; set; }

        public int? sr_flag { get; set; }

        [StringLength(255)]
        public string TYPE { get; set; }

        [StringLength(255)]
        public string DEC { get; set; }
    }
}
