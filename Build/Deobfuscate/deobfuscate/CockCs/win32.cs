using System;
using System.Runtime.InteropServices;

namespace CockCs
{
	// Token: 0x02000017 RID: 23
	public class win32
	{
		// Token: 0x06000087 RID: 135
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllocConsole();

		// Token: 0x06000088 RID: 136 RVA: 0x000044D8 File Offset: 0x000026D8
		public static void Update(string title, string message)
		{
			Console.Title = title;
			Console.WriteLine(message);
		}
	}
}
