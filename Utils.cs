using System.Runtime.CompilerServices;
using Serilog;

namespace ModShardPacker;

public static class Utils
{
    // https://stackoverflow.com/questions/70430422/how-to-implicitly-convert-nullable-type-to-non-nullable-type
    public static T ThrowIfNull<T>(
        this T? argument,
        string? message = default,
        [CallerArgumentExpression("argument")] string? paramName = default
    ) where T : notnull
    {
        if (argument is null)
        {
            Log.Error("{0} is null.", argument);
            throw new ArgumentNullException(paramName, message);
        }
        else
        {
            return argument;
        }
    }
    public static T ThrowIfNull<T>(
        this T? argument,
        string? message = default,
        [CallerArgumentExpression("argument")] string? paramName = default
    ) where T : unmanaged
    {
        if (argument is null)
        {
            Log.Error("{0} is null.", argument);
            throw new ArgumentNullException(paramName, message);
        }
        else
        {
            return (T)argument;
        }
    }
}

