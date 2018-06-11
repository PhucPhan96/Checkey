using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace CheckKey
{
    public partial class frmTrial : Form
    {
        public string api = "http://120.72.107.61:9780/Licence/";
        //public string api = "http://localhost:1234/NFCDashboard-server/";
        public frmTrial()
        {
            InitializeComponent();
            HttpClient clint = new HttpClient();
            clint.BaseAddress = new Uri(api);
            HttpResponseMessage response = clint.GetAsync("app/keytrial/getAll").Result;
            Object key_single = response.Content.ReadAsAsync<Object>().Result;
            List<KeyTrial> list = JsonConvert.DeserializeObject<List<KeyTrial>>(key_single.ToString());
            dtgTrial.DataSource = convertJsonToDataTable(list);
        }


        DataTable convertJsonToDataTable(List<KeyTrial> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("main", typeof(string));
            dt.Columns.Add("cpu", typeof(string));
            dt.Columns.Add("start_date", typeof(DateTime));
            dt.Columns.Add("end_date", typeof(DateTime));
            dt.Columns.Add("expired_time", typeof(int));
            dt.Columns.Add("key_left", typeof(int));

            foreach (KeyTrial key in list)
            {
                TimeSpan time = new TimeSpan();
                time = (TimeSpan)(key.end_date - DateTime.Now);
                dt.Rows.Add(key.id, key.main, key.cpu, key.start_date, key.end_date, key.expired_time, (int)time.TotalDays);
            }
            return dt;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api);
                var result = client.PostAsync(string.Format("app/keytrial/checkTrial?main={0}&cpu={1}", txtMain.Text, txtCPU.Text), null).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultContent);
                lbResult.Text = resultContent;
            }
        }
    }
}
