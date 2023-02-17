using Newtonsoft.Json;
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
/*
    #=====================================================================================
    *#  ______                    _____      _   _   _                 
    *# |  ____|                  / ____|    | | | | (_)                
    *# | |__ ___  _ __ _ __ ___ | (___   ___| |_| |_ _ _ __   __ _ ___ 
    *# |  __/ _ \| '__| '_ ` _ \ \___ \ / _ \ __| __| | '_ \ / _` / __|
    *# | | | (_) | |  | | | | | |____) |  __/ |_| |_| | | | | (_| \__ \
    *# |_|  \___/|_|  |_| |_| |_|_____/ \___|\__|\__|_|_| |_|\__, |___/
    *#                                                        __/ |    
    *#                                                       |___/     
    #------------------------------------------------------------------------------------
    *# Name: FormSettings
    *# params: [NO]
    *# Purpose:
    # Esta es la clase que gestiona los ajustes y configuraciones del programa EnviadorEmails.
    # Aquí se definen los métodos para guardar y leer la configuración en un archivo JSON, y se proporciona
    # una interfaz gráfica para que el usuario pueda editar la configuración.
    #------------------------------------------------------------------------------------
    *# Created: 09/01/2023
    *# Author: J0rd1s3rr4n0
    #----------------------------------
    *# Last Update: 10/02/2023
    *# Update Author: J0rd1s3rr4n0
    #------------------------------------------------------------------------------------
    *# Dev How it works
        La clase contiene varios métodos para gestionar la configuración del programa, que se guardan en un archivo JSON.
        Al cargar la clase, se llama al método "ReadConfig" que lee la configuración del archivo JSON y actualiza los controles
        de la interfaz gráfica con los valores leídos.
        Cuando el usuario guarda la configuración, se llama al método "Save_Settings" que crea un objeto Config
        con los valores de los controles de la interfaz gráfica y los guarda en un archivo JSON.
        También hay un método para enviar correos electrónicos "SendEmail".
    # CVEs Patch & Dependencies
        La clase depende de las librerías "Newtonsoft.Json" y "System.Windows.Forms".
        No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
*/
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

        /*
        #=====================================================================================
        *#   _____                    _____      _   _   _                 
        *#  / ____|                  / ____|    | | | | (_)                
        *# | (___   __ ___   _____  | (___   ___| |_| |_ _ _ __   __ _ ___ 
        *#  \___ \ / _` \ \ / / _ \  \___ \ / _ \ __| __| | '_ \ / _` / __|
        *#  ____) | (_| |\ V /  __/  ____) |  __/ |_| |_| | | | | (_| \__ \
        *# |_____/ \__,_| \_/ \___| |_____/ \___|\__|\__|_|_| |_|\__, |___/
        *#                      ______                            __/ |    
        *#                     |______|                          |___/       
        #------------------------------------------------------------------------------------
        *# Name: Save_Settings
        *# params: [NO]
            sender  :   objeto que envía el evento.
            e       :   argumentos del evento.
        *# Purpose:
            Cuando se hace clic en el botón "Guardar", se guarda la configuración en un archivo JSON.

            La función Save_Settings se encarga de guardar los valores del formulario en un objeto
            Config, y luego los convierte a formato JSON y los escribe en un archivo de configuración.

            Los valores que se guardan son los siguientes: dirección de correo electrónico del remitente,
            contraseña de la cuenta de correo electrónico del remitente, dirección del servidor SMTP
            utilizado para enviar correos electrónicos, número de puerto utilizado por el servidor SMTP,
            texto que se utilizará como asunto en el correo electrónico, contenido del correo electrónico,
            lista de objetos email que contienen la dirección de correo electrónico de cada destinatario
            que recibirá una copia del correo electrónico (CC) y lista de objetos email que contienen la
            dirección de correo electrónico de cada destinatario que recibirá una copia oculta del correo
            electrónico (CCO).
        #------------------------------------------------------------------------------------
        *# Created: 11/01/2023
        *# Author: J0rd1s3rr4n0
        #----------------------------------
        *# Last Update: 11/01/2023
        *# Update Author: J0rd1s3rr4n0
        #------------------------------------------------------------------------------------
        *# Dev How it works
            - La función primero guarda los valores ingresados en el formulario en un objeto de tipo Config.
            - Luego, utiliza la librería Json.NET para convertir el objeto Config en formato JSON.
            - Finalmente, escribe el JSON en un archivo llamado "config.conf" utilizando la función File.WriteAllText().
            - La función también cierra la ventana de configuración.
        *# CVEs Patch & Dependencies
            La clase depende de la librería "Json.NET" 
            Puede presentar vulnerabilidades dependiendo de la versión utilizada.
            Se recomienda mantener la librería actualizada a su versión más reciente para minimizar los riesgos de vulnerabilidades.
            La función también depende de las entradas proporcionadas por el usuario en el formulario.
            Es importante tener en cuenta la validación de datos para evitar problemas de seguridad.

         */
        private void Save_Settings(object sender, EventArgs e)
        {
            // Guarda los valores del formulario en un objeto de tipo Config y luego los convierte a JSON y los escribe en el archivo config.conf
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
            // Cierra la ventana de configuración
            this.Close();

        }

        /*
        #=====================================================================================
        *# Name: ReadConfig
        *# params: [NO]
        *# Purpose:
            Este método se encarga de leer la configuración del archivo JSON y actualizar los
            valores de los controles de la interfaz gráfica con los valores leídos. En caso de que
            no se pueda leer el archivo config.conf, se crea un objeto de tipo Config con valores
            predeterminados y se escribe en el archivo config.conf.
        #------------------------------------------------------------------------------------
        *# Created: 11/01/2023
        *# Author: J0rd1s3rr4n0
        #----------------------------------
        *# Last Update: 11/01/2023
        *# Update Author: J0rd1s3rr4n0
        #------------------------------------------------------------------------------------
        *# Dev How it works
            Esta función se encarga de leer la configuración almacenada en el archivo "config.conf" y
            actualizar los valores de los controles en la interfaz gráfica con los valores leídos. Si el
            archivo no puede ser leído, crea un objeto de configuración predeterminado y lo escribe en el
            archivo "config.conf".
        *# CVEs Patch & Dependencies
            La clase depende de las librerías "Newtonsoft.Json" y "System.IO".
            Una posible vulnerabilidad es que un usuario malintencionado puede modificar el archivo
            "config.conf" para cambiar la configuración del programa. Para mitigar este riesgo, se deben
            aplicar medidas de seguridad adecuadas, como la validación de la entrada de datos y la
            restricción de permisos de archivo.
         */
        // Función para leer los valores de configuración guardados en el archivo config.conf
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
            // Si no se puede leer el archivo config.conf, crea un objeto de tipo Config con valores predeterminados y lo escribe en el archivo config.conf
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
            *#   _____      _   _   _                   _                     _ 
            *#  / ____|    | | | | (_)                 | |                   | |
            *# | (___   ___| |_| |_ _ _ __   __ _ ___  | |     ___   __ _  __| |
            *#  \___ \ / _ \ __| __| | '_ \ / _` / __| | |    / _ \ / _` |/ _` |
            *#  ____) |  __/ |_| |_| | | | | (_| \__ \ | |___| (_) | (_| | (_| |
            *# |_____/ \___|\__|\__|_|_| |_|\__, |___/_|______\___/ \__,_|\__,_|
            *#                               __/ | |______|                        
            *#                              |___/ 
            #------------------------------------------------------------------------------------   
            *# Name: Settings_Load
            *# params: [NO]
            *# Purpose:
                Este método se ejecuta cuando se carga el formulario. Carga los valores de configuración
                desde el archivo config.conf y actualiza los valores de las listas de correos electrónicos.
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función se llama cuando se carga la ventana de configuración. Primero llama a la función "ReadConfig"
                que lee los valores de configuración desde el archivo "config.conf" y los muestra en los controles de la
                interfaz gráfica. Luego carga los valores de la lista de correos electrónicos CC y CCO en las tablas
                correspondientes en la interfaz gráfica.

            *# CVEs Patch & Dependencies
                La clase depende de las librerías "Newtonsoft.Json" y "System.Windows.Forms".
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
         */
        private void Settings_Load(object sender, EventArgs e)
        {
            // Carga los valores de configuración desde el archivo config.conf
            ReadConfig();
            lista_cc = config.EmailsCC;
            lista_cco = config.EmailsCCO;
            grid_CC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid_CCO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid_CC.DataSource= lista_cc;
            grid_CCO.DataSource= lista_cco;



        }
        /*
        #=====================================================================================
        *#      _     _                                   _ 
        *#     | |   | |                                 | |
        *#     | |__ | |_ _ __    ___ __ _ _ __   ___ ___| |
        *#     | '_ \| __| '_ \  / __/ _` | '_ \ / __/ _ \ |
        *#     | |_) | |_| | | || (_| (_| | | | | (_|  __/ |
        *#     |_.__/ \__|_| |_|_\___\__,_|_| |_|\___\___|_|                       
        *#                  |______|                        
        #------------------------------------------------------------------------------------
        *# Name: btn_Cancel
        *# params: [NO]
        *# Purpose:
            Este método se encarga de cerrar la ventana actual de configuración.
        #------------------------------------------------------------------------------------
        *# Created: 11/01/2023
        *# Author: J0rd1s3rr4n0
        #----------------------------------
        *# Last Update: 11/01/2023
        *# Update Author: J0rd1s3rr4n0
        #------------------------------------------------------------------------------------
        *# Dev How it works
            Esta función se llama cuando se carga la ventana de configuración. Primero llama a la función "ReadConfig"
            que lee los valores de configuración desde el archivo "config.conf" y los muestra en los controles de la
            interfaz gráfica. Luego carga los valores de la lista de correos electrónicos CC y CCO en las tablas
            correspondientes en la interfaz gráfica.

        *# CVEs Patch & Dependencies
            La clase depende de las librerías "Newtonsoft.Json" y "System.Windows.Forms".
            No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.

         */
        /*
         * 
         * Este método se encarga de cerrar la ventana actual de configuración.
         * 
         *# Dev How it works
            Esta función se llama cuando se hace clic en el botón "Cancelar" en la ventana de configuración.
            Simplemente cierra la ventana sin hacer nada más.
        *# CVEs Patch & Dependencies
            No hay dependencias ni vulnerabilidadesrelacionadas con esta función.
            No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
         */
        private void btn_Cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
            #=====================================================================================
            *#   _____                _ ______                 _ _ 
            *#  / ____|              | |  ____|               (_) |
            *# | (___   ___ _ __   __| | |__   _ __ ___   __ _ _| |
            *#  \___ \ / _ \ '_ \ / _` |  __| | '_ ` _ \ / _` | | |
            *#  ____) |  __/ | | | (_| | |____| | | | | | (_| | | |
            *# |_____/ \___|_| |_|\__,_|______|_| |_| |_|\__,_|_|_|
            #------------------------------------------------------------------------------------
            *# Name: SendEmail
            *# params: [subject, body]
            *# Purpose:
                Este método se encarga de enviar un correo electrónico con los valores ingresados por el usuario.
            #------------------------------------------------------------------------------------
            *# Created: 15/02/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 15/02/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función se llama cuando se hace clic en el botón "Enviar" en la ventana de correo electrónico.
                Primero muestra el estado del correo electrónico como "Pendiente". Luego, verifica que se haya ingresado
                una dirección de correo electrónico válida para el destinatario y, en caso afirmativo, crea un objeto MailMessage
                con los valores ingresados en el formulario. A continuación, se configura un objeto SmtpClient con la dirección
                del servidor SMTP y los valores de autenticación y SSL. Finalmente, se envía el correo electrónico y se actualiza
                el estado a "Enviado". Si se produce un error durante el envío del correo electrónico, se actualiza el estado
                con un mensaje de error apropiado.
            *# CVEs Patch & Dependencies
                La clase depende de las librerías "System.Net", "System.Net.Mail" y "System.Windows.Forms".
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
        */

        // Función para enviar el email
        void SendEmail(string subject, string body)
        {
            // Muestra el estado del email como "Pendiente"
            tv_estado.Text = "Pendiente";
            if ((tb_emailToTest.Text.ToString().Length > 0) && (tb_emailToTest != null))
            {
                try
                {
                    // Crea un objeto de tipo MailMessage con los valores del formulario
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
        /*
            #=====================================================================================
            *#  _     _           _                            _             
            *# | |   | |         (_)                          | |            
            *# | |__ | |_ _ __    _ _ __ ___  _ __   ___  _ __| |_   ___ ___ 
            *# | '_ \| __| '_ \  | | '_ ` _ \| '_ \ / _ \| '__| __| / __/ __|
            *# | |_) | |_| | | | | | | | | | | |_) | (_) | |  | |_ | (_| (__ 
            *# |_.__/ \__|_| |_|_|_|_| |_| |_| .__/ \___/|_|   \__|_\___\___|
            *#             |______|          | |                |______|           
            *#                               |_|    
            #------------------------------------------------------------------------------------
            *# Name: btn_import_cc
            *# params: [sender, e]
            *# Purpose:
                Esta función se encarga de importar una lista de correos electrónicos CC en formato CSV y
                cargarlos en una tabla de la interfaz gráfica.
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función se llama cuando se hace clic en el botón "Importar lista de CC" en la ventana de configuración.
                Abre un cuadro de diálogo para que el usuario seleccione el archivo CSV que contiene la lista de correos electrónicos CC.
                Lee los valores del archivo CSV y los carga en una tabla de la interfaz gráfica.

            *# CVEs Patch & Dependencies
                No hay dependencias relacionadas con esta función.
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
        */
        private void btn_import_cc(object sender, EventArgs e)
        {
            lista_cc = new ArrayList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    lista_cc.Add(new email
                    {
                        Email = fields[0],
                    });
                }
                grid_CC.DataSource = lista_cc;
            }
        }

        /*
            #=====================================================================================
            *#  _     _           _____                            _                  
            *# | |   | |         |_   _|                          | |                 
            *# | |__ | |_ _ __     | |  _ __ ___  _ __   ___  _ __| |_   ___ ___ ___  
            *# | '_ \| __| '_ \    | | | '_ ` _ \| '_ \ / _ \| '__| __| / __/ __/ _ \ 
            *# | |_) | |_| | | |  _| |_| | | | | | |_) | (_) | |  | |_ | (_| (_| (_) |
            *# |_.__/ \__|_| |_|_|_____|_| |_| |_| .__/ \___/|_|   \__|_\___\___\___/ 
            *#              |______|             | |                |______|           
            *#                                   |_|               
            #------------------------------------------------------------------------------------
            *# Name: btn_Import_cco
            *# params: [sender, e]
            *# Purpose:
                Esta función se encarga de importar una lista de correos electrónicos CCO en formato CSV y
                cargarlos en una tabla de la interfaz gráfica.
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función se llama cuando se hace clic en el botón "Importar lista de CCO" en la ventana de configuración.
                Abre un cuadro de diálogo para que el usuario seleccione el archivo CSV que contiene la lista de correos electrónicos CCO.
                Lee los valores del archivo CSV y los carga en una tabla de la interfaz gráfica.

            *# CVEs Patch & Dependencies
                No hay dependencias relacionadas con esta función.
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
        */
        private void btn_Import_cco(object sender, EventArgs e)
        {
            lista_cco = new ArrayList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    lista_cco.Add(new email
                    {
                        Email = fields[0],
                    });
                }
                grid_CCO.DataSource = lista_cco;
            }
        }
        /*
            #=====================================================================================
            *#    _______        _   ______                 _ _ 
            *#   |__   __|      | | |  ____|               (_) |
            *#      | | ___  ___| |_| |__   _ __ ___   __ _ _| |
            *#      | |/ _ \/ __| __|  __| | '_ ` _ \ / _` | | |
            *#      | |  __/\__ \ |_| |____| | | | | | (_| | | |
            *#      |_|\___||___/\__|______|_| |_| |_|\__,_|_|_|
            #------------------------------------------------------------------------------------
            *# Name: TestEmail
            *# params: [NO]
            *# Purpose:
                Este método se encarga de enviar un email de prueba al destinatario especificado en el campo
                "tb_emailToTest" usando los valores de configuración ingresados por el usuario.
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función se llama cuando se hace clic en el botón "Probar email" en la ventana de configuración.
                Llama a la función "SendEmail" para enviar un email de prueba al destinatario especificado en el campo "tb_emailToTest".
            *# CVEs Patch & Dependencies
                La clase depende de las librerías "System.Net.Mail" y "System.Net".
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
        */
        private async void TestEmail(object sender, EventArgs e)
        {
            SendEmail("EMAIL DE TEST", "Esto es un email de test.");
        }
        /*
            #=====================================================================================
            *#    _      _                 _             _____      _     _  _____ ______
            *#   | |    (_)               (_)           / ____|    (_)   | |/ ____/ ____/
            *#   | |     _ _ __ ___  _ __  _  __ _ _ __| |  __ _ __ _  __| | |   | |   
            *#   | |    | | '_ ` _ \| '_ \| |/ _` | '__| | |_ | '__| |/ _` | |   | |   
            *#   | |____| | | | | | | |_) | | (_| | |  | |__| | |  | | (_| | |___| |____ 
            *#   |______|_|_| |_| |_| .__/|_|\__,_|_|   \_____|_|  |_|\__,_|\_____\_____\
            *#                      | |                                                        
            *#                      |_|                                                        
            #------------------------------------------------------------------------------------
            *# Name: LimpiarGridCC
            *# params: [NO]
            *# Purpose:
                Este método se encarga de borrar los datos de la lista de correos electrónicos CC.
            #------------------------------------------------------------------------------------
            *# Created: 11/01/2023
            *# Author: J0rd1s3rr4n0
            #----------------------------------
            *# Last Update: 11/01/2023
            *# Update Author: J0rd1s3rr4n0
            #------------------------------------------------------------------------------------
            *# Dev How it works
                Esta función se llama cuando se hace clic en el botón "Limpiar" en la sección "CC" de la ventana de configuración.
                Borra los datos de la lista "lista_cc" y actualiza la tabla "grid_CC" en la interfaz gráfica.
            *# CVEs Patch & Dependencies
                No hay dependencias ni vulnerabilidades relacionadas con esta función.
                No se han encontrado vulnerabilidades (CVEs) y no hay parches disponibles.
        */

        private void LimpiarGridCC(object sender, EventArgs e)
        {
            lista_cc = new ArrayList();
            grid_CC.DataSource = lista_cc;
        }
        /*
            #=====================================================================================
            *#    _      _                 _             _____      _     _  _____ _____ ____  
            *#   | |    (_)               (_)           / ____|    (_)   | |/ ____/ ____/ __ \ 
            *#   | |     _ _ __ ___  _ __  _  __ _ _ __| |  __ _ __ _  __| | |   | |   | |  | |
            *#   | |    | | '_ ` _ \| '_ \| |/ _` | '__| | |_ | '__| |/ _` | |   | |   | |  | |
            *#   | |____| | | | | | | |_) | | (_| | |  | |__| | |  | | (_| | |___| |___| |__| |
            *#   |______|_|_| |_| |_| .__/|_|\__,_|_|   \_____|_|  |_|\__,_|\_____\_____\____/ 
            *#                      | |                                                        
            *#                      |_|                                                        
            #------------------------------------------------------------------------------------
            *# Name: LimpiarGridCCO
            *# params: [NO]
            *# Purpose:
                Este método se encarga de borrar los datos de la lista de correos electrónicos CCO.
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

        private void LimpiarGridCCO(object sender, EventArgs e)
        {
            lista_cco = new ArrayList();
            grid_CCO.DataSource = lista_cco;

        }
    }
}
