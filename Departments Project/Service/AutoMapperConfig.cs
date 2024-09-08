using AutoMapper;

namespace Departments_Project.Service
{
    public static class AutoMapperConfig
    {
        public static IMapper? Mapper { get; private set; }

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            Mapper = config.CreateMapper();
        }
    }
}
