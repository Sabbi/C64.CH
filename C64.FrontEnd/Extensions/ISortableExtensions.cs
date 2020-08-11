using C64.Data.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace C64.FrontEnd.Extensions
{
    public static class ISortableExtensions
    {
        public static void UpdateSort<T>(this ICollection<T> source) where T : ISortable
        {
            if (source == null || !source.Any())
                return;

            for (var i = 0; i < source.Count; i++)
                source.ElementAt(i).Sort = i;
        }
    }
}