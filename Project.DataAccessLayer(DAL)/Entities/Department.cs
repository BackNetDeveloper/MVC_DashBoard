using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer_DAL_.Entities
{
    public class Department
    {
        public Department()
        {
            DateOfCreation = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code is Required")]
        public int Code { get; set; }

        [Required(ErrorMessage = "Department Name is Required")]      // ErrorMessages Will Appear In UI
        [MinLength(3,ErrorMessage = "MinLength Is 3 Charachters")]   
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public virtual ICollection<Employee>  Employees { get; set; } = new HashSet<Employee>();

    }
}
