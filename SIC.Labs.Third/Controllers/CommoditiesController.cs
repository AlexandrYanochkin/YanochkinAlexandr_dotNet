using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Third.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIC.Labs.Second.Components.Models.Exceptions;
using Microsoft.Extensions.Logging;

namespace SIC.Labs.Third.Controllers
{
    public class CommoditiesController : Controller
    {
        public DAO DataAccess { get; set; }

        public CommoditiesController(DAO dataAccess)
        {
            DataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var commodities = (await DataAccess.Commodities.GetCollectionAsync())
                .MapCollection<Commodity, CommodityViewModel>();

            foreach (var commodity in commodities)
            {
                commodity.Manufacturer = (await DataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId))
                    .Map<Manufacturer, ManufacturerViewModel>();
            }


            return View(commodities);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var commodity = (await DataAccess.Commodities.ReadAsync(id)).Map<Commodity, CommodityViewModel>();

            commodity.Manufacturer = (await DataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId))
                .Map<Manufacturer, ManufacturerViewModel>();

            return View(commodity);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CommodityViewModel commodity = new CommodityViewModel();

            var manufacturers = await DataAccess.Manufacturers.GetCollectionAsync();

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

                var commodityForAdd = commodity.Map<CommodityViewModel, Commodity>();

                await DataAccess.Commodities.CreateAsync(commodityForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                commodity.Manufacturers = new SelectList(await DataAccess.Manufacturers.GetCollectionAsync(), "Id", "Name");
                return View(commodity);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var commodity = (await DataAccess.Commodities.ReadAsync(id)).Map<Commodity, CommodityViewModel>();       

            var manufacturers = await DataAccess.Manufacturers.GetCollectionAsync();

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

                var commodityForEdit = commodity.Map<CommodityViewModel, Commodity>();

                await DataAccess.Commodities.UpdateAsync(commodityForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                commodity.Manufacturers = 
                    new SelectList(await DataAccess.Manufacturers.GetCollectionAsync(), "Id", "Name");
                return View(commodity);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var commodity = (await DataAccess.Commodities.ReadAsync(id)).Map<Commodity, CommodityViewModel>();

            commodity.Manufacturer = (await DataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId))
                .Map<Manufacturer, ManufacturerViewModel>();

            return View(commodity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CommodityViewModel commodity)
        {
            try
            {
                await DataAccess.Commodities.DeleteAsync(commodity.Id);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

    }
}
