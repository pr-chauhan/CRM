namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State_ID { get; set; }

        [StringLength(255)]
        public string State_Name { get; set; }

        public DateTime? DoE { get; set; }

        public DateTime? DoM { get; set; }

        [StringLength(50)]
        public string E_UserID { get; set; }

        [StringLength(50)]
        public string M_UserID { get; set; }
    }
}
