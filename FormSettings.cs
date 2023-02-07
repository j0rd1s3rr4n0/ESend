﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
        ArrayList lista_cc = new ArrayList();
        ArrayList lista_cco = new ArrayList();
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
                Puerto = Int32.Parse(tb_port.Text.ToString()),
                Asunto = tb_asunto.Text,
                Cuerpo = tb_cuerpo.Text,
                EmailsCC = lista_cc,
                EmailsCCO = lista_cco
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

        void SendEmail(string subject, string body)
        {
            tv_estado.Text = "Pendiente";
            if ((tb_emailToTest.Text.ToString().Length > 0) && (tb_emailToTest != null))
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(tb_server.Text.ToString());

                    mail.From = new MailAddress(tb_email.Text.ToString());
                    mail.To.Add(tb_emailToTest.Text.ToString());
                    mail.Subject = subject;
                    mail.Body = body;
                    SmtpServer.Port = Int32.Parse(tb_port.Text);
                    SmtpServer.Credentials = new NetworkCredential(tb_email.Text.ToString(), tb_password.Text.ToString());
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    //MessageBox.Show("REVISE SU EMAIL", "EMAIL ENVIADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tv_estado.Text = "Enviado";

                }
                catch (SmtpException ex)
                {
                    //MessageBox.Show(ex.Message, "ERROR SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tv_estado.Text = "Error - Connexion";
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "ERROR GENERAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tv_estado.Text = "Error - "+ ex.Message;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lista_cc = new ArrayList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lista_cc.Add(line);
                    }
                }

                //MailMessage message = new MailMessage();
                //foreach (string elemento_cc in lista_cc)
                //{
                //    //message.Bcc.Add(new MailAddress(elemento_cc));
                //}
                grid_CC.DataSource = lista_cc;
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {   
            lista_cco = new ArrayList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lista_cco.Add(line);
                    }
                }

                //MailMessage message = new MailMessage();
                foreach (string elemento_cco in lista_cco)
                {
                    grid_CCO.Rows.Add(elemento_cco);
                }

                grid_CCO.ColumnCount = 1;
                grid_CCO.Columns[0].Name = "Direcciones de correo electrónico";

            }
        }

        private async void btnTestEmail_Click(object sender, EventArgs e)
        {
            SendEmail("EMAIL DE TEST", "Esto es un email de test.");
        }
    }
}
