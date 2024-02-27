using System.Text;

namespace Aquifer.Common.Extensions;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendIf(this StringBuilder sb, string value, bool condition)
    {
        if (condition)
        {
            sb.Append(value);
        }

        return sb;
    }

    public static StringBuilder AppendIf(this StringBuilder sb, char value, bool condition)
    {
        if (condition)
        {
            sb.Append(value);
        }

        return sb;
    }

    public static StringBuilder AppendLineIf(this StringBuilder sb, bool condition)
    {
        if (condition)
        {
            sb.AppendLine();
        }

        return sb;
    }
}