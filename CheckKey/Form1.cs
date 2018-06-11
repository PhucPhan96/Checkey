using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Forms;

namespace CheckKey
{
    public partial class Form1 : Form
    {
        public string api = "http://120.72.107.61:9780/Licence/";
        //public string api = "http://localhost:1234/NFCDashboard-server/";
        public Form1()
        {
            InitializeComponent();
            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri(api);
            HttpResponseMessage response = clint.GetAsync("app/keysingle/getAll").Result;
            Object key_single = response.Content.ReadAsAsync<Object>().Result;
            List<KeySingle> list = JsonConvert.DeserializeObject<List<KeySingle>>(key_single.ToString());
            dtgKey.DataSource = convertJsonToDataTable(list);
        }

        DataTable convertJsonToDataTable(List<KeySingle> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("key_single", typeof(string));
            dt.Columns.Add("ex_main", typeof(string));
            dt.Columns.Add("ex_cpu", typeof(string));
            dt.Columns.Add("start_date", typeof(DateTime));
            dt.Columns.Add("end_date", typeof(DateTime));
            dt.Columns.Add("key_log", typeof(int));
            dt.Columns.Add("key_left", typeof(int));
            dt.Columns.Add("status_key", typeof(string));

            foreach(KeySingle key in list)
            {
                TimeSpan time = new TimeSpan();
                if(key.end_date == null)
                {
                    dt.Rows.Add(key.id, key.key_single, key.ex_main, key.ex_cpu, key.start_date, key.end_date, key.key_log, 0, key.status_key);
                }
                else
                {
                    time = (TimeSpan)(key.end_date - DateTime.Now);
                    dt.Rows.Add(key.id, key.key_single, key.ex_main, key.ex_cpu, key.start_date, key.end_date, key.key_log,(int) time.TotalDays, key.status_key);
                }
                
            }
            return dt;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api);
                var result = client.PostAsync(string.Format("app/keysingle/findByKey?key_single={0}&ex_main={1}&ex_cpu={2}", txtKey.Text, txtMain.Text, txtCPU.Text), null).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultContent);
                label4.Text = resultContent;
                Form1_Load(sender, e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri(api);
            HttpResponseMessage response = clint.GetAsync("app/keysingle/getAll").Result;
            Object key_single = response.Content.ReadAsAsync<Object>().Result;
            List<KeySingle> list = JsonConvert.DeserializeObject<List<KeySingle>>(key_single.ToString());
            dtgKey.DataSource = convertJsonToDataTable(list);
        }
    }
}
