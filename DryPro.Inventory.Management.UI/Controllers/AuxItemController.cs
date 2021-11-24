using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Controllers
{
    public class AuxItemController : Controller
    {
        // GET: AuxItemController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AuxItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuxItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuxItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuxItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuxItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuxItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuxItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
