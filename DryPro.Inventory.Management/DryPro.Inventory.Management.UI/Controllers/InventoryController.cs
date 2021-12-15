using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Infrastructure.Data;
using DryPro.Inventory.Management.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IEndpoint _ep;
        private readonly InventoryDetailsViewModel _inventoryDetailsViewModel;
        private readonly InventoryManageViewModel _inventoryManageViewModel;

        public InventoryController(InventoryDetailsViewModel inventoryDetailsViewModel,
                                   InventoryManageViewModel inventoryManageViewModel,
                                   IEndpoint ep)
        {
            _ep = ep;
            _inventoryDetailsViewModel = inventoryDetailsViewModel;
            _inventoryManageViewModel = inventoryManageViewModel;
        }

        private async Task Init()
        {
            var products = await GetAvailableProducts();
            _inventoryManageViewModel.Inventory = ProductToInventory(products);
        }

        public async Task<IActionResult> Index()
        {
            await Init();
            return View(_inventoryDetailsViewModel);
        }

        public IActionResult Manage()
        {
            return View(_inventoryManageViewModel);
        }

        private async Task<IEnumerable<Product>> GetAvailableProducts()
        {
            var products = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_ep.Value}/api/Product/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            var distinctProductList = products.GroupBy(m => new { m.Type, m.Color }).Select(group => group.First()).ToList();
            return distinctProductList;
        }

        private IEnumerable<Core.Entities.Inventory> ProductToInventory(IEnumerable<Product> products)
        {
            var inventory = new List<Core.Entities.Inventory>();
            foreach (var product in products)
            {
                var item = ProductMapper.Mapper.Map<Core.Entities.Inventory>(product);
                item._id = ObjectId.GenerateNewId().ToString();
                inventory.Add(item);
            }
            return inventory;
        }
    }
}