using System.Text;

namespace Guava;

public class Strings
{
    /// <summary>
    /// Provides common prefix
    /// </summary>
    /// <param name="a"> First string </param>
    /// <param name="b"> Second string</param>
    /// <returns> Returns the longest string prefix such that a.toString().startsWith(prefix) && b.toString()
    /// .startsWith(prefix), taking care not to split surrogate pairs. If a and b have no common prefix,
    /// returns the empty string </returns>
    /// <exception cref="ArgumentException"> String a and String b cannot be null</exception>
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

    /// <summary>
    /// Provides common suffix
    /// </summary>
    /// <param name="a"> First string </param>
    /// <param name="b"> Second string </param>
    /// <returns> Returns the longest string suffix such that a.toString().endsWith(suffix) && b.toString()
    /// .endsWith(suffix), taking care not to split surrogate pairs. If a and b have no common suffix,
    /// returns the empty string. </returns>
    /// <exception cref="ArgumentException"> String a and String b cannot be null </exception>
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

    /// <summary>
    /// Returns the given string if it is nonempty; null otherwise.
    /// </summary>
    /// <param name="str"> the string to test and possibly return </param>
    /// <returns> string itself if it is nonempty; null if it is empty or null </returns>
    public static String EmptyToNull(String str)
    {
        if (!str.Equals(String.Empty)) return str;
        return null!;
    }

    /// <summary>
    /// Returns the given string if it is non-null; the empty string otherwise.
    /// </summary>
    /// <param name="str"> the string to test and possibly return </param>
    /// <returns> string itself if it is non-null; "" if it is null </returns>
    public static bool IsNullOrEmpty(String str)
    {
        return String.IsNullOrEmpty(str);
    }

    /// <summary>
    /// Returns the given string if it is non-null; the empty string otherwise.
    /// </summary>
    /// <param name="str"> the string to test and possibly return </param>
    /// <returns> string itself if it is non-null; "" if it is null </returns>
    public static String NullToEmpty(String str)
    {
        if (!str.Equals(null)) return str;
        return String.Empty;
    }

    /// <summary>
    /// Pads the right of string as much as possible, given the length. For example:
    /// * PadEnd("4.", 5, '0') returns "4.000"
    /// * PadEnd("2010", 3, '!') returns "2010"
    /// </summary>
    /// <param name="str"> the string which should appear at the beginning of the result </param>
    /// <param name="minLength"> the minimum length the resulting string must have. Can be zero or negative, in which
    /// case the input string is always returned.</param>
    /// <param name="padChar"> the character to append to the end of the result until the minimum length is reached</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"> String cannot be null</exception>
    public static String PadEnd(String str, int minLength, Char padChar)
    {
        if (str.Equals(null)) throw new ArgumentException("Cannot be null", nameof(str));
        if (str.Length >= minLength) return str;
        StringBuilder sb = new StringBuilder(minLength);
        for (int i = str.Length; i < minLength; i++) sb.Append(padChar);
        return sb.ToString();
    }

    /// <summary>
    /// Pads the left of string as much as possible, given the length. For example:
    /// * PadStart("7", 3, '0') returns "007"
    /// * PadStart("2010", 3, '0') returns "2010"
    /// </summary>
    /// <param name="str"> the string which should appear at the end of the result </param>
    /// <param name="minLength"> the minimum length the resulting string must have. Can be zero or negative,
    ///  in which case the input string is always returned.</param>
    /// <param name="padChar"> the character to insert at the beginning of the result until the minimum length is reached </param>
    /// <returns> Returns a string, of length at least minLength, consisting of string prepended with as many copies of padChar as are necessary to reach that length</returns>
    /// <exception cref="ArgumentException"></exception>
    public static String PadStart(String str, int minLength, Char padChar)
    {
        if (str.Equals(null)) throw new ArgumentException("Cannot be null", nameof(str));
        if (str.Length >= minLength) return str;
        StringBuilder sb = new StringBuilder(minLength);
        for (int i = str.Length; i < minLength; i++) sb.Append(padChar);
        return sb.Append(str).ToString();
    }

    /// <summary>
    /// Returns a string consisting of a specific number of concatenated copies of an input string. For example, repeat("hey", 3) returns the string "heyheyhey"
    /// </summary>
    /// <param name="str"> any non-null string </param>
    /// <param name="count"> the number of times to repeat it; a nonnegative integer </param>
    /// <returns> a string containing string repeated count times (the empty string if count is zero) </returns>
    /// <exception cref="ArgumentException"> If count is negative</exception>
    public static String Repeat(String str, int count)
    {
        if (count < 0) throw new ArgumentException("Count cannot be negative", nameof(count));
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
