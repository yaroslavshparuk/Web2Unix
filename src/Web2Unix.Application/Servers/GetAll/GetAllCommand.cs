using MediatR;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Application.Servers.GetAll;

public record ConnectCommand() : IRequest<ICollection<Server>>;