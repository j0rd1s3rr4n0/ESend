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

namespace EnviadorEmails
{

    public partial class ControlPanelFileAdapter : Form
    {
        ArrayList nombres_nuevos;
        ArrayList nombres_antiguos;
        public string ruta; // = "C:\\test\\";
        public ControlPanelFileAdapter()
        {
            InitializeComponent();
        }

        private void btn_ChangeNames(object sender, EventArgs e)
        {
            if (ruta != null && ruta.Length > 0)
            {
                if(nombres_antiguos != null && nombres_nuevos != null) { 
                    if (nombres_antiguos.Count > 0 && nombres_nuevos.Count > 0 && (nombres_nuevos.Count == nombres_antiguos.Count))
                    {
                        for (int i = 0; i < nombres_antiguos.Count; i++)
                        {
                                string nombreAntiguo = (string)nombres_antiguos[i];
                                string nombreNuevo = (string)nombres_nuevos[i];
                                string rutaAntigua = Path.Combine(ruta, nombreAntiguo);
                            string rutaNueva = Path.Combine(ruta, nombreNuevo);

                            if (File.Exists(rutaAntigua))
                            {
                                try
                                {
                                    File.Move(rutaAntigua, rutaNueva);
                                    MessageBox.Show("El archivo '" + nombreAntiguo + "' ha sido renombrado a '" + nombreNuevo + "'");
                                }
                                catch (Exception err)
                                {
                                    MessageBox.Show(err.Message,"NO SE PUDOO CAMBIAR EL ARCHIVO");
                                }
                            }
                            else
                            {
                                MessageBox.Show("El archivo '" + nombreAntiguo + "' no existe en la ruta especificada");
                            }
                        }
                    }
                    else
                    {
                        if (nombres_antiguos.Count < 0)
                        {
                            MessageBox.Show("Listado Archivos Antiguos Vacio", "NO SE ENCONTRARON REGISTROS");
                        }
                        if (nombres_nuevos.Count < 0)
                        {
                            MessageBox.Show("Listado Archivos Nuevs Vacio", "NO SE ENCONTRARON REGISTROS");
                        }
                        if ((nombres_nuevos.Count != nombres_antiguos.Count))
                        {
                            MessageBox.Show("Para que sean compatibles deben tener el mismo numero de lineas.", "Archivos No Compatibles");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Se requiere importar las listas de nombres.", "NO SE ENCONTRARON REGISTROS");
                }
            }
            else
            {
                MessageBox.Show("Se requiere una ruta valida", "NO SE ENCONTRO LA RUTA");
            }
        }

        private void ControlPanelFileAdapter_Load(object sender, EventArgs e)
        {

        }

        private void btnImportNamesOld(object sender, EventArgs e)
        {
            try { 
                nombres_antiguos.Clear();
            }
            catch (Exception err)
            {
                for (int i = grid_oldNames.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = grid_oldNames.Rows[i];
                    if (!row.IsNewRow)
                    {
                        grid_oldNames.Rows.Remove(row);
                    }
                }
            }
            //personas.Clear();
            //personas = new ArrayList();
            //filePath = openFileDialog.FileName;
            nombres_antiguos = new ArrayList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Abrir Archivo de Nombres Actuales";
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    nombres_antiguos.Add(new filename
                    {
                        Name = fields[0],
                    });
                }
                grid_oldNames.DataSource = nombres_antiguos;
            }
        }

        private void btnImportNamesNew(object sender, EventArgs e)
        {
            try {
                nombres_nuevos.Clear();
            }
            catch (Exception err) {
                for (int i = grid_newNames.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = grid_newNames.Rows[i];
                    if (!row.IsNewRow)
                    {
                        grid_newNames.Rows.Remove(row);
                    }
                }
            }
            //personas.Clear();
            //personas = new ArrayList();
            //filePath = openFileDialog.FileName;
            nombres_nuevos = new ArrayList();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Abrir Archivo de Nombres Nuevos";
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    nombres_nuevos.Add(new filename
                    {
                        Name = fields[0],
                    });
                }
                grid_newNames.DataSource = nombres_nuevos;
            }
        }
    }
}
