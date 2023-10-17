using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer_DAL_.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="MaxLength 50 charachter")]
        [MinLength(5,ErrorMessage ="MinLength 5 charachter")]
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime DateOfCreation { get; set; }= DateTime.Now;
        public string  ImageUrl { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


    }
}
