using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int cId { get; set; }
        [Column(TypeName = "varchar(25)")]

        public string cName { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string Type { get; set; }

    }
}
