using Imux.Enums;

namespace Imux.App.Requests;

public struct FindOptionValueQuery
{
    public string Query;
    public OptionType OptionType;

    public FindOptionValueQuery(string query, OptionType optionType)
    {
        Query = query;
        OptionType = optionType;
    }
}