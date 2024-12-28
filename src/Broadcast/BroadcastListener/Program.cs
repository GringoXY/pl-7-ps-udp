using BroadcastListenerProgram;
using Shared;

int port = Configs.DefaultBroadcastPort;

Console.ForegroundColor = ConsoleColor.Gray;
Console.Write($"Podaj port (domyślnie {port}): ");

if (int.TryParse(Console.ReadLine(), out int parsedPort))
{
    port = parsedPort;
}

new BroadcastListener(port).Start();
