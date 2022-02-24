namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("City ID")]
        public int City_ID { get; set; }
        [DisplayName("State ID")]
        public int? State_ID { get; set; }

        [StringLength(255)]
        [DisplayName("City Name")]
        public string City_Name { get; set; }

        public DateTime? DoE { get; set; }

        public DateTime? DoM { get; set; }

        [StringLength(50)]
        public string E_UserID { get; set; }

        [StringLength(50)]
        public string M_UserID { get; set; }
    }
}
