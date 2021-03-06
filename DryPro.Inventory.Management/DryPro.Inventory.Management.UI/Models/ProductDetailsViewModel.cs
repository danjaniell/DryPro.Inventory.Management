using DryPro.Inventory.Management.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace DryPro.Inventory.Management.UI.Models
{
    public class ProductDetailsViewModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }
    }
}
