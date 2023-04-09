using MediatR;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Application.Servers.Connect;

public record ConnectCommand() : IRequest<ICollection<Server>>;