using Imux.App.Handlers;

namespace Imux;

public static class TaskManager
{
    public static readonly Dictionary<string, IHandler> Tasks = [];

    public static void InitTasks(string[] args)
    {
        //commands
        Tasks["copy"] = new CopyHandler(args);
    }
}