﻿namespace NR_Resultat_Import
{
    partial class Form2
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LoadUsers = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSubmitResults = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.comboBoxStages = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(687, 404);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 422);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Event:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 422);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(319, 422);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(244, 56);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // LoadUsers
            // 
            this.LoadUsers.Location = new System.Drawing.Point(570, 422);
            this.LoadUsers.Name = "LoadUsers";
            this.LoadUsers.Size = new System.Drawing.Size(130, 23);
            this.LoadUsers.TabIndex = 4;
            this.LoadUsers.Text = "Synkroniser brukerliste";
            this.LoadUsers.UseVisualStyleBackColor = true;
            this.LoadUsers.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Location = new System.Drawing.Point(15, 700);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(471, 94);
            this.textBox1.TabIndex = 5;
            // 
            // btnSubmitResults
            // 
            this.btnSubmitResults.Location = new System.Drawing.Point(570, 455);
            this.btnSubmitResults.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSubmitResults.Name = "btnSubmitResults";
            this.btnSubmitResults.Size = new System.Drawing.Size(130, 23);
            this.btnSubmitResults.TabIndex = 6;
            this.btnSubmitResults.Text = "Last opp resultater";
            this.btnSubmitResults.UseVisualStyleBackColor = true;
            this.btnSubmitResults.Click += new System.EventHandler(this.btnSubmitResults_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // comboBoxStages
            // 
            this.comboBoxStages.FormattingEnabled = true;
            this.comboBoxStages.Location = new System.Drawing.Point(15, 438);
            this.comboBoxStages.Name = "comboBoxStages";
            this.comboBoxStages.Size = new System.Drawing.Size(298, 21);
            this.comboBoxStages.TabIndex = 7;
            this.comboBoxStages.Text = "Velg stage";
            this.comboBoxStages.Visible = false;
            this.comboBoxStages.SelectedIndexChanged += new System.EventHandler(this.comboBoxStages_SelectedIndexChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 492);
            this.Controls.Add(this.comboBoxStages);
            this.Controls.Add(this.btnSubmitResults);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LoadUsers);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button LoadUsers;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSubmitResults;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox comboBoxStages;
    }
}