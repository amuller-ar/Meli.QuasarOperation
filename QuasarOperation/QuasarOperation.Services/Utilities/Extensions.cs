using System;
using System.Collections.Generic;

namespace QuasarOperation.Services.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// empaqueta 3 ienumerables en 1 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ZipThree<T1, T2, T3, TResult>(
            this IEnumerable<T1> source,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            Func<T1, T2, T3, TResult> projection)
        {
            using var firstIterator = source.GetEnumerator();
            using var secondIterator = second.GetEnumerator();
            using var thirdIterator = third.GetEnumerator();

            while (true)
            {
                var hasFirst = firstIterator.MoveNext();
                var hasSecond = secondIterator.MoveNext();
                var hasThird = thirdIterator.MoveNext();

                if (hasFirst || hasSecond || hasThird)
                    yield return projection(hasFirst ? firstIterator.Current : default,
                                            hasSecond ? secondIterator.Current : default,
                                            hasThird ? thirdIterator.Current : default);
                else
                    yield break;
            }
        }

        /// <summary>
        /// retorna null si hay espacios en blanco en la cadena
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string AsNullIfWhiteSpace(this string items)
        {
            if (string.IsNullOrWhiteSpace(items))
            {
                return null;
            }
            return items;
        }
    }
}
