using System.ComponentModel.DataAnnotations;

namespace MusicMarket.Services.Products.ViewModels
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }


    }
}
