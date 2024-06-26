﻿using MediatR;
using System.Collections.Generic;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<List<string>>
{
    public string Id { get; set; }
}
