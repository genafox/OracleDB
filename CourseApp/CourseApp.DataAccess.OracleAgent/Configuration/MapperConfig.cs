﻿using AutoMapper;
using CourseApp.DataAccess.DataSource.API.DTOs;
using CourseApp.DataAccess.Models;

namespace CourseApp.DataAccess.OracleAgent.Configuration
{
    public static class MapperConfig
    {
        public static void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CourseDto, Course>().ReverseMap();
            });
        }
    }
}