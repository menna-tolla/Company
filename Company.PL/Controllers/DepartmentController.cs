using AutoMapper;
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
        private readonly IMapper _mapper;

        //Ask CLR Create Object From DepartmentRepository
        public DepartmentController(
            IDepartmentRepository departmentRepository ,
            IEmplyeeRepository employeeRepository,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        //----------------------Index-----------------------

        [HttpGet] // get 
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Department> departments;

            if (string.IsNullOrEmpty(SearchInput))
            {
                departments = _departmentRepository.GetAll();
            }
            else
            {
                departments = _departmentRepository.GetByName(SearchInput);
            }

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

            Console.WriteLine($"ModelState Valid: {ModelState.IsValid}");
            Console.WriteLine($"Code: {model.Code ?? "NULL"}");
            Console.WriteLine($"Name: {model.Name ?? "NULL"}");
            Console.WriteLine($"CreateAt: {model.CreateAt}");


            try
            {
                if (ModelState.IsValid)
            {
                    //var department = new Department()
                    //{
                    //    Code = model.Code,
                    //    Name = model.Name,
                    //    CreateAt = model.CreateAt,
                    //};
                    var department = _mapper.Map<Department>(model);

                    var count = _departmentRepository.Add(department);

                if (count > 0)
                {
                        TempData["Message"] = "Department is Created";

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

                Code = department.Code

            };

            return View(employeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateDepartmentDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var department = _departmentRepository.Get(id);
            if (department == null)
                return NotFound();

            // تحديث البيانات
            department.Code = model.Code;
            department.Name = model.Name;
            department.CreateAt = model.CreateAt;

            var count = _departmentRepository.Update(department);

            if (count > 0)
                return RedirectToAction(nameof(Index));

            return View(model); // ✅ نرجع نفس الـ ViewModel
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
        public IActionResult Delete([FromRoute] int id)
        {
            var department = _departmentRepository.Get(id);
            if (department == null)
                return NotFound();

            var count = _departmentRepository.Delete(department);

            if (count > 0)
                return RedirectToAction(nameof(Index));

            // ترجع كـ ViewModel عشان الـ View متوقع CreateDepartmentDto
            var model = new CreateDepartmentDto
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };

            return View(model);
        }



        //---------------------------------------------

    }
}
