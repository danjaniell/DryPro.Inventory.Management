using MediatR;

namespace DryPro.Inventory.Management.Application.Queries.AuxilliaryItem
{
    public class GetTotalCostQuery : IRequest<decimal>
    {
        public GetTotalCostQuery(string productId)
        {
            ProductId = productId;
        }

        public string ProductId { get; }
    }
}