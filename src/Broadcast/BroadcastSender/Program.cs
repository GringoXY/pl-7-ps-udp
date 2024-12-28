using Shared;
using BroadcastSenderProgram;
using System.Net;

int port = Configs.DefaultBroadcastPort;
string ipAddress = Configs.DefaultBroadcastIpAddress;

Console.ForegroundColor = ConsoleColor.Gray;
Console.Write("Podaj adres IP rozgłoszeniowy: ");

if (IPAddress.TryParse(Console.ReadLine(), out IPAddress? parsedIpAddress) == false)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Error.WriteLine("Podany adres IP rozgłoszeniowy jest niepoprawny");
    return;
}

ipAddress = parsedIpAddress.ToString();

Console.Write($"Podaj port (domyślnie {port}): ");
if (int.TryParse(Console.ReadLine(), out int parsedPort))
{
    port = parsedPort;
}

new BroadcastSender(ipAddress, port).Start();
