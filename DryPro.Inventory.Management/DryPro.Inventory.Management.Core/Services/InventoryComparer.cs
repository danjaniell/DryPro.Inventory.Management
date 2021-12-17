using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DryPro.Inventory.Management.Core.Services
{
    public class InventoryComparer : IEqualityComparer<Entities.Inventory>
    {
        public bool Equals(Entities.Inventory a, Entities.Inventory b) =>  a._id == b._id || (a.Type == b.Type && a.Color == b.Color);

        public int GetHashCode([DisallowNull] Entities.Inventory obj)
        {
            return base.GetHashCode();
        }
    }
}
