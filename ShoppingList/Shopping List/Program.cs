using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Shopping_List
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args)
				.UseUrls("https://0.0.0.0:5001")
				//.UseUrls("http://localhost:5001")
				.Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
		    WebHost.CreateDefaultBuilder(args)
			   .UseStartup<Startup>();
	}
}
