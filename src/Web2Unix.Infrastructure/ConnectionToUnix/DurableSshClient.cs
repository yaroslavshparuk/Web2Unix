using Renci.SshNet;
using System.Text;

namespace Web2Unix.Infrastructure.ConnectionToUnix;

public class DurableSshClient : IDisposable
{
    private const string _promptEnd = "~$ ";
    private const string _startTag = "\x1b[?2004l";
    private const string _endTag = "\x1b[?2004h";
    private readonly SshClient _sshClient;
    private readonly List<string> _allOutput;
    private ShellStream _shellStream;
    private StreamReader _reader;

    public DurableSshClient(ConnectionInfo connectionInfo)
    {
        _sshClient = new SshClient(connectionInfo);
        _allOutput = new List<string>();
    }

    public string Output { get { return string.Join(Environment.NewLine, _allOutput); } }

    public async Task<string> Open()
    {
        _sshClient.Connect();
        _shellStream = _sshClient.CreateShellStream("xterm", 80, 24, 800, 600, 1024);
        _reader = new StreamReader(_shellStream, Encoding.UTF8);
        return await ReadOutput();
    }

    public async Task<string> Execute(string command)
    {
        _shellStream.WriteLine(command);
        _shellStream.Flush();
        return await ReadOutput();
    }

    public void Dispose()
    {
        _sshClient.Disconnect();
        _sshClient.Dispose();
        _shellStream.Dispose();
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
        }
        while (!output.EndsWith(_promptEnd) && output != string.Empty);
        var dirtyOutput = outputBuilder.ToString();
        var startIndex = dirtyOutput.IndexOf(_startTag) + _startTag.Length;
        var endIndex = dirtyOutput.IndexOf(_endTag, startIndex);

        var finalOutput = string.Empty;
        if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
        {
            finalOutput = dirtyOutput.Substring(startIndex, endIndex - startIndex);
        }
        else
        {
            finalOutput = dirtyOutput;
        }

        _allOutput.Add(finalOutput);
        return finalOutput;
    }
}