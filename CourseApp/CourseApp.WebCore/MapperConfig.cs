using AutoMapper;
using CourseApp.DataAccess.Models;
using CourseApp.WebCore.Models;

namespace CourseApp.DataAccess.OracleAgent.Configuration
{
    public static class MapperConfig
    {
        public static void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CourseModel, Course>().ReverseMap();
            });
        }
    }
}