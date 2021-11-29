﻿using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries.AuxilliaryItem
{
    public class GetAllAuxItemsQuery : IRequest<List<Core.Entities.AuxilliaryItem>>
    {
        public GetAllAuxItemsQuery(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }
    }
}