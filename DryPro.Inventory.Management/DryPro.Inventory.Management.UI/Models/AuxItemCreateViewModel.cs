using DryPro.Inventory.Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.UI.Models
{
    public class AuxItemCreateViewModel
    {
        public IList<Core.Entities.AuxilliaryItem> AuxilliaryItems { get; set; } = new List<Core.Entities.AuxilliaryItem>();

        public AuxilliaryItem Current { get; set; }
    }
}
