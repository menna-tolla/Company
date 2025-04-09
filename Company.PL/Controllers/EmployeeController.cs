using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositores;
using Company.DAL.Models;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmplyeeRepository _emplyeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        //Ask CLR Create Object From DepartmentRepository
        public EmployeeController(
            IEmplyeeRepository emplyeeRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _emplyeeRepository = emplyeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        //----------------------Index-----------------------

        [HttpGet] // get 
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _emplyeeRepository.GetAll();
            }
            else
            {
                employees = _emplyeeRepository.GetByName(SearchInput);
            }

            // Dictionary   : 3 Property
            // 1. ViewData  : Transfer Extra Information From Controller (Action) To View

            //ViewData["Message"] = "Hello From ViewDat,a";

            // 2. ViewBag   : Transfer Extra Information From Controller (Action) To View

            //ViewBag.Massage = "Hello From ViewBag";

            //ViewBag.Message = new { Message = "Hello From ViewBag" };

            // 3. TempData  :

            return View(employees);
        }

        //----------------------Create-----------------------

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;

            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                var count = _emplyeeRepository.Add(employee);

                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created";
                    return RedirectToAction(nameof(Index));
                }
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
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("InValilld Id");

            var employee = _emplyeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, mssage = $"Employee with id = {id} is not found" });

            return View(viewName, employee);
        }

        //----------------------Edit-----------------------

        [HttpGet] // get 
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;

            if (id is null) return BadRequest("InValilld Id");

            var employee = _emplyeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, mssage = $"Department with id = {id} is not found" });

            var employeeDto = new CreateEmployeeDto()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                Age = employee.Age,

                HiringDate = employee.HiringDate,
                Phone = employee.Phone,
                CreateAt = employee.CreateAt,
                Email = employee.Email,

            };

            return View(employeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _emplyeeRepository.Get(id);

                if (employee == null) return NotFound(new { statusCode = 400, messege = $"Employee With Id:{id} is Not Found" });


                employee.Email = model.Email;
                employee.Phone = model.Phone;
                employee.Address = model.Address;
                employee.Age = model.Age;
                employee.HiringDate = model.HiringDate;
                employee.Name = model.Name;
                employee.IsActive = model.IsActive;
                employee.IsDeleted = model.IsDeleted;
                employee.CreateAt = model.CreateAt;
                employee.Salary = model.Salary;
                employee.DepartmentId = model.DepartmentId;

                var count = _emplyeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
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
            return Details(id, "Delete");
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute] int id, Employee employee)
        //{
        //    if (id != employee.Id) return BadRequest();

        //    var count = _emplyeeRepository.Delete(employee);

        //    if (ModelState.IsValid)
        //    {
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    return View(employee);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id)
        {
            var employee = _emplyeeRepository.Get(id);

            if (employee == null)
                return NotFound();

            var count = _emplyeeRepository.Delete(employee);

            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        //--------------------
    }
}
