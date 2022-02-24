namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consignee")]
    public partial class Consignee
    {
        [Key]
        [DisplayName("Consignee ID")]
        public int Consignee_ID { get; set; }

        [StringLength(255)]
        [DisplayName("Consignee Name")]
        public string Consignee_Name { get; set; }

        [StringLength(255)]
        [DisplayName("Address")]
        public string address { get; set; }
        [DisplayName("City")]
        public int? City_ID { get; set; }

        [StringLength(255)]
        [DisplayName("Phone Number")]
        public string Phone_No { get; set; }

        [StringLength(255)]
        [DisplayName("Fax Number")]
        public string Fax { get; set; }
        [DisplayName("Email ID")]
        [StringLength(255)]
        public string Email_id { get; set; }

        [StringLength(255)]
        [DisplayName("TIN No")]
        public string Tin_no { get; set; }

        [StringLength(255)]
        [DisplayName("CST No.")]
        public string CST_No { get; set; }

        [StringLength(255)]
        [DisplayName("ECC No.")]
        public string ECC_No { get; set; }

        [StringLength(255)]
        [DisplayName("Range")]
        public string RAnge { get; set; }

        [StringLength(255)]
        [DisplayName("Division")]
        public string Division { get; set; }

        [StringLength(255)]
        [DisplayName("PAN No.")]
        public string PANNO { get; set; }

        [StringLength(255)]
        [DisplayName("Commission Rate")]
        public string commission_rate { get; set; }

        [StringLength(255)]
        [DisplayName("GST No.")]
        public string GSTNO { get; set; }

        public DateTime? DoE { get; set; }

        public DateTime? DoM { get; set; }

        [StringLength(50)]
        public string E_UserID { get; set; }

        [StringLength(50)]
        public string M_UserID { get; set; }
    }
}
