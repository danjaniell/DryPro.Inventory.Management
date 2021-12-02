using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class DeleteAllAndGenerateRandomDataCommand : IRequest<string>
    {
    }
}