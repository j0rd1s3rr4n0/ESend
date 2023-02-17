using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace EnviadorEmails
{
    public partial class Form1 : Form
    {
        ArrayList personas = new ArrayList();
        string filePath = "";
        string configFile = "config.conf";
        Config config = new Config();
        int numEnviados = 0;
        int numTotal = 0;
        int numTotalTotal = 0;
        int numErrors = 0;
        int tiempoEspra = 2;
        string ruta = "C:\\";

        Boolean is_stop = false;
        Boolean carpeta_defined = false;

        public Form1()
        {
            InitializeComponent();
        }
        // nombre_apellidos ; email ; fichero.pdf

        /*
            #=====================================================================================
            *#    _____            _             __      __        _       _     _           
            *#   |  __ \          | |            \ \    / /       (_)     | |   | |          
            *#   | |__) |___ _ __ | | __ _  ___ __\ \  / /_ _ _ __ _  __ _| |__ | | ___  ___ 
            *#   |  _  // _ \ '_ \| |/ _` |/ __/ _ \ \/ / _` | '__| |/ _` | '_ \| |/ _ \/ __|
            *#   | | \ \  __/ |_) | | (_| | (_|  __/\  / (_| | |  | | (_| | |_) | |  __/\__ \
            *#   |_|  \_\___| .__/|_|\__,_|\___\___| \/ \__,_|_|  |_|\__,_|_.__/|_|\___||___/
            *#              | |                                                              
            *#              |_|                                                                                            
            #------------------------------------------------------------------------------------
            *# Name: ReplaceVariables
            *# params: 
                    text            :   string
                    nombre          :   string
                    emailTo         :   string
                    fichero_txt     :   string
                    fechaFormateada :   string
            *# Purpose:
                La función reemplaza las variables en el cuerpo de correo electrónico para personalizar
                el mensaje que se enviará a los destinatarios del correo electrónico.
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                La función toma el cuerpo del correo electrónico y busca variables en él. Si encuentra
                variables, las reemplaza por el valor correspondiente proporcionado en los parámetros.
                Luego devuelve el cuerpo del correo electrónico personalizado.
            *# CVEs Patch & Dependencies
                No hay dependencias relacionadas con esta función.
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
        */
        private string ReplaceVariables(string text, string nombre, string emailTo, string fichero_txt, string fechaFormateada)
        {
            return text.Replace("$name$", nombre)
                       .Replace("$email$", emailTo)
                       .Replace("$file$", fichero_txt)
                       .Replace("$fecha$", fechaFormateada);
        }

        /*
            #=====================================================================================
            *#    _     _            _____                _ ______                 _ _     
            *#   | |   | |          / ____|              | |  ____|               (_) |    
            *#   | |__ | |_ _ __   | (___   ___ _ __   __| | |__   _ __ ___   __ _ _| |___ 
            *#   | '_ \| __| '_ \   \___ \ / _ \ '_ \ / _` |  __| | '_ ` _ \ / _` | | / __|
            *#   | |_) | |_| | | |  ____) |  __/ | | | (_| | |____| | | | | | (_| | | \__ \
            *#   |_.__/ \__|_| |_| |_____/ \___|_| |_|\__,_|______|_| |_| |_|\__,_|_|_|___/
            *#                 ______                                                      
            *#                |______|                                                     
            #------------------------------------------------------------------------------------
            *# Name: btn_SendEmails
            *# params: [NO]
            *# Purpose:
                La función ReplaceVariables se utiliza para reemplazar variables de una cadena de texto con valores especificados.
                La cadena de texto que se reemplaza es el primer parámetro, y los valores de las variables son los siguientes cuatro parámetros.
                Se busca una coincidencia con cada una de las variables $name$, $email$, $file$, $fecha$ y se reemplaza por los valores de los
                parámetros nombre, emailTo, fichero_txt, fechaFormateada, respectivamente. La función devuelve la cadena de texto resultante.
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función se llama cuando se hace clic en el botón "Limpiar" en la sección "CCO" de la ventana de configuración.
                Borra los datos de la lista "lista_cco" y actualiza la tabla "grid_CCO" en la interfaz gráfica.
            *# CVEs Patch & Dependencies
                No hay dependencias ni vulnerabilidades relacionadas con esta función.
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
        */
        private async void btn_SendEmails(object sender, EventArgs e)
        {
            if (carpeta_defined || !cb_requireFile.Checked)
            {
                numErrors = 0;
                tv_NumErrores.Text = numErrors.ToString();
                await readConfigFile();
                string myemail = config.Email.ToString();
                string mypassword = config.Contraseña.ToString();
                string server = config.Servidor.ToString();
                int port = Int32.Parse(config.Puerto.ToString());

                while ((myemail == null || myemail == "") || (mypassword == null || mypassword.Length < 1) || (server == null || server.Length < 1) || (myemail == null || myemail.Length < 1))
                {
                    MessageBox.Show("Algun parametro necesario no fue asignado", "REVISE LOS CAMPOS, ERROR CONFIG");
                    FormSettings f = new FormSettings();
                    f.ShowDialog();
                    await readConfigFile();
                    myemail = config.Email.ToString();
                    mypassword = config.Contraseña.ToString();
                    server = config.Servidor.ToString();
                    port = Int32.Parse(config.Puerto.ToString());
                }
                SmtpClient client = new SmtpClient(server, port);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(myemail, mypassword);
                client.EnableSsl = true;
                foreach (Persona persona in personas)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(myemail);

                    try
                    {
                        ArrayList gs = config.EmailsCC;
                        string jsonString = JsonConvert.SerializeObject(gs);
                        var emailList = JsonConvert.DeserializeObject<List<email>>(jsonString);
                        string[] emailAddressesCC = emailList.Select(x => x.Email).ToArray();
                        try
                        {
                            foreach(string em in emailAddressesCC) { 
                                mailMessage.CC.Add(em);
                            }
                        }catch(Exception ex) {}
                    }catch(Exception e_cco)
                    {
                        
                    }
                    try
                    {
                        ArrayList gs = config.EmailsCCO;
                        string jsonString = JsonConvert.SerializeObject(gs);
                        var emailList = JsonConvert.DeserializeObject<List<email>>(jsonString);
                        string[] emailAddressesCCO = emailList.Select(x => x.Email).ToArray();
                        try
                        {
                            foreach (string em in emailAddressesCCO)
                            {
                                mailMessage.Bcc.Add(em);
                            }
                        }
                        catch (Exception ex) { }
                    }
                    catch (Exception e_cco)
                    {

                    }



                    string emailTo = persona.Email;
                    mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
                    mailMessage.Headers.Add("Disposition-Notification-To", "<" + myemail + ">");
                    
                    mailMessage.Headers.Add("Return-Receipt-To", "<" + myemail + ">");
                    string nombre = persona.Nombre;
                    string selectedFileName = ruta + "\\" + persona.Archivo;
                    try
                    {
                        File.AppendAllText("Results/done.csv", $"{persona.Nombre},{persona.Email},{persona.Archivo}\n");
                        mailMessage.To.Add(emailTo);

                        await Task.Run(() =>
                        {
                            string[] textSplit = selectedFileName.Split(new string[] { "\\" }, StringSplitOptions.None);
                            string fichero_txt;
                            if (cb_requireFile.Checked == true)
                            {
                                try
                                {
                                    Attachment attachment = new Attachment(selectedFileName);
                                    mailMessage.Attachments.Add(attachment);
                                    fichero_txt = textSplit[textSplit.Length - 1];
                                }
                                catch (DirectoryNotFoundException err)
                                {
                                    MessageBox.Show(err.Message, "ERROR - DIRECTORIO NO ENCONTRADO");
                                }
                                catch (Exception errgen)
                                {
                                    MessageBox.Show(errgen.Message, "ERROR");
                                }
                            }
                            else
                            {
                                if (selectedFileName != "" || selectedFileName != null)
                                {
                                    try
                                    {
                                        Attachment attachment = new Attachment(selectedFileName);
                                        mailMessage.Attachments.Add(attachment);
                                    }
                                    catch (DirectoryNotFoundException err)
                                    {
                                        MessageBox.Show(err.Message, "ERROR - DIRECTORIO NO ENCONTRADO");
                                    }
                                    catch (Exception errgen)
                                    {
                                        MessageBox.Show(errgen.Message, "ERROR");
                                    }
                                }
                                fichero_txt = (textSplit[textSplit.Length - 1].Length == 0) ? "No Se Asigno Ningun Archivo" : textSplit[textSplit.Length - 1];
                                string fechaFormateada = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                mailMessage.Subject = ReplaceVariables(config.Asunto.ToString(), nombre, emailTo, fichero_txt, fechaFormateada);

                                mailMessage.IsBodyHtml = IsHtml(config.Cuerpo.ToString());
                                mailMessage.Body = ReplaceVariables(config.Cuerpo.ToString(), nombre, emailTo, fichero_txt, fechaFormateada);

                                if (mailMessage.IsBodyHtml)
                                {
                                    mailMessage.Headers.Add("Content-Type", "text/html");
                                }
                                else
                                {
                                    mailMessage.Headers.Add("Content-Type", "text/plane");
                                }
                                try
                                {
                                    client.Send(mailMessage);
                                    numEnviados += 1;
                                    numTotal -= 1;
                                }
                                catch (Exception wtf)
                                {
                                    MessageBox.Show(wtf.Message, "ERROR NO SE PUDO ENVIAR");
                                }
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        // Escribir en el archivo errors.csv
                        File.AppendAllText("Results/errors.csv", $"{persona.Nombre},{persona.Email},{persona.Archivo},{ex.Message}\n");
                        numErrors += 1;
                        numTotal -= 1;


                    }
                    tv_NumErrores.Text = numErrors.ToString().Replace("-", "");
                    tv_NumEnviados.Text = numEnviados.ToString().Replace("-", "") + "/1000";
                    tv_NumPendientes.Text = numTotal.ToString().Replace("-", "") + "/" + numTotalTotal.ToString().Replace("-", "");

                    tv_TiempoEspera.Text = "-";
                }
            }
            else
            {
                MessageBox.Show("Es Necesario Especificar la ruta\n de donde extraera los archivos que se\nespecifican en el csv", "ERROR");
            }
        }

        /*
            #=====================================================================================
            *#                       _                                     _   
            *#                      | |                                   | |  
            *#     __ _ _   _  ___  | |__   __ _     _ __   __ _ ___  __ _| |_ 
            *#    / _` | | | |/ _ \ | '_ \ / _` |   | '_ \ / _` / __|/ _` | __|
            *#   | (_| | |_| |  __/ | | | | (_| |   | |_) | (_| \__ \ (_| | |_ 
            *#    \__, |\__,_|\___|_|_| |_|\__,_|___| .__/ \__,_|___/\__,_|\__|
            *#       | |       |______|        |____| |_|                        
            *#       |_|                            |_|                        
            #------------------------------------------------------------------------------------
            *# Name: que_ha_pasat
            *# params: [Exception ex]
            *# Purpose:
                Esta función se utiliza para detectar el tipo de error que se ha producido durante el envío de un correo electrónico.
            #------------------------------------------------------------------------------------
            *# Created: 10/02/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 10/02/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función primero llama a la función readConfigFile() para leer el archivo de configuración y configurar
                los parámetros necesarios para el envío de correo electrónico.
                A continuación, comprueba si hay problemas de conexión a Internet y, si los hay, devuelve un mensaje de error
                correspondiente. Luego comprueba si hay un problema de conexión al servidor SMTP, y si lo hay, devuelve un mensaje de error correspondiente.
                A continuación, comprueba si hay un problema de credenciales, y si lo hay, devuelve un mensaje de error correspondiente.
                Si no se encuentra ninguno de estos problemas, devuelve el mensaje de error original.
            *# CVEs Patch & Dependencies
                No hay vulnerabilidades conocidas ni dependencias.
        */
        private string que_ha_pasat(Exception ex)
        {
            readConfigFile();
            if(testConnection()){
                return "ERROR CONEXION";
            }
            else if (IsSMTPOpen(config.Servidor))
            {
                return "ERROR DE CONEXION - COMPRUEBE NUEVAMENE EL PUERTO";
            }
            else if (TestSmtpServerConnection(config.Servidor, Int32.Parse(config.Puerto.ToString())))
            {
                return "ERROR SERVIDOR - CONEXION SMTP";
            }
            else if (CheckCredentials())
            {
                return "ERROR DE CREDENCIALES";
            }
            else
            {
                return ex.Message.ToString();
            }
        }
        /*
            #=====================================================================================
            *#    _                                                
            *#   | |                                               
            *#   | |_ ___ _ __ ___  _ __  ___ _ __   ___ _ __ __ _ 
            *#   | __/ _ \ '_ ` _ \| '_ \/ __| '_ \ / _ \ '__/ _` |
            *#   | ||  __/ | | | | | |_) \__ \ |_) |  __/ | | (_| |
            *#    \__\___|_| |_| |_| .__/|___/ .__/ \___|_|  \__,_|
            *#                     | |       | |                   
            *#                     |_|       |_|                   
            #------------------------------------------------------------------------------------
            *# Name: tempspera
            *# params: int a
            *# Purpose:
                Hace una pausa de a segundos e imprime el tiempo restante en tv_TiempoEspera

            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Se usa la función Thread.Sleep para pausar el hilo actual por 1000 ms, es decir 1s, 
                luego se imprime en tv_TiempoEspera el número de segundos que quedan, y se repite 
                este proceso hasta llegar a cero, momento en que se indica que el tiempo de espera
                ha finalizado.

            *# CVEs Patch & Dependencies
                Ninguna
        */
        public async Task tempspera(int a)
        {
            for(int i = a; i>0; i--)
            {
                System.Threading.Thread.Sleep(1000);
                tv_TiempoEspera.Text = i.ToString() + "s";
            }
            tv_TiempoEspera.Text = "0s";
            System.Threading.Thread.Sleep(500);
            tv_TiempoEspera.Text =  "-";
        }



        /*
         #=====================================================================================
         *#    _     _           _____             __ _       _____                            
         *#   | |   | |         / ____|           / _(_)     |_   _|                           
         *#   | |__ | |_ _ __  | |     ___  _ __ | |_ _  __ _  | |  _ __ ___   __ _  __ _  ___ 
         *#   | '_ \| __| '_ \ | |    / _ \| '_ \|  _| |/ _` | | | | '_ ` _ \ / _` |/ _` |/ _ \
         *#   | |_) | |_| | | || |___| (_) | | | | | | | (_| |_| |_| | | | | | (_| | (_| |  __/
         *#   |_.__/ \__|_| |_|_\_____\___/|_| |_|_| |_|\__, |_____|_| |_| |_|\__,_|\__, |\___|
         *#                |______|                      __/ |                       __/ |     
         *#                                             |___/                       |___/      
         #------------------------------------------------------------------------------------
            *# Name: btn_ConfigImage
            *# params: object sender, EventArgs e
            *# Purpose:
                Abre el formulario de configuración de imágenes y carga la configuración 
                del archivo config.json

            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Llama a la función openConfig para abrir el formulario de configuración, luego
                espera a que se termine de leer el archivo de configuración con readConfigFile.

            *# CVEs Patch & Dependencies
                Ninguna
        */
        private async void btn_ConfigImage(object sender, EventArgs e)
        {
            //await Task.Run(() => openConfig());
            openConfig();
            await readConfigFile();
        }

        /*
            #=====================================================================================
            *#    _     _           _____             __      
            *#   | |   | |         / ____|           / _|     
            *#   | |__ | |_ _ __  | |     ___  _ __ | |_ __ _ 
            *#   | '_ \| __| '_ \ | |    / _ \| '_ \|  _/ _` |
            *#   | |_) | |_| | | || |___| (_) | | | | || (_| |
            *#   |_.__/ \__|_| |_|_\_____\___/|_| |_|_| \__, |
            *#                |______|                   __/ |
            *#                                          |___/ 
            #------------------------------------------------------------------------------------
            *# Name: btn_Confg
            *# params: object sender, EventArgs e
            *# Purpose:
                Abre el formulario de configuración y carga la configuración del archivo
                config.json

            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Llama a la función openConfig para abrir el formulario de configuración, luego
                espera a que se termine de leer el archivo de configuración con readConfigFile.

            *# CVEs Patch & Dependencies
                Ninguna
        */
        // Configuración Button
        private async void btn_Confg(object sender, EventArgs e)
        {
            //await Task.Run(() => openConfig());
            openConfig();
            await readConfigFile();
        }



        /*
            #=====================================================================================
            *#                            _____             __ _       
            *#                           / ____|           / _(_)      
            *#     ___  _ __   ___ _ __ | |     ___  _ __ | |_ _  __ _ 
            *#    / _ \| '_ \ / _ \ '_ \| |    / _ \| '_ \|  _| |/ _` |
            *#   | (_) | |_) |  __/ | | | |___| (_) | | | | | | | (_| |
            *#    \___/| .__/ \___|_| |_|\_____\___/|_| |_|_| |_|\__, |
            *#         | |                                        __/ |
            *#         |_|                                       |___/ 
            #------------------------------------------------------------------------------------
            *# Name: openConfig
            *# params: N/A
            *# Purpose:
                Abre el formulario de configuración
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Crea una nueva instancia del formulario de configuración y lo muestra en pantalla.
            *# CVEs Patch & Dependencies
                N/A
        */
        private async Task openConfig()
        {
            Form formulario = new FormSettings();
            formulario.Show();
        }

        /*
            #=====================================================================================
            *#                       _  _____             __ _       ______ _ _      
            *#                      | |/ ____|           / _(_)     |  ____(_) |     
            *#    _ __ ___  __ _  __| | |     ___  _ __ | |_ _  __ _| |__   _| | ___ 
            *#   | '__/ _ \/ _` |/ _` | |    / _ \| '_ \|  _| |/ _` |  __| | | |/ _ \
            *#   | | |  __/ (_| | (_| | |___| (_) | | | | | | | (_| | |    | | |  __/
            *#   |_|  \___|\__,_|\__,_|\_____\___/|_| |_|_| |_|\__, |_|    |_|_|\___|
            *#                                                  __/ |                
            *#                                                 |___/                          
            #------------------------------------------------------------------------------------
            *# Name: readConfigFile
            *# params: N/A
            *# Purpose:
                Lee el archivo de configuración y carga la información en la aplicación
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Lee el archivo de configuración y carga la información en la aplicación. Si el archivo no existe, 
                crea una nueva instancia de Config con valores por defecto y lo guarda en el archivo.
            *# CVEs Patch & Dependencies
                N/A
        */
        public async Task readConfigFile()
        {
            try
            {
                string json = File.ReadAllText(configFile);
                config = JsonConvert.DeserializeObject<Config>(json);
                tv_emailEnUso.Text = config.Email.ToString();

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


        /*
            #=====================================================================================
            *#    _     _           _____                _ ______ _ _      
            *#   | |   | |         |  __ \              | |  ____(_) |     
            *#   | |__ | |_ _ __   | |__) |___  __ _  __| | |__   _| | ___ 
            *#   | '_ \| __| '_ \  |  _  // _ \/ _` |/ _` |  __| | | |/ _ \
            *#   | |_) | |_| | | | | | \ \  __/ (_| | (_| | |    | | |  __/
            *#   |_.__/ \__|_| |_|_|_|  \_\___|\__,_|\__,_|_|    |_|_|\___|
            *#                 |______|                                      
            *#                                                             
            #------------------------------------------------------------------------------------
            *# Name: btn_ReadFile
            *# params: object sender, EventArgs e
            *# Purpose:
               Carga el contenido del archivo de Personas, lo inserta en el grid y tambien genra los 
               archivos de salida. Y define los valores de los parametros de arranque.
            #------------------------------------------------------------------------------------
            *# Created: 09/02/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 17/02/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Este código tiene la tarea de leer los datos de un archivo de texto, almacenar los
                datos de cada persona en una lista de objetos Persona, y luego mover los archivos
                a una carpeta específica y mostrar el número total de personas, el número de personas
                enviadas, el número de errores y el número de personas pendientes. El código también
                agrega cada persona a una tabla para facilitar la visualización de los datos.
            *# CVEs Patch & Dependencies
                El código anterior se refiere a una aplicación que realiza un proceso de envío de
                correos electrónicos masivos. Utiliza un archivo CSV que contiene información de
                destinatarios, como nombre y correo electrónico. La aplicación crea una carpeta
                para guardar resultados y mover los archivos CSV originales a la carpeta creada.
                Luego, recorre el archivo CSV para leer los datos y comenzar el proceso de envío de
                correos. Por último, se actualizan los valores para los contadores de envíos, errores
                y pendientes. Esto permite al usuario tener una idea de los resultados del proceso.
                El código, además, está diseñado para evitar problemas como la sobreescritura de archivos
                y la creación de directorios en caso de que existan.
        */

        private void btn_ReadFile(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivo de Personas (*.csv, *.txt)|*.csv;*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    carpeta_defined = false;
                    grid_datos.Rows.Clear();
                    personas.Clear();
                    personas = new ArrayList();
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


                try { if (!Directory.Exists("Results")) { Directory.CreateDirectory("Results"); } }
                catch (Exception ext) { MessageBox.Show(ext.ToString()); }



                string rutaTodo = @"Results\todo.csv";
                string rutaDone = @"Results\done.csv";
                string rutaErrors = @"Results\errors.csv";
                string rutaCarpeta = @"Results\" + DateTime.Now.ToString("dd_MM_yyyy_ff_f_ffff_fffff_fff");
                // Verificar si existen las carpetas y archivos
                if (Directory.Exists(@"Results"))
                {
                    if (File.Exists(rutaTodo))
                    {
                        // Crear una nueva carpeta con la fecha actual
                        if (!Directory.Exists(rutaCarpeta))
                        {
                            Directory.CreateDirectory(rutaCarpeta);
                        }
                        // Mover done.csv a la nueva carpeta
                        File.Move(rutaTodo, rutaCarpeta + @"\todo.csv");
                    }
                    if (!File.Exists(rutaTodo))
                    {
                        foreach (Persona persona in personas)
                        {
                            using (StreamWriter sw = new StreamWriter("Results/todo.csv", true))
                            { sw.WriteLine($"{persona.Nombre},{persona.Email},{persona.Archivo}"); }
                        }
                        MessageBox.Show("HEYY");
                    }
                    //MessageBox.Show("OUCH!");

                    if (File.Exists(rutaDone))
                    {
                        // Crear una nueva carpeta con la fecha actual
                        if (!Directory.Exists(rutaCarpeta))
                        {
                            Directory.CreateDirectory(rutaCarpeta);
                        }
                        // Mover done.csv a la nueva carpeta
                        File.Move(rutaDone, rutaCarpeta + @"\done.csv");

                    }

                    if (File.Exists(rutaErrors))
                    {
                        // Crear una nueva carpeta con la fecha actual
                        if (!Directory.Exists(rutaCarpeta))
                        {
                            Directory.CreateDirectory(rutaCarpeta);
                        }
                        // Mover errors.csv a la nueva carpeta
                        File.Move(rutaErrors, rutaCarpeta + @"\errors.csv");

                    }
                }
                else
                {
                    Console.WriteLine("La carpeta Results no existe");
                    return;
                }

            }
            numTotal = personas.Count;
            numTotalTotal = numTotal;
            tv_NumEnviados.Text = numEnviados + "/1000";
            tv_NumErrores.Text = numErrors.ToString();
            int numeroPendientes = (numTotal - numErrors + numEnviados);
            if (numeroPendientes < 0) { numeroPendientes = 0; }
            tv_NumPendientes.Text = numeroPendientes.ToString();

            foreach (Persona persona in personas)
            {
                grid_datos.Rows.Add(persona.Nombre, persona.Email, persona.Archivo);
            }
        }


        public int GetRandomNumberEspera()
        {
            Random random = new Random();
            return random.Next(2, 15);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            readConfigFile();
            tv_emailEnUso.Text = config.Email.ToString();
            grid_datos.AutoGenerateColumns = false;
            tb_FolderFile.Text = ruta;


        }

        private void btn_SelFolder(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tb_FolderFile.Text = fbd.SelectedPath;
                    ruta = fbd.SelectedPath;
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    MessageBox.Show("La carpeta contiene: " + files.Length.ToString() + " archivos.", "Información");
                    carpeta_defined = true;
                }
            }
        }


        private void btn_STOP(object sender, EventArgs e)
        {is_stop = true;}

/*
#    ______                        _____ _               _    _             
#   |  ____|                      / ____| |             | |  (_)            
#   | |__   _ __ _ __ ___  _ __  | |    | |__   ___  ___| | ___ _ __   __ _ 
#   |  __| | '__| '__/ _ \| '__| | |    | '_ \ / _ \/ __| |/ / | '_ \ / _` |
#   | |____| |  | | | (_) | |    | |____| | | |  __/ (__|   <| | | | | (_| |
#   |______|_|  |_|  \___/|_|     \_____|_| |_|\___|\___|_|\_\_|_| |_|\__, |
#                                                                      __/ |
#                                                                     |___/ 
*/

        public bool testConnection()
        {
            try
            {
                string HostURI = "https://dns.google/query?name=8.8.8.8";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(HostURI);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK: return false; break;
                        default: return false;
                    }
                }
            }
            catch (Exception ex){return true;}
        }
        public bool CheckCredentials()
        {
            try
            {
                using (var testClient = new SmtpClient(config.Servidor, Int32.Parse(config.Puerto.ToString())))
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(config.Servidor);

                    mail.From = new MailAddress(config.Email);
                    mail.To.Add("");
                    mail.Subject = "";
                    mail.Body = "";
                    SmtpServer.Port = Int32.Parse(config.Puerto.ToString());
                    SmtpServer.Credentials = new NetworkCredential(config.Email, config.Contraseña);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    return false;
                }
            }
            catch (SmtpException ex)
            {
                return true;
            }
        }

        public bool TestSmtpServerConnection(string server, int port)
        {
            try
            {
                using (var smtpClient = new SmtpClient(server, port))
                {
                    smtpClient.Timeout = 5000; // Establecer tiempo de espera a 5 segundos
                    smtpClient.Send("test@example.com", "test@example.com", "Test Message", "This is a test message.");
                }

                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }


        private bool HandleSendEmailException(SmtpException errorSend)
        {
            ArrayList errores = new ArrayList();
            if (errorSend.Message.Contains("No es posible conectar con el servidor remoto"))
            {
                MessageBox.Show("ERROR EN LA CONFIGURACIÓN DE CONNEXIÓN\nREVISE EL SERVIDOR Y EL PUERTO.", "ERROR DE CONEXIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                is_stop = true;
            }
            else if (errorSend.Message.Contains("This message was blocked because its content presents a potential"))
            {
                MessageBox.Show("ERROR AL ENVIAR EL ARCHIVO,\n FUE BLOQUEADO POR CONTENIDO POTENCIALMENTE PELIGROSO.", "ERROR AL ENVIAR ARCHIVO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                is_stop = true;
            }
            else if (errorSend.Message.Contains("No se pueden leer los datos de la conexión de transporte"))
            {
                MessageBox.Show("ERROR AL ENVIAR EL ARCHIVO,\n ARCHIVO DEMASIADO GRANDE, RECHAZADO POR EL SERVIDOR.", "ERROR AL ENVIAR ARCHIVO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(errorSend.Message, "ERROR GENERAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                is_stop = true;
            }
            return true;
        }

        private void HandleContactServerException(Exception error)
        {
            MessageBox.Show("ERROR AL CONTACTAR CON EL SERVIDOR.", "ERROR DE CONEXIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(error.Message, error.Source);
            is_stop = true;
        }

        public static bool IsPortOpen(string server, int port)
        {
            try
            {
                using (var client = new TcpClient(server, port))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsSMTPOpen(string server)
        {
            int[] smtpPorts = { 25, 587, 465 };
            foreach (int port in smtpPorts)
            {
                if (IsPortOpen(server, port))
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsHtml(string text)
        {
            return Regex.IsMatch(text, @"<\w+>");
        }

        private void btn_FileNameAdapter(object sender, EventArgs e)
        {
            ControlPanelFileAdapter ventana_FileAdapter= new ControlPanelFileAdapter();
            string ruta = tb_FolderFile.Text;
            ventana_FileAdapter.ruta = ruta;
            ventana_FileAdapter.ShowDialog();
        }

        private void tv_NumErrores_Click(object sender, EventArgs e)
        {
            string folderPath = System.IO.Directory.GetCurrentDirectory();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = folderPath,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        private void btn_AssistenciaImage(object sender, EventArgs e)
        { 
            //TODO
        }




        /*
         
         */
        private void groupBox1_Enter(object sender, EventArgs e){}

        private void textBox3_TextChanged(object sender, EventArgs e){}

        private void label17_Click(object sender, EventArgs e) {}

        private void label4_Click(object sender, EventArgs e) {}
        private void label3_Click(object sender, EventArgs e) {}
        private void textBox4_TextChanged(object sender, EventArgs e) {}
        private void label2_Click(object sender, EventArgs e) {}
        private void label5_Click(object sender, EventArgs e) {}
        private void label6_Click(object sender, EventArgs e) {}
    }
}
