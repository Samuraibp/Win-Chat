using System.Net.Sockets;

namespace Server
{
    internal class ClientObject
    {
        protected internal string Id { get; } = Guid.NewGuid().ToString();
        protected internal string UserName { get; private set; } = "";
        protected internal StreamWriter Writer { get; }
        protected internal StreamReader Reader { get; }

        private TcpClient client;
        private ServerObject server;

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            client = tcpClient;
            server = serverObject;
            var stream = client.GetStream();
            Reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            Writer = new StreamWriter(stream, System.Text.Encoding.UTF8) { AutoFlush = true };
        }

        public async Task ProcessAsync()
        {
            try
            {
                // Перше повідомлення від клієнта — його ім'я
                string? userName = await Reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(userName))
                {
                    server.RemoveConnection(Id);
                    return;
                }

                UserName = userName;
                string message = $"{UserName} увійшов до чату";

                await server.BroadcastMessageAsync(message, true);
                await server.SendHistoryAsync(this);
                await server.SendUsersListAsync();

                // Основний цикл читання повідомлень
                while (true)
                {
                    string? input = await Reader.ReadLineAsync();
                    if (input == null || input == "EXIT") break;

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        // Сервер берет чистый текст (input), добавляет имя автора и рассылает всем
                        message = $"{UserName}: {input}";
                        await server.BroadcastMessageAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка клієнта {UserName}: {ex.Message}");
            }
            finally
            {
                if (!string.IsNullOrEmpty(UserName))
                {
                    string message = $"{UserName} покинув чат";
                    await server.BroadcastMessageAsync(message);
                }
                server.RemoveConnection(Id);
            }
        }

        protected internal void Close()
        {
            try { Writer.Close(); } catch { }
            try { Reader.Close(); } catch { }
            try { client.Close(); } catch { }
        }
    }
}
