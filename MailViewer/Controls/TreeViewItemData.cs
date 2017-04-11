using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Robot1que.MailViewer.Extensions;

namespace Robot1que.MailViewer.Controls
{
    public class TreeViewItemData<T>
    {
        public bool IsVisible { get; set; } = true;

        public int NestingLevel { get; }

        public T Data { get; }

        public ObservableCollection<TreeViewItemData<T>> Children { get; }

        public TreeViewItemData(T data, Func<T, IEnumerable<T>> childrenSelector, int nestingLevel = 0)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (childrenSelector == null)
            {
                throw new ArgumentNullException(nameof(childrenSelector));
            }

            this.Data = data;
            this.NestingLevel = nestingLevel;
            this.Children = 
                childrenSelector.Invoke(data)
                    .Select(item => new TreeViewItemData<T>(item, childrenSelector, nestingLevel + 1))
                    .ToObservableCollection();
        }
    }
}
