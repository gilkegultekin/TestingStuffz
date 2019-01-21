using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PostgreSqlTest.Domain;
using PostgreSqlTest.Dto;

namespace PostgreSqlTest.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<Blog, BlogDto>();
        }
    }
}
