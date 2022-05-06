namespace CinemasNVS.BLL.DTOs
{
    public class DirectorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImdbLink { get; set; }

        public MovieResponse MovieResponse { get; set; }
    }
}
