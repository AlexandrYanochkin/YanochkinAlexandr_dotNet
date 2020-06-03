using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;
using SIC.Labs.Third.Models.ViewModels;

namespace SIC.Labs.Third.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DAO _dataAccess;

        private readonly IMapper _mapper;

        private readonly ILogger<CommoditiesController> _logger;

        public EmployeesController(DAO dataAccess, IMapper mapper, ILogger<CommoditiesController> logger)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _dataAccess.Employees.GetCollectionAsync();

            return View(_mapper.Map<List<EmployeeViewModel>>(employees));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _dataAccess.Employees.ReadAsync(id);

            return View(_mapper.Map<Employee, EmployeeViewModel>(employee));
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

                var employeeForAdd = _mapper.Map<EmployeeViewModel, Employee>(employee);

                await _dataAccess.Employees.CreateAsync(employeeForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(employee);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _dataAccess.Employees.ReadAsync(id);

            return View(_mapper.Map<Employee, EmployeeViewModel>(employee));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new EmployeeException("Incorrect employee!!!");

                var employeeForEdit = _mapper.Map<EmployeeViewModel, Employee>(employee);

                await _dataAccess.Employees.UpdateAsync(employeeForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(employee);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _dataAccess.Employees.ReadAsync(id);

            return View(_mapper.Map<Employee, EmployeeViewModel>(employee));
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
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(employee);
            }
        }

    }
}
