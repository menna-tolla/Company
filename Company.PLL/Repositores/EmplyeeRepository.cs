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
    public class EmplyeeRepository : GenaricRepository<Employee>, IEmplyeeRepository
    {
        private readonly CompanyDbContext _context;
        public EmplyeeRepository(CompanyDbContext context) : base(context) //Ask CLR Creat Object
        {
            _context = context;
        }

        public List<Employee> GetByName(string name)
        {
            return _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }

    }
}
