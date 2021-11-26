﻿using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Common.Helpers;
using DryPro.Inventory.Management.Core.Entities;
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
        // GET: AuxItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: AuxItem/Create
        [HttpPost]
        [Route("[controller]/Create")]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            var x = collection["name"];
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuxItem/Edit/5?productId=1
        public IActionResult Edit(int productId, int id, string name, decimal cost, string description)
        {
            var auxItem = new AuxilliaryItem()
            {
                ProductId = productId,
                Id = id,
                Name = name,
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
            int? result = null;
            var command = AuxItemMapper.Mapper.Map<UpdateAuxilliaryItemCommand>(auxItem);
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("https://localhost:5001/api/AuxilliaryItem/Update", request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<int>(apiResponse); //should be the product Id
                    }
                }
                return RedirectToAction("Details", "Inventory", new { id = result });
            }
            return View(auxItem);
        }

        // GET: AuxItem/Delete/5?productId=1
        //public IActionResult Delete(int productId, int id, string name, decimal cost, string description)
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
            int? result = null;
            var command = AuxItemMapper.Mapper.Map<DeleteAuxilliaryItemCommand>(auxItem);
            if (ModelState.IsValid)
            {
                HttpContent request = HttpContentHelper.CreateRequest(command);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("https://localhost:5001/api/AuxilliaryItem/Delete", request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<int>(apiResponse); //should be the product Id
                    }
                }
                return RedirectToAction("Details", "Inventory", new { id = result });
            }
            return View(auxItem);
        }
    }
}
