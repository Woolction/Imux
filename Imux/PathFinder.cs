using Imux.App.Responses;
using Imux.App.Requests;

namespace Imux;

public class PathFinder
{
    public static List<OptionValue> FindValue(string[] values, params FindOptionValueQuery[] queries)
    {
        List<OptionValue> paths = new();

        for (int i = 0; i < values.Length; i++)
        {
            string value = values[i];

            for (int j = 0; j < queries.Length; j++)
            {
                if (value == queries[j].Query)
                {
                    if (i++ < values.Length)
                    {
                        string path = Path.GetFullPath(values[i++]);

                        if (Path.Exists(path))
                        {
                            paths.Add(new OptionValue()
                            {
                                Path = path,
                                OptionType = queries[j].OptionType
                            });
                        }
                    }
                }
            }
        }

        return paths;
    }
}