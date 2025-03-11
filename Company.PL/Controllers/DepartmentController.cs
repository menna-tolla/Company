using Company.BLL.Interfaces;
using Company.BLL.Repositores;
using Company.DAL.Models;
using Company.PL.Dtos;
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
            //var departments = _departmentRepository.GetAll();
            var departments = new List<Department>
        {
            new Department { Code = "D001", Name = "HR", CreateAt = DateTime.Now },
            new Department { Code = "D002", Name = "IT", CreateAt = DateTime.Now },
            new Department { Code = "D003", Name = "Finance", CreateAt = DateTime.Now }
        };
            return View(departments);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create (CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };

                var count = _departmentRepository.Add(department); 

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
