namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int State_ID { get; set; }

        [StringLength(255)]
        [Required]
        [DisplayName("State Name")]
        public string State_Name { get; set; }

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
