using System.Diagnostics;
using RERLogger.Web.Azure;
using Xunit;

namespace RERLogger.Test
{

    public class StorageEmulatorFixture 
    {
        public void StartEmulator(string argument)
        {
            var start = new ProcessStartInfo
            {
                Arguments = argument,
                FileName = @"c:\Program Files (x86)\Microsoft SDKs\Windows Azure\Storage Emulator\WAStorageEmulator.exe"
            };
            var exitCode = ExecuteProcess(start);
        }

        private static int ExecuteProcess(ProcessStartInfo start)
        {
            int exitCode;
            using (var proc = new Process { StartInfo = start })
            {
                proc.Start();
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }
            return exitCode;
        }

        
    }

    public class LoggingTest  :IUseFixture<StorageEmulatorFixture>
    {
        [Fact]
        public void WriteVerbose()
        {
            var logger = new AzureTableStorageLogger();
            logger.LogVerbose("hello");
        }

        public void SetFixture(StorageEmulatorFixture data)
        {
            data.StartEmulator("start -inprocess");
        }
    }
}