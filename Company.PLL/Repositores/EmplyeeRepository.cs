using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositores
{
    public class EmplyeeRepository : GenaricRepository<Employee> , IEmplyeeRepository
    {
        public EmplyeeRepository(CompanyDbContext context) : base(context) //Ask CLR Creat Object
        {
            
        }
       




    }
}
