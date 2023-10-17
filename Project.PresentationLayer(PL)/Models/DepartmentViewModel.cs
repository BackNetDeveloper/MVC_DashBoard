using System.ComponentModel.DataAnnotations;

namespace Project.PresentationLayer_PL_.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code is Required")]
        public int Code { get; set; }

        [Required(ErrorMessage = "Department Name is Required")]      // ErrorMessages Will Appear In UI
        [MinLength(3, ErrorMessage = "MinLength Is 3 Charachters")]
        public string Name { get; set; }
    }
}
