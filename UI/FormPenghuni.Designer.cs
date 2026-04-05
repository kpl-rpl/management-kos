namespace management_kos.UI
{
    partial class FormPenghuni
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
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtNIK = new TextBox();
            txtNama = new TextBox();
            txtTelpon = new TextBox();
            txtKamar = new TextBox();
            btnReset = new Button();
            btnHapus = new Button();
            btnUpdate = new Button();
            btnTambah = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(41, 296);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(792, 327);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 51);
            label1.Name = "label1";
            label1.Size = new Size(33, 20);
            label1.TabIndex = 1;
            label1.Text = "NIK";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 105);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 2;
            label2.Text = "Nama";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 167);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 3;
            label3.Text = "No Telpon";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(41, 233);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 4;
            label4.Text = "Kamar";
            // 
            // txtNIK
            // 
            txtNIK.Location = new Point(151, 48);
            txtNIK.Multiline = true;
            txtNIK.Name = "txtNIK";
            txtNIK.Size = new Size(273, 34);
            txtNIK.TabIndex = 5;
            // 
            // txtNama
            // 
            txtNama.Location = new Point(151, 102);
            txtNama.Multiline = true;
            txtNama.Name = "txtNama";
            txtNama.Size = new Size(273, 34);
            txtNama.TabIndex = 6;
            // 
            // txtTelpon
            // 
            txtTelpon.Location = new Point(151, 164);
            txtTelpon.Multiline = true;
            txtTelpon.Name = "txtTelpon";
            txtTelpon.Size = new Size(273, 34);
            txtTelpon.TabIndex = 7;
            // 
            // txtKamar
            // 
            txtKamar.Location = new Point(151, 230);
            txtKamar.Multiline = true;
            txtKamar.Name = "txtKamar";
            txtKamar.Size = new Size(273, 34);
            txtKamar.TabIndex = 8;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(467, 99);
            btnReset.Margin = new Padding(3, 4, 3, 4);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(114, 40);
            btnReset.TabIndex = 21;
            btnReset.Text = "Reset Form";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            btnHapus.Location = new Point(719, 51);
            btnHapus.Margin = new Padding(3, 4, 3, 4);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(114, 40);
            btnHapus.TabIndex = 20;
            btnHapus.Text = "Hapus";
            btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(593, 51);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(114, 40);
            btnUpdate.TabIndex = 19;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(467, 51);
            btnTambah.Margin = new Padding(3, 4, 3, 4);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(114, 40);
            btnTambah.TabIndex = 18;
            btnTambah.Text = "Tambah";
            btnTambah.UseVisualStyleBackColor = true;
            btnTambah.Click += btnTambah_Click;
            // 
            // FormPenghuni
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(891, 653);
            Controls.Add(btnReset);
            Controls.Add(btnHapus);
            Controls.Add(btnUpdate);
            Controls.Add(btnTambah);
            Controls.Add(txtKamar);
            Controls.Add(txtTelpon);
            Controls.Add(txtNama);
            Controls.Add(txtNIK);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "FormPenghuni";
            Text = "FormPenghuni";
            Load += FormPenghuni_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtNIK;
        private TextBox txtNama;
        private TextBox txtTelpon;
        private TextBox txtKamar;
        private Button btnReset;
        private Button btnHapus;
        private Button btnUpdate;
        private Button btnTambah;
    }
}