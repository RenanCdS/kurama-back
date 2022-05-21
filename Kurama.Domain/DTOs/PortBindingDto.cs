namespace Kurama.Domain.DTOs
{
    public class PortBindingDto
    {
        public string HostIp { get; set; }
        public string HostPort { get; set; }

        public PortBindingDto(string hostIp, string hostPort)
        {
            HostIp = hostIp;
            HostPort = hostPort;
        }
    }
}
