namespace Imux;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1 || args[0] != "Imux")
        {
            Console.WriteLine($"empty args: {args.Length}");

            return;
        }
        if (args[0] != "Imux")
        {
            Console.WriteLine($"first argument must be Imux: {args[0]}");

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
