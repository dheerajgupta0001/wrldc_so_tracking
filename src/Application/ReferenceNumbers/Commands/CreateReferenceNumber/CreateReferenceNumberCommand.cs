using MediatR;
using System;
using System.Collections.Generic;

namespace Application.ReferenceNumbers.Commands.CreateReferenceNumber
{
    public class CreateReferenceNumberCommand : IRequest<List<string>>
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
    }
}