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
                .ForMember(dest => dest.PortBindings,
                            opt => opt.MapFrom(s => new Dictionary<string, dynamic>()
                            {
                                {  
                                    $"{s.InternalPort}/tcp",  new List<Dictionary<string, string>>()
                                    {
                                        new Dictionary<string, string>()
                                        {
                                            {  "hostPort", $"{s.ExternalPort}" } 
                                        }
                                    }
                                }
                            }));

        }
    }
}
