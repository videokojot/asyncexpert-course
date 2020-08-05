using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskCompletionSourceExercises.Core
{
    public class AsyncTools
    {
        public static Task<string> RunProgramAsync(string path, string args = "")
        {
            var tcs = new TaskCompletionSource<string>();

            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(path, args)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.Exited += (sender, eventArgs) =>
            {
                var senderProcess = sender as Process;

                try
                {
                    var exitCode = senderProcess?.ExitCode;


                    var stdErr = senderProcess?.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(stdErr))
                    {
                        tcs.SetException(new Exception(stdErr));
                    }
                    else
                    {
                        var stdOut = senderProcess?.StandardOutput.ReadToEnd();

                        tcs.SetResult(stdOut);
                    }
                }
                finally
                {
                    senderProcess?.Dispose();
                }
            };

            process.Start();

            return tcs.Task;
        }
    }
}