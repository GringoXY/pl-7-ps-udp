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
        try
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
            } while (receiveMessage != string.Empty);
        }
        catch (ObjectDisposedException ode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Błąd serwera: {ode.Message}");
            if (ode.InnerException is not null)
            {
                Console.WriteLine($"Wyjątek wewnętrzny: {ode.InnerException?.Message}");
            }
        }
        catch (SocketException se)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Błąd gniazda: {se.Message}");
            if (se.InnerException is not null)
            {
                Console.WriteLine($"Wyjątek wewnętrzny: {se.InnerException?.Message}");
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Błąd: {e.Message}");
            if (e.InnerException is not null)
            {
                Console.WriteLine($"Wyjątek wewnętrzny: {e.InnerException?.Message}");
            }
        }
    }
}
