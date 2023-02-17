namespace EnviadorEmails
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSendMail = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.grid_datos = new System.Windows.Forms.DataGridView();
            this.fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gb_estadisticas = new System.Windows.Forms.GroupBox();
            this.lb_countCCO = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_countCC = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tv_TiempoEspera = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tv_NumErrores = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tv_NumPendientes = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tv_NumEnviados = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_requireFile = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_AdaptarArchivos = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_fileNameData = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tb_FolderFile = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tv_emailEnUso = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid_datos)).BeginInit();
            this.gb_estadisticas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSendMail
            // 
            resources.ApplyResources(this.btnSendMail, "btnSendMail");
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btn_SendEmails);
            // 
            // btnConfig
            // 
            resources.ApplyResources(this.btnConfig, "btnConfig");
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btn_Confg);
            // 
            // grid_datos
            // 
            this.grid_datos.AllowUserToAddRows = false;
            this.grid_datos.AllowUserToDeleteRows = false;
            this.grid_datos.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.grid_datos, "grid_datos");
            this.grid_datos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid_datos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grid_datos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grid_datos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid_datos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grid_datos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.grid_datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_datos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fullname,
            this.email,
            this.file});
            this.grid_datos.GridColor = System.Drawing.SystemColors.Control;
            this.grid_datos.MultiSelect = false;
            this.grid_datos.Name = "grid_datos";
            this.grid_datos.ReadOnly = true;
            this.grid_datos.RowTemplate.Height = 24;
            // 
            // fullname
            // 
            this.fullname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.fullname, "fullname");
            this.fullname.Name = "fullname";
            this.fullname.ReadOnly = true;
            // 
            // email
            // 
            this.email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.email, "email");
            this.email.Name = "email";
            this.email.ReadOnly = true;
            // 
            // file
            // 
            this.file.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.file, "file");
            this.file.Name = "file";
            this.file.ReadOnly = true;
            // 
            // gb_estadisticas
            // 
            resources.ApplyResources(this.gb_estadisticas, "gb_estadisticas");
            this.gb_estadisticas.BackColor = System.Drawing.Color.Transparent;
            this.gb_estadisticas.Controls.Add(this.lb_countCCO);
            this.gb_estadisticas.Controls.Add(this.label3);
            this.gb_estadisticas.Controls.Add(this.lb_countCC);
            this.gb_estadisticas.Controls.Add(this.label11);
            this.gb_estadisticas.Controls.Add(this.tv_TiempoEspera);
            this.gb_estadisticas.Controls.Add(this.label4);
            this.gb_estadisticas.Controls.Add(this.tv_NumErrores);
            this.gb_estadisticas.Controls.Add(this.label16);
            this.gb_estadisticas.Controls.Add(this.tv_NumPendientes);
            this.gb_estadisticas.Controls.Add(this.label14);
            this.gb_estadisticas.Controls.Add(this.tv_NumEnviados);
            this.gb_estadisticas.Controls.Add(this.label1);
            this.gb_estadisticas.Name = "gb_estadisticas";
            this.gb_estadisticas.TabStop = false;
            this.gb_estadisticas.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lb_countCCO
            // 
            resources.ApplyResources(this.lb_countCCO, "lb_countCCO");
            this.lb_countCCO.Name = "lb_countCCO";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lb_countCC
            // 
            resources.ApplyResources(this.lb_countCC, "lb_countCC");
            this.lb_countCC.Name = "lb_countCC";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // tv_TiempoEspera
            // 
            resources.ApplyResources(this.tv_TiempoEspera, "tv_TiempoEspera");
            this.tv_TiempoEspera.Name = "tv_TiempoEspera";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tv_NumErrores
            // 
            resources.ApplyResources(this.tv_NumErrores, "tv_NumErrores");
            this.tv_NumErrores.ForeColor = System.Drawing.Color.Red;
            this.tv_NumErrores.Name = "tv_NumErrores";
            this.tv_NumErrores.Click += new System.EventHandler(this.tv_NumErrores_Click);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // tv_NumPendientes
            // 
            resources.ApplyResources(this.tv_NumPendientes, "tv_NumPendientes");
            this.tv_NumPendientes.Name = "tv_NumPendientes";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // tv_NumEnviados
            // 
            resources.ApplyResources(this.tv_NumEnviados, "tv_NumEnviados");
            this.tv_NumEnviados.ForeColor = System.Drawing.Color.Lime;
            this.tv_NumEnviados.Name = "tv_NumEnviados";
            this.tv_NumEnviados.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.grid_datos);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cb_requireFile);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btn_AdaptarArchivos);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tb_fileNameData);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.tb_FolderFile);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // cb_requireFile
            // 
            resources.ApplyResources(this.cb_requireFile, "cb_requireFile");
            this.cb_requireFile.Checked = true;
            this.cb_requireFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_requireFile.Name = "cb_requireFile";
            this.cb_requireFile.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // btn_AdaptarArchivos
            // 
            resources.ApplyResources(this.btn_AdaptarArchivos, "btn_AdaptarArchivos");
            this.btn_AdaptarArchivos.Name = "btn_AdaptarArchivos";
            this.btn_AdaptarArchivos.UseVisualStyleBackColor = true;
            this.btn_AdaptarArchivos.Click += new System.EventHandler(this.btn_FileNameAdapter);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // tb_fileNameData
            // 
            resources.ApplyResources(this.tb_fileNameData, "tb_fileNameData");
            this.tb_fileNameData.Name = "tb_fileNameData";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_ReadFile);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btn_SelFolder);
            // 
            // tb_FolderFile
            // 
            resources.ApplyResources(this.tb_FolderFile, "tb_FolderFile");
            this.tb_FolderFile.Name = "tb_FolderFile";
            this.tb_FolderFile.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::EnviadorEmails.Properties.Resources.settings_icon_gear_3d_render_png;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.btn_ConfigImage);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::EnviadorEmails.Properties.Resources.soporte1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.btn_AssistenciaImage);
            // 
            // tv_emailEnUso
            // 
            resources.ApplyResources(this.tv_emailEnUso, "tv_emailEnUso");
            this.tv_emailEnUso.Name = "tv_emailEnUso";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btn_STOP);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tv_emailEnUso);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_estadisticas);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnSendMail);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_datos)).EndInit();
            this.gb_estadisticas.ResumeLayout(false);
            this.gb_estadisticas.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSendMail;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.DataGridView grid_datos;
        private System.Windows.Forms.GroupBox gb_estadisticas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label tv_NumEnviados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tv_NumErrores;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label tv_NumPendientes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_FolderFile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label tv_TiempoEspera;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_AdaptarArchivos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_fileNameData;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label tv_emailEnUso;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn file;
        private System.Windows.Forms.Label lb_countCCO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_countCC;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cb_requireFile;
    }
}

