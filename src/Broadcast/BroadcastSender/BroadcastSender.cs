using Shared;
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
        IPEndPoint remoteIpEndPoint = new(IPAddress.Parse(IpAddress), Port);
        try
        {
            // `using` automatically disposes (closes) connection
            using Socket serverSocket = new(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);

            Console.Write($"Wyślij wiadomość (\"{Configs.CloseCommand}\" zamyka serwer): ");
            string message = string.Empty;
            while ((message = Console.ReadLine() ?? string.Empty).Length > 0 && message.ToLower() != Configs.CloseCommand   )
            {
                byte[] dgram = Encoding.ASCII.GetBytes(message);
                serverSocket.SendTo(dgram, remoteIpEndPoint);
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
