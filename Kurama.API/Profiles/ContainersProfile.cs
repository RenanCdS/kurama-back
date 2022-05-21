using AutoMapper;
using Kurama.Application.Commands;
using Kurama.Domain.DTOs;
using System.Collections.Generic;

namespace Kurama.API.Profiles
{
    public class ContainersProfile : Profile
    {
        public ContainersProfile()
        {
            CreateMap<CreateContainerCommand, CreateContainerDto>()
                .ForMember(dest => dest.Env,
                            opt => opt.MapFrom(s => s.EnvironmentVariables))
                .ForMember(dest => dest.HostConfig,
                            opt => opt.MapFrom(s => new HostConfig() { PortBindings = GetPortBinding(s.InternalPort, s.ExternalPort) })); ;

        }

        private Dictionary<string, dynamic> GetPortBinding(int internalPort, int externalPort)
        {
            return new Dictionary<string, dynamic>()
                            {
                                {
                                    $"{internalPort}/tcp",  new List<Dictionary<string, string>>()
                                    {
                                        new Dictionary<string, string>()
                                        {
                                            {  "hostPort", $"{externalPort}" }
                                        }
                                    }
                                }
                            };
        }
    }
}
