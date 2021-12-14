using DryPro.Inventory.Management.Common.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel;

namespace DryPro.Inventory.Management.UI.Models
{
    public class InventoryDetailsViewModel : PageModel
    {
        public IEnumerable<InventoryDetails> Inventory { get; set; } = new List<InventoryDetails>();
        public InventoryDetails Current { get; set; }
    }

    public class InventoryDetails
    {
        [DisplayName("Product Type")]
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }

        [DisplayName("Remaining Stock")]
        public int Remaining { get; set; }
        [DisplayName("# of Sold Items")]
        public int Sold { get; set; }
    }
}
