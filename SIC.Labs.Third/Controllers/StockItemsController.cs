using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;
using SIC.Labs.Third.Models.ViewModels;

namespace SIC.Labs.Third.Controllers
{
    public class StockItemsController : Controller
    {
        private readonly DAO _dataAccess;

        private readonly IMapper _mapper;

        private readonly ILogger<CommoditiesController> _logger;

        public StockItemsController(DAO dataAccess, IMapper mapper, ILogger<CommoditiesController> logger)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stockItems = _mapper
              .Map<List<StockItemViewModel>>(await _dataAccess.StockItems.GetCollectionAsync());

            foreach(var stockItem in stockItems)
            {
                await GetFieldsForStockItem(stockItem);
            }


            return View(stockItems);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var stockItem = _mapper
              .Map<StockItem, StockItemViewModel>(await _dataAccess.StockItems.ReadAsync(id));

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

                var stockItemForAdd = _mapper.Map<StockItemViewModel, StockItem>(stockItem);

                await _dataAccess.StockItems.CreateAsync(stockItemForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await SetDropDownLists(stockItem);
                return View(stockItem);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stockItem = _mapper
               .Map<StockItem, StockItemViewModel>(await _dataAccess.StockItems.ReadAsync(id));

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

                var stockItemForEdit = _mapper.Map<StockItemViewModel, StockItem>(stockItem);

                await _dataAccess.StockItems.UpdateAsync(stockItemForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await SetDropDownLists(stockItem);
                return View(stockItem);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var stockItem = _mapper
                .Map<StockItem, StockItemViewModel>(await _dataAccess.StockItems.ReadAsync(id));

            await GetFieldsForStockItem(stockItem);


            return View(stockItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StockItemViewModel stockItem)
        {
            try
            {
                await _dataAccess.StockItems.DeleteAsync(stockItem.Id);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(stockItem);
            }
        }

        private async Task GetFieldsForStockItem(StockItemViewModel stockItem)
        {
            stockItem.Stock = _mapper
              .Map<Stock, StockViewModel>(await _dataAccess.Stocks.ReadAsync(stockItem.StockId));

            stockItem.Commodity = _mapper
                .Map<Commodity, CommodityViewModel>(await _dataAccess.Commodities.ReadAsync(stockItem.CommodityId));
        }

        private async Task SetDropDownLists(StockItemViewModel stockItem)
        {

            stockItem.Stocks = new SelectList(await _dataAccess.Stocks.GetCollectionAsync(), "Id", "Name");

            stockItem.Commodities = new SelectList(await _dataAccess.Commodities.GetCollectionAsync(), "Id", "Name");
        }

    }
}
