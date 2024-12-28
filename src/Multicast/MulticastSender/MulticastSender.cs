using Shared;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulticastSenderProgram;

// https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.joinmulticastgroup?view=net-8.0
internal sealed class MulticastSender(string ipAddress, int port)
{
    public string IpAddress => ipAddress;
    public int Port => port;
    public void Start()
    {
        try
        {
            IPAddress multicastIpAddress = IPAddress.Parse(IpAddress);
            IPEndPoint remoteIpEndPoint = new(multicastIpAddress, Port);

            // `using` automatically disposes (closes) connection
            using UdpClient udpClient = new();
            udpClient.JoinMulticastGroup(multicastIpAddress);

            Console.Write($"Wyślij wiadomość (\"{Configs.CloseCommand}\" zamyka serwer): ");
            string message = string.Empty;
            while ((message = Console.ReadLine() ?? string.Empty).Length > 0 && message.Equals(Configs.CloseCommand, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                byte[] dgram = Encoding.ASCII.GetBytes(message);
                udpClient.Send(dgram, dgram.Length, remoteIpEndPoint);
                Console.Write("Wyślij wiadomość: ");
            }
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
