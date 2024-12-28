using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BroadcastListenerProgram;

// https://learn.microsoft.com/en-us/answers/questions/562659/(solved)-how-can-send-a-udp-broadcast-to-any-ip-ad
internal sealed class BroadcastListener(int port)
{
    public int Port => port;

    public void Start()
    {
        // `using` automatically disposes (closes) connection
        using UdpClient udpClient = new();

        udpClient.Client.SetSocketOption(
            SocketOptionLevel.Socket,
            SocketOptionName.ReuseAddress,
            true);

        IPEndPoint remoteIpEndPoint = new(IPAddress.Any, Port);
        udpClient.Client.Bind(remoteIpEndPoint);

        string receiveMessage = string.Empty;
        do
        {
            byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
            receiveMessage = Encoding.ASCII.GetString(receiveBytes);
            Console.WriteLine(receiveMessage);
        } while(receiveMessage != string.Empty);
    }
}
