using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Controllers
{
    public class ProductController : Controller
    {
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(nameof(Product.Id),
                                                nameof(Product.Type),
                                                nameof(Product.Color),
                                                nameof(Product.SellingPrice),
                                                nameof(Product.SoldPrice),
                                                nameof(Product.Cost),
                                                nameof(Product.Discount),
                                                nameof(Product.AuxilliaryItems))] CreateProductCommand command)
        {
            Product result = null;
            if (ModelState.IsValid)
            {
                //var command = ProductMapper.Mapper.Map<CreateProductCommand>(product);
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("https://localhost:5001/api/Product/Add", request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<Product>(apiResponse);
                    }
                }
            }
            return View(result);
        }
    }
}
