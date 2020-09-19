using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Test_WebService_Project
{
	class Program
	{
		static void Main(string[] args)
		{
			//TODO: In Operation Mode Delete the bellow line. This is for Bypassing invalid ssl certification
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

			ServicePointManager.ServerCertificateValidationCallback += (mender, certificate, chain, sslPolicyErrors) => true;

			string startupPath = System.IO.Directory.GetCurrentDirectory().Replace("bin\\Debug", "") + "cert.p12";

			using (var client = new Behdad2_85.AccountServiceClient())
			{
				client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(startupPath, "changeit", X509KeyStorageFlags.MachineKeySet);

				var result3 = client.getAccountNumbers(
					new Behdad2_85.credential()
					{
						username = "4001000606374719",
						password = "Aa123456@@"
					}
					);

				Console.WriteLine();
				Console.WriteLine($"||||||||||||||||||||||||||||||||||||||||||||||  getAccountNumbers  {Environment.NewLine}");
				foreach (var item in result3)
				{
					Console.WriteLine($"Result : {item.accountNumber}");
				}

				Console.WriteLine();

				

				Console.WriteLine($"||||||||||||||||||||||||||||||||||||||||||||||  getBankTransactionsDetails  {Environment.NewLine}");

				////////////////////////////////////////////////   getBankTransactionsDetails
				Behdad2_85.pagedData myPageData = client.getBankTransactionsDetails(
					new Behdad2_85.credential()
					{
						username = "4001000606374719",
						password = "Aa123456@@"
					},
					new Behdad2_85.accountTransactionFilter()
					{
						accountNumber = "4001000606374719",
						fromDateTime = "139603310800",
						toDateTime = "139903310922",
						//paymentIdentifier = "111111111111111111111111111111",
					},
					new Behdad2_85.paging()
					{
						pageNumber = 1,
						recordCount = 1,
						pageNumberSpecified = true,
						recordCountSpecified = true
					}
					);

				var currentPageData = myPageData.currentPageData;

				long totalCount = myPageData.totalCount;

				for (int i = 0; i < currentPageData.Length; i++)
				{
					string acResult = string.Empty;


					XmlNode[] xmlNode = (XmlNode[])(currentPageData[i]);

					Console.WriteLine($"  -- No : {i + 1} of {totalCount}{Environment.NewLine}");

					for (int j = 2; j < xmlNode.Length; j++)
					{
						string titleName = xmlNode[j].Name;
						string value = (xmlNode[j]).InnerText;

						acResult += $"{titleName}: ".PadRight(32) + $"{value}{Environment.NewLine}";
					}
					Console.WriteLine($"{acResult}");
					Console.WriteLine($"========================================================{Environment.NewLine}");

				}



				Console.WriteLine($"||||||||||||||||||||||||||||||||||||||||||||||  getMultipleAccountTransactionsDetails  {Environment.NewLine}");



				////////////////////////////////////////////////   getMultipleAccountTransactionsDetails

				string[] accNumbers = { "4001000606374719" };

				Behdad2_85.pagedData myPageData2 = client.getMultipleAccountTransactionsDetails(
					new Behdad2_85.credential()
					{
						username = "4001000606374719",
						password = "Aa123456@@"
					},
					new Behdad2_85.multipleAccountTransactionFilter()
					{
						accountNumbers = accNumbers,
						fromDateTime = "139603310800",
						toDateTime = "139903310922",
							//paymentIdentifier = "111111111111111111111111111111",
						},
					new Behdad2_85.paging()
					{
						pageNumber = 1,
						recordCount = 1,
						pageNumberSpecified = true,
						recordCountSpecified = true
					}
					);

				var currentPageData2 = myPageData2.currentPageData;

				long totalCount2 = myPageData2.totalCount;

				for (int j = 0; j < currentPageData.Length; j++)
				{
					string acResult2 = string.Empty;


					XmlNode[] xmlNode2 = (XmlNode[])(currentPageData[j]);

					Console.WriteLine($"  -- No : {j + 1} of {totalCount}{Environment.NewLine}");

					for (int k = 2; k < xmlNode2.Length; k++)
					{
						string titleName = xmlNode2[k].Name;
						string value = (xmlNode2[k]).InnerText;

						acResult2 += $"{titleName}: ".PadRight(32) + $"{value}{Environment.NewLine}";
					}
					Console.WriteLine($"{acResult2}");
					Console.WriteLine($"========================================================{Environment.NewLine}");
				}

				



				Console.Write("Press Enter to Exit..");
				Console.ReadLine();
			}
		}
	}
}