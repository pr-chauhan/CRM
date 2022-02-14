namespace EntityClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_detail
    {
        [Key]
        [StringLength(50)]
        public string User_Name { get; set; }

        [StringLength(50)]
        public string Passwrd { get; set; }

        public DateTime? DoE { get; set; }

        public DateTime? DoM { get; set; }

        [StringLength(50)]
        public string E_UserID { get; set; }

        [StringLength(50)]
        public string M_UserID { get; set; }
    }
}
