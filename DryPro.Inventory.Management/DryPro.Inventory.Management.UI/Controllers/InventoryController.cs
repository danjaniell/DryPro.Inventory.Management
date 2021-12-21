using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Core.Services;
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
        private readonly InventoryManageViewModel _inventoryManageViewModel;

        public InventoryController(InventoryManageViewModel inventoryManageViewModel,
                                   IEndpoint ep)
        {
            _ep = ep;
            _inventoryManageViewModel = inventoryManageViewModel;
        }

        private async Task Init()
        {
            //Retrieve existing data in Inventory Collection
            var inventory = await GetInventory();

            //Add non existing in Inventory Collection, but in Products Collection
            var extraInventory = ProductToInventory(await GetAvailableProducts());

            _inventoryManageViewModel.Inventory = inventory.Union(extraInventory, new InventoryComparer()).OrderBy(x=>x.Type).ThenBy(x=>x.Color).ToList();
        }

        public async Task<IActionResult> Manage()
        {
            await Init();
            return View(_inventoryManageViewModel);
        }

        // POST: Inventory/Save
        [HttpPost]
        [Route("[controller]/Save")]
        public async Task<IActionResult> Save(IFormCollection collection)
        {
            var command = JsonConvert.DeserializeObject<SaveInventoryCommand>(collection["data"]);
            string result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{_ep.Value}/api/Inventory/SaveOne", request))
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction(nameof(Manage));
            }
            return View(result);
        }


        // POST: Inventory/SaveAll
        [HttpPost]
        [Route("[controller]/SaveAll")]
        public async Task<IActionResult> SaveAll(IFormCollection collection)
        {
            var command = new SaveAllInventoryCommand()
            {
                ItemsToSave = JsonConvert.DeserializeObject<IEnumerable<SaveInventoryCommand>>(collection["data"])
            };
            string result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{_ep.Value}/api/Inventory/SaveAll", request))
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        private async Task<IEnumerable<Core.Entities.Inventory>> GetInventory()
        {
            var inventory = new List<Core.Entities.Inventory>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_ep.Value}/api/Inventory/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    inventory = JsonConvert.DeserializeObject<List<Core.Entities.Inventory>>(apiResponse);
                }
            }
            return inventory;
        }

        private async Task<IEnumerable<Product>> GetAvailableProducts()
        {
            var products = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_ep.Value}/api/Inventory/GetAvailableProducts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            var distinctProductList = products.GroupBy(m => new { m.Type, m.Color }).Select(group => group.OrderBy(x=>x.Type).First()).ToList();
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