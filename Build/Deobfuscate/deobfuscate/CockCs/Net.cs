using System;
using System.Net;
using System.Windows.Forms;

namespace CockCs
{
	// Token: 0x02000016 RID: 22
	public class Net
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000442C File Offset: 0x0000262C
		public static void ReadWebString(string website)
		{
			try
			{
				Net.webstring = new WebClient().DownloadString(website);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004470 File Offset: 0x00002670
		public static void CompareWebString(string website, string compare)
		{
			try
			{
				if (new WebClient().DownloadString(website).Contains(compare))
				{
					Net.WebStringTrue = true;
				}
				else
				{
					Net.WebStringTrue = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000044C4 File Offset: 0x000026C4
		public static void DownloadFile(string url, string fileName)
		{
			new WebClient();
			new WebClient().DownloadFile(url, fileName);
		}

		// Token: 0x0400003B RID: 59
		public static string webstring;

		// Token: 0x0400003C RID: 60
		public static bool WebStringTrue;
	}
}
