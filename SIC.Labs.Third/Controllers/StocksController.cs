using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;
using SIC.Labs.Third.Models.ViewModels;

namespace SIC.Labs.Third.Controllers
{
    public class StocksController : Controller
    {
        private readonly DAO _dataAccess;

        private readonly IMapper _mapper;

        private readonly ILogger<CommoditiesController> _logger;

        public StocksController(DAO dataAccess, IMapper mapper, ILogger<CommoditiesController> logger)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stockCollection = _mapper
                .Map<List<StockViewModel>>(await _dataAccess.Stocks.GetCollectionAsync());

            return View(stockCollection);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var stock = _mapper.Map<Stock, StockViewModel>(await _dataAccess.Stocks.ReadAsync(id));

            return View(stock);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockViewModel stock)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new StockException("Stock model isn't valid!!!");

                var stockForAdd = _mapper.Map<StockViewModel, Stock>(stock);

                await _dataAccess.Stocks.CreateAsync(stockForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(stock);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stock = _mapper
                .Map<Stock, StockViewModel>(await _dataAccess.Stocks.ReadAsync(id));

            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockViewModel stock)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new StockException("Stock model isn't valid!!!");

                var stockForEdit = _mapper.Map<StockViewModel, Stock>(stock);

                await _dataAccess.Stocks.UpdateAsync(stockForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(stock);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = _mapper.Map<Stock, StockViewModel>(await _dataAccess.Stocks.ReadAsync(id));

            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StockViewModel stock)
        {
            try
            {
                await _dataAccess.Stocks.DeleteAsync(stock.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(stock);
            }
        }

    }
}
