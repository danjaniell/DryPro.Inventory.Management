using System.Collections.Generic;

namespace DryPro.Inventory.Management.Infrastructure.Data
{
    public interface IEndpoint
    {
        string Environment { get; set; }
        string Value { get; set; }
    }

    public class Endpoints : IEndpoint
    {
        public string Environment { get; set; }
        public string Value { get; set; }
    }
}
