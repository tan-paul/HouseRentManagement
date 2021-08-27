using AutoMapper;
using HouseRentManagement.Data;
using HouseRentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentManagement.Helper
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<HouseDetails, HouseDetailsModel>().ReverseMap();
        }
    }
}
