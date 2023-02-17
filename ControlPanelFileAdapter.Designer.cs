namespace EnviadorEmails
{
    partial class ControlPanelFileAdapter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanelFileAdapter));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.grid_oldNames = new System.Windows.Forms.DataGridView();
            this.grid_newNames = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Lb_Estado = new System.Windows.Forms.Label();
            this.lb_estado_actual = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid_oldNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_newNames)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(11, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lista de Nombres Actuales";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(690, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Lista de Nombres Nuevos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label4.Location = new System.Drawing.Point(368, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(412, 39);
            this.label4.TabIndex = 5;
            this.label4.Text = "Renombrador de Archivos";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 40.8F);
            this.button1.Location = new System.Drawing.Point(530, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 100);
            this.button1.TabIndex = 6;
            this.button1.Text = "→";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_ChangeNames);
            // 
            // grid_oldNames
            // 
            this.grid_oldNames.AllowUserToAddRows = false;
            this.grid_oldNames.AllowUserToDeleteRows = false;
            this.grid_oldNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_oldNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_oldNames.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grid_oldNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_oldNames.GridColor = System.Drawing.SystemColors.Control;
            this.grid_oldNames.Location = new System.Drawing.Point(16, 126);
            this.grid_oldNames.Name = "grid_oldNames";
            this.grid_oldNames.ReadOnly = true;
            this.grid_oldNames.RowHeadersVisible = false;
            this.grid_oldNames.RowHeadersWidth = 51;
            this.grid_oldNames.RowTemplate.Height = 24;
            this.grid_oldNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid_oldNames.Size = new System.Drawing.Size(454, 347);
            this.grid_oldNames.TabIndex = 7;
            // 
            // grid_newNames
            // 
            this.grid_newNames.AllowUserToAddRows = false;
            this.grid_newNames.AllowUserToDeleteRows = false;
            this.grid_newNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_newNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_newNames.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grid_newNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_newNames.GridColor = System.Drawing.SystemColors.Control;
            this.grid_newNames.Location = new System.Drawing.Point(695, 126);
            this.grid_newNames.Name = "grid_newNames";
            this.grid_newNames.ReadOnly = true;
            this.grid_newNames.RowHeadersVisible = false;
            this.grid_newNames.RowHeadersWidth = 51;
            this.grid_newNames.RowTemplate.Height = 24;
            this.grid_newNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid_newNames.Size = new System.Drawing.Size(454, 347);
            this.grid_newNames.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(329, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 29);
            this.button2.TabIndex = 9;
            this.button2.Text = "Importar Lista";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnImportNamesOld);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(1008, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 29);
            this.button3.TabIndex = 10;
            this.button3.Text = "Importar Lista";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnImportNamesNew);
            // 
            // Lb_Estado
            // 
            this.Lb_Estado.AutoSize = true;
            this.Lb_Estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.Lb_Estado.Location = new System.Drawing.Point(476, 313);
            this.Lb_Estado.Name = "Lb_Estado";
            this.Lb_Estado.Size = new System.Drawing.Size(73, 24);
            this.Lb_Estado.TabIndex = 11;
            this.Lb_Estado.Text = "Estado:";
            // 
            // lb_estado_actual
            // 
            this.lb_estado_actual.AutoSize = true;
            this.lb_estado_actual.Location = new System.Drawing.Point(556, 320);
            this.lb_estado_actual.Name = "lb_estado_actual";
            this.lb_estado_actual.Size = new System.Drawing.Size(11, 16);
            this.lb_estado_actual.TabIndex = 12;
            this.lb_estado_actual.Text = "-";
            // 
            // ControlPanelFileAdapter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1171, 598);
            this.Controls.Add(this.lb_estado_actual);
            this.Controls.Add(this.Lb_Estado);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.grid_newNames);
            this.Controls.Add(this.grid_oldNames);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ControlPanelFileAdapter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ControlPanelFileAdapter";
            this.Load += new System.EventHandler(this.ControlPanelFileAdapter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_oldNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_newNames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView grid_oldNames;
        private System.Windows.Forms.DataGridView grid_newNames;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label Lb_Estado;
        private System.Windows.Forms.Label lb_estado_actual;
    }
}