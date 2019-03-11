namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hospital")]
    public partial class Hospital
    {
        public int HospitalID { get; set; }

        [StringLength(1000)]
        public string HospitalName { get; set; }

        public string Description { get; set; }

        [StringLength(1000)]
        [DataType(DataType.Url)]
        public string WebsiteURL { get; set; }

        [StringLength(1000)]
        public string LogoImage { get; set; }

        public int? AddressID { get; set; }

        public virtual Address Address { get; set; }
    }
}
