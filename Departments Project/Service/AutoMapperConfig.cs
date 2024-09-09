using AutoMapper;

namespace Departments_Project.Service
{
    public static class AutoMapperConfig
    {
        public static IMapper? Mapper { get; private set; }

        public static void ConfigureMappings(IMapperConfigurationExpression config)
        {
            // Add your profiles here
            config.AddProfile<MappingProfile>();

            // Alternatively, add multiple profiles
            // config.AddProfiles(typeof(OtherProfile1), typeof(OtherProfile2));
        }
    }
}
