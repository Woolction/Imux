namespace Imux.Handlers;

public class CompressHandler : IHandler
{
    private readonly string[] values;
    public CompressHandler(string[] args)
    {
        values = args;
    }

    public void Handle()
    {
        Console.WriteLine("Success");
    }
}
