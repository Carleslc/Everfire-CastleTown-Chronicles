using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

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
        System.Random rnd = new System.Random();
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
    /// Utility class to measure time spans.
    /// </summary>
    public static class Diagnosis
    {
        private static Stopwatch timer = new Stopwatch();

        /// <summary>
        /// Continues the timer from the current elapsed time.
        /// <para/>If is the first call then initialices the timer without elapsed time.
        /// <para/>Starting the timer if it's already running has no effect.
        /// </summary>
        public static void StartTimer()
        {
            timer.Start();
        }

        /// <summary>
        /// Stops a running timer and gets the total time elapsed in milliseconds.
        /// <para/>Stopping the timer if it is not running has no effect.
        /// </summary>
        /// <param name="reset">If you want to reset the elapsed time.</param>
        /// <returns>Total time elapsed in milliseconds.</returns>
        public static long StopTimer(bool reset)
        {
            timer.Stop();
            long elapsed = timer.ElapsedMilliseconds;
            if (reset)
                timer.Reset();
            return elapsed;
        }

        /// <summary>
        /// Stops a running timer and gets the total time elapsed in milliseconds.
        /// <para/>Stopping the timer if it is not running has no effect.
        /// <para/>Note that this method resets the timer.
        /// This call is equals to <c>StopTimer(true)</c>.
        /// </summary>
        /// <seealso cref="StopTimer(bool)"/>
        /// <returns>Total time elapsed in milliseconds.</returns>
        public static long StopTimer()
        {
            return StopTimer(true);
        }
    }
}
