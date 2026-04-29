using Imux.App.Responses;
using Imux.App.Requests;

namespace Imux;

public class PathFinder
{
    private static readonly string header = "[PathFinder]";

    public static List<OptionValue> FindValue(string[] values, params FindOptionValueQuery[] queries)
    {
        List<OptionValue> paths = new();

        Console.WriteLine($"{header} args lenght: {values.Length}, queries lenght: {queries.Length}");

        for (int i = 0; i < values.Length; i++)
        {
            string value = values[i];

            for (int j = 0; j < queries.Length; j++)
            {
                if (value == queries[j].Query)
                {
                    Console.WriteLine($"{header} parametr: {value}");

                    int index = i + 1;

                    if (index < values.Length)
                    {
                        string path = Path.GetFullPath(values[index]);

                        if (Path.Exists(path))
                        {
                            Console.WriteLine($"{header} finded path: {path} index: {index}");
                            
                            paths.Add(new OptionValue()
                            {
                                Path = path,
                                OptionType = queries[j].OptionType
                            });
                        }
                    }

                    break;
                }
            }
        }

        return paths;
    }
}