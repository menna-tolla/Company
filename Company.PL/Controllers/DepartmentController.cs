using Company.BLL.Interfaces;
using Company.BLL.Repositores;
using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmplyeeRepository _employeeRepository;

        //Ask CLR Create Object From DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository , IEmplyeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        //----------------------Index-----------------------

        [HttpGet] // get 
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();

            return View(departments);
        }

        //----------------------Create-----------------------

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Create (CreateDepartmentDto model)
        {
            Console.WriteLine("POST Request Received");
            Console.WriteLine($"Model: Code={model.Code}, Name={model.Name}, CreateAt={model.CreateAt}");

            try
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

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            return View(model);
        }

        //---------------------Details------------------------

        #region Details 
        //[HttpGet] // get 
        //public IActionResult Details(int? id )
        //{
        //    if(id is null )  return BadRequest("InValilld Id");

        //    var department=_departmentRepository.Get(id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, mssage = $"Department with id = {id} is not found" });

        //    return View(department);
        //}

        #endregion

        [HttpGet] // get 
        public IActionResult Details(int? id , string viewName= "Details")
        {
            if (id is null) return BadRequest("InValilld Id");

            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, mssage = $"Department with id = {id} is not found" });

            return View(viewName , department);
        }

        //----------------------Edit-----------------------

        #region Edit
        //[HttpGet] // get 
        //public IActionResult Edit(int? id)
        //{
        //    if (id is null) return BadRequest("InValilld Id");

        //    var department = _departmentRepository.Get(id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, mssage = $"Department with id = {id} is not found" });

        //    return View(department);
        //}


        //[HttpGet] // get 
        //public IActionResult Edit(int? id)
        //{

        //    return Details(id, "Edit");
        //}
        #endregion



        //[HttpGet] // get 
        //public IActionResult Edit(int? id)
        //{
        //    if (id is null) return BadRequest("InValilld Id");

        //    var employee = _employeeRepository.Get(id.Value);

        //    ViewData["employee"] = employee;

        //    var department = _departmentRepository.Get(id.Value);

        //    if (department is null) return NotFound(new { StatusCode = 400, message = $"Department With Id : {id} is not found" });

        //    var departmentDto = new CreateDepartmentDto()
        //    {
        //        Name = department.Name,
        //        CreateAt = department.CreateAt,
        //    };
        //    return View(departmentDto);
        //}


        [HttpGet] // get 
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("InValilld Id");

            var department = _departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, mssage = $"Department with id = {id} is not found" });

            var employeeDto = new CreateDepartmentDto()
            {
                Name = department.Name,
               
                CreateAt = department.CreateAt,
                
                Code=department.Code

            };

            return View(employeeDto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( [FromRoute] int id,Department department)
        {
            if (id != department.Id) return BadRequest();

            var count = _departmentRepository.Update(department);

            if(ModelState.IsValid)
            {
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }

        //----------------------Delete-----------------------

        #region Delete
        //[HttpGet] // get 
        //public IActionResult Delete(int? id)
        //{
        //    if (id is null) return BadRequest("InValilld Id");

        //    var department = _departmentRepository.Get(id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, mssage = $"Department with id = {id} is not found" });

        //    return View(department);
        //}
        #endregion

        [HttpGet] // get 
        public IActionResult Delete(int? id)
        {
            return Details(id , "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();

            var count = _departmentRepository.Delete(department);

            if (ModelState.IsValid)
            {
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }

        //---------------------------------------------

    }
}
