using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmplyeeRepository
    {
        IEnumerable<Employee> GetAll();

        Employee? Get(int Id);

        int Add(Employee department);

        int Update(Employee department);

        int Delete(Employee department);
    }
}
