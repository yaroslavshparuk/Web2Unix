using Renci.SshNet;
using System.Text;

namespace Web2Unix.Infrastructure.ConnectionToUnix;

public class SshClientConnection : IDisposable
{
    private const string _promptEnd = "~$ ";
    private const string _startTag = "\x1b[?2004l";
    private const string _endTag = "\x1b[?2004h";
    private readonly SshClient sshClient;
    private ShellStream shellStream;
    private StreamReader _reader;

    public SshClientConnection(ConnectionInfo connectionInfo)
    {
        sshClient = new SshClient(connectionInfo);
    }

    public async Task<string> Open()
    {
        sshClient.Connect();
        shellStream = sshClient.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
        _reader = new StreamReader(shellStream, Encoding.UTF8);
        return await ReadOutput();
    }

    public async Task<string> Execute(string command)
    {
        shellStream.WriteLine(command);
        shellStream.Flush();

        var dirtyOutput = await ReadOutput();

        var startIndex = dirtyOutput.IndexOf(_startTag) + _startTag.Length;
        var endIndex = dirtyOutput.IndexOf(_endTag, startIndex);

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

    private async Task<string> ReadOutput()
    {
        var outputBuilder = new StringBuilder();
        var output = string.Empty;
        do
        {
            await Task.Delay(200);
            output = await _reader.ReadToEndAsync();
            outputBuilder.Append(output);
        } while (!output.EndsWith(_promptEnd) && output != string.Empty);
        return outputBuilder.ToString();
    }
}