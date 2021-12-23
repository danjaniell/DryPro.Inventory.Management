using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Infrastructure.Data;
using DryPro.Inventory.Management.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Controllers
{
    public class AuxItemController : Controller
    {
        private readonly AuxItemCreateViewModel _auxItemCreateViewModel;
        private readonly IEndpoint _ep;

        public AuxItemController(AuxItemCreateViewModel auxItemCreateViewModel, IEndpoint ep)
        {
            _auxItemCreateViewModel = auxItemCreateViewModel;
            _ep = ep;
        }

        // GET: AuxItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: AuxItem/Create
        [HttpPost]
        [Route("[controller]/Create")]
        public IActionResult Create(IFormCollection collection)
        {
            var auxItem = JsonConvert.DeserializeObject<AuxilliaryItem>(collection["data"]);
            if (ModelState.IsValid)
            {
                _auxItemCreateViewModel.AuxilliaryItems.Add(auxItem);
            }

            return PartialView("_AuxItemCreate", _auxItemCreateViewModel);
        }

        // GET: AuxItem/Edit/5?productId=1
        public IActionResult Edit(string productId, int id, AuxItemCategory category, decimal cost, string description)
        {
            var auxItem = new AuxilliaryItem()
            {
                ProductId = productId,
                Id = id,
                Category = category,
                Cost = cost,
                Description = description
            };
            return View(auxItem);
        }

        // POST: AuxItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuxilliaryItem auxItem)
        {
            string result = null;
            var command = AuxItemMapper.Mapper.Map<UpdateAuxilliaryItemCommand>(auxItem);
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{_ep.Value}/api/AuxilliaryItem/Update", request))
                    {
                        result = await response.Content.ReadAsStringAsync(); //should be the product Id
                    }
                }
                return RedirectToAction("Details", "Product", new { id = result });
            }
            return View(auxItem);
        }

        // GET: AuxItem/Delete/5?productId=1
        //public IActionResult Delete(string productId, int id, string name, decimal cost, string description)
        //{
        //    var auxItem = new AuxilliaryItem()
        //    {
        //        ProductId = productId,
        //        Id = id,
        //        Name = name,
        //        Cost = cost,
        //        Description = description
        //    };
        //    return View(auxItem);
        //}

        // POST: AuxItem/Delete
        [HttpPost]
        [Route("[controller]/Delete")]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            var auxItem = JsonConvert.DeserializeObject<AuxilliaryItem>(collection["auxItemJson"]);
            string result = null;
            var command = AuxItemMapper.Mapper.Map<DeleteAuxilliaryItemCommand>(auxItem);
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{_ep.Value}/api/AuxilliaryItem/Delete", request))
                    {
                        result = await response.Content.ReadAsStringAsync(); //should be the product Id
                    }
                }
                return RedirectToAction("Details", "Product", new { id = result });
            }
            return View(auxItem);
        }
    }
}
