using System;
using DiscordRPC;

namespace CockCs
{
	// Token: 0x02000013 RID: 19
	public class Discord
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000040D0 File Offset: 0x000022D0
		public static void Start(string ApplicationId, string Details, string State, string ImageKey, string ImageText)
		{
			Discord.client = new DiscordRpcClient(ApplicationId);
			Discord.client.Initialize();
			Discord.client.SetPresence(new RichPresence
			{
				Details = Details,
				State = State,
				Assets = new Assets
				{
					LargeImageKey = ImageKey,
					LargeImageText = ImageText
				}
			});
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000412A File Offset: 0x0000232A
		public static void Update(string ApplicationId, string Details, string State, string ImageKey, string ImageText)
		{
			Discord.client.SetPresence(new RichPresence
			{
				Details = Details,
				State = State,
				Assets = new Assets
				{
					LargeImageKey = ImageKey,
					LargeImageText = ImageText
				}
			});
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004163 File Offset: 0x00002363
		public static void ShutDown()
		{
			if (!Discord.client.IsDisposed)
			{
				Discord.client.Dispose();
			}
		}

		// Token: 0x04000038 RID: 56
		public static DiscordRpcClient client;
	}
}
