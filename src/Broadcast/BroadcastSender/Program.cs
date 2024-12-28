using Shared;
using BroadcastSenderProgram;
using System.Net;

int port = Configs.DefaultBroadcastPort;
string ipAddress = Configs.DefaultBroadcastIpAddress;

Console.ForegroundColor = ConsoleColor.Gray;
Console.Write($"Podaj adres IP rozgłoszeniowy (domyślnie {ipAddress}): ");

if (IPAddress.TryParse(Console.ReadLine(), out IPAddress? parsedIpAddress))
{
    ipAddress = parsedIpAddress.ToString();
}

Console.Write($"Podaj port (domyślnie {port}): ");
if (int.TryParse(Console.ReadLine(), out int parsedPort))
{
    port = parsedPort;
}

new BroadcastSender(ipAddress, port).Start();
