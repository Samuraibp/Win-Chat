namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;


            ServerObject server = new ServerObject();
            await server.ListenAsync();
        }
    }
}
