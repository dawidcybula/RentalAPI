﻿using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TNAI.Model.Entities;

using TNAI_2022_Framework.Models.OutputModels;

namespace TNAI_2022_Framework.App_Start
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<Category, CategoryOutputModel>();
            CreateMap<Product, ProductOutputModel>();
            CreateMap<Rental, RentalOutputModel>();

        }
    }
}