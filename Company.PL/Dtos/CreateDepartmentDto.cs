using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Code in Required !")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name in Required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt in Required !")]
        public DateTime CreateAt { get; set; }
    }
}
