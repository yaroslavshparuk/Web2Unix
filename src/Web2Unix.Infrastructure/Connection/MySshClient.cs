using Renci.SshNet;
using System.Text;

namespace Web2Unix.Infrastructure.Connection;

public class MySshClient : IDisposable
{
    private readonly SshClient sshClient;
    private ShellStream shellStream;
    //private StreamReader reader;

    public MySshClient(string host, string username, string password)
    {
        sshClient = new SshClient(host, username, password);
    }

    public async Task<string> Connect()
    {
        sshClient.Connect();
        shellStream = sshClient.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
        var reader = new StreamReader(shellStream, Encoding.UTF8);

        var promptEnd = "~$ "; // Customize to your needs
        var outputBuilder = new StringBuilder();
        var output = string.Empty;
        while (!output.EndsWith(promptEnd))
        {
            await Task.Delay(200);
            output = await reader.ReadToEndAsync();
            outputBuilder.Append(output);
        }
        return outputBuilder.ToString();
    }

    public async Task<string> ExecuteCommand(string command)
    {
        shellStream.WriteLine(command);
        shellStream.Flush();
        var reader = new StreamReader(shellStream, Encoding.UTF8);

        var promptEnd = "~$ "; // Customize to your needs
        var outputBuilder = new StringBuilder();
        var output = string.Empty;
        do
        {
            await Task.Delay(200);
            output = await reader.ReadToEndAsync();
            outputBuilder.Append(output);
        } while (!output.EndsWith(promptEnd) && output != string.Empty);

        var dirtyOutput = outputBuilder.ToString();
        string startTag = "\x1b[?2004l";
        string endTag = "\x1b[?2004h";

        int startIndex = dirtyOutput.IndexOf(startTag) + startTag.Length;
        int endIndex = dirtyOutput.IndexOf(endTag, startIndex);

        if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
        {
            return dirtyOutput.Substring(startIndex, endIndex - startIndex);
        }

        return dirtyOutput;
    }


    public void Dispose()
    {
        sshClient.Disconnect();
        sshClient.Dispose();
        shellStream.Dispose();
    }
}
