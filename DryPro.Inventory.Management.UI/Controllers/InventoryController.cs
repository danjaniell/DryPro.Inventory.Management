using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryCreateViewModel _inventoryCreateViewModel;

        public InventoryController(InventoryCreateViewModel inventoryCreateViewModel)
        {
            _inventoryCreateViewModel = inventoryCreateViewModel;
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

        // GET: Product/Create
        public IActionResult Create()
        {
            return View(_inventoryCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int count, Product product)
        {
            Product result = null;
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
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
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
                }
            }
            if (result is null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductCommand command)
        {
            int? result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("https://localhost:5001/api/Product/Update", request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<int>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            int? result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(id);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"https://localhost:5001/api/Product/Delete/{id}", request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<int>(apiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }
    }
}
