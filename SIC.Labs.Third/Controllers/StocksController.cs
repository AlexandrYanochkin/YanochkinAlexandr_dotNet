using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Third.Models;

namespace SIC.Labs.Third.Controllers
{
    public class StocksController : Controller
    {
        public DAO DataAccess { get; set; }

        public StocksController(DAO dataAccess)
        {
            DataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stockCollection = (await DataAccess.Stocks.GetCollectionAsync()).MapCollection<Stock, StockViewModel>();

            return View(stockCollection);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var stock = (await DataAccess.Stocks.ReadAsync(id)).Map<Stock, StockViewModel>();

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

                var stockForAdd = stock.Map<StockViewModel, Stock>();

                await DataAccess.Stocks.CreateAsync(stockForAdd);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stock = (await DataAccess.Stocks.ReadAsync(id)).Map<Stock, StockViewModel>();

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

                var stockForEdit = stock.Map<StockViewModel, Stock>();

                await DataAccess.Stocks.UpdateAsync(stockForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = (await DataAccess.Stocks.ReadAsync(id)).Map<Stock, StockViewModel>();

            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(StockViewModel stock)
        {
            try
            {
                await DataAccess.Stocks.DeleteAsync(stock.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
