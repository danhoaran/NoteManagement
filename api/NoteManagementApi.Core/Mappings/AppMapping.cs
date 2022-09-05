using AutoMapper;
using NoteManagementApi.Core.DTOs;
using NoteManagementCore.Models;

namespace NoteManagementCore.Mappings
{
    public class AppMapping : Profile
    {
        public AppMapping()
        {
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<NoteForCreationDto, Note>()
                .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<NoteForUpdateDto, Note>();
            CreateMap<Note, NoteDto>();
            CreateMap<Note, NoteForHtmlDto>()
                .ForMember(dest => dest.BodyHtml,
                opt => opt.MapFrom(src => src.Body));
            CreateMap<Note, NoteForListingDto>()
                .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToString("f")))
                .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.Categories != null ? src.Categories.Select(x => x.Name) : new List<string>()));
        }

    }
}
