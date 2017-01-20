using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL
{
    public static class Helper
    {
        public static List<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source, Func<TSource, TDestination> action) where TDestination : class
        {
            var count = source.Count();
            var destination = new List<TDestination>();
            for (int i = 0; i < count; i++)
            {
                var dest = action(source.ElementAtOrDefault(i));
                destination.Add(dest);
            }
            return destination;
        }
    }
}
