namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consignee")]
    public partial class Consignee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Consignee_ID { get; set; }

        [StringLength(255)]
        public string Consignee_Name { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        public int? City_ID { get; set; }

        [StringLength(255)]
        public string Phone_No { get; set; }

        [StringLength(255)]
        public string Fax { get; set; }

        [StringLength(255)]
        public string Email_id { get; set; }

        [StringLength(255)]
        public string Tin_no { get; set; }

        [StringLength(255)]
        public string CST_No { get; set; }

        [StringLength(255)]
        public string ECC_No { get; set; }

        [StringLength(255)]
        public string RAnge { get; set; }

        [StringLength(255)]
        public string Division { get; set; }

        [StringLength(255)]
        public string PANNO { get; set; }

        [StringLength(255)]
        public string commission_rate { get; set; }

        [StringLength(255)]
        public string GSTNO { get; set; }

        public DateTime? DoE { get; set; }

        public DateTime? DoM { get; set; }

        [StringLength(50)]
        public string E_UserID { get; set; }

        [StringLength(50)]
        public string M_UserID { get; set; }
    }
}
