using Company.BLL.Interfaces;
using Company.BLL.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        //Ask CLR Create Object From DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet] // get 
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();

            return View(departments);
            //return View();
        }
    }
}
