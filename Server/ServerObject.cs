using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class ServerObject
    {
        private TcpListener tcpListener = new TcpListener(IPAddress.Any, 8888);
        private List<ClientObject> clients = new List<ClientObject>();
        private List<string> messages = new List<string>();

        private readonly object lockObj = new object();

        protected internal async Task ListenAsync()
        {
            try
            {
                tcpListener.Start();
                Console.WriteLine("Сервер запущено. Очікування підключень...");

                while (true)
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    ClientObject clientObject = new ClientObject(tcpClient, this);

                    lock (lockObj)
                    {
                        clients.Add(clientObject);
                    }

                    _ = Task.Run(clientObject.ProcessAsync);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка сервера: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }

        protected internal async Task BroadcastMessageAsync(string message, bool isSystem = false)
        {
            List<ClientObject> localClients;
            lock (lockObj)
            {
                if (!isSystem) messages.Add(message);
                localClients = clients.ToList(); // копія для безпечного перебору
            }

            Console.WriteLine(message);

            foreach (var client in localClients)
            {
                try
                {
                    await client.Writer.WriteLineAsync("MESSAGE|" + message);
                    await client.Writer.FlushAsync();
                }
                catch
                {
                    RemoveConnection(client.Id);
                }
            }
        }

        protected internal async Task SendUsersListAsync()
        {
            List<ClientObject> localClients;
            string users;

            lock (lockObj)
            {
                localClients = clients.ToList();
                users = string.Join(",", clients
                    .Where(c => !string.IsNullOrEmpty(c.UserName))
                    .Select(c => c.UserName));
            }

            foreach (var client in localClients)
            {
                try
                {
                    await client.Writer.WriteLineAsync("USERS|" + users);
                    await client.Writer.FlushAsync();
                }
                catch
                {
                    RemoveConnection(client.Id);
                }
            }
        }

        protected internal async Task SendHistoryAsync(ClientObject client)
        {
            List<string> historyCopy;
            lock (lockObj)
            {
                historyCopy = messages.ToList();
            }

            try
            {
                foreach (string message in historyCopy)
                {
                    await client.Writer.WriteLineAsync("MESSAGE|" + message);
                }
                await client.Writer.FlushAsync();
            }
            catch { }
        }

        protected internal void RemoveConnection(string id)
        {
            ClientObject? client = null;
            lock (lockObj)
            {
                client = clients.FirstOrDefault(c => c.Id == id);
                if (client != null)
                {
                    clients.Remove(client);
                }
            }

            if (client != null)
            {
                client.Close();
                _ = SendUsersListAsync();
            }
        }

        protected internal void Disconnect()
        {
            lock (lockObj)
            {
                foreach (var client in clients)
                {
                    client.Close();
                }
                clients.Clear();
            }
            tcpListener.Stop();
            Console.WriteLine("Сервер зупинено.");
        }
    }
}