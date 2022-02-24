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
        [DisplayName("item ID")]
        public int Item_ID { get; set; }

        [DisplayName("Tarrif No")]
        public decimal? Tarriff_no { get; set; }

        [StringLength(255)]
        [DisplayName("Item Name")]
        public string Item_Name { get; set; }

        [StringLength(255)]
        [DisplayName("Full Description")]
        public string Full_Desc { get; set; }

        public DateTime? DoE { get; set; }

        public DateTime? DoM { get; set; }

        [StringLength(50)]
        public string E_UserID { get; set; }

        [StringLength(50)]
        public string M_UserID { get; set; }
    }
}
