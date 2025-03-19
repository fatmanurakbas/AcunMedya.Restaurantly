using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcunMedya.Restaurantly.Entities
{
    public class NavbarViewModel
    {
        public int NavbarViewModelId { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Contact> UnreadMessages { get; set; }
        public int UnreadMessageCount { get; set; }
        public int UnreadNotificationCount { get; set; }
    }
}