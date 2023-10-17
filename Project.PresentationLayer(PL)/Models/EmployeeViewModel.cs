using Project.DataAccessLayer_DAL_.Entities;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace Project.PresentationLayer_PL_.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "MaxLength 50 charachter")]
        [MinLength(5, ErrorMessage = "MinLength 5 charachter")]
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
        public int DepartmentId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public  DepartmentViewModel Department { get; set; }
    }
}
