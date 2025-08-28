using AiNavigator.Api.Dtos;
using AiNavigator.Api.Models;
using AutoMapper;
using System.Linq;

namespace AiNavigator.Api.Entities
{
    public class RequestHistory
    {
        public int Id { get; set; }
        public Guid RequestId { get; set; }
        public PromptDetailsDto Details { get; set; }

        public RequestHistory(PromptDetailsDto details)
        {
            Details = details;
        }

        public RequestHistory()
        {
            
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PromptDetails, List<RequestHistory>>()
                .ConvertUsing((src, dest, context) =>
                {
                    var requestId = context.Items.ContainsKey("RequestId")
                        ? (Guid)context.Items["RequestId"]
                        : Guid.NewGuid();

                    return src.TopModels.Select(model => new RequestHistory
                    {
                        RequestId = requestId,
                        Details = new PromptDetailsDto
                        {
                            QueryDate = src.QueryDate,
                            Category = (int)src.Category,
                            Name = model.Name,
                            ShortDescription = model.ShortDescription,
                            Link = model.Link,
                            Pros = string.Join(", ", model.Pros ?? new List<string>()),
                            Cons = string.Join(", ", model.Cons ?? new List<string>()),
                            Rank = model.Rank,
                            GeneralSummary = src.GeneralSummary
                        }
                    }).ToList();
                });

            CreateMap<ModelInfo, PromptDetailsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Pros, opt => opt.MapFrom(src => string.Join(", ", src.Pros ?? new List<string>())))
                .ForMember(dest => dest.Cons, opt => opt.MapFrom(src => string.Join(", ", src.Cons ?? new List<string>())))
                .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank))
                .ForMember(dest => dest.GeneralSummary, opt => opt.Ignore());



        }
    }
}
