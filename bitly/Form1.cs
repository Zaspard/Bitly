using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Json;
using System.Net.Http;

namespace bitly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /////https://www.youtube.com/watch?time_continue=4060&v=a4wEaFAA5XQ
        /////https://www.youtube.com/watch?v=wWhtcU4-xAM

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            var url = textBox1.Text;
            if (url == "")
            {
                MessageBox.Show("Введите url", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else{
                if (!url.Contains("https://"))
                    url = "https://" + textBox1.Text;
                await ShortAsync(url);
            }
        }

        async Task ShortAsync(string Url)
        {
            var id = "8a62540b2285d87d47c82078368c01d900790b6b";
            var url = $"https://api-ssl.bitly.com/v3/shorten?access_token={id}&longUrl={HttpUtility.UrlEncode(Url)}";
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(url);
                var json = JsonValue.Parse(response);
                try
                {
                    textBox2.Text = json["data"]["url"];
                }
                catch
                {
                    MessageBox.Show("Фиговый чет у вас url. Напиши нормально", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox2.Text = json["status_txt"];
                }
            }
            catch
            {
                MessageBox.Show("Отсутствует соединение с интернетом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
} 