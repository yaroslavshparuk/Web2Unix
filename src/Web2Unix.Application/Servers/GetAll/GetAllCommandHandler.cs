﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Web2Unix.Application.Data;
using Web2Unix.Domain.Entities;

namespace Web2Unix.Application.Servers.GetAll;

public class ConnectCommandHandler : IRequestHandler<GetAllCommand, ICollection<Server>>
{
    private readonly IApplicationDbContext _context;

    public ConnectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Server>> Handle(GetAllCommand request, CancellationToken cancellationToken)
    {
        return await _context.Servers.ToArrayAsync(cancellationToken);
    }
}