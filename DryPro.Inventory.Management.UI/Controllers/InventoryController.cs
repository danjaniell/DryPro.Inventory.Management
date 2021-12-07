﻿using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryCreateViewModel _inventoryCreateViewModel;
        private readonly InventoryDetailsViewModel _inventoryDetailsViewModel;

        public InventoryController(InventoryCreateViewModel inventoryCreateViewModel,
                                   InventoryDetailsViewModel inventoryDetailsViewModel)
        {
            _inventoryCreateViewModel = inventoryCreateViewModel;
            _inventoryDetailsViewModel = inventoryDetailsViewModel;
        }

        public async Task<IActionResult> Index()
        {
            var products = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Product/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(products);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View(_inventoryCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int count, Product product)
        {
            Product result = null;
            product.AuxilliaryItems = _inventoryCreateViewModel.AuxItemCreateViewModel.AuxilliaryItems;
            if (ModelState.IsValid)
            {
                var command = ProductMapper.Mapper.Map<CreateProductCommand>(product);
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    for (int i = 0; i < count; i++)
                    {
                        using (var response = await httpClient.PostAsync("https://localhost:5001/api/Product/Add", request))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<Product>(apiResponse);
                        }
                    }
                }
                _inventoryCreateViewModel.Clear();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Product result = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Product/Get/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Product>(apiResponse);
                    _inventoryDetailsViewModel.Product = result;
                }
            }
            if (result is null)
            {
                return NotFound();
            }

            return View(_inventoryDetailsViewModel);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
            {
                return NotFound();
            }
            Product product = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Product/Get/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            if (product is null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductCommand command)
        {
            string result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("https://localhost:5001/api/Product/Update", request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<string>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Product product = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Product/Get/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            string result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(id);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"https://localhost:5001/api/Product/Delete/{id}", request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<string>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult SaveToViewModel(IFormCollection collection)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(collection["data"]);
                _inventoryCreateViewModel.Type = product.Type;
                _inventoryCreateViewModel.Color = product.Color;
                _inventoryCreateViewModel.SellingPrice = product.SellingPrice;
                _inventoryCreateViewModel.SoldPrice = product.SoldPrice;
                _inventoryCreateViewModel.Cost = product.Cost;
                _inventoryCreateViewModel.Discount = product.Discount;
                _inventoryCreateViewModel.Count = int.Parse(collection["createCount"]);
                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }
    }
}
