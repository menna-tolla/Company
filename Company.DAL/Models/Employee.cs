using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [Column("CreatAt")]
        public DateTime CreateAt { set; get; }
        public DateTime HiringDate { get; set; }


        [DisplayName ("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }


    }
}
