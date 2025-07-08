using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

class Server
{
    static void Main()
    {
        int port = 5000;
        TcpListener server = new TcpListener(IPAddress.Any, port);

        server.Start();
        Console.WriteLine($"[Servidor] A escutar na porta {port}...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("[Servidor] Cliente conectado.");

            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    static void HandleClient(TcpClient client)
    {
        using NetworkStream stream = client.GetStream();
        using StreamReader reader = new StreamReader(stream);
        using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

        try
        {
            writer.WriteLine("Bem-vindo ao SGFD! Digite 'SAIR' para terminar.");

            string? msg;
            while ((msg = reader.ReadLine()) != null)
            {
                Console.WriteLine($"[Cliente]: {msg}");

                if (msg.ToUpper() == "SAIR")
                {
                    writer.WriteLine("Sessão encerrada. Até logo!");
                    break;
                }

                writer.WriteLine($"Comando recebido: {msg}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Erro] {ex.Message}");
        }
        finally
        {
            client.Close();
            Console.WriteLine("[Servidor] Cliente desconectado.");
        }
    }
}
