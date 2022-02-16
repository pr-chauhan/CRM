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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Consignee_ID { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("Consignee Name")]
        public string Consignee_Name { get; set; }

        [StringLength(255)]
        [DisplayName("Address")]
        public string address { get; set; }
        [Required]
        [DisplayName("City")]
        public int? City_ID { get; set; }

        [StringLength(255)]
        [DisplayName("Phone No")]
        public string Phone_No { get; set; }

        [StringLength(255)]
        [DisplayName("Fax No")]
        public string Fax { get; set; }

        [StringLength(255)]
        [DisplayName("Email ID")]
        public string Email_id { get; set; }

        [StringLength(255)]
        [DisplayName("Tin No")]
        public string Tin_no { get; set; }

        [StringLength(255)]
        [DisplayName("CST No")]
        public string CST_No { get; set; }

        [StringLength(255)]
        [DisplayName("ECC No")]
        public string ECC_No { get; set; }

        [StringLength(255)]
        [DisplayName("Range")]
        public string RAnge { get; set; }

        [StringLength(255)]
        [DisplayName("Division")]
        public string Division { get; set; }

        [StringLength(255)]
        [DisplayName("Pan No")]
        public string PANNO { get; set; }

        [StringLength(255)]
        [DisplayName("Commision Rate")]
        public string commission_rate { get; set; }

        [StringLength(255)]
        [DisplayName("GST No")]
        public string GSTNO { get; set; }
        [DisplayName("Date of Entry")]
        public DateTime? DoE { get; set; }

        [DisplayName("Date of Modify")]
        public DateTime? DoM { get; set; }

        [StringLength(50)]
        [DisplayName("Enter By")]
        public string E_UserID { get; set; }

        [StringLength(50)]
        [DisplayName("Modify By")]
        public string M_UserID { get; set; }
    }
}
