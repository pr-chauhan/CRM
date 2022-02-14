namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Financial_Yr { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Invoice_ID { get; set; }

        public DateTime? Invoice_Date { get; set; }

        public int? Consignee_ID { get; set; }

        [StringLength(255)]
        public string PO_NO { get; set; }

        public DateTime? Removal_Date { get; set; }

        [StringLength(255)]
        public string Removal_Time { get; set; }

        [StringLength(255)]
        public string Con_Adr_Check { get; set; }

        [StringLength(255)]
        public string Consignee_DelAddress { get; set; }

        [StringLength(255)]
        public string Consignee_DelRange { get; set; }

        [StringLength(255)]
        public string Consignee_DelDivision { get; set; }

        [StringLength(255)]
        public string Consignee_DelCommRate { get; set; }

        [StringLength(255)]
        public string Transporter_Name { get; set; }

        [StringLength(255)]
        public string Vehicle_No { get; set; }

        [StringLength(255)]
        public string GRR_No { get; set; }

        public double? Total_Ass_Amt { get; set; }

        public int? BED_Per { get; set; }

        public double? BED_Val { get; set; }

        public decimal? PCess_Per { get; set; }

        public double? PCess_Val { get; set; }

        public int? EDU_Per { get; set; }

        public double? EDU_Val { get; set; }

        public int? SHE_PEr { get; set; }

        public double? She_val { get; set; }

        public double? Total_Excise { get; set; }

        public double? Sub_total { get; set; }

        public decimal? VAT_CST_per { get; set; }

        public double? Vat_CST_Val { get; set; }

        [StringLength(255)]
        public string Vat_CST_flag { get; set; }

        public decimal? Surcharge_PER { get; set; }

        public double? Surcharge_Val { get; set; }

        public double? Frieght { get; set; }

        public double? Others { get; set; }

        public decimal? Insurance_per { get; set; }

        public double? Insurance_val { get; set; }

        public double? Total_amount { get; set; }

        [StringLength(255)]
        public string DESTINATION1 { get; set; }

        [StringLength(255)]
        public string DESTINATION2 { get; set; }

        [StringLength(255)]
        public string DEST_THROUGH2 { get; set; }

        [StringLength(255)]
        public string DEST_FLAG { get; set; }

        [StringLength(255)]
        public string EXTRA_INFO { get; set; }

        public DateTime? DOE { get; set; }

        public DateTime? DOM { get; set; }

        [StringLength(50)]
        public string E_userid { get; set; }

        [StringLength(50)]
        public string M_userid { get; set; }

        public int? DEL_PARTY { get; set; }
    }
}
