using System;
using System.Diagnostics;
using Foundation;
using Security;

namespace shawn.iOS
{
	public class KeyValueManager
	{
		public KeyValueManager()
		{
		}

		public void SaveRecord()
		{

			var record = new SecRecord(SecKind.GenericPassword)
			{
				Label = "交易密碼",
				Description = "用於xxx服務",
				Account = "liddle.fang@gmail.com",
				Service = "Transcation",
				Comment = "Demo",
				ValueData = NSData.FromString("P@ssw0rd"),
				Generic = NSData.FromString("SecurityChainDemo")
			};

			var status = SecKeyChain.Add(record);

			if (SecStatusCode.Success == status)
			{
				Debug.WriteLine("Keychain Saved!");
			}
			else if (SecStatusCode.DuplicateItem == status || SecStatusCode.DuplicateKeyChain == status)
			{
				Debug.WriteLine("Duplicate !");
				SecKeyChain.Remove(record);
			}
			else {
				Debug.WriteLine($"{ status }");
			}

		}

		public string QueryRecord()
		{

			SecStatusCode status;

			var rec = new SecRecord(SecKind.GenericPassword)
			{
				Generic = NSData.FromString("SecurityChainDemo")
			};

			var match = SecKeyChain.QueryAsRecord(rec, out status);

			if (SecStatusCode.Success == status && null != match)
			{

				Debug.WriteLine($"{match.Account};{match.ValueData.ToString()}");

				return match.Account;
			}

			Debug.WriteLine("Nothing found.");
			return string.Empty;

		}
	}
}

