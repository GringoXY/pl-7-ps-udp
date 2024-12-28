# pl-7-ps-udp
Project of the multicast &amp; broadcast UDP server-client for the faculty "PS" (Network Programming) in Lodz University of Technology

# Docs
Refer to [PS - Lab 07 - Rozsylanie UDP.pdf](./docs/PS%20-%20Lab%2007%20-%20Rozsylanie%20UDP.pdf)

## Requirements
Install [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Run apps via CLI
Make sure you conform [Requirements](#requirements) and your CLI points to the root of the repository.

### UDP Broadcast
#### Run server
```terminal
dotnet run --project src\Broadcast\BroadcastSender
```

#### Run client
```terminal
dotnet run --project src\Broadcast\BroadcastListener
```