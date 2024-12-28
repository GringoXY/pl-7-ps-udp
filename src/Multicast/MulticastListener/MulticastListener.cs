using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MulticastListenerProgram;

internal sealed class MulticastListener(string ipAddress, int port)
{
    public string IpAddress => ipAddress;
    public int Port => port;

    public void Start()
    {
        try
        {
            // `using` automatically disposes (closes) connection
            using UdpClient udpClient = new()
            {
                // Based on docs: https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.exclusiveaddressuse?view=net-8.0
                // "Gets or sets a value that indicates whether the Socket allows only one process to bind to a port."
                ExclusiveAddressUse = false,
            };

            udpClient.Client.SetSocketOption(
                SocketOptionLevel.Socket,
                SocketOptionName.ReuseAddress,
                true);

            IPEndPoint localEndPoint = new(IPAddress.Any, Port);
            udpClient.Client.Bind(localEndPoint);

            IPAddress multicastIpAddress = IPAddress.Parse(ipAddress);
            udpClient.JoinMulticastGroup(multicastIpAddress);

            string receiveMessage = string.Empty;
            do
            {
                byte[] receiveBytes = udpClient.Receive(ref localEndPoint);
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
