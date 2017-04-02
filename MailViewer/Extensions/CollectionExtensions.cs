using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot1que.MailViewer.Extensions
{
    public static class CollectionExtensions
    {
        public static ImmutableArray<TResult> ToImmutableArray<T, TResult>(
            this IEnumerable<T> items, 
            Func<T, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return 
                items != null ?
                    items.Select(selector).ToImmutableArray() :
                    ImmutableArray<TResult>.Empty;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            return items != null ? new ObservableCollection<T>(items) : new ObservableCollection<T>();
        }

        public static ObservableCollection<object> ToObservableCollection(this IEnumerable items)
        {
            return 
                items != null ?
                    items.Cast<object>().ToObservableCollection() : 
                    new ObservableCollection<object>();
        }
    }
}
