using System.Net.Sockets;
using System.Net;

IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
int port = 8080;

TcpListener listener = new(ipAddress, port);

try
{
    listener.Start();
    Console.WriteLine("The server is running. Waiting for connections...");

    var tcpClient = listener.AcceptTcpClient();
    Console.WriteLine($"Incoming connection: {tcpClient.Client.RemoteEndPoint}");
}

finally
{
    listener.Stop();
}