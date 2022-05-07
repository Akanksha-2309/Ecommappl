using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }
        [Column(TypeName = "varchar(25)")]

        public string Name { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Email { get; set; }
        public long ContactNumber { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(10)")]

        public string City { get; set; }
        [Column(TypeName = "varchar(10)")]

        public string State { get; set; }
        public int Pincode { get; set; }
        [Column(TypeName = "varchar(10)")]

        public string Role { get; set; }

    }
}
