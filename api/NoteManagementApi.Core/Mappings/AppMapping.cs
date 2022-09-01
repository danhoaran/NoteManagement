using AutoMapper;
using Markdig;
using NoteManagementCore.DTOs;
using NoteManagementCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementCore.Mappings
{
    public class AppMapping : Profile
    {
        public AppMapping()
        {
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<NoteForCreationDto, Note>();
            CreateMap<NoteForUpdateDto, Note>();
            CreateMap<Note, NoteDto>();
            CreateMap<Note, NoteForHtmlDto>()
                .ForMember(dest => dest.BodyHtml,
                opt => opt.MapFrom(src => src.Body));
        }

    }
}
