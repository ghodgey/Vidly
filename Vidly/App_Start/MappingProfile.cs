using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Controllers.Api;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
                
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());//maps these two - finds the names of the properties and maps them based on their name
            //convention based mapping tool - uses names of properties to map objects
            //Global.asax.c use mapper initialize to start mapper - starts the mappingprofile

            Mapper.CreateMap<Movies, MoviesDto>();
                
            Mapper.CreateMap<MoviesDto, Movies>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<Genres, GenresDto>();



        }
    }
}