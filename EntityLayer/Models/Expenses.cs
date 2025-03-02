using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace EntityLayer.Models
{
    public class Expenses
    {
        [Key]
        public int expenseId { get; set; }

        [Required(ErrorMessage = "ExpenseName is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string category { get; set; }

        [Required(ErrorMessage = "Expenditure is required")]
        public int expenditure { get; set; }
        public DateTime Date { get; set; }
        public int userId { get; set; }
    }
   
}
