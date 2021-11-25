using MediatR;

namespace DryPro.Inventory.Management.Application.Queries.AuxilliaryItem
{
    public class GetTotalCostQuery : IRequest<decimal>
    {
        public GetTotalCostQuery(int productId)
        {
            Id = productId;
        }

        public int Id { get; }
    }
}