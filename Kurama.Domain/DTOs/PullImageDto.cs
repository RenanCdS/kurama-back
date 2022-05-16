namespace Kurama.Domain.DTOs
{
    public class PullImageDto
    {
        public string FromImage { get; set; }
        public string FromSrc { get; set; } = "https://hub.docker.com/";
        public PullImageDto(string imageName)
        {
            FromImage = imageName;
        }
    }
}
