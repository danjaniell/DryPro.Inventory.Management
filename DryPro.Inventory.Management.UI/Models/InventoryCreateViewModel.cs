using DryPro.Inventory.Management.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace DryPro.Inventory.Management.UI.Models
{
    public class InventoryCreateViewModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public int Count { get; set; } = 1;
    }
}
