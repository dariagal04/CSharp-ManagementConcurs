using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace App
{
    partial class OficiuLoggedIN : Form
    {
        private IContainer components = null;

        private DataGridView dataGridView1;
        private Label persoanaOficiuName;
        private Button logOutBtn;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void LoadProbaListBox()
        {
            probaListBox.Items.Clear();
            var probe = _numeProbaService.FindAll(); // Presupunem că returnează o listă de obiecte Proba

            foreach (var proba in probe)
            {
                // Poți afișa ID-ul și numele pentru claritate
                probaListBox.Items.Add($"{proba.Id}");
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.dataGridView1 = new DataGridView();
            this.persoanaOficiuName = new Label();
            this.logOutBtn = new Button();
            this.searchBox = new TextBox();
            this.searchBtn = new Button();
            this.nameTextBox = new TextBox();
            this.cnpTextBox = new TextBox();
            this.probaListBox = new ListBox();
            this.varstaTextBox = new TextBox();
            this.categorieListBox = new ListBox();
            this.inscriereBtn = new Button();

            this.SuspendLayout();

            // dataGridView1
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(460, 200);
            this.dataGridView1.TabIndex = 0;

            // searchBox
            this.searchBox.Location = new System.Drawing.Point(12, 220);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(300, 20);
            this.searchBox.TabIndex = 1;

            // searchBtn
            this.searchBtn.Location = new System.Drawing.Point(320, 218);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(100, 25);
            this.searchBtn.TabIndex = 2;
            this.searchBtn.Text = "Căutare";
            this.searchBtn.UseVisualStyleBackColor = true;

            // nameTextBox
            this.nameTextBox.Location = new System.Drawing.Point(12, 260);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(300, 20);
            this.nameTextBox.TabIndex = 3;

            // cnpTextBox
            this.cnpTextBox.Location = new System.Drawing.Point(12, 300);
            this.cnpTextBox.Name = "cnpTextBox";
            this.cnpTextBox.Size = new System.Drawing.Size(300, 20);
            this.cnpTextBox.TabIndex = 4;

            // probaListBox
            this.probaListBox.Location = new System.Drawing.Point(12, 340);
            this.probaListBox.Name = "probaListBox";
            this.probaListBox.Size = new System.Drawing.Size(300, 80);
            this.probaListBox.TabIndex = 5;

            // varstaTextBox
            this.varstaTextBox.Location = new System.Drawing.Point(12, 430);
            this.varstaTextBox.Name = "varstaTextBox";
            this.varstaTextBox.Size = new System.Drawing.Size(300, 20);
            this.varstaTextBox.TabIndex = 6;

            // categorieListBox
            this.categorieListBox.Location = new System.Drawing.Point(320, 260);
            this.categorieListBox.Name = "categorieListBox";
            this.categorieListBox.Size = new System.Drawing.Size(150, 80);
            this.categorieListBox.TabIndex = 7;
            this.categorieListBox.Items.AddRange(new object[] { "1", "2", "3" });

            // inscriereBtn
            this.inscriereBtn.Location = new System.Drawing.Point(320, 380);
            this.inscriereBtn.Name = "inscriereBtn";
            this.inscriereBtn.Size = new System.Drawing.Size(100, 30);
            this.inscriereBtn.TabIndex = 8;
            this.inscriereBtn.Text = "Înscriere";
            this.inscriereBtn.UseVisualStyleBackColor = true;

            // persoanaOficiuName
            this.persoanaOficiuName.AutoSize = true;
            this.persoanaOficiuName.Location = new System.Drawing.Point(12, 460);
            this.persoanaOficiuName.Name = "persoanaOficiuName";
            this.persoanaOficiuName.Size = new System.Drawing.Size(150, 13);
            this.persoanaOficiuName.TabIndex = 9;
            this.persoanaOficiuName.Text = "Bine ai venit!";

            // logOutBtn
            this.logOutBtn.Location = new System.Drawing.Point(380, 455);
            this.logOutBtn.Name = "logOutBtn";
            this.logOutBtn.Size = new System.Drawing.Size(90, 30);
            this.logOutBtn.TabIndex = 10;
            this.logOutBtn.Text = "Log Out";
            this.logOutBtn.UseVisualStyleBackColor = true;

            // OficiuLoggedIN Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 501);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.cnpTextBox);
            this.Controls.Add(this.probaListBox);
            this.Controls.Add(this.varstaTextBox);
            this.Controls.Add(this.categorieListBox);
            this.Controls.Add(this.inscriereBtn);
            this.Controls.Add(this.persoanaOficiuName);
            this.Controls.Add(this.logOutBtn);
            this.Name = "OficiuLoggedIN";
            this.Text = "Oficiu - Statistici";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
