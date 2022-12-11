using AutoMapper;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.ViewModels;

namespace MusicMarket.Services.Products
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryViewModel>()
                    .ReverseMap();

                config.CreateMap<Product, ProductViewModel>()
                    .ReverseMap();
            });

            return mappingConfig;
        }
    }
}
