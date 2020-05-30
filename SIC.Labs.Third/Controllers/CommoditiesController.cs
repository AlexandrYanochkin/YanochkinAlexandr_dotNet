using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Third.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIC.Labs.Second.Components.Models.Exceptions;

namespace SIC.Labs.Third.Controllers
{
    public class CommoditiesController : Controller
    {
        private readonly DAO _dataAccess;

        public CommoditiesController(DAO dataAccess)
        {
            _dataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var commodities = (await _dataAccess.Commodities.GetCollectionAsync())
                .MapCollection<Commodity, CommodityViewModel>();

            foreach (var commodity in commodities)
            {
                commodity.Manufacturer = (await _dataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId))
                    .Map<Manufacturer, ManufacturerViewModel>();
            }


            return View(commodities);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var commodity = (await _dataAccess.Commodities.ReadAsync(id)).Map<Commodity, CommodityViewModel>();

            commodity.Manufacturer = (await _dataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId))
                .Map<Manufacturer, ManufacturerViewModel>();

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

                var commodityForAdd = commodity.Map<CommodityViewModel, Commodity>();

                await _dataAccess.Commodities.CreateAsync(commodityForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                commodity.Manufacturers = new SelectList(await _dataAccess.Manufacturers.GetCollectionAsync(), "Id", "Name");
                return View(commodity);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var commodity = (await _dataAccess.Commodities.ReadAsync(id)).Map<Commodity, CommodityViewModel>();       

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

                var commodityForEdit = commodity.Map<CommodityViewModel, Commodity>();

                await _dataAccess.Commodities.UpdateAsync(commodityForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                commodity.Manufacturers = 
                    new SelectList(await _dataAccess.Manufacturers.GetCollectionAsync(), "Id", "Name");
                return View(commodity);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var commodity = (await _dataAccess.Commodities.ReadAsync(id)).Map<Commodity, CommodityViewModel>();

            commodity.Manufacturer = (await _dataAccess.Manufacturers.ReadAsync(commodity.ManufacturerId))
                .Map<Manufacturer, ManufacturerViewModel>();

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
                return View();
            }
        }

    }
}
