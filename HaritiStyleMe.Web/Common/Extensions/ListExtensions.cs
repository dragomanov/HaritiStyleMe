namespace HaritiStyleMe.Web.Common.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ListExtensions
    {
        private static Random rng = new Random();

        public static T GetRandom<T>(this IList<T> items)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentNullException("items", "Cannot get a random element from empty collection");
            }

            return items[rng.Next(0, items.Count)];
        }
    }
}