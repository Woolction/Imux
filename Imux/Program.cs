namespace Imux;

public class Program
{
    private static readonly string header = "[Program]";
    
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine($"{header} empty args: {args.Length}");

            return;
        }
        if (args[0] != "imux")
        {
            Console.WriteLine($"{header} first argument must be <imux>: {args[0]}");

            return;
        }

        TaskManager.InitTasks(args);

        for (int i = 1; i < args.Length; i++)
        {
            if (TaskManager.Tasks.TryGetValue(args[i], out var handler))
            {
                handler.Handle();

                return;
            }
        }
    }
}
