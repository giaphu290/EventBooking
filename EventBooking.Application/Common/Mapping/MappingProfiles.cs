using AutoMapper;
using EventBooking.Application.Features.Auth.LoginManage.Models;
using EventBooking.Application.Features.Auth.RoleManage.Models;
using EventBooking.Application.Features.Auth.UserManage.Commands;
using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Domain.Entities;
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
        {    // Auth
            CreateMap<User, GetUserDetailResponse>();
            CreateMap<User, GetAllUserResponse>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, CreateUserResponse>().ReverseMap();
            CreateMap<User, GetCurrentUserResponse>();

            // Role
            CreateMap<IdentityRole, RoleResponse>().ReverseMap();
            // Event
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, CreateUserResponse>().ReverseMap();


        }
    }
}
