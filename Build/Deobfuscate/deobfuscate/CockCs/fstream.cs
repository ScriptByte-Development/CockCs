using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CockCs
{
	// Token: 0x02000014 RID: 20
	public class fstream
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000417C File Offset: 0x0000237C
		public static void CheckFolderExist(string path)
		{
			try
			{
				bool flag = Directory.Exists(path);
				if (!flag)
				{
					fstream.FolderExists = false;
				}
				else if (flag)
				{
					fstream.FolderExists = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000041C8 File Offset: 0x000023C8
		public static void CheckFileExist(string path)
		{
			try
			{
				bool flag = File.Exists(path);
				if (!flag)
				{
					fstream.FileExists = false;
				}
				else if (flag)
				{
					fstream.FileExists = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004214 File Offset: 0x00002414
		public static void RunFile(string file)
		{
			try
			{
				Process.Start(file);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000424C File Offset: 0x0000244C
		public static bool FileCompare(string file1, string file2)
		{
			if (file1 == file2)
			{
				return true;
			}
			FileStream fileStream = new FileStream(file1, FileMode.Open);
			FileStream fileStream2 = new FileStream(file2, FileMode.Open);
			if (fileStream.Length != fileStream2.Length)
			{
				fileStream.Close();
				fileStream2.Close();
				return false;
			}
			int num;
			int num2;
			do
			{
				num = fileStream.ReadByte();
				num2 = fileStream2.ReadByte();
			}
			while (num == num2 && num != -1);
			fileStream.Close();
			fileStream2.Close();
			return num - num2 == 0;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000042B8 File Offset: 0x000024B8
		public static void CheckFileSimilaritie(string file1, string file2)
		{
			try
			{
				if (fstream.FileCompare(file1, file2))
				{
					MessageBox.Show("files are equal");
				}
				else
				{
					MessageBox.Show("files are not equal");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000430C File Offset: 0x0000250C
		public static void RenameFile(string currentname, string newname)
		{
			try
			{
				File.Move(currentname ?? "", newname ?? "");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004358 File Offset: 0x00002558
		public static void WriteFile(string file, string data)
		{
			try
			{
				if (!File.Exists(file))
				{
					using (StreamWriter streamWriter = File.CreateText(file))
					{
						streamWriter.WriteLine(data);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Concat(new string[]
				{
					"Error writing ",
					data,
					" to ",
					file,
					", ",
					ex.Message
				}), Application.ProductName);
			}
		}

		// Token: 0x04000039 RID: 57
		public static bool FolderExists;

		// Token: 0x0400003A RID: 58
		public static bool FileExists;
	}
}
