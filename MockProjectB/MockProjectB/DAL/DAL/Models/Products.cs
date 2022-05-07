using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName ="int")]
        public int pId { get; set; }

        [Display(Name = "Category")]
        [Column(TypeName = "int")]
        public int cId { get; set; }
        [ForeignKey("cId")]
        public virtual Category Category { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string pName { get; set; }
        [Column(TypeName = "float")]
        public float Price { get; set; }
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
        [Column(TypeName = "Datetime")]
        public DateTime DateofCreation { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Status { get; set; } = null;

    }
}
