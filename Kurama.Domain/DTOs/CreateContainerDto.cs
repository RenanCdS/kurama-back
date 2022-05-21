using System.Collections.Generic;

namespace Kurama.Domain.DTOs
{
    public class CreateContainerDto
    {
        public string Image { get; set; }
        public IEnumerable<string> Env { get; set; }
        public HostConfig HostConfig { get; set; }
    }

    public class HostConfig
    {
        public Dictionary<string, object> PortBindings { get; set; }
    }
}
