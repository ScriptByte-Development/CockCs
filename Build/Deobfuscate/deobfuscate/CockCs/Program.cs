using System;
using System.Windows.Forms;

namespace CockCs
{
	// Token: 0x02000015 RID: 21
	public class Program
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000043E8 File Offset: 0x000025E8
		public static void Message(string text, string label)
		{
			try
			{
				MessageBox.Show(text, label);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004424 File Offset: 0x00002624
		public static void Close()
		{
			Environment.Exit(0);
		}
	}
}
