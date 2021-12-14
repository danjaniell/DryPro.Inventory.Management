using DryPro.Inventory.Management.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DryPro.Inventory.Management.UI.Models
{
    public class InventoryAddViewModel : PageModel
    {
        public InventoryAddViewModel()
        {

        }

        [BindProperty]
        public ProductType Type { get; set; }
        [BindProperty]
        public ProductColor Color { get; set; }
        [BindProperty]
        public int Remaining { get; set; }
        [BindProperty]
        public int Sold { get; set; }

        public void Clear()
        {
            Type = ProductType.BathTowel;
            Color = ProductColor.Blue;
            Remaining = 0;
            Sold = 0;
        }
    }
}
