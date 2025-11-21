
using CRMApp.Data;
using System.Collections.Generic;

namespace CRMApp.Chat
{
    public class ChatConfig
    {
     
        public List<Noti> allNotificationMgs { get; set; } = new List<Noti>();
        public List<Noti> currentUserMgsList { get; set; } = new List<Noti>();
        public Noti notificationMessage { get; set; } = new Noti();
              public string BoxCss { get; set; } = "none";
    }
}
