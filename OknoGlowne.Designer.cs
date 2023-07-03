
namespace RobotRysujacyLinie
{
    partial class OknoGlowne
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxBoisko = new System.Windows.Forms.PictureBox();
            this.comboBoxTypBoiska = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTrybPracy = new System.Windows.Forms.ComboBox();
            this.buttonLogi = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonAnuluj = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelBateria = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxRysowanie = new System.Windows.Forms.ComboBox();
            this.labelPozycja = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelKrok = new System.Windows.Forms.Label();
            this.numericUpDownKrok = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.labelProcent = new System.Windows.Forms.Label();
            this.buttonSet1 = new System.Windows.Forms.Button();
            this.labelPozycjaRobota = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoisko)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKrok)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBoisko
            // 
            this.pictureBoxBoisko.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBoxBoisko.Location = new System.Drawing.Point(786, 12);
            this.pictureBoxBoisko.Name = "pictureBoxBoisko";
            this.pictureBoxBoisko.Size = new System.Drawing.Size(466, 657);
            this.pictureBoxBoisko.TabIndex = 0;
            this.pictureBoxBoisko.TabStop = false;
            this.pictureBoxBoisko.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBoisko_MouseClick);
            // 
            // comboBoxTypBoiska
            // 
            this.comboBoxTypBoiska.AccessibleDescription = "";
            this.comboBoxTypBoiska.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBoxTypBoiska.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypBoiska.Enabled = false;
            this.comboBoxTypBoiska.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxTypBoiska.FormattingEnabled = true;
            this.comboBoxTypBoiska.Location = new System.Drawing.Point(140, 159);
            this.comboBoxTypBoiska.Name = "comboBoxTypBoiska";
            this.comboBoxTypBoiska.Size = new System.Drawing.Size(167, 28);
            this.comboBoxTypBoiska.TabIndex = 1;
            this.comboBoxTypBoiska.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypBoiska_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(45, 597);
            this.progressBar1.Maximum = 6695;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(690, 38);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 2;
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStart.Location = new System.Drawing.Point(46, 508);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(98, 39);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(42, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Typ boiska:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(40, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "Menu opcji";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(42, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tryb pracy:";
            // 
            // comboBoxTrybPracy
            // 
            this.comboBoxTrybPracy.AccessibleDescription = "";
            this.comboBoxTrybPracy.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBoxTrybPracy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTrybPracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxTrybPracy.FormattingEnabled = true;
            this.comboBoxTrybPracy.Location = new System.Drawing.Point(140, 112);
            this.comboBoxTrybPracy.Name = "comboBoxTrybPracy";
            this.comboBoxTrybPracy.Size = new System.Drawing.Size(167, 28);
            this.comboBoxTrybPracy.TabIndex = 7;
            this.comboBoxTrybPracy.SelectedIndexChanged += new System.EventHandler(this.comboBoxTrybPracy_SelectedIndexChanged);
            // 
            // buttonLogi
            // 
            this.buttonLogi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonLogi.Location = new System.Drawing.Point(594, 508);
            this.buttonLogi.Name = "buttonLogi";
            this.buttonLogi.Size = new System.Drawing.Size(141, 39);
            this.buttonLogi.TabIndex = 8;
            this.buttonLogi.Text = "Logi (sterowanie)";
            this.buttonLogi.UseVisualStyleBackColor = true;
            this.buttonLogi.Click += new System.EventHandler(this.buttonLogi_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStop.Location = new System.Drawing.Point(194, 508);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(98, 39);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Visible = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonAnuluj
            // 
            this.buttonAnuluj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonAnuluj.Location = new System.Drawing.Point(344, 508);
            this.buttonAnuluj.Name = "buttonAnuluj";
            this.buttonAnuluj.Size = new System.Drawing.Size(135, 39);
            this.buttonAnuluj.TabIndex = 10;
            this.buttonAnuluj.Text = "Anuluj rysowanie";
            this.buttonAnuluj.UseVisualStyleBackColor = true;
            this.buttonAnuluj.Visible = false;
            this.buttonAnuluj.Click += new System.EventHandler(this.buttonAnuluj_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(590, 476);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Bateria:";
            // 
            // labelBateria
            // 
            this.labelBateria.AutoSize = true;
            this.labelBateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBateria.Location = new System.Drawing.Point(660, 476);
            this.labelBateria.Name = "labelBateria";
            this.labelBateria.Size = new System.Drawing.Size(70, 20);
            this.labelBateria.TabIndex = 12;
            this.labelBateria.Text = "procent";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(43, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Rysowanie:";
            // 
            // comboBoxRysowanie
            // 
            this.comboBoxRysowanie.AccessibleDescription = "";
            this.comboBoxRysowanie.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBoxRysowanie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRysowanie.Enabled = false;
            this.comboBoxRysowanie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxRysowanie.FormattingEnabled = true;
            this.comboBoxRysowanie.Location = new System.Drawing.Point(140, 204);
            this.comboBoxRysowanie.Name = "comboBoxRysowanie";
            this.comboBoxRysowanie.Size = new System.Drawing.Size(167, 28);
            this.comboBoxRysowanie.TabIndex = 14;
            this.comboBoxRysowanie.SelectedIndexChanged += new System.EventHandler(this.comboBoxRysowanie_SelectedIndexChanged);
            // 
            // labelPozycja
            // 
            this.labelPozycja.AutoSize = true;
            this.labelPozycja.Location = new System.Drawing.Point(700, 656);
            this.labelPozycja.Name = "labelPozycja";
            this.labelPozycja.Size = new System.Drawing.Size(44, 13);
            this.labelPozycja.TabIndex = 15;
            this.labelPozycja.Text = "Pozycja";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(398, 112);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(337, 290);
            this.listBox1.TabIndex = 17;
            this.listBox1.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 554);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Krok:";
            // 
            // labelKrok
            // 
            this.labelKrok.AutoSize = true;
            this.labelKrok.Location = new System.Drawing.Point(86, 554);
            this.labelKrok.Name = "labelKrok";
            this.labelKrok.Size = new System.Drawing.Size(13, 13);
            this.labelKrok.TabIndex = 19;
            this.labelKrok.Text = "0";
            this.labelKrok.TextChanged += new System.EventHandler(this.labelKrok_TextChanged);
            // 
            // numericUpDownKrok
            // 
            this.numericUpDownKrok.Location = new System.Drawing.Point(194, 474);
            this.numericUpDownKrok.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownKrok.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownKrok.Name = "numericUpDownKrok";
            this.numericUpDownKrok.Size = new System.Drawing.Size(65, 20);
            this.numericUpDownKrok.TabIndex = 20;
            this.numericUpDownKrok.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownKrok.ValueChanged += new System.EventHandler(this.numericUpDownKrok_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 476);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Prędkość (ustawienie kroku):";
            // 
            // labelProcent
            // 
            this.labelProcent.AutoSize = true;
            this.labelProcent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelProcent.Location = new System.Drawing.Point(377, 608);
            this.labelProcent.Name = "labelProcent";
            this.labelProcent.Size = new System.Drawing.Size(53, 16);
            this.labelProcent.TabIndex = 22;
            this.labelProcent.Text = "procent";
            // 
            // buttonSet1
            // 
            this.buttonSet1.Location = new System.Drawing.Point(265, 474);
            this.buttonSet1.Name = "buttonSet1";
            this.buttonSet1.Size = new System.Drawing.Size(16, 20);
            this.buttonSet1.TabIndex = 23;
            this.buttonSet1.Text = "1";
            this.buttonSet1.UseVisualStyleBackColor = true;
            this.buttonSet1.Click += new System.EventHandler(this.buttonSet1_Click);
            // 
            // labelPozycjaRobota
            // 
            this.labelPozycjaRobota.AutoSize = true;
            this.labelPozycjaRobota.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPozycjaRobota.Location = new System.Drawing.Point(394, 435);
            this.labelPozycjaRobota.Name = "labelPozycjaRobota";
            this.labelPozycjaRobota.Size = new System.Drawing.Size(66, 20);
            this.labelPozycjaRobota.TabIndex = 24;
            this.labelPozycjaRobota.Text = "PozRob";
            // 
            // OknoGlowne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.labelPozycjaRobota);
            this.Controls.Add(this.buttonSet1);
            this.Controls.Add(this.labelProcent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownKrok);
            this.Controls.Add(this.labelKrok);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labelPozycja);
            this.Controls.Add(this.comboBoxRysowanie);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelBateria);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonAnuluj);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonLogi);
            this.Controls.Add(this.comboBoxTrybPracy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTypBoiska);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBoxBoisko);
            this.Name = "OknoGlowne";
            this.Text = "Robot rysujący linie";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoisko)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKrok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBoisko;
        private System.Windows.Forms.ComboBox comboBoxTypBoiska;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTrybPracy;
        private System.Windows.Forms.Button buttonLogi;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonAnuluj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelBateria;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxRysowanie;
        private System.Windows.Forms.Label labelPozycja;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelKrok;
        private System.Windows.Forms.NumericUpDown numericUpDownKrok;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelProcent;
        private System.Windows.Forms.Button buttonSet1;
        private System.Windows.Forms.Label labelPozycjaRobota;
    }
}

