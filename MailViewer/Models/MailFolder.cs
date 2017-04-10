using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Robot1que.MailViewer.Extensions;

namespace Robot1que.MailViewer.Models
{
    public class MailFolder
    {
        public string Id { get; private set; }

        public string ParentFolderId { get; private set; }

        public string DisplayName { get; private set; }

        public ImmutableArray<MailFolder> ChildFolders { get; private set; }

        public int? UnreadItemCount { get; private set; }

        private MailFolder()
        {

        }

        public static MailFolder FromData(Microsoft.Graph.MailFolder data)
        {
            return 
                new MailFolder()
                {
                    Id = data.Id,
                    ParentFolderId = data.ParentFolderId,
                    DisplayName = data.DisplayName,
                    ChildFolders = data.ChildFolders.ToImmutableArray(item => MailFolder.FromData(item)),
                    UnreadItemCount = data.UnreadItemCount
                };
        }
    }
}
