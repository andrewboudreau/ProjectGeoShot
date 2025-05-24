namespace ProjectGeoShot.Game.Weather;

internal static class QueryHelpers
{
    public static string AddQueryString(string uri, IDictionary<string, string?> query)
    {
        var builder = new System.Text.StringBuilder(uri);
        bool hasQuestion = uri.Contains('?');
        foreach (var kvp in query)
        {
            builder.Append(hasQuestion ? '&' : '?');
            hasQuestion = true;
            builder.Append(Uri.EscapeDataString(kvp.Key));
            builder.Append('=');
            builder.Append(Uri.EscapeDataString(kvp.Value ?? string.Empty));
        }
        return builder.ToString();
    }
}
