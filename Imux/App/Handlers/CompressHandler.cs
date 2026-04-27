using Imux.App.Responses;
using Imux.Enums;

namespace Imux.App.Handlers;

public class CompressHandler : IHandler
{
    private readonly string[] values;

    private byte[] compressedBytes;

    public CompressHandler(string[] args)
    {
        values = args;
    }

    public void Handle()
    {
        List<OptionValue> paths = PathFinder.FindValue(values,
            new("input", OptionType.Input),
            new("output", OptionType.Output));

        if (paths.Count == 2)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                OptionValue value = paths[i];

                if (value.OptionType == OptionType.Input)
                {
                    byte[] bytes = File.ReadAllBytes(value.Path);

                    compressedBytes = bytes;
                }
                else if (value.OptionType == OptionType.Output)
                {
                    File.WriteAllBytes(value.Path, compressedBytes);
                }
            }
        }
        else if (paths.Count == 1)
        {
            OptionValue value = paths[0];

            if (value.OptionType != OptionType.Input)
            {
                Console.WriteLine("you must write input!");

                return;
            }
        }
        else
        {
            Console.WriteLine();
        }

        Console.WriteLine("Success");
    }
}
