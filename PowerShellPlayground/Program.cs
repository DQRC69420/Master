using System.Diagnostics;

namespace PowerShellPlayground
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Task.Run(() => Process.Start(Path.Combine("Scripts", "PowerShell", "UdkuCaller.ps1")));
		}
	}
}
