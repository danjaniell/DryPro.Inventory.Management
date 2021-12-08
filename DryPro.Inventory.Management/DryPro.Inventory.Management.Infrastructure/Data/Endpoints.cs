using System.Collections.Generic;

namespace DryPro.Inventory.Management.Infrastructure.Data
{
    public interface IEndpoint
    {
        KeyValuePair<string, string> Value { get; set; }
    }

    public class Endpoints : IEndpoint
    {
        public KeyValuePair<string, string> Value { get; set; }
    }
}
