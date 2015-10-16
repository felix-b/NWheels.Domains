using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NWheels.Utilities;

namespace NWheels.Domains.ECommerce.Tests.Stacks
{
    public static class StackTestUtility
    {
        public static void ConfigureStack(StackName stackName, out string bootConfigFilePath)
        {
            var binFolder = PathUtility.ModuleBinPath(typeof(StackTestUtility).Assembly);
            var stackFolder = PathUtility.ModuleBinPath(typeof(StackTestUtility).Assembly, "Stacks\\" + stackName);

            bootConfigFilePath = Path.Combine(binFolder, "boot.config");

            File.Copy(
                sourceFileName: Path.Combine(stackFolder, "boot.config"),
                destFileName: bootConfigFilePath,
                overwrite: true);

            File.Copy(
                sourceFileName: Path.Combine(stackFolder, "stack.config"),
                destFileName: Path.Combine(binFolder, "stack.config"),
                overwrite: true);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static void RunManualTest(StackName stackName, string runExecutable = null, string browseUrl = null)
        {
            if ( runExecutable != null )
            {
                string bootConfigFilePath;
                ConfigureStack(stackName, out bootConfigFilePath);
                Process.Start(runExecutable, bootConfigFilePath);
            }

            if ( browseUrl != null )
            {
                WaitUntilUrlIsListenedTo(browseUrl, timeout: TimeSpan.FromSeconds(30));
                Process.Start(browseUrl);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static string ConsoleHostExeFilePath
        {
            get
            {
                return PathUtility.GetAbsolutePath(@"..\..\..\..\..\NWheels\Source\Bin\Core\nwc.exe", relativeTo: PathUtility.HostBinPath());
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static string TestBoardExeFilePath
        {
            get
            {
                return PathUtility.GetAbsolutePath(@"..\..\..\..\..\NWheels\Source\Bin\Tools\ntest.exe", relativeTo: PathUtility.HostBinPath());
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        private static void WaitUntilUrlIsListenedTo(string browseUrl, TimeSpan timeout)
        {
            var watch = Stopwatch.StartNew();

            while ( watch.Elapsed < timeout )
            {
                try
                {
                    using ( var client = new WebClient() )
                    {
                        client.DownloadString(browseUrl);
                    }

                    return;
                }
                catch ( Exception e )
                {
                    if ( !(e is SocketException || e.InnerException is SocketException) )
                    {
                        throw;
                    }
                }

                Thread.Sleep(500);
            }

            throw new TimeoutException("Could not browse to URL '" + browseUrl + "'. Timed out waiting for server to activate.");
        }
    }
}
