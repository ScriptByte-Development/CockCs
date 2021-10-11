using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;

namespace CockCs
{
    public class Discord
    {
        public static DiscordRpcClient client;

        public static void Start(string ApplicationId, string Details, string State, string ImageKey, string ImageText)
        {
            client = new DiscordRpcClient(ApplicationId);
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = Details,
                State = State,
                Assets = new Assets()
                {
                    LargeImageKey = ImageKey,
                    LargeImageText = ImageText,
                }
            });
        }

        public static void Update(string ApplicationId, string Details, string State, string ImageKey, string ImageText)
        {
            //you dont want to init antoher client so use this update function to update the status 
            client.SetPresence(new RichPresence()
            {
                Details = Details,
                State = State,
                Assets = new Assets()
                {
                    LargeImageKey = ImageKey,
                    LargeImageText = ImageText,
                }
            });
        }

        public static void ShutDown()
        {
            if (client.IsDisposed)
            {
                //do nothing
            }
            else
            {
                client.Dispose();
            }
        }
    }
}