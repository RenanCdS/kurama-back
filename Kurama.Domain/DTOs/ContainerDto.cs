using System.Collections.Generic;

namespace Kurama.Domain.DTOs
{
    public class ContainerDto
    {
        public string Id { get; set; }
        public IEnumerable<string> Names { get; set; }
        public string Image { get; set; }
        public string ImageID { get; set; }
        public string Status { get; set; }
        public long Created { get; set; }
        public string State { get; set; }
    }
}
