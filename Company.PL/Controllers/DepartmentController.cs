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

        //Ask CLR Create Object From DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        //----------------------Index-----------------------

        [HttpGet] // get 
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();

        //    var departments = new List<Department>
        //{
        //    new Department { Code = "D001", Name = "HR", CreateAt = DateTime.Now },
        //    new Department { Code = "D002", Name = "IT", CreateAt = DateTime.Now },
        //    new Department { Code = "D003", Name = "Finance", CreateAt = DateTime.Now }
        //};

            return View(departments);
        }

        //----------------------Create-----------------------

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
        //public IActionResult Edit(int ? id)
        //{
        //    if (id is null) return BadRequest("InValilld Id");

        //    var department = _departmentRepository.Get(id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, mssage = $"Department with id = {id} is not found" });

        //    return View(department);
        //}
        #endregion

        [HttpGet] // get 
        public IActionResult Edit(int? id)
        {
           
            return Details( id , "Edit");
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
