using System.Security.Cryptography;

namespace SlackOverflow.Web.Extensions
{
    public static class EnumerableExtensions
    {
        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            return source.GetRandom(1).Single();
        }

        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            // Use Crypto RandomNumberGenerator because it's static and thread-safe.
            return source.OrderBy(x => RandomNumberGenerator.GetInt32(source.Count()));
        }
    }
}