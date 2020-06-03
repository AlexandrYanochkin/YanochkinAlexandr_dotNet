using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;
using SIC.Labs.Third.Models.ViewModels;

namespace SIC.Labs.Third.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly DAO _dataAccess;

        private readonly IMapper _mapper;

        private readonly ILogger<CommoditiesController> _logger;

        public ManufacturersController(DAO dataAccess, IMapper mapper, ILogger<CommoditiesController> logger)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var manufacturers = await _dataAccess.Manufacturers.GetCollectionAsync();

            return View(_mapper.Map<List<ManufacturerViewModel>>(manufacturers));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var manufacturer = await _dataAccess.Manufacturers.ReadAsync(id);

            return View(_mapper.Map<Manufacturer, ManufacturerViewModel>(manufacturer));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManufacturerViewModel manufacturer)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ManufacturerException("Manufacturer model isn't valid!!!");

                var manufacturerForAdd = _mapper.Map<ManufacturerViewModel, Manufacturer>(manufacturer);

                await _dataAccess.Manufacturers.CreateAsync(manufacturerForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(manufacturer);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturer = await _dataAccess.Manufacturers.ReadAsync(id);       

            return View(_mapper.Map<Manufacturer, ManufacturerViewModel>(manufacturer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManufacturerViewModel manufacturer)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ManufacturerException("Manufacturer model isn't valid!!!");

                var manufacturerForEdit = _mapper.Map<ManufacturerViewModel, Manufacturer>(manufacturer);

                await _dataAccess.Manufacturers.UpdateAsync(manufacturerForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(manufacturer);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await _dataAccess.Manufacturers.ReadAsync(id);

            return View(_mapper.Map<Manufacturer, ManufacturerViewModel>(manufacturer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ManufacturerViewModel manufacturer)
        {
            try
            {
                await _dataAccess.Manufacturers.DeleteAsync(manufacturer.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(manufacturer);
            }
        }

    }
}
