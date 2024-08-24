using AutoMapper;
using EventBooking.Application.Features.Auth.LoginManage.Models;
using EventBooking.Application.Features.Auth.RoleManage.Models;
using EventBooking.Application.Features.Auth.UserManage.Commands;
using EventBooking.Application.Features.Auth.UserManage.Models;
using EventBooking.Application.Features.EventManagement.Commands;
using EventBooking.Application.Features.EventManagement.Models;
using EventBooking.Application.Features.GroupManagement.Commands;
using EventBooking.Application.Features.GroupManagement.Models;
using EventBooking.Application.Features.GroupUserManagement.Commands;
using EventBooking.Application.Features.GroupUserManagement.Models;
using EventBooking.Application.Features.PostManagement.Commands;
using EventBooking.Application.Features.PostManagement.Models;
using EventBooking.Application.Features.TicketManagement.Models;
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
            CreateMap<Event, EventResponse>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // Group
            CreateMap<Group, CreateGroupCommand>().ReverseMap();
            CreateMap<Group, GroupResponse>().ReverseMap();
            CreateMap<Group, UpdateGroupCommand>().ReverseMap()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // Group User
            CreateMap<GroupUser, CreateGroupUserCommand>().ReverseMap();
            CreateMap<GroupUser, GroupUserResponse>().ReverseMap();
            CreateMap<GroupUser, UpdateGroupUserCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // EventTIcket
            CreateMap<EventTicket, CreateEventTicketCommand>().ReverseMap();
            CreateMap<EventTicket, EventTicketResponse>().ReverseMap();
            CreateMap<EventTicket, UpdateEventTicketCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // EventTIcket
            CreateMap<EventPost, CreateEventPostCommand>().ReverseMap();
            CreateMap<EventPost, EventPostResponse>().ReverseMap();
            CreateMap<EventPost, UpdateEventPostCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



        }
    }
}
