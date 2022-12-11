using System.ComponentModel.DataAnnotations.Schema;

namespace MusicMarket.Services.Products.DbStuff.DbModels
{
    public class Category : BaseModel
    {
        public virtual IEnumerable<Product> Product { get; set; }
        public string Name { get; set; }
    }
}
