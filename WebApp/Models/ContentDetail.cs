namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;
    using System.Web.UI.WebControls;

    public partial class ContentDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContentDetail()
        {
            Events = new HashSet<Event>();
            News = new HashSet<News>();
            Resources = new HashSet<Resource>();
        }

        [Key]
        public int ContentID { get; set; }

        [StringLength(1000)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        [StringLength(1000)]
        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishDate { get; set; }

        [StringLength(1000)]
        public string Image { get; set; }

        [StringLength(1000)]
        [DataType(DataType.Url)]
        public string URL { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resource> Resources { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser UpdatedBy { get; set; }


        //Not Mapped are below
        [NotMapped]
        public HttpPostedFileBase Upload { get; set; }
    }
}
