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
using SIC.Labs.Second.Components.Services.Validators;
using SIC.Labs.Third.Models;
using SIC.Labs.Third.Models.ViewModels;

namespace SIC.Labs.Third.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DAO _dataAccess;

        private readonly IMapper _mapper;

        private readonly ILogger<CommoditiesController> _logger;

        public OrdersController(DAO dataAccess, IMapper mapper, ILogger<CommoditiesController> logger)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = _mapper
                .Map<List<OrderViewModel>>(await _dataAccess.Orders.GetCollectionAsync());

            foreach(var order in orders)
            {
                await GetFieldsForOrder(order);
            }

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var order = _mapper
                    .Map<Order, OrderViewModel>(await _dataAccess.Orders.ReadAsync(id));

            await GetFieldsForOrder(order);

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            OrderViewModel order = new OrderViewModel();

            await GetListsForView(order);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel order)
        {
            try
            {
                var orderForAdd = _mapper.Map<OrderViewModel, Order>(order);

                orderForAdd.Validate();

                if (!ModelState.IsValid)
                    throw new OrderException("Order model isn't valid!!!");


                await _dataAccess.Orders.CreateAsync(orderForAdd);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await GetListsForView(order);
                return View(order);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            OrderViewModel order = _mapper
                .Map<Order, OrderViewModel>(await _dataAccess.Orders.ReadAsync(id));

            await GetListsForView(order);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderViewModel order)
        {
            try
            {
                var orderForEdit = _mapper.Map<OrderViewModel, Order>(order);

                orderForEdit.Validate();

                if (!ModelState.IsValid)
                    throw new OrderException("Order model isn't valid!!!");
            

                await _dataAccess.Orders.UpdateAsync(orderForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await GetListsForView(order);
                return View(order);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var order = _mapper
                        .Map<Order, OrderViewModel>(await _dataAccess.Orders.ReadAsync(id));

            await GetFieldsForOrder(order);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(OrderViewModel order)
        {
            try
            {
                await _dataAccess.Orders.DeleteAsync(order.Id);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(order);
            }
        }

        private async Task GetFieldsForOrder(OrderViewModel order)
        {
            order.Employee = _mapper
                .Map<Employee, EmployeeViewModel>(await _dataAccess.Employees.ReadAsync(order.EmployeeId));

            order.StockItem = _mapper
                .Map<StockItem, StockItemViewModel>(await _dataAccess.StockItems.ReadAsync(order.StockItemId));

            order.StockItem.Commodity = _mapper
                    .Map<Commodity, CommodityViewModel>(await _dataAccess.Commodities.ReadAsync(order.StockItem.CommodityId));
        }

        private async Task GetListsForView(OrderViewModel order)
        {
            var stockItems = _mapper
                .Map<List<StockItemViewModel>>(await _dataAccess.StockItems.GetCollectionAsync());

            foreach(var stockItem in stockItems)
            {
                stockItem.Commodity = _mapper
                                .Map<Commodity, CommodityViewModel>(await _dataAccess.Commodities.ReadAsync(stockItem.CommodityId));
            }

            var collectionForView = stockItems.Select(t => new { t.Id, StockItemName = t.Commodity.Name });

            order.Statuses = new SelectList((OrderStatus[])Enum.GetValues(typeof(OrderStatus)));

            order.StockItems = new SelectList(collectionForView, "Id", "StockItemName");

            order.Employees = new SelectList(await _dataAccess.Employees.GetCollectionAsync(), "Id", "FullName");
        }

    }
}
