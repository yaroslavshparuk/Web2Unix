﻿using Web2Unix.Domain.Primitives;
using Web2Unix.Domain.ValueObjects;

namespace Web2Unix.Domain.Entities;

public class Server : Entity
{
    private Server(
        int id,
        Hostname hostname,
        IpAddress ipAddress,
        short port,
        DateTimeOffset createdAt, 
        DateTimeOffset updatedAt) 
        : base(id)
    {
        Hostname = hostname;
        IpAddress = ipAddress;
        Port = port;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Hostname Hostname { get; }

    public IpAddress IpAddress { get; }

    public short Port { get; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset UpdatedAt { get; }

    public static Server Create(
        int id,
        Hostname hostname,
        IpAddress ipAddress,
        short port,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        return new Server(id, hostname, ipAddress, port, createdAt, updatedAt);
    }
}