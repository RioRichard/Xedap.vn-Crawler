namespace ConsoleApp1.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductAttributes = new HashSet<ProductAttribute>();
        }

        [Key]
        public int IDProduct { get; set; }

        public int IDCategory { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public int? Price { get; set; }

        public int? Stock { get; set; }

        [StringLength(256)]
        public string ImageURL { get; set; }

        public bool? IsDelete { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
