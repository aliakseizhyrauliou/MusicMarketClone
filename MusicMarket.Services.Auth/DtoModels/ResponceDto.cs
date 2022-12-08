namespace MusicMarket.Services.Auth.DtoModels
{
    public class ResponceDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
