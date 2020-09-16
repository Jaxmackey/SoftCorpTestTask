namespace UIApp
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxCities = new System.Windows.Forms.ComboBox();
            this.DayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxTempC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxTempF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinTempC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinTempF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WindMs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KmH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCitiesData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCitiesData)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxCities
            // 
            this.comboBoxCities.FormattingEnabled = true;
            this.comboBoxCities.Location = new System.Drawing.Point(13, 13);
            this.comboBoxCities.Name = "comboBoxCities";
            this.comboBoxCities.Size = new System.Drawing.Size(146, 23);
            this.comboBoxCities.TabIndex = 0;
            this.comboBoxCities.SelectedIndexChanged += new System.EventHandler(this.comboBoxCities_SelectedIndexChanged);
            // 
            // DayName
            // 
            this.DayName.HeaderText = "DayName";
            this.DayName.Name = "DayName";
            // 
            // DayNumber
            // 
            this.DayNumber.HeaderText = "DayNumber";
            this.DayNumber.Name = "DayNumber";
            // 
            // MaxTempC
            // 
            this.MaxTempC.HeaderText = "MaxTempC";
            this.MaxTempC.Name = "MaxTempC";
            // 
            // MaxTempF
            // 
            this.MaxTempF.HeaderText = "MaxTempF";
            this.MaxTempF.Name = "MaxTempF";
            // 
            // MinTempC
            // 
            this.MinTempC.HeaderText = "MinTempC";
            this.MinTempC.Name = "MinTempC";
            // 
            // MinTempF
            // 
            this.MinTempF.HeaderText = "MinTempF";
            this.MinTempF.Name = "MinTempF";
            // 
            // WindMs
            // 
            this.WindMs.HeaderText = "WindMs";
            this.WindMs.Name = "WindMs";
            // 
            // MiH
            // 
            this.MiH.HeaderText = "MiH";
            this.MiH.Name = "MiH";
            // 
            // KmH
            // 
            this.KmH.HeaderText = "KmH";
            this.KmH.Name = "KmH";
            // 
            // Prec
            // 
            this.Prec.HeaderText = "Prec";
            this.Prec.Name = "Prec";
            // 
            // dataGridViewCitiesData
            // 
            this.dataGridViewCitiesData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCitiesData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DayName,
            this.DayNumber,
            this.MaxTempC,
            this.MaxTempF,
            this.MinTempC,
            this.MinTempF,
            this.WindMs,
            this.MiH,
            this.KmH,
            this.Prec});
            this.dataGridViewCitiesData.Location = new System.Drawing.Point(13, 53);
            this.dataGridViewCitiesData.Name = "dataGridViewCitiesData";
            this.dataGridViewCitiesData.Size = new System.Drawing.Size(1038, 446);
            this.dataGridViewCitiesData.TabIndex = 1;
            this.dataGridViewCitiesData.Text = "dataGridView1";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 511);
            this.Controls.Add(this.dataGridViewCitiesData);
            this.Controls.Add(this.comboBoxCities);
            this.Name = "MainView";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCitiesData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCities;
        private System.Windows.Forms.DataGridView dataGridViewCitiesData;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxTempC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxTempF;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinTempC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinTempF;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindMs;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiH;
        private System.Windows.Forms.DataGridViewTextBoxColumn KmH;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prec;
    }
}

