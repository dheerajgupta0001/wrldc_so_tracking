using AutoMapper;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using static Application.Common.Mappings.MappingProfile;

namespace Application.Pams.Commands.EditPam;

public class EditPamCommand : IRequest<List<string>>, IMapFrom<Pam>
{
    public int Id { get; set; }
    public int SubStationId { get; set; }
    public int OwnerId { get; set; }
    public string Category { get; set; }
    public int DepartmentId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime StatusDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Pam, EditPamCommand>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
    }
}
