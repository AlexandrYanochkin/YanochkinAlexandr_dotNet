using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;

namespace SIC.Labs.Third.Controllers
{
    public class StockItemsController : Controller
    {
        public DAO DataAccess { get; set; }

        public StockItemsController(DAO dataAccess)
        {
            DataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stockItems = (await DataAccess.StockItems.GetCollectionAsync())
              .MapCollection<StockItem, StockItemViewModel>();

            foreach(var stockItem in stockItems)
            {
                await GetFieldsForStockItem(stockItem);
            }


            return View(stockItems);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var stockItem = (await DataAccess.StockItems.ReadAsync(id))
              .Map<StockItem, StockItemViewModel>();

            await GetFieldsForStockItem(stockItem);


            return View(stockItem);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            StockItemViewModel stockItem = new StockItemViewModel();

            await SetDropDownLists(stockItem);


            return View(stockItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockItemViewModel stockItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new StockItemException("StockItem model isn't valid!!!");

                var stockItemForAdd = stockItem.Map<StockItemViewModel, StockItem>();

                await DataAccess.StockItems.CreateAsync(stockItemForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                await SetDropDownLists(stockItem);
                return View(stockItem);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stockItem = (await DataAccess.StockItems.ReadAsync(id))
               .Map<StockItem, StockItemViewModel>();

            await SetDropDownLists(stockItem);

            return View(stockItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockItemViewModel stockItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new StockItemException("StockItem model isn't valid!!!");

                var stockItemForEdit = stockItem.Map<StockItemViewModel, StockItem>();

                await DataAccess.StockItems.UpdateAsync(stockItemForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                await SetDropDownLists(stockItem);

                return View(stockItem);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var stockItem = (await DataAccess.StockItems.ReadAsync(id))
                .Map<StockItem, StockItemViewModel>();

            await GetFieldsForStockItem(stockItem);


            return View(stockItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StockItemViewModel stockItem)
        {
            try
            {
                await DataAccess.StockItems.DeleteAsync(stockItem.Id);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(stockItem);
            }
        }

        private async Task GetFieldsForStockItem(StockItemViewModel stockItem)
        {
            stockItem.Stock = (await DataAccess.Stocks.ReadAsync(stockItem.StockId))
              .Map<Stock, StockViewModel>();

            stockItem.Commodity = (await DataAccess.Commodities.ReadAsync(stockItem.CommodityId))
                .Map<Commodity, CommodityViewModel>();
        }

        private async Task SetDropDownLists(StockItemViewModel stockItem)
        {

            stockItem.Stocks = new SelectList(await DataAccess.Stocks.GetCollectionAsync(), "Id", "Name");

            stockItem.Commodities = new SelectList(await DataAccess.Commodities.GetCollectionAsync(), "Id", "Name");
        }

    }
}
