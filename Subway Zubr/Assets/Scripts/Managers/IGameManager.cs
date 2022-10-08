public interface IGameManager
{
    ManagerStatus status { get; }
    void Initialize();
}
public enum ManagerStatus
{
    Shutdown,
    Initializing,
    Started
}