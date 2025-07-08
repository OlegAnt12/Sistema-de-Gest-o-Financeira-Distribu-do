using System;
using System.IO;
using System.Net.Sockets;

class Client
{
    static void Main()
    {
        string serverIp = "127.0.0.1";
        int port = 5000;

        try
        {
            TcpClient client = new TcpClient(serverIp, port);
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream);
            using StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

            Console.WriteLine(reader.ReadLine()); // Mensagem de boas-vindas

            string? input;
            while (true)
            {
                Console.Write("Comando > ");
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) continue;

                writer.WriteLine(input);
                string response = reader.ReadLine();
                Console.WriteLine($"[Servidor]: {response}");

                if (input.ToUpper() == "SAIR")
                    break;
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Erro] {ex.Message}");
        }
    }
}
