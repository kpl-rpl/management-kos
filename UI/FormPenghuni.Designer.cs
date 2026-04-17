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
            btnReset = new Button();
            btnHapus = new Button();
            btnUpdate = new Button();
            btnTambah = new Button();
            dropDownKamar = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(36, 222);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(693, 245);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 38);
            label1.Name = "label1";
            label1.Size = new Size(26, 15);
            label1.TabIndex = 1;
            label1.Text = "NIK";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 79);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 2;
            label2.Text = "Nama";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 125);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 3;
            label3.Text = "No Telpon";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 175);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 4;
            label4.Text = "Kamar";
            // 
            // txtNIK
            // 
            txtNIK.Location = new Point(132, 36);
            txtNIK.Margin = new Padding(3, 2, 3, 2);
            txtNIK.Multiline = true;
            txtNIK.Name = "txtNIK";
            txtNIK.Size = new Size(239, 26);
            txtNIK.TabIndex = 5;
            // 
            // txtNama
            // 
            txtNama.Location = new Point(132, 76);
            txtNama.Margin = new Padding(3, 2, 3, 2);
            txtNama.Multiline = true;
            txtNama.Name = "txtNama";
            txtNama.Size = new Size(239, 26);
            txtNama.TabIndex = 6;
            // 
            // txtTelpon
            // 
            txtTelpon.Location = new Point(132, 123);
            txtTelpon.Margin = new Padding(3, 2, 3, 2);
            txtTelpon.Multiline = true;
            txtTelpon.Name = "txtTelpon";
            txtTelpon.Size = new Size(239, 26);
            txtTelpon.TabIndex = 7;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(409, 74);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 30);
            btnReset.TabIndex = 21;
            btnReset.Text = "Reset Form";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            btnHapus.Location = new Point(629, 38);
            btnHapus.Name = "btnHapus";
            btnHapus.Size = new Size(100, 30);
            btnHapus.TabIndex = 20;
            btnHapus.Text = "Hapus";
            btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(519, 38);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 30);
            btnUpdate.TabIndex = 19;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnTambah
            // 
            btnTambah.Location = new Point(409, 38);
            btnTambah.Name = "btnTambah";
            btnTambah.Size = new Size(100, 30);
            btnTambah.TabIndex = 18;
            btnTambah.Text = "Tambah";
            btnTambah.UseVisualStyleBackColor = true;
            btnTambah.Click += btnTambah_Click;
            // 
            // dropDownKamar
            // 
            dropDownKamar.FormattingEnabled = true;
            dropDownKamar.Location = new Point(132, 172);
            dropDownKamar.Name = "dropDownKamar";
            dropDownKamar.Size = new Size(239, 23);
            dropDownKamar.TabIndex = 22;
            // 
            // FormPenghuni
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 490);
            Controls.Add(dropDownKamar);
            Controls.Add(btnReset);
            Controls.Add(btnHapus);
            Controls.Add(btnUpdate);
            Controls.Add(btnTambah);
            Controls.Add(txtTelpon);
            Controls.Add(txtNama);
            Controls.Add(txtNIK);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 2, 3, 2);
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
        private Button btnReset;
        private Button btnHapus;
        private Button btnUpdate;
        private Button btnTambah;
        private ComboBox dropDownKamar;
    }
}