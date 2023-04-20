namespace Web2Unix.Application.Abstractions;

public interface IFirewall
{
    Task<bool> CanAccess(int userId, int serverId);
}