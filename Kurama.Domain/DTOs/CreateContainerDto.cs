using System.Collections.Generic;

namespace Kurama.Domain.DTOs
{
    public class CreateContainerDto
    {
        public string Image { get; set; }
        public IEnumerable<string> Env { get; set; }
        public Dictionary<string, object> PortBindings { get; set; }
    }
}
