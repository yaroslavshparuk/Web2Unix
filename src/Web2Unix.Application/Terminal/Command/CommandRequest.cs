namespace Web2Unix.Application.Terminal.Command;

public record CommandRequest(int userId, int serverId, string commandValue);