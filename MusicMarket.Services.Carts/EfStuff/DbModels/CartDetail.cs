namespace MusicMarket.Services.Carts.EfStuff.DbModels
{
    public class CartDetail
    {
        public long Id { get; set; }
        public virtual CartHeader CartHeader { get; set; }
        public long CartHeaderId { get; set; }
        public long ProductId { get; set; }
        public virtual Product  { get; set; }
    }
}
