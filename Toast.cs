using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;


namespace CockCs
{
    public static class Toast
    {
        public static void Create(string topMessage, string bottomMessage)
        {
            new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddText(topMessage)
            .AddText(bottomMessage)
            .Show();
        }
    }
}
