using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CockCs
{
	// Token: 0x02000003 RID: 3
	internal class Auth
	{
		// Token: 0x02000004 RID: 4
		internal class App
		{
			// Token: 0x06000006 RID: 6 RVA: 0x00002230 File Offset: 0x00000430
			public static string GrabVariable(string name)
			{
				string result;
				try
				{
					if (Auth.User.ID != null || Auth.User.HWID != null || Auth.User.IP != null || !Auth.Constants.Breached)
					{
						result = Auth.App.Variables[name];
					}
					else
					{
						Auth.Constants.Breached = true;
						result = "User is not logged in, possible breach detected!";
					}
				}
				catch
				{
					result = "N/A";
				}
				return result;
			}

			// Token: 0x04000002 RID: 2
			public static string Error = null;

			// Token: 0x04000003 RID: 3
			public static Dictionary<string, string> Variables = new Dictionary<string, string>();
		}

		// Token: 0x02000005 RID: 5
		internal class Login
		{
			// Token: 0x06000009 RID: 9 RVA: 0x000022A2 File Offset: 0x000004A2
			public static void CeckLogin(string user, string password)
			{
				if (user == Auth.User.Username && password == Auth.User.Password)
				{
					Auth.Login.correctpassword = true;
					return;
				}
				Auth.Login.correctpassword = false;
			}

			// Token: 0x04000004 RID: 4
			public static bool correctpassword;
		}

		// Token: 0x02000006 RID: 6
		internal class Constants
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600000B RID: 11 RVA: 0x000022CB File Offset: 0x000004CB
			// (set) Token: 0x0600000C RID: 12 RVA: 0x000022D2 File Offset: 0x000004D2
			public static string Token { get; set; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000D RID: 13 RVA: 0x000022DA File Offset: 0x000004DA
			// (set) Token: 0x0600000E RID: 14 RVA: 0x000022E1 File Offset: 0x000004E1
			public static string Date { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000F RID: 15 RVA: 0x000022E9 File Offset: 0x000004E9
			// (set) Token: 0x06000010 RID: 16 RVA: 0x000022F0 File Offset: 0x000004F0
			public static string APIENCRYPTKEY { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000011 RID: 17 RVA: 0x000022F8 File Offset: 0x000004F8
			// (set) Token: 0x06000012 RID: 18 RVA: 0x000022FF File Offset: 0x000004FF
			public static string APIENCRYPTSALT { get; set; }

			// Token: 0x06000013 RID: 19 RVA: 0x00002307 File Offset: 0x00000507
			public static string RandomString(int length)
			{
				return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
				select s[Auth.Constants.random.Next(s.Length)]).ToArray<char>());
			}

			// Token: 0x06000014 RID: 20 RVA: 0x00002342 File Offset: 0x00000542
			public static string HWID()
			{
				return WindowsIdentity.GetCurrent().User.Value;
			}

			// Token: 0x04000009 RID: 9
			public static bool Breached = false;

			// Token: 0x0400000A RID: 10
			public static bool Started = false;

			// Token: 0x0400000B RID: 11
			public static string IV = null;

			// Token: 0x0400000C RID: 12
			public static string Key = null;

			// Token: 0x0400000D RID: 13
			public static string ApiUrl = "https://api.auth.gg/csharp/";

			// Token: 0x0400000E RID: 14
			public static bool Initialized = false;

			// Token: 0x0400000F RID: 15
			public static Random random = new Random();
		}

		// Token: 0x02000008 RID: 8
		internal class User
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001A RID: 26 RVA: 0x000023AB File Offset: 0x000005AB
			// (set) Token: 0x0600001B RID: 27 RVA: 0x000023B2 File Offset: 0x000005B2
			public static string ID { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001C RID: 28 RVA: 0x000023BA File Offset: 0x000005BA
			// (set) Token: 0x0600001D RID: 29 RVA: 0x000023C1 File Offset: 0x000005C1
			public static string Username { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001E RID: 30 RVA: 0x000023C9 File Offset: 0x000005C9
			// (set) Token: 0x0600001F RID: 31 RVA: 0x000023D0 File Offset: 0x000005D0
			public static string Password { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000020 RID: 32 RVA: 0x000023D8 File Offset: 0x000005D8
			// (set) Token: 0x06000021 RID: 33 RVA: 0x000023DF File Offset: 0x000005DF
			public static string Email { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000022 RID: 34 RVA: 0x000023E7 File Offset: 0x000005E7
			// (set) Token: 0x06000023 RID: 35 RVA: 0x000023EE File Offset: 0x000005EE
			public static string HWID { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000024 RID: 36 RVA: 0x000023F6 File Offset: 0x000005F6
			// (set) Token: 0x06000025 RID: 37 RVA: 0x000023FD File Offset: 0x000005FD
			public static string IP { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000026 RID: 38 RVA: 0x00002405 File Offset: 0x00000605
			// (set) Token: 0x06000027 RID: 39 RVA: 0x0000240C File Offset: 0x0000060C
			public static string UserVariable { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00002414 File Offset: 0x00000614
			// (set) Token: 0x06000029 RID: 41 RVA: 0x0000241B File Offset: 0x0000061B
			public static string Rank { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600002A RID: 42 RVA: 0x00002423 File Offset: 0x00000623
			// (set) Token: 0x0600002B RID: 43 RVA: 0x0000242A File Offset: 0x0000062A
			public static string Expiry { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00002432 File Offset: 0x00000632
			// (set) Token: 0x0600002D RID: 45 RVA: 0x00002439 File Offset: 0x00000639
			public static string LastLogin { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600002E RID: 46 RVA: 0x00002441 File Offset: 0x00000641
			// (set) Token: 0x0600002F RID: 47 RVA: 0x00002448 File Offset: 0x00000648
			public static string RegisterDate { get; set; }
		}

		// Token: 0x02000009 RID: 9
		internal class ApplicationSettings
		{
			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00002450 File Offset: 0x00000650
			// (set) Token: 0x06000032 RID: 50 RVA: 0x00002457 File Offset: 0x00000657
			public static bool Status { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000033 RID: 51 RVA: 0x0000245F File Offset: 0x0000065F
			// (set) Token: 0x06000034 RID: 52 RVA: 0x00002466 File Offset: 0x00000666
			public static bool DeveloperMode { get; set; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000035 RID: 53 RVA: 0x0000246E File Offset: 0x0000066E
			// (set) Token: 0x06000036 RID: 54 RVA: 0x00002475 File Offset: 0x00000675
			public static string Hash { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000037 RID: 55 RVA: 0x0000247D File Offset: 0x0000067D
			// (set) Token: 0x06000038 RID: 56 RVA: 0x00002484 File Offset: 0x00000684
			public static string Version { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000039 RID: 57 RVA: 0x0000248C File Offset: 0x0000068C
			// (set) Token: 0x0600003A RID: 58 RVA: 0x00002493 File Offset: 0x00000693
			public static string Update_Link { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600003B RID: 59 RVA: 0x0000249B File Offset: 0x0000069B
			// (set) Token: 0x0600003C RID: 60 RVA: 0x000024A2 File Offset: 0x000006A2
			public static bool Freemode { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600003D RID: 61 RVA: 0x000024AA File Offset: 0x000006AA
			// (set) Token: 0x0600003E RID: 62 RVA: 0x000024B1 File Offset: 0x000006B1
			public static bool Login { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600003F RID: 63 RVA: 0x000024B9 File Offset: 0x000006B9
			// (set) Token: 0x06000040 RID: 64 RVA: 0x000024C0 File Offset: 0x000006C0
			public static string Name { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000041 RID: 65 RVA: 0x000024C8 File Offset: 0x000006C8
			// (set) Token: 0x06000042 RID: 66 RVA: 0x000024CF File Offset: 0x000006CF
			public static bool Register { get; set; }
		}

		// Token: 0x0200000A RID: 10
		internal class OnProgramStart
		{
			// Token: 0x06000044 RID: 68 RVA: 0x000024D8 File Offset: 0x000006D8
			public static void Initialize(string name, string aid, string secret, string version)
			{
				if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(aid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
				{
					Process.GetCurrentProcess().Kill();
				}
				Auth.OnProgramStart.AID = aid;
				Auth.OnProgramStart.Secret = secret;
				Auth.OnProgramStart.Version = version;
				Auth.OnProgramStart.Name = name;
				string[] array = new string[0];
				using (WebClient webClient = new WebClient())
				{
					try
					{
						webClient.Proxy = null;
						Auth.Security.Start();
						Encoding @default = Encoding.Default;
						WebClient webClient2 = webClient;
						string apiUrl = Auth.Constants.ApiUrl;
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection["token"] = Auth.Encryption.EncryptService(Auth.Constants.Token);
						nameValueCollection["timestamp"] = Auth.Encryption.EncryptService(DateTime.Now.ToString());
						nameValueCollection["aid"] = Auth.Encryption.APIService(Auth.OnProgramStart.AID);
						nameValueCollection["session_id"] = Auth.Constants.IV;
						nameValueCollection["api_id"] = Auth.Constants.APIENCRYPTSALT;
						nameValueCollection["api_key"] = Auth.Constants.APIENCRYPTKEY;
						nameValueCollection["session_key"] = Auth.Constants.Key;
						nameValueCollection["secret"] = Auth.Encryption.APIService(Auth.OnProgramStart.Secret);
						nameValueCollection["type"] = Auth.Encryption.APIService("start");
						array = Auth.Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
						if (Auth.Security.MaliciousCheck(array[1]))
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Constants.Breached)
						{
							Process.GetCurrentProcess().Kill();
						}
						if (array[0] != Auth.Constants.Token)
						{
							Process.GetCurrentProcess().Kill();
						}
						string a = array[2];
						if (!(a == "success"))
						{
							if (a == "binderror")
							{
								Process.GetCurrentProcess().Kill();
								return;
							}
							if (a == "banned")
							{
								Process.GetCurrentProcess().Kill();
								return;
							}
						}
						else
						{
							Auth.Constants.Initialized = true;
							if (array[3] == "Enabled")
							{
								Auth.ApplicationSettings.Status = true;
							}
							if (array[4] == "Enabled")
							{
								Auth.ApplicationSettings.DeveloperMode = true;
							}
							Auth.ApplicationSettings.Hash = array[5];
							Auth.ApplicationSettings.Version = array[6];
							Auth.ApplicationSettings.Update_Link = array[7];
							if (array[8] == "Enabled")
							{
								Auth.ApplicationSettings.Freemode = true;
							}
							if (array[9] == "Enabled")
							{
								Auth.ApplicationSettings.Login = true;
							}
							Auth.ApplicationSettings.Name = array[10];
							if (array[11] == "Enabled")
							{
								Auth.ApplicationSettings.Register = true;
							}
							if (Auth.ApplicationSettings.DeveloperMode)
							{
								File.Create(Environment.CurrentDirectory + "/integrity.log").Close();
								string contents = Auth.Security.Integrity(Process.GetCurrentProcess().MainModule.FileName);
								File.WriteAllText(Environment.CurrentDirectory + "/integrity.log", contents);
							}
							else
							{
								if (array[12] == "Enabled" && Auth.ApplicationSettings.Hash != Auth.Security.Integrity(Process.GetCurrentProcess().MainModule.FileName))
								{
									Process.GetCurrentProcess().Kill();
								}
								if (Auth.ApplicationSettings.Version != Auth.OnProgramStart.Version)
								{
									Process.Start(Auth.ApplicationSettings.Update_Link);
									Process.GetCurrentProcess().Kill();
								}
							}
							if (!Auth.ApplicationSettings.Status)
							{
								Process.GetCurrentProcess().Kill();
							}
						}
						Auth.Security.End();
					}
					catch (Exception)
					{
						Process.GetCurrentProcess().Kill();
					}
				}
			}

			// Token: 0x04000026 RID: 38
			public static string AID;

			// Token: 0x04000027 RID: 39
			public static string Secret;

			// Token: 0x04000028 RID: 40
			public static string Version;

			// Token: 0x04000029 RID: 41
			public static string Name;

			// Token: 0x0400002A RID: 42
			public static string Salt;
		}

		// Token: 0x0200000B RID: 11
		internal class API
		{
			// Token: 0x06000046 RID: 70 RVA: 0x00002854 File Offset: 0x00000A54
			public static void Log(string username, string action)
			{
				if (!Auth.Constants.Initialized)
				{
					Process.GetCurrentProcess().Kill();
				}
				if (string.IsNullOrWhiteSpace(action))
				{
					Process.GetCurrentProcess().Kill();
				}
				new string[0];
				using (WebClient webClient = new WebClient())
				{
					try
					{
						Auth.Security.Start();
						webClient.Proxy = null;
						Encoding @default = Encoding.Default;
						WebClient webClient2 = webClient;
						string apiUrl = Auth.Constants.ApiUrl;
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection["token"] = Auth.Encryption.EncryptService(Auth.Constants.Token);
						nameValueCollection["aid"] = Auth.Encryption.APIService(Auth.OnProgramStart.AID);
						nameValueCollection["username"] = Auth.Encryption.APIService(username);
						nameValueCollection["pcuser"] = Auth.Encryption.APIService(Environment.UserName);
						nameValueCollection["session_id"] = Auth.Constants.IV;
						nameValueCollection["api_id"] = Auth.Constants.APIENCRYPTSALT;
						nameValueCollection["api_key"] = Auth.Constants.APIENCRYPTKEY;
						nameValueCollection["data"] = Auth.Encryption.APIService(action);
						nameValueCollection["session_key"] = Auth.Constants.Key;
						nameValueCollection["secret"] = Auth.Encryption.APIService(Auth.OnProgramStart.Secret);
						nameValueCollection["type"] = Auth.Encryption.APIService("log");
						Auth.Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
						Auth.Security.End();
					}
					catch (Exception)
					{
						Process.GetCurrentProcess().Kill();
					}
				}
			}

			// Token: 0x06000047 RID: 71 RVA: 0x000029EC File Offset: 0x00000BEC
			public static bool AIO(string AIO)
			{
				return Auth.API.AIOLogin(AIO) || Auth.API.AIORegister(AIO);
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00002A04 File Offset: 0x00000C04
			public static bool AIOLogin(string AIO)
			{
				if (!Auth.Constants.Initialized)
				{
					Process.GetCurrentProcess().Kill();
				}
				if (string.IsNullOrWhiteSpace(AIO))
				{
					Process.GetCurrentProcess().Kill();
				}
				string[] array = new string[0];
				bool result;
				using (WebClient webClient = new WebClient())
				{
					try
					{
						Auth.Security.Start();
						webClient.Proxy = null;
						Encoding @default = Encoding.Default;
						WebClient webClient2 = webClient;
						string apiUrl = Auth.Constants.ApiUrl;
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection["token"] = Auth.Encryption.EncryptService(Auth.Constants.Token);
						nameValueCollection["timestamp"] = Auth.Encryption.EncryptService(DateTime.Now.ToString());
						nameValueCollection["aid"] = Auth.Encryption.APIService(Auth.OnProgramStart.AID);
						nameValueCollection["session_id"] = Auth.Constants.IV;
						nameValueCollection["api_id"] = Auth.Constants.APIENCRYPTSALT;
						nameValueCollection["api_key"] = Auth.Constants.APIENCRYPTKEY;
						nameValueCollection["username"] = Auth.Encryption.APIService(AIO);
						nameValueCollection["password"] = Auth.Encryption.APIService(AIO);
						nameValueCollection["hwid"] = Auth.Encryption.APIService(Auth.Constants.HWID());
						nameValueCollection["session_key"] = Auth.Constants.Key;
						nameValueCollection["secret"] = Auth.Encryption.APIService(Auth.OnProgramStart.Secret);
						nameValueCollection["type"] = Auth.Encryption.APIService("login");
						array = Auth.Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
						if (array[0] != Auth.Constants.Token)
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Security.MaliciousCheck(array[1]))
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Constants.Breached)
						{
							Process.GetCurrentProcess().Kill();
						}
						string a = array[2];
						if (a == "success")
						{
							Auth.Security.End();
							Auth.User.ID = array[3];
							Auth.User.Username = array[4];
							Auth.User.Password = array[5];
							Auth.User.Email = array[6];
							Auth.User.HWID = array[7];
							Auth.User.UserVariable = array[8];
							Auth.User.Rank = array[9];
							Auth.User.IP = array[10];
							Auth.User.Expiry = array[11];
							Auth.User.LastLogin = array[12];
							Auth.User.RegisterDate = array[13];
							string[] array2 = array[14].Split(new char[]
							{
								'~'
							});
							for (int i = 0; i < array2.Length; i++)
							{
								string[] array3 = array2[i].Split(new char[]
								{
									'^'
								});
								try
								{
									Auth.App.Variables.Add(array3[0], array3[1]);
								}
								catch
								{
								}
							}
							return true;
						}
						if (a == "invalid_details")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "time_expired")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "hwid_updated")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "invalid_hwid")
						{
							Auth.Security.End();
							return false;
						}
					}
					catch (Exception)
					{
						Auth.Security.End();
						Process.GetCurrentProcess().Kill();
					}
					result = false;
				}
				return result;
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00002D50 File Offset: 0x00000F50
			public static bool AIORegister(string AIO)
			{
				if (!Auth.Constants.Initialized)
				{
					Auth.Security.End();
					Process.GetCurrentProcess().Kill();
				}
				if (string.IsNullOrWhiteSpace(AIO))
				{
					Process.GetCurrentProcess().Kill();
				}
				new string[0];
				bool result;
				using (WebClient webClient = new WebClient())
				{
					try
					{
						Auth.Security.Start();
						webClient.Proxy = null;
						Encoding @default = Encoding.Default;
						WebClient webClient2 = webClient;
						string apiUrl = Auth.Constants.ApiUrl;
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection["token"] = Auth.Encryption.EncryptService(Auth.Constants.Token);
						nameValueCollection["timestamp"] = Auth.Encryption.EncryptService(DateTime.Now.ToString());
						nameValueCollection["aid"] = Auth.Encryption.APIService(Auth.OnProgramStart.AID);
						nameValueCollection["session_id"] = Auth.Constants.IV;
						nameValueCollection["api_id"] = Auth.Constants.APIENCRYPTSALT;
						nameValueCollection["api_key"] = Auth.Constants.APIENCRYPTKEY;
						nameValueCollection["session_key"] = Auth.Constants.Key;
						nameValueCollection["secret"] = Auth.Encryption.APIService(Auth.OnProgramStart.Secret);
						nameValueCollection["type"] = Auth.Encryption.APIService("register");
						nameValueCollection["username"] = Auth.Encryption.APIService(AIO);
						nameValueCollection["password"] = Auth.Encryption.APIService(AIO);
						nameValueCollection["email"] = Auth.Encryption.APIService(AIO);
						nameValueCollection["license"] = Auth.Encryption.APIService(AIO);
						nameValueCollection["hwid"] = Auth.Encryption.APIService(Auth.Constants.HWID());
						string[] array = Auth.Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
						if (array[0] != Auth.Constants.Token)
						{
							Auth.Security.End();
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Security.MaliciousCheck(array[1]))
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Constants.Breached)
						{
							Process.GetCurrentProcess().Kill();
						}
						Auth.Security.End();
						string a = array[2];
						if (a == "success")
						{
							return true;
						}
						if (a == "error")
						{
							return false;
						}
					}
					catch (Exception)
					{
						Process.GetCurrentProcess().Kill();
					}
					result = false;
				}
				return result;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002F98 File Offset: 0x00001198
			public static bool Login(string username, string password)
			{
				if (!Auth.Constants.Initialized)
				{
					Process.GetCurrentProcess().Kill();
				}
				if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
				{
					Process.GetCurrentProcess().Kill();
				}
				string[] array = new string[0];
				bool result;
				using (WebClient webClient = new WebClient())
				{
					try
					{
						Auth.Security.Start();
						webClient.Proxy = null;
						Encoding @default = Encoding.Default;
						WebClient webClient2 = webClient;
						string apiUrl = Auth.Constants.ApiUrl;
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection["token"] = Auth.Encryption.EncryptService(Auth.Constants.Token);
						nameValueCollection["timestamp"] = Auth.Encryption.EncryptService(DateTime.Now.ToString());
						nameValueCollection["aid"] = Auth.Encryption.APIService(Auth.OnProgramStart.AID);
						nameValueCollection["session_id"] = Auth.Constants.IV;
						nameValueCollection["api_id"] = Auth.Constants.APIENCRYPTSALT;
						nameValueCollection["api_key"] = Auth.Constants.APIENCRYPTKEY;
						nameValueCollection["username"] = Auth.Encryption.APIService(username);
						nameValueCollection["password"] = Auth.Encryption.APIService(password);
						nameValueCollection["hwid"] = Auth.Encryption.APIService(Auth.Constants.HWID());
						nameValueCollection["session_key"] = Auth.Constants.Key;
						nameValueCollection["secret"] = Auth.Encryption.APIService(Auth.OnProgramStart.Secret);
						nameValueCollection["type"] = Auth.Encryption.APIService("login");
						array = Auth.Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
						if (array[0] != Auth.Constants.Token)
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Security.MaliciousCheck(array[1]))
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Constants.Breached)
						{
							Process.GetCurrentProcess().Kill();
						}
						string a = array[2];
						if (a == "success")
						{
							Auth.User.ID = array[3];
							Auth.User.Username = array[4];
							Auth.User.Password = array[5];
							Auth.User.Email = array[6];
							Auth.User.HWID = array[7];
							Auth.User.UserVariable = array[8];
							Auth.User.Rank = array[9];
							Auth.User.IP = array[10];
							Auth.User.Expiry = array[11];
							Auth.User.LastLogin = array[12];
							Auth.User.RegisterDate = array[13];
							string[] array2 = array[14].Split(new char[]
							{
								'~'
							});
							for (int i = 0; i < array2.Length; i++)
							{
								string[] array3 = array2[i].Split(new char[]
								{
									'^'
								});
								try
								{
									Auth.App.Variables.Add(array3[0], array3[1]);
								}
								catch
								{
								}
							}
							Auth.Security.End();
							return true;
						}
						if (a == "invalid_details")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "time_expired")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "hwid_updated")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "invalid_hwid")
						{
							Auth.Security.End();
							return false;
						}
					}
					catch (Exception)
					{
						Auth.Security.End();
						Process.GetCurrentProcess().Kill();
					}
					result = false;
				}
				return result;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x000032EC File Offset: 0x000014EC
			public static bool Register(string username, string password, string email, string license)
			{
				if (!Auth.Constants.Initialized)
				{
					Auth.Security.End();
					Process.GetCurrentProcess().Kill();
				}
				if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(license))
				{
					Process.GetCurrentProcess().Kill();
				}
				new string[0];
				bool result;
				using (WebClient webClient = new WebClient())
				{
					try
					{
						Auth.Security.Start();
						webClient.Proxy = null;
						Encoding @default = Encoding.Default;
						WebClient webClient2 = webClient;
						string apiUrl = Auth.Constants.ApiUrl;
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection["token"] = Auth.Encryption.EncryptService(Auth.Constants.Token);
						nameValueCollection["timestamp"] = Auth.Encryption.EncryptService(DateTime.Now.ToString());
						nameValueCollection["aid"] = Auth.Encryption.APIService(Auth.OnProgramStart.AID);
						nameValueCollection["session_id"] = Auth.Constants.IV;
						nameValueCollection["api_id"] = Auth.Constants.APIENCRYPTSALT;
						nameValueCollection["api_key"] = Auth.Constants.APIENCRYPTKEY;
						nameValueCollection["session_key"] = Auth.Constants.Key;
						nameValueCollection["secret"] = Auth.Encryption.APIService(Auth.OnProgramStart.Secret);
						nameValueCollection["type"] = Auth.Encryption.APIService("register");
						nameValueCollection["username"] = Auth.Encryption.APIService(username);
						nameValueCollection["password"] = Auth.Encryption.APIService(password);
						nameValueCollection["email"] = Auth.Encryption.APIService(email);
						nameValueCollection["license"] = Auth.Encryption.APIService(license);
						nameValueCollection["hwid"] = Auth.Encryption.APIService(Auth.Constants.HWID());
						string[] array = Auth.Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
						if (array[0] != Auth.Constants.Token)
						{
							Auth.Security.End();
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Security.MaliciousCheck(array[1]))
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Constants.Breached)
						{
							Process.GetCurrentProcess().Kill();
						}
						string a = array[2];
						if (a == "success")
						{
							Auth.Security.End();
							return true;
						}
						if (a == "invalid_license")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "email_used")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "invalid_username")
						{
							Auth.Security.End();
							return false;
						}
					}
					catch (Exception)
					{
						Process.GetCurrentProcess().Kill();
					}
					result = false;
				}
				return result;
			}

			// Token: 0x0600004C RID: 76 RVA: 0x0000357C File Offset: 0x0000177C
			public static bool ExtendSubscription(string username, string password, string license)
			{
				if (!Auth.Constants.Initialized)
				{
					Auth.Security.End();
					Process.GetCurrentProcess().Kill();
				}
				if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(license))
				{
					Process.GetCurrentProcess().Kill();
				}
				new string[0];
				bool result;
				using (WebClient webClient = new WebClient())
				{
					try
					{
						Auth.Security.Start();
						webClient.Proxy = null;
						Encoding @default = Encoding.Default;
						WebClient webClient2 = webClient;
						string apiUrl = Auth.Constants.ApiUrl;
						NameValueCollection nameValueCollection = new NameValueCollection();
						nameValueCollection["token"] = Auth.Encryption.EncryptService(Auth.Constants.Token);
						nameValueCollection["timestamp"] = Auth.Encryption.EncryptService(DateTime.Now.ToString());
						nameValueCollection["aid"] = Auth.Encryption.APIService(Auth.OnProgramStart.AID);
						nameValueCollection["session_id"] = Auth.Constants.IV;
						nameValueCollection["api_id"] = Auth.Constants.APIENCRYPTSALT;
						nameValueCollection["api_key"] = Auth.Constants.APIENCRYPTKEY;
						nameValueCollection["session_key"] = Auth.Constants.Key;
						nameValueCollection["secret"] = Auth.Encryption.APIService(Auth.OnProgramStart.Secret);
						nameValueCollection["type"] = Auth.Encryption.APIService("extend");
						nameValueCollection["username"] = Auth.Encryption.APIService(username);
						nameValueCollection["password"] = Auth.Encryption.APIService(password);
						nameValueCollection["license"] = Auth.Encryption.APIService(license);
						string[] array = Auth.Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
						if (array[0] != Auth.Constants.Token)
						{
							Auth.Security.End();
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Security.MaliciousCheck(array[1]))
						{
							Process.GetCurrentProcess().Kill();
						}
						if (Auth.Constants.Breached)
						{
							Process.GetCurrentProcess().Kill();
						}
						string a = array[2];
						if (a == "success")
						{
							Auth.Security.End();
							return true;
						}
						if (a == "invalid_token")
						{
							Auth.Security.End();
							return false;
						}
						if (a == "invalid_details")
						{
							Auth.Security.End();
							return false;
						}
					}
					catch (Exception)
					{
						Process.GetCurrentProcess().Kill();
					}
					result = false;
				}
				return result;
			}
		}

		// Token: 0x0200000C RID: 12
		internal class Security
		{
			// Token: 0x0600004E RID: 78 RVA: 0x000037C8 File Offset: 0x000019C8
			public static string Signature(string value)
			{
				string result;
				using (MD5 md = MD5.Create())
				{
					byte[] bytes = Encoding.UTF8.GetBytes(value);
					result = BitConverter.ToString(md.ComputeHash(bytes)).Replace("-", "");
				}
				return result;
			}

			// Token: 0x0600004F RID: 79 RVA: 0x00003820 File Offset: 0x00001A20
			private static string Session(int length)
			{
				Random random = new Random();
				return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz", length)
				select s[random.Next(s.Length)]).ToArray<char>());
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00003864 File Offset: 0x00001A64
			public static string Obfuscate(int length)
			{
				Random random = new Random();
				return new string((from s in Enumerable.Repeat<string>("gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=", length)
				select s[random.Next(s.Length)]).ToArray<char>());
			}

			// Token: 0x06000051 RID: 81 RVA: 0x000038A8 File Offset: 0x00001AA8
			public static void Start()
			{
				string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
				if (Auth.Constants.Started)
				{
					Process.GetCurrentProcess().Kill();
					return;
				}
				using (StreamReader streamReader = new StreamReader(pathRoot + "Windows\\System32\\drivers\\etc\\hosts"))
				{
					if (streamReader.ReadToEnd().Contains("api.auth.gg"))
					{
						Auth.Constants.Breached = true;
						Process.GetCurrentProcess().Kill();
					}
				}
				new Auth.InfoManager().StartListener();
				Auth.Constants.Token = Guid.NewGuid().ToString();
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Auth.Security.PinPublicKey));
				Auth.Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(Auth.Security.Session(32)));
				Auth.Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(Auth.Security.Session(16)));
				Auth.Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Auth.Constants.RandomString(16)));
				Auth.Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Auth.Constants.RandomString(32)));
				Auth.Constants.Started = true;
			}

			// Token: 0x06000052 RID: 82 RVA: 0x000039D8 File Offset: 0x00001BD8
			public static void End()
			{
				if (!Auth.Constants.Started)
				{
					Process.GetCurrentProcess().Kill();
					return;
				}
				Auth.Constants.Token = null;
				ServicePointManager.ServerCertificateValidationCallback = ((object <p0>, X509Certificate <p1>, X509Chain <p2>, SslPolicyErrors <p3>) => true);
				Auth.Constants.APIENCRYPTKEY = null;
				Auth.Constants.APIENCRYPTSALT = null;
				Auth.Constants.IV = null;
				Auth.Constants.Key = null;
				Auth.Constants.Started = false;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00003A3F File Offset: 0x00001C3F
			private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				return certificate != null && certificate.GetPublicKeyString() == "04D9F7C0C68DA3FDE380C3BBE2F87D09BB546B7DE5254DEAC4DC2DCA4A612A83585431E98B49A91A6D854D1128C133E92D5A6BFED12EF5043FF6AC5E77973135E6";
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00003A58 File Offset: 0x00001C58
			public static string Integrity(string filename)
			{
				string result;
				using (MD5 md = MD5.Create())
				{
					using (FileStream fileStream = File.OpenRead(filename))
					{
						result = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
					}
				}
				return result;
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00003AC8 File Offset: 0x00001CC8
			public static bool MaliciousCheck(string date)
			{
				DateTime d = DateTime.Parse(date);
				DateTime now = DateTime.Now;
				TimeSpan timeSpan = d - now;
				if (Convert.ToInt32(timeSpan.Seconds.ToString().Replace("-", "")) >= 5 || Convert.ToInt32(timeSpan.Minutes.ToString().Replace("-", "")) >= 1)
				{
					Auth.Constants.Breached = true;
					return true;
				}
				return false;
			}

			// Token: 0x0400002B RID: 43
			private const string _key = "04D9F7C0C68DA3FDE380C3BBE2F87D09BB546B7DE5254DEAC4DC2DCA4A612A83585431E98B49A91A6D854D1128C133E92D5A6BFED12EF5043FF6AC5E77973135E6";
		}

		// Token: 0x02000010 RID: 16
		internal class Encryption
		{
			// Token: 0x0600005E RID: 94 RVA: 0x00003B80 File Offset: 0x00001D80
			public static string APIService(string value)
			{
				string @string = Encoding.Default.GetString(Convert.FromBase64String(Auth.Constants.APIENCRYPTKEY));
				byte[] key = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(@string));
				byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Auth.Constants.APIENCRYPTSALT)));
				return Auth.Encryption.EncryptString(value, key, bytes);
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00003BE0 File Offset: 0x00001DE0
			public static string EncryptService(string value)
			{
				string @string = Encoding.Default.GetString(Convert.FromBase64String(Auth.Constants.APIENCRYPTKEY));
				byte[] key = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(@string));
				byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Auth.Constants.APIENCRYPTSALT)));
				string str = Auth.Encryption.EncryptString(value, key, bytes);
				int length = int.Parse(Auth.OnProgramStart.AID.Substring(0, 2));
				return str + Auth.Security.Obfuscate(length);
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00003C5C File Offset: 0x00001E5C
			public static string DecryptService(string value)
			{
				string @string = Encoding.Default.GetString(Convert.FromBase64String(Auth.Constants.APIENCRYPTKEY));
				byte[] key = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(@string));
				byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Auth.Constants.APIENCRYPTSALT)));
				return Auth.Encryption.DecryptString(value, key, bytes);
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00003CBC File Offset: 0x00001EBC
			public static string EncryptString(string plainText, byte[] key, byte[] iv)
			{
				Aes aes = Aes.Create();
				aes.Mode = CipherMode.CBC;
				aes.Key = key;
				aes.IV = iv;
				MemoryStream memoryStream = new MemoryStream();
				ICryptoTransform transform = aes.CreateEncryptor();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				byte[] bytes = Encoding.ASCII.GetBytes(plainText);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				byte[] array = memoryStream.ToArray();
				memoryStream.Close();
				cryptoStream.Close();
				return Convert.ToBase64String(array, 0, array.Length);
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00003D38 File Offset: 0x00001F38
			public static string DecryptString(string cipherText, byte[] key, byte[] iv)
			{
				Aes aes = Aes.Create();
				aes.Mode = CipherMode.CBC;
				aes.Key = key;
				aes.IV = iv;
				MemoryStream memoryStream = new MemoryStream();
				ICryptoTransform transform = aes.CreateDecryptor();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				string result = string.Empty;
				try
				{
					byte[] array = Convert.FromBase64String(cipherText);
					cryptoStream.Write(array, 0, array.Length);
					cryptoStream.FlushFinalBlock();
					byte[] array2 = memoryStream.ToArray();
					result = Encoding.ASCII.GetString(array2, 0, array2.Length);
				}
				finally
				{
					memoryStream.Close();
					cryptoStream.Close();
				}
				return result;
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00003DD4 File Offset: 0x00001FD4
			public static string Decode(string text)
			{
				text = text.Replace('_', '/').Replace('-', '+');
				int num = text.Length % 4;
				if (num != 2)
				{
					if (num == 3)
					{
						text += "=";
					}
				}
				else
				{
					text += "==";
				}
				return Encoding.UTF8.GetString(Convert.FromBase64String(text));
			}
		}

		// Token: 0x02000011 RID: 17
		private class InfoManager
		{
			// Token: 0x06000065 RID: 101 RVA: 0x00003E35 File Offset: 0x00002035
			public InfoManager()
			{
				this.lastGateway = this.GetGatewayMAC();
			}

			// Token: 0x06000066 RID: 102 RVA: 0x00003E49 File Offset: 0x00002049
			public void StartListener()
			{
				this.timer = new Timer(delegate(object _)
				{
					this.OnCallBack();
				}, null, 5000, -1);
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00003E6C File Offset: 0x0000206C
			private void OnCallBack()
			{
				this.timer.Dispose();
				if (!(this.GetGatewayMAC() == this.lastGateway))
				{
					Auth.Constants.Breached = true;
					Process.GetCurrentProcess().Kill();
				}
				else
				{
					this.lastGateway = this.GetGatewayMAC();
				}
				this.timer = new Timer(delegate(object _)
				{
					this.OnCallBack();
				}, null, 5000, -1);
			}

			// Token: 0x06000068 RID: 104 RVA: 0x00003ED4 File Offset: 0x000020D4
			public static IPAddress GetDefaultGateway()
			{
				return (from a in (from n in NetworkInterface.GetAllNetworkInterfaces()
				where n.OperationalStatus == OperationalStatus.Up
				where n.NetworkInterfaceType != NetworkInterfaceType.Loopback
				select n).SelectMany(delegate(NetworkInterface n)
				{
					IPInterfaceProperties ipproperties = n.GetIPProperties();
					if (ipproperties == null)
					{
						return null;
					}
					return ipproperties.GatewayAddresses;
				}).Select(delegate(GatewayIPAddressInformation g)
				{
					if (g == null)
					{
						return null;
					}
					return g.Address;
				})
				where a != null
				select a).FirstOrDefault<IPAddress>();
			}

			// Token: 0x06000069 RID: 105 RVA: 0x00003FA0 File Offset: 0x000021A0
			private string GetArpTable()
			{
				string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
				string result;
				using (Process process = Process.Start(new ProcessStartInfo
				{
					FileName = pathRoot + "Windows\\System32\\arp.exe",
					Arguments = "-a",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}))
				{
					using (StreamReader standardOutput = process.StandardOutput)
					{
						result = standardOutput.ReadToEnd();
					}
				}
				return result;
			}

			// Token: 0x0600006A RID: 106 RVA: 0x00004038 File Offset: 0x00002238
			private string GetGatewayMAC()
			{
				string arg = Auth.InfoManager.GetDefaultGateway().ToString();
				return new Regex(string.Format("({0} [\\W]*) ([a-z0-9-]*)", arg)).Match(this.GetArpTable()).Groups[2].ToString();
			}

			// Token: 0x04000030 RID: 48
			private Timer timer;

			// Token: 0x04000031 RID: 49
			private string lastGateway;
		}
	}
}
