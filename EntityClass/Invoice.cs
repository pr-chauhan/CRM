namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        [Required]
        [DisplayName("Financial Year")]
        public string Financial_Yr { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Invoice Id")]
        [Required]
        public int Invoice_ID { get; set; }
        [DisplayName("Invoice Date")]
        [Required]
        public DateTime? Invoice_Date { get; set; }
        [DisplayName("Consignee Id")]
        public int? Consignee_ID { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("PO Number")]
        public string PO_NO { get; set; }
        [DisplayName("Removal Date")]
        public DateTime? Removal_Date { get; set; }

        [StringLength(255)]
        [DisplayName("Removal Time")]
        public string Removal_Time { get; set; }
        [DisplayName("Same")]
        public string Con_Adr_Check { get; set; }

        [StringLength(255)]
        [DisplayName("Consignee Address")]
        [Required]
        public string Consignee_DelAddress { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("Range")]
        public string Consignee_DelRange { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("Division")]
        public string Consignee_DelDivision { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("Commission Rate")]
        public string Consignee_DelCommRate { get; set; }

        [StringLength(255)]
        [DisplayName("Transporter Name")]
        public string Transporter_Name { get; set; }

        [StringLength(255)]
        [DisplayName("Vehicle No.")]
        public string Vehicle_No { get; set; }

        [StringLength(255)]
        [DisplayName("GRN No.")]
        public string GRR_No { get; set; }
        [Required]
        [DisplayName("Total Assesment")]
        public double? Total_Ass_Amt { get; set; }

        [DisplayName("GST Percent")]
        public int? BED_Per { get; set; }
        [DisplayName("GST Value")]
        public double? BED_Val { get; set; }
        [DisplayName("SGST Percent")]
        public decimal? PCess_Per { get; set; }
        [DisplayName("SGST Value")]
        public double? PCess_Val { get; set; }
        [DisplayName("EDU Percent")]
        public int? EDU_Per { get; set; }
        [DisplayName("EDU Value")]
        public double? EDU_Val { get; set; }

        public int? SHE_PEr { get; set; }

        public double? She_val { get; set; }
        [DisplayName("Total Tax")]
        [Required]
        public double? Total_Excise { get; set; }
        [DisplayName("Sub Total")]
        [Required]
        public double? Sub_total { get; set; }

        public decimal? VAT_CST_per { get; set; }

        public double? Vat_CST_Val { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("GST Type")]
        public string Vat_CST_flag { get; set; }

        public decimal? Surcharge_PER { get; set; }

        public double? Surcharge_Val { get; set; }

        public double? Frieght { get; set; }

        public double? Others { get; set; }
        [DisplayName("Insurance Percent")]
        public decimal? Insurance_per { get; set; }
        [DisplayName("Insurance Value")]
        public double? Insurance_val { get; set; }
        [DisplayName("Total Amount")]
        [Required]
        public double? Total_amount { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("From Batouli To")]
        public string DESTINATION1 { get; set; }

        [StringLength(255)]
        [DisplayName("To")]
        public string DESTINATION2 { get; set; }

        [StringLength(255)]
        [DisplayName("Through Transport")]
        public string DEST_THROUGH2 { get; set; }

        [DisplayName("Transhipment")]
        public string DEST_FLAG { get; set; }

        [StringLength(255)]
        public string EXTRA_INFO { get; set; }

        public DateTime? DOE { get; set; }

        public DateTime? DOM { get; set; }

        [StringLength(50)]
        public string E_userid { get; set; }

        [StringLength(50)]
        public string M_userid { get; set; }
        [DisplayName("Delivery")]
        public int? DEL_PARTY { get; set; }
        [DisplayName("TCS Percent")]
        public decimal? TCSPer { get; set; }
        [DisplayName("TCS Value")]
        public decimal? TCSVal { get; set; }
    }
}
