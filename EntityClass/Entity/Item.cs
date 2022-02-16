namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Item_ID { get; set; }
        [Required]
        [DisplayName("Tarif No")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public decimal? Tarriff_no { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("Item Name")]
        public string Item_Name { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("Full Description")]
        public string Full_Desc { get; set; }

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
