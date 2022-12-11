using MusicMarket.Services.Products.DbStuff.DbModels;
using System.ComponentModel.DataAnnotations;

namespace MusicMarket.Services.Products.ViewModels
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        [Required]
        public virtual CategoryViewModel Category { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
