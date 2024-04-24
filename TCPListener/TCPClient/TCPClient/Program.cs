using System.Net.Sockets;

string serverIP = "127.0.0.1";
int serverPort = 8080;

TcpClient tcpClient = new();
Console.WriteLine("The client is running");
await tcpClient.ConnectAsync(serverIP, serverPort);

if (tcpClient.Connected)
{
    Console.WriteLine($"Connection to {tcpClient.Client.RemoteEndPoint} is established");
}

else
{
    Console.WriteLine("Failed to connect");
}