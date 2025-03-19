using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Department
    {
        public int Id { set; get; }

        public string Code { set; get; }

        public string Name { set; get; }

        [Column("CreatAt")]
        public DateTime CreateAt { set; get; }


    }
}
