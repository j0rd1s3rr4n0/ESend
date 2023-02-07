using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviadorEmails
{
    class Config
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Servidor { get; set; }
        public int Puerto { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public ArrayList EmailsCC { get; set; }
        public ArrayList EmailsCCO { get; set; }
    }
}
