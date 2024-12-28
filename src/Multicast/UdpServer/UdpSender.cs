using System.Net;
using System.Net.Sockets;

namespace UdpServerProgram;

// References:
// https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.joinmulticastgroup?view=net-8.0
internal sealed class UdpSender(int port)
{
    public int Port => port;

    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Uruchamiam serwer...");

        try
        {
            IPEndPoint ipEndPoint = new(IPAddress.Any, Port);

            // Connecting to the server.
            // "using" automatically disposes the client.
            // We don't have to worry about properly closing the client.
            // Underneath `Close()` method uses `Dispose()`.
            using Socket serverSocket = new(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);

            serverSocket.Bind(ipEndPoint);

            serverSocket.MulticastLoopback = true;
            Console.WriteLine("Uruchomiono serwer");

        }
        catch(ObjectDisposedException ode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Błąd serwera: {ode.Message}");
            if (ode.InnerException is not null)
            {
                Console.WriteLine($"Wyjątek wewnętrzny: {ode.InnerException?.Message}");
            }
        }
        catch(SocketException se)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Błąd gniazda serwera: {se.Message}");
            if (se.InnerException is not null)
            {
                Console.WriteLine($"Wyjątek wewnętrzny: {se.InnerException?.Message}");
            }
        }
        catch(Exception e)
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
