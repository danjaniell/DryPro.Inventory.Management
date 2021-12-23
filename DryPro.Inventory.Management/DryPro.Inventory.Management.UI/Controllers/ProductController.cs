using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Infrastructure.Data;
using DryPro.Inventory.Management.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductCreateViewModel _productCreateViewModel;
        private readonly ProductDetailsViewModel _productDetailsViewModel;
        private readonly IEndpoint _ep;

        public ProductController(ProductCreateViewModel productCreateViewModel,
                                   ProductDetailsViewModel productDetailsViewModel,
                                   IEndpoint ep)
        {
            _productCreateViewModel = productCreateViewModel;
            _productDetailsViewModel = productDetailsViewModel;
            _ep = ep;
        }

        public async Task<IActionResult> Index()
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
            return View(products);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            _productCreateViewModel.Clear();
            return View(_productCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int count, Product product)
        {
            Product result = null;
            product.AuxilliaryItems = _productCreateViewModel.AuxItemCreateViewModel.AuxilliaryItems;
            if (ModelState.IsValid)
            {
                var command = ProductMapper.Mapper.Map<CreateProductCommand>(product);
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    for (int i = 0; i < count; i++)
                    {
                        using (var response = await httpClient.PostAsync($"{_ep.Value}/api/Product/Add", request))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<Product>(apiResponse);
                        }
                    }
                }
                _productCreateViewModel.Clear();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Product result = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_ep.Value}/api/Product/Get/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Product>(apiResponse);
                    _productDetailsViewModel.Product = result;
                }
            }
            if (result is null)
            {
                return NotFound();
            }

            return View(_productDetailsViewModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
            {
                return NotFound();
            }
            Product product = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_ep.Value}/api/Product/Get/{id}"))
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
            string result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{_ep.Value}/api/Product/Update", request))
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(result);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Product product = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_ep.Value}/api/Product/Get/{id}"))
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            string result = null;
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(id);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{_ep.Value}/api/Product/Delete/{id}", request))
                    {
                        result = await response.Content.ReadAsStringAsync();
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
                _productCreateViewModel.Type = product.Type;
                _productCreateViewModel.Color = product.Color;
                _productCreateViewModel.SellingPrice = product.SellingPrice;
                _productCreateViewModel.SoldPrice = product.SoldPrice;
                _productCreateViewModel.Cost = product.Cost;
                _productCreateViewModel.Discount = product.Discount;
                _productCreateViewModel.Count = int.Parse(collection["createCount"]);
                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }
    }
}