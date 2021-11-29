using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace DryPro.Inventory.Management.UI.Models
{
    public class InventoryCreateViewModel : PageModel
    {
        public InventoryCreateViewModel(AuxItemCreateViewModel auxItemCreateViewModel)
        {
            AuxItemCreateViewModel = auxItemCreateViewModel;
        }

        [BindProperty]
        public ProductType Type { get; set; }
        [BindProperty]
        public ProductColor Color { get; set; }
        [BindProperty]
        public decimal SellingPrice { get; set; }
        [BindProperty]
        public decimal SoldPrice { get; set; }
        [BindProperty]
        public decimal Cost { get; set; }
        [BindProperty]
        public decimal Discount { get; set; }
        [BindProperty]
        public int Count { get; set; } = 1;
        public AuxItemCreateViewModel AuxItemCreateViewModel { get; }
        public Product Current { get; set; } = new Product();
    }
}
