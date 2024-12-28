using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BroadcastSenderProgram;

// https://learn.microsoft.com/en-us/answers/questions/562659/(solved)-how-can-send-a-udp-broadcast-to-any-ip-ad
internal sealed class BroadcastSender(string ipAddress, int port)
{
    public string IpAddress => ipAddress;
    public int Port => port;

    public void Start()
    {
        string message = "Hello! Broadcast message!";
        byte[] dgram = Encoding.ASCII.GetBytes("Hello! Broadcast message!");

        Console.WriteLine($"Wysyłanie wiadomości: \"{message}\"");

        IPEndPoint remoteIpEndPoint = new(IPAddress.Parse(ipAddress), Port);

        // `using` automatically disposes (closes) connection
        using Socket serverSocket = new(
            AddressFamily.InterNetwork,
            SocketType.Dgram,
            ProtocolType.Udp)
        {
            EnableBroadcast = true,
        };

        serverSocket.SendTo(dgram, remoteIpEndPoint);

        Console.WriteLine("Wiadomość wysłana...");
    }
}
