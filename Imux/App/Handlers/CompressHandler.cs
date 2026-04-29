using System.Text;
using Imux.App.Responses;
using Imux.Enums;

namespace Imux.App.Handlers;

public class CompressHandler : IHandler
{
    private readonly string[] values;

    private byte[] compressedBytes = [];

    public CompressHandler(string[] args)
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

                    compressedBytes = bytes;

                    Console.WriteLine("[CompressHandler] image compressed");
                }
                else if (value.OptionType == OptionType.Output)
                {
                    try
                    {
                        File.WriteAllBytes(value.Path, compressedBytes);

                        Console.WriteLine("[CompressHandler] new bytes writed");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[CompressHandler] Error in writting: {ex}");

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
                Console.WriteLine("[CompressHandler] you must write input!");

                return;
            }

            byte[] bytes = File.ReadAllBytes(value.Path);

            compressedBytes = bytes;

            Console.WriteLine("[CompressHandler] image compressed");

            string? pathDir = Path.GetDirectoryName(value.Path);

            if (pathDir == null)
            {
                Console.WriteLine($"[CompressHandler] directory is null: {value.Path}");

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
            

            Console.WriteLine("[CompressHandler] ");

            try
            {
                File.WriteAllBytes(newPath, compressedBytes);

                Console.WriteLine($"[CompressHandler] create new file {newPath} bytes writed");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[CompressHandler] Error in writting: {ex}");

                return;
            }
        }
        else
        {
            Console.WriteLine($"[CompressHandler] unknown error {paths.Count}");

            return;
        }

        Console.WriteLine($"[CompressHandler] Success {paths.Count}");
    }
}
