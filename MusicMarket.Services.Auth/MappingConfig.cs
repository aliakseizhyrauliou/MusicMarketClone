using AutoMapper;
using MusicMarket.Services.Auth.DbStuff.DbModels;
using MusicMarket.Services.Auth.ViewModels;

namespace MusicMarket.Services.Auth
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserViewModel>()
                    .ReverseMap();
            });

            return mappingConfig;
        }
    }
}
