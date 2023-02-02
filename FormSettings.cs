using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EnviadorEmails
{
    public partial class FormSettings : Form
    {
        ArrayList personas = new ArrayList();
        string filePath = "";
        string configFile = "config.conf";
        Config config = new Config();
        public FormSettings()
        {
            InitializeComponent();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Config config = new Config
            {
                Email = tb_email.Text,
                Contraseña = tb_password.Text,
                Servidor = tb_server.Text,
                Puerto = Int32.Parse(tb_port.Text.ToString()) ,
                Asunto = tb_asunto.Text,
                Cuerpo = tb_cuerpo.Text,
            };

            string json = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("config.conf", json);
            this.Close();

        }
        private void ReadConfig()
        {
            try
            {
                string json = File.ReadAllText(configFile);
                config = JsonConvert.DeserializeObject<Config>(json);

                tb_asunto.Text = config.Asunto.ToString();
                tb_cuerpo.Text = config.Cuerpo.ToString();
                tb_email.Text = config.Email.ToString();
                tb_password.Text = config.Contraseña.ToString();
                tb_server.Text = config.Servidor.ToString();
                tb_port.Text = config.Puerto.ToString();
            }
            catch (Exception ex)
            {
                config = new Config
                {
                    Email = "",
                    Contraseña = "",
                    Servidor = "",
                    Puerto = 0,
                    Asunto = "",
                    Cuerpo = "",
                };

                string json = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(configFile, json);
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            ReadConfig();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
