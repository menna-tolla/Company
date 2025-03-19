using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Company.BLL.Repositores
{
    public class DepartmentRepository :  GenaricRepository<Department> , IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext context) : base(context) //Ask CLR Creat Object
        {
             
        }
     

    }
}
