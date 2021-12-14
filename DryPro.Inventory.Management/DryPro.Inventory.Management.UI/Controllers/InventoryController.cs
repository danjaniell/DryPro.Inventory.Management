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
    public class InventoryController : Controller
    {
        private readonly IEndpoint _ep;
        private readonly InventoryDetailsViewModel _inventoryDetailsViewModel;

        public InventoryController(InventoryDetailsViewModel inventoryDetailsViewModel, IEndpoint ep)
        {
            _ep = ep;
            _inventoryDetailsViewModel = inventoryDetailsViewModel;
        }

        public IActionResult Index()
        {
            return View(_inventoryDetailsViewModel);
        }
    }
}