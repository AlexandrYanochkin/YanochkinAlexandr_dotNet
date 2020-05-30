using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;

namespace SIC.Labs.Third.Controllers
{
    public class ManufacturersController : Controller
    {
        public DAO DataAccess { get; set; }
 
        public ManufacturersController(DAO dataAccess)
        {
            DataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var manufacturers = await DataAccess.Manufacturers.GetCollectionAsync();

            return View(manufacturers.MapCollection<Manufacturer, ManufacturerViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var manufacturer = await DataAccess.Manufacturers.ReadAsync(id);

            return View(manufacturer.Map<Manufacturer, ManufacturerViewModel>());
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

                var manufacturerForAdd = manufacturer.Map<ManufacturerViewModel, Manufacturer>();

                await DataAccess.Manufacturers.CreateAsync(manufacturerForAdd);


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
            var manufacturer = await DataAccess.Manufacturers.ReadAsync(id);       

            return View(manufacturer.Map<Manufacturer, ManufacturerViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManufacturerViewModel manufacturer)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ManufacturerException("Manufacturer model isn't valid!!!");

                var manufacturerForEdit = manufacturer.Map<ManufacturerViewModel, Manufacturer>();

                await DataAccess.Manufacturers.UpdateAsync(manufacturerForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await DataAccess.Manufacturers.ReadAsync(id);

            return View(manufacturer.Map<Manufacturer, ManufacturerViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ManufacturerViewModel manufacturer)
        {
            try
            {
                await DataAccess.Manufacturers.DeleteAsync(manufacturer.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
