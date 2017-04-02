using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graph;

namespace Robot1que.MailViewer.Models
{
    public class UserProfile
    {
        public string Id { get; private set; }

        public string DisplayName { get; private set; }

        private UserProfile()
        {

        }

        public static UserProfile FromData(User user)
        {
            return
                new UserProfile()
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName
                };
        }
    }
}
