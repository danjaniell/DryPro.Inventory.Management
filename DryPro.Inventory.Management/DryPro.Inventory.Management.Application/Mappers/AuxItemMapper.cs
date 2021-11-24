﻿using AutoMapper;
using System;

namespace DryPro.Inventory.Management.Application.Mappers
{
    public class AuxItemMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = x => x.GetMethod.IsPublic || x.GetMethod.IsAssembly;
                cfg.AddProfile<AuxItemMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}