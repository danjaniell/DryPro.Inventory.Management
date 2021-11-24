﻿using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class DeleteProductCommand : IRequest<int?>
    {
        public int Id { get; set; }
    }
}