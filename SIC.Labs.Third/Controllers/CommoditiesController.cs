using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Third.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIC.Labs.Second.Components.Models.Exceptions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SIC.Labs.Third.Models.ViewModels;
using System.Collections.Generic;

namespace SIC.Labs.Third.Controllers
{
    public class CommoditiesController : Controller
    {
        private readonly DAO _dataAccess;

        private readonly IMapper _mapper;

        private readonly ILogger<CommoditiesController> _logger;

        public CommoditiesController(DAO dataAccess, IMapper mapper, ILogger<CommoditiesController> logger)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var commodities = _mapper
                .Map<List<CommodityViewModel>>(await _dataAccess.Commodities.GetCollectionAsync());

            foreach (var commodity in commodities)
            {   
                commodity.Manufacturer = _mapper
                    .Map<Manufacturer, ManufacturerViewModel>(await _dataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId));
            }


            return View(commodities);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var commodity = _mapper.Map<Commodity, CommodityViewModel>(await _dataAccess.Commodities.ReadAsync(id));

            commodity.Manufacturer = _mapper
                .Map<Manufacturer, ManufacturerViewModel>(await _dataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId));
                

            return View(commodity);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CommodityViewModel commodity = new CommodityViewModel();

            var manufacturers = await _dataAccess.Manufacturers.GetCollectionAsync();

            commodity
                .Manufacturers = new SelectList(manufacturers, "Id", "Name");


            return View(commodity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommodityViewModel commodity)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new CommodityException("Commodity Model isn't valid!!!");

                var commodityForAdd = _mapper.Map<CommodityViewModel, Commodity>(commodity);

                await _dataAccess.Commodities.CreateAsync(commodityForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                commodity.Manufacturers = new SelectList(await _dataAccess.Manufacturers.GetCollectionAsync(), "Id", "Name");
                return View(commodity);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var commodity = _mapper.Map<Commodity, CommodityViewModel>(await _dataAccess.Commodities.ReadAsync(id));       

            var manufacturers = await _dataAccess.Manufacturers.GetCollectionAsync();

            commodity
                .Manufacturers = new SelectList(manufacturers, "Id", "Name");

            return View(commodity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CommodityViewModel commodity)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new CommodityException("Commodity Model isn't valid!!!");

                var commodityForEdit = _mapper.Map<CommodityViewModel, Commodity>(commodity);

                await _dataAccess.Commodities.UpdateAsync(commodityForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                commodity.Manufacturers = 
                    new SelectList(await _dataAccess.Manufacturers.GetCollectionAsync(), "Id", "Name");
                return View(commodity);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var commodity = _mapper.Map<Commodity, CommodityViewModel>(await _dataAccess.Commodities.ReadAsync(id));

            commodity.Manufacturer = _mapper
                .Map<Manufacturer, ManufacturerViewModel>(await _dataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId));

            return View(commodity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CommodityViewModel commodity)
        {
            try
            {
                await _dataAccess.Commodities.DeleteAsync(commodity.Id);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(commodity);
            }
        }

    }
}
