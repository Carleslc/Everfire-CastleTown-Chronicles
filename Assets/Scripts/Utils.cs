using System;
using System.Collections.Generic;
using System.Linq;

public static class Utils {

    /// <summary>
    /// Gets a new randomly shuffled enumerable of this enumerable.
    /// <para/>This enumerable remains unchanged.
    /// </summary>
    /// <typeparam name="T">The IEnumerable type.</typeparam>
    /// <param name="source">This enumerable.</param>
    /// <returns>This enumerable shuffled.</returns>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        T[] elements = source.ToArray();
        // Note i > 0 to avoid final pointless iteration
        for (int i = elements.Length - 1; i > 0; i--)
        {
            // Swap element "i" with a random earlier element it (or itself)
            int swapIndex = rnd.Next(i + 1);
            T tmp = elements[i];
            elements[i] = elements[swapIndex];
            elements[swapIndex] = tmp;
        }
        // Lazily yield (avoiding aliasing issues etc)
        foreach (T element in elements)
            yield return element;
    }

    /// <summary>
    /// Gets the distance in tiles from this position to another.
    /// </summary>
    /// <param name="from">This (start) position.</param>
    /// <param name="to">The (destination) other position.</param>
    /// <returns>The distance in tiles from this position to another.</returns>
    public static int Distance(this Pos from, Pos to)
    {
        int x = to.X - from.X;
        int y = to.Y - from.Y;
        return (x >= 0 ? x : -x) + (y >= 0 ? y : -y);
    }
}
