using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TerraSharp
{
    public static class CommandShell
    {

        ///<summary>
        ///Run a given command in a given directory, if directory is blank then it will be ran in the current directory
        ///</summary>
        ///<returns>The output of the command run</returns>
        public static string RunCommand(string command)
        {
            string shell = GetShell();

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = shell,
                    Arguments = command,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };

            var stringBuilder = new StringBuilder();
            process.Start();

            while(!process.StandardOutput.EndOfStream) 
            {
                stringBuilder.AppendLine(process.StandardOutput.ReadLine());
            }

            return stringBuilder.ToString();
        }

        private static string GetShell()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "cmd.exe";
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "/bin/bash";
            }
            else 
            {
                throw new System.Exception("Unsupported OS type");
            }
        }
    }
}