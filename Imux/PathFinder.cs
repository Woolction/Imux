using Imux.App.Responses;
using Imux.App.Requests;

namespace Imux;

public class PathFinder
{
    public static List<OptionValue> FindValue(string[] values, params FindOptionValueQuery[] queries)
    {
        List<OptionValue> paths = new();

        Console.WriteLine($"[PathFinder] args lenght: {values.Length}, queries lenght: {queries.Length}");

        for (int i = 0; i < values.Length; i++)
        {
            string value = values[i];

            //Console.WriteLine($"[PathFinder] arg value: {value}");

            for (int j = 0; j < queries.Length; j++)
            {
                if (value == queries[j].Query)
                {
                    Console.WriteLine($"[PathFinder] parametr: {value}");

                    int index = i + 1;

                    if (index < values.Length)
                    {
                        string path = Path.GetFullPath(values[index]);

                        if (Path.Exists(path))
                        {
                            Console.WriteLine($"[PathFinder] finded path: {path} index: {index}");
                            
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