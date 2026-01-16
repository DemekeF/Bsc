using System;
using MediatR;

namespace Application.DeletePerspective;

public class DeletePerspectiveCommand(int Id):IRequest
{
    public int Id { get; } = Id;
}
