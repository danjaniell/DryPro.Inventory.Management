using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.UI.Models
{
    public class InventoryManageViewModel : PageModel
    {
        public IEnumerable<Core.Entities.Inventory> Inventory { get; set; } = new List<Core.Entities.Inventory>();
    }
}
