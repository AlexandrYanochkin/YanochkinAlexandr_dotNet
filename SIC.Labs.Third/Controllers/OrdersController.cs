﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIC.Labs.Second.Components.DAL;
using SIC.Labs.Second.Components.Models.DTO;
using SIC.Labs.Second.Components.Models.Exceptions;
using SIC.Labs.Second.Components.Services.Validators;
using SIC.Labs.Third.Models;

namespace SIC.Labs.Third.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DAO _dataAccess;

        public OrdersController(DAO dataAccess)
        {
            _dataAccess = dataAccess;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = (await _dataAccess.Orders.GetCollectionAsync())
                .MapCollection<Order, OrderViewModel>();

            foreach(var order in orders)
            {
                await GetFieldsForOrder(order);
            }

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var order = (await _dataAccess.Orders.ReadAsync(id))
                    .Map<Order, OrderViewModel>();

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
                var orderForAdd = order.Map<OrderViewModel, Order>();

                orderForAdd.Validate();

                if (!ModelState.IsValid)
                    throw new OrderException("Order model isn't valid!!!");


                await _dataAccess.Orders.CreateAsync(orderForAdd);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await GetListsForView(order);
                return View(order);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            OrderViewModel order = (await _dataAccess.Orders.ReadAsync(id))
                .Map<Order, OrderViewModel>();

            await GetListsForView(order);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderViewModel order)
        {
            try
            {
                var orderForEdit = order.Map<OrderViewModel, Order>();

                orderForEdit.Validate();

                if (!ModelState.IsValid)
                    throw new OrderException("Order model isn't valid!!!");
            

                await _dataAccess.Orders.UpdateAsync(orderForEdit);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await GetListsForView(order);
                return View(order);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var order = (await _dataAccess.Orders.ReadAsync(id))
                        .Map<Order, OrderViewModel>();

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
                return View(order);
            }
        }

        private async Task GetFieldsForOrder(OrderViewModel order)
        {
            order.Employee = (await _dataAccess.Employees.ReadAsync(order.EmployeeId))
                .Map<Employee, EmployeeViewModel>();

            order.StockItem = (await _dataAccess.StockItems.ReadAsync(order.StockItemId))
                .Map<StockItem, StockItemViewModel>();

            order.StockItem.Commodity = (await _dataAccess.Commodities.ReadAsync(order.StockItem.CommodityId))
                    .Map<Commodity, CommodityViewModel>();
        }

        private async Task GetListsForView(OrderViewModel order)
        {
            var stockItems = (await _dataAccess.StockItems.GetCollectionAsync())
                .MapCollection<StockItem, StockItemViewModel>();

            foreach(var stockItem in stockItems)
            {
                stockItem.Commodity = (await _dataAccess.Commodities.ReadAsync(stockItem.CommodityId))
                                .Map<Commodity, CommodityViewModel>();
            }

            var collectionForView = stockItems.Select(t => new { t.Id, StockItemName = t.Commodity.Name });

            order.Statuses = new SelectList((OrderStatus[])Enum.GetValues(typeof(OrderStatus)));

            order.StockItems = new SelectList(collectionForView, "Id", "StockItemName");

            order.Employees = new SelectList(await _dataAccess.Employees.GetCollectionAsync(), "Id", "FullName");
        }

    }
}