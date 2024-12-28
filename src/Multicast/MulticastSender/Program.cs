using MulticastSenderProgram;
using Shared;
using System.Net;

int port = Configs.DefaultMulticastPort;
string ipAddress = Configs.DefaultMulticastIpAddress;

Console.ForegroundColor = ConsoleColor.Gray;
Console.Write($"Podaj grupowy adres IP (domyślnie {ipAddress}): ");

if (IPAddress.TryParse(Console.ReadLine(), out IPAddress? parsedIpAddress))
{
    ipAddress = parsedIpAddress.ToString();
}

Console.Write($"Podaj port (domyślnie {port}): ");
if (int.TryParse(Console.ReadLine(), out int parsedPort))
{
    port = parsedPort;
}

new MulticastSender(ipAddress, port).Start();
