using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int CmtId { get; set; }

       
        public DateTime Date { get; set; }

        [Display(Name = "User")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
       public virtual User User { get; set; }

        [Display(Name = "Products")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
       public virtual Products Products { get; set; }

        public int ParentId { get; set; }
        [DataType("varchar(200)")]
        
        public string Comments { get; set; }
    }
}
