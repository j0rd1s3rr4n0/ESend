﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Collections;
using System.Xml;
using Newtonsoft.Json;
using MailKit.Security;
using System.Data.OleDb;

namespace EnviadorEmails { 
    public partial class Form1 : Form
    {
        ArrayList personas = new ArrayList();
        string filePath = "";
        string configFile = "config.conf";
        Config config = new Config();
        int numEnviados = 0;
        int numTotal = 0;
        int numErrors = 0;
        int tiempoEspra = 2;


        public Form1()
        {
            InitializeComponent();
        }
        // nombre_apellidos ; email ; fichero.pdf
        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            
            string emailTo = "sanchezcarla2204@gmail.com"; // "jordiserrano@protonmail.ch"
            await readConfigFile();

            string myemail = config.Email.ToString();
            string mypassword = config.Contraseña.ToString();
            string server = config.Servidor.ToString();
            int port = Int32.Parse(config.Puerto.ToString());


            SmtpClient client = new SmtpClient(server, port);
            client.UseDefaultCredentials = false;
            // PASSWORD GENERATED BY https://myaccount.google.com/apppasswords
            client.Credentials = new NetworkCredential(myemail, mypassword);
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(myemail);
            mailMessage.To.Add(emailTo);
            
            mailMessage.CC.Add(myemail);
            //mailMessage.Subject = "Subject: Heyy";
            //mailMessage.Body = "Body: heyyy!";
            DateTime fecha = DateTime.Now;
            string fechaFormateada = fecha.ToString("dd/MM/yyyy");

            mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
            mailMessage.Headers.Add("Disposition-Notification-To", "<"+myemail+">");
            mailMessage.Headers.Add("Return-Receipt-To", "<"+myemail+">");


            string filepath = "";


            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            //openFileDialog1.Filter = "Database files (*.mdb, *.accdb)|*.mdb;*.accdb";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                await Task.Run(async () => { 
                
                string selectedFileName = openFileDialog1.FileName;
                Attachment atachment = new Attachment(selectedFileName);
                mailMessage.Attachments.Add(atachment);
                string nombre = "Carla";
                mailMessage.Subject = config.Asunto.ToString()
                  .Replace("$name$", nombre)
                      .Replace("$email$", emailTo)
                          .Replace("$file$", selectedFileName)
                            .Replace("$fecha", fechaFormateada);

                    if (config.Cuerpo.ToString().Contains("<html"))
                    {
                        mailMessage.IsBodyHtml = true;


                        mailMessage.Body = config.Cuerpo.ToString()
                                            .Replace("$name$", nombre)
                                                .Replace("$email$", emailTo)
                                                    .Replace("$file$", selectedFileName)
                                                    .Replace("$fecha$", fechaFormateada);
                        mailMessage.Headers.Add("Content-Type", "text/html");
                    }
                    else
                    {
                        mailMessage.IsBodyHtml = false;


                        mailMessage.Body = config.Cuerpo.ToString()
                                            .Replace("$name$", nombre)
                                                .Replace("$email$", emailTo)
                                                    .Replace("$file$", selectedFileName)
                                                    .Replace("$fecha$", fechaFormateada);
                        mailMessage.Headers.Add("Content-Type", "text/plane");
                    }
                




                    try
                    {
                        //client.Send(mailMessage);
                    int sleeperint = GetRandomNumberEspera();
                    tv_TiempoEspera.Text = sleeperint.ToString()+"s";
                    System.Threading.Thread.Sleep(sleeperint * 1000);
                    await client.SendMailAsync(mailMessage).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // Verificar conexión a Internet
                    try
                    {
                        string HostURI = "https://www.google.com/";
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(HostURI);
                        request.Method = "GET";
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            try
                            {
                                using (var testClient = new SmtpClient(server, port))
                                {
                                    testClient.Timeout = 5000; // Set timeout to 5 seconds
                                    testClient.EnableSsl = true; // Use SSL
                                    testClient.Send("test@example.com", "test@example.com", "Test Message", "This is a test message.");
                                }
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    // Enviar correo electrónico
                                    try
                                    {
                                        await client.SendMailAsync(mailMessage).ConfigureAwait(false);
                                        //MessageBox.Show("Correo electrónico enviado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    catch (SmtpException errorSend)
                                    {
                                        if (errorSend.Message.Contains("No es posible conectar con el servidor remoto"))
                                        {
                                            MessageBox.Show("ERROR EN LA CONFIGURACIÓN DE CONNEXIÓN\nREVISE EL SERVIDOR Y EL PUERTO.", "ERROR DE CONEXIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else if (errorSend.Message.Contains("This message was blocked because its content presents a potential"))
                                        {
                                            MessageBox.Show("ERROR AL ENVIAR EL ARCHIVO,\n FUE BLOQUEADO POR CONTENIDO POTENCIALMENTE PELIGROSO.", "ERROR AL ENVIAR ARCHIVO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else if (errorSend.Message.Contains("No se pueden leer los datos de la conexión de transporte"))
                                        {
                                            MessageBox.Show("ERROR AL ENVIAR EL ARCHIVO,\n ARCHIVO DEMASIADO GRANDE, RECHAZADO POR EL SERVIDOR.", "ERROR AL ENVIAR ARCHIVO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else
                                        {
                                            MessageBox.Show(errorSend.Message, "ERROR GENERAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show("ERROR AL CONTACTAR CON EL SERVIDOR.", "ERROR DE CONEXIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }
                    catch (WebException errorConn)
                    {
                        MessageBox.Show("ERROR DE CONEXIÓN,\n REVISE LA CONEXIÓN A INTERNET.", "ERROR DE CONEXIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                });


                /*
                 tb_fileNameData.Text= ex.ToString();
                     if (ex.Message.ToString().Contains("No es posible conectar con el servidor remoto")) {
                         MessageBox.Show("ERROR EN LA CONFIGURACIÓN DE CONEXIÓN\nREVISE EL SERVIDOR Y EL PUERTO.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     else if (ex.Message.ToString().Contains("This message was blocked because its content presents a potential")){
                         MessageBox.Show("ERROR AL ENVIAR EL ARCHIVO,\n FUE BLOQUEADO POR CONTENIDO POTENCIALMENTE PELIGROSO.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     else if (ex.Message.ToString().Contains("No se pueden leer los datos de la conexión de transporte"))
                     {
                         MessageBox.Show("ERROR AL ENVIAR EL ARCHIVO,\n ARCHIVO DEMASIADO GRANDE, RECHAZADO POREL SERVIDOR.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     else
                     {
                         MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 */
            }
            

            /*
             using(var reader = new StreamReader(@"C:\test.csv"))
                {
                    List<string> listA = new List<string>();
                    List<string> listB = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        listA.Add(values[0]);
                        listB.Add(values[1]);
                    }
                }
             */


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }



        // Configuración Image
        private async void pictureBox2_ClickAsync(object sender, EventArgs e)
        {
            //await Task.Run(() => openConfig());
            openConfig();
            await readConfigFile();
        }
        // Configuración Button
        private async void btnConfig_ClickAsync(object sender, EventArgs e) 
        {
            //await Task.Run(() => openConfig());
            openConfig();
            await readConfigFile();
        }
        private async Task openConfig()
        {   
            Form formulario = new FormSettings();
            formulario.Show();
        }

        public async Task readConfigFile()
        {
            try { 
                string json = File.ReadAllText(configFile);
                config = JsonConvert.DeserializeObject<Config>(json);
                tv_emailEnUso.Text = config.Email.ToString();

            }
            catch(Exception ex)
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

        private void btn_ReadFile(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
            }
            if (filePath != null && filePath.Length > 0)
            {
                tb_fileNameData.Text = filePath;
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    personas.Add(new Persona
                    {
                        Nombre = fields[0],
                        Email = fields[1],
                        Archivo = fields[2]
                    });
                }
            }
            numTotal = personas.Count;
            tv_NumEnviados.Text = numEnviados+"/1000";
            tv_NumErrores.Text = numErrors.ToString();
            int numeroPendientes = (numTotal-numErrors + numEnviados);
            if (numeroPendientes < 0){numeroPendientes = 0;}
            tv_NumPendientes.Text = numeroPendientes.ToString();

            foreach (Persona persona in personas)
            {
                grid_datos.Rows.Add(persona.Nombre, persona.Email, persona.Archivo);
            }
        }


        public int GetRandomNumberEspera()
        {
            Random random = new Random();
            return random.Next(2, 11);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            readConfigFile();
            tv_emailEnUso.Text = config.Email.ToString();
            grid_datos.AutoGenerateColumns = false;
            

        }

    }
}
