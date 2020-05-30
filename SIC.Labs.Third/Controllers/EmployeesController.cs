using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;

namespace SIC.Labs.Third.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DAO _dataAccess;

        public EmployeesController(DAO dataAccess)
        {
            _dataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _dataAccess.Employees.GetCollectionAsync();

            return View(employees.MapCollection<Employee, EmployeeViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _dataAccess.Employees.ReadAsync(id);

            return View(employee.Map<Employee, EmployeeViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                     throw new EmployeeException("Incorrect employee!!!");

                var employeeForAdd = employee.Map<EmployeeViewModel, Employee>();

                await _dataAccess.Employees.CreateAsync(employeeForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _dataAccess.Employees.ReadAsync(id);

            return View(employee.Map<Employee, EmployeeViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new EmployeeException("Incorrect employee!!!");

                var employeeForEdit = employee.Map<EmployeeViewModel, Employee>();

                await _dataAccess.Employees.UpdateAsync(employeeForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _dataAccess.Employees.ReadAsync(id);

            return View(employee.Map<Employee, EmployeeViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employee)
        {
            try
            {
                await _dataAccess.Employees.DeleteAsync(employee.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
