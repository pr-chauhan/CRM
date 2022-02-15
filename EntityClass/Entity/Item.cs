namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Item_ID { get; set; }

        public decimal? Tarriff_no { get; set; }

        [StringLength(255)]
        public string Item_Name { get; set; }

        [StringLength(255)]
        public string Full_Desc { get; set; }

        public DateTime? DoE { get; set; }

        public DateTime? DoM { get; set; }

        [StringLength(50)]
        public string E_UserID { get; set; }

        [StringLength(50)]
        public string M_UserID { get; set; }
    }
}
