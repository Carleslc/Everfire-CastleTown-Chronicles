using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public static class Utils {

    /// <summary>
    /// Shuffles this list.
    /// </summary>
    /// <typeparam name="T">The list type.</typeparam>
    /// <param name="list">This list.</param>
    /// <returns>This list, now shuffled.</returns>
    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        System.Random rnd = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
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
