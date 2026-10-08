using System.Net.Sockets;

namespace WinChat
{
    public partial class Form1 : Form
    {
        private TcpClient? client;
        private StreamReader? reader;
        private StreamWriter? writer;
        private bool isDisconnected = false;

        public Form1()
        {
            InitializeComponent();
            buttonSend.Enabled = false;
            textBoxMessage.Enabled = false;
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Введіть ваше ім'я!");
                return;
            }

            try
            {
                client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 8888);

                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                writer = new StreamWriter(stream, System.Text.Encoding.UTF8) { AutoFlush = true };

                // Надсилаємо нікнейм відразу після підключення
                await writer.WriteLineAsync(textBoxName.Text);
                

                buttonSend.Enabled = true;
                textBoxMessage.Enabled = true;
                buttonConnect.Enabled = false;
                textBoxName.Enabled = false;
                isDisconnected = false;

                // Запускаємо фонове читання повідомлень
                _ = ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення: " + ex.Message);
            }
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            if (writer == null) return;

            string message = textBoxMessage.Text.Trim();
            if (string.IsNullOrEmpty(message)) return;

            try
            {
                // Отправляем на сервер ТОЛЬКО чистый текст сообщения
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();

                textBoxMessage.Clear();
            }
            catch
            {
                MessageBox.Show("Не вдалося надіслати повідомлення.");
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            try
            {
                while (reader != null)
                {
                    string? message = await reader.ReadLineAsync();
                    if (message == null) break;

                    if (message.StartsWith("MESSAGE|"))
                    {
                        string text = message.Substring(8);
                        Invoke(() => {
                            richTextBoxChat.AppendText(text + Environment.NewLine);
                            richTextBoxChat.ScrollToCaret();
                        });
                    }
                    else if (message.StartsWith("USERS|"))
                    {
                        string users = message.Substring(6);
                        Invoke(() => {
                            listBoxUsers.Items.Clear();
                            if (!string.IsNullOrEmpty(users))
                            {
                                string[] userList = users.Split(',');
                                foreach (string user in userList)
                                {
                                    listBoxUsers.Items.Add(user);
                                }
                            }
                        });
                    }
                }
            }
            catch
            {
                if (!isDisconnected)
                {
                    Invoke(() => MessageBox.Show("З'єднання з сервером втрачено."));
                }
            }
            finally
            {
                ResetUI();
            }
        }

        private void ResetUI()
        {
            Invoke(() => {
                buttonSend.Enabled = false;
                textBoxMessage.Enabled = false;
                buttonConnect.Enabled = true;
                textBoxName.Enabled = true;
                listBoxUsers.Items.Clear();
            });
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            isDisconnected = true;
            try
            {
                if (writer != null)
                {
                    await writer.WriteLineAsync("EXIT");
                    await writer.FlushAsync();
                }
                client?.Close();
            }
            catch { }
            base.OnFormClosing(e);
        }
    }
}
