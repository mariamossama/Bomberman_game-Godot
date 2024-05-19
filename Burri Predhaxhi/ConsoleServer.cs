using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ConsoleServer
{
	private TcpListener server;
	private bool isRunning;

	public void StartServer(int port)
	{
		server = new TcpListener(IPAddress.Any, port);
		server.Start();
		isRunning = true;
		Console.WriteLine("Server started on port " + port);

		Thread serverThread = new Thread(Run);
		serverThread.Start();
	}

	private void Run()
	{
		while (isRunning)
		{
			try
			{
				TcpClient client = server.AcceptTcpClient();
				Console.WriteLine("Client connected");

				Thread clientThread = new Thread(HandleClient);
				clientThread.Start(client);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
		}
	}

	private void HandleClient(object obj)
	{
		TcpClient client = (TcpClient)obj;
		NetworkStream stream = client.GetStream();

		byte[] buffer = new byte[1024];
		int bytesRead;

		while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
		{
			string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
			Console.WriteLine("Received: " + message);

			byte[] response = Encoding.ASCII.GetBytes("Echo: " + message);
			stream.Write(response, 0, response.Length);
		}

		client.Close();
	}

	public void StopServer()
	{
		isRunning = false;
		server.Stop();
	}
}
