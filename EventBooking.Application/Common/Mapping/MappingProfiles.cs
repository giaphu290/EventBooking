using AutoMapper;
using EventBooking.Application.Features.Auth.RoleManage.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Role
            CreateMap<IdentityRole, RoleResponse>().ReverseMap();
        }
    }
}
