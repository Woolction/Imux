using System.Text;
using Imux.App.Responses;
using Imux.Enums;

namespace Imux.App.Handlers;

public class CopyHandler : IHandler
{
    private readonly string header = "[CopyHandler]";
    private readonly string[] values;

    private byte[] copyedBytes = [];

    public CopyHandler(string[] args)
    {
        values = args;
    }

    public void Handle()
    {
        List<OptionValue> paths = PathFinder.FindValue(values,
            new("input", OptionType.Input), new("output", OptionType.Output));

        if (paths.Count == 2)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                OptionValue value = paths[i];

                if (value.OptionType == OptionType.Input)
                {
                    byte[] bytes = File.ReadAllBytes(value.Path);

                    copyedBytes = bytes;
                }
                else if (value.OptionType == OptionType.Output)
                {
                    try
                    {
                        File.WriteAllBytes(value.Path, copyedBytes);

                        Console.WriteLine($"{header} new bytes writed");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{header} Error in writting: {ex}");

                        return;
                    }
                }
            }
        }
        else if (paths.Count == 1)
        {
            OptionValue value = paths[0];

            if (value.OptionType != OptionType.Input)
            {
                Console.WriteLine($"{header} you must write input!");

                return;
            }

            byte[] bytes = File.ReadAllBytes(value.Path);

            copyedBytes = bytes;

            string? pathDir = Path.GetDirectoryName(value.Path);

            if (pathDir == null)
            {
                Console.WriteLine($"{header} directory is null: {value.Path}");

                return;
            }

            string pathName = Path.GetFileNameWithoutExtension(value.Path);
            string pathExt = Path.GetExtension(value.Path);

            int counter = 1;

            string newPath;

            do
            {
                newPath = Path.Combine(pathDir, pathName + $"({counter})" + pathExt);

                counter++;
            }
            while (File.Exists(newPath));


            Console.WriteLine($"{header} ");

            try
            {
                File.WriteAllBytes(newPath, copyedBytes);

                Console.WriteLine($"{header} create new file {newPath} bytes writed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{header} Error in writting: {ex}");

                return;
            }
        }
        else
        {
            Console.WriteLine($"{header} unknown error {paths.Count}");

            return;
        }

        Console.WriteLine($"{header} Success {paths.Count}");
    }
}
