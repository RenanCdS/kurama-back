using System.Collections.Generic;

namespace Kurama.Domain.DTOs
{
    public class ImageDto
    {
        public string Id { get; set; }
        public long Size { get; set; }
        public int Containers { get; set; }
        public IEnumerable<string> RepoTags { get; set; }
    }
}
