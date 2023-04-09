namespace Web2Unix.Application.Servers.Command;

public record CommandRequest(int userId, int serverId, string commandValue);