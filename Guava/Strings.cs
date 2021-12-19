using System.Text;

namespace Guava;

public class Strings
{
    public static String CommonPrefix(String a, String b)
    {
        if (a.Equals(null)) 
            throw new ArgumentException("Cannot be null", nameof(a));
        if (b.Equals(null)) 
            throw new ArgumentException("Cannot be null", nameof(b));
        int maxPrefixLength = Math.Min(a.Length, b.Length), p=0;
        while (p < maxPrefixLength && a[p] == b[p]) p++;
        if (ValidSurrogatePairAt(a, p - 1) || ValidSurrogatePairAt(b, p - 1)) p--;
        return a.Substring(0, p);
    }

    public static String CommonSuffix(String a, String b)
    {
        if (a.Equals(null)) 
            throw new ArgumentException("Cannot be null", nameof(a));
        if (b.Equals(null)) 
            throw new ArgumentException("Cannot be null", nameof(b));
        int maxSuffixLength = Math.Min(a.Length, b.Length), s=0;
        while (s < maxSuffixLength && a[a.Length - s - 1] == b[b.Length - s - 1]) s++;
        if (ValidSurrogatePairAt(a, a.Length - s - 1) || ValidSurrogatePairAt(b, b.Length - s - 1)) s--;
        return a.Substring(a.Length - s, a.Length);
    }

    public static String EmptyToNull(String str)
    {
        if (!str.Equals(String.Empty)) return str;
        return null!;
    }

    public static bool IsNullOrEmpty(String str)
    {
        return String.IsNullOrEmpty(str);
    }

    public static String NullToEmpty(String str)
    {
        if (!str.Equals(null)) return str;
        return String.Empty;
    }

    public static String PadEnd(String str, int minLength, Char padChar)
    {
        if (str.Equals(null)) throw new ArgumentException("Cannot be null", nameof(str));
        if (str.Length >= minLength) return str;
        StringBuilder sb = new StringBuilder(minLength);
        for (int i = str.Length; i < minLength; i++) sb.Append(padChar);
        return sb.ToString();
    }

    public static String PadStart(String str, int minLength, Char padChar)
    {
        if (str.Equals(null)) throw new ArgumentException("Cannot be null", nameof(str));
        if (str.Length >= minLength) return str;
        StringBuilder sb = new StringBuilder(minLength);
        for (int i = str.Length; i < minLength; i++) sb.Append(padChar);
        return sb.Append(str).ToString();
    }

    public static String Repeat(String str, int count)
    {
        return String.Concat(Enumerable.Repeat(str, count));
    }

    private static bool ValidSurrogatePairAt(String str, int index)
    {
        return index >= 0
               && index <= (str.Length - 2)
               && char.IsHighSurrogate(str[index])
               && char.IsLowSurrogate(str[index + 1]);
    }
}
