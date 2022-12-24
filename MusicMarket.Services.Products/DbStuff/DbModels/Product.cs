namespace MusicMarket.Services.Products.DbStuff.DbModels
{
    public class Product : BaseModel
    {
        public  virtual Category Category { get; set; }
        public string Name { get; set; }

    }
}
