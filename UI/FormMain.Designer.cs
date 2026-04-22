namespace management_kos.UI;

partial class FormMain
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        pnlSidebar = new Panel();
        btnDataPenghuni = new Button();
        btnDataKamar = new Button();
        btnDataKos = new Button();
        btnHome = new Button();
        lblAppTitle = new Label();
        pnlTopBar = new Panel();
        lblCurrentView = new Label();
        pnlContent = new Panel();
        pnlSidebar.SuspendLayout();
        pnlTopBar.SuspendLayout();
        SuspendLayout();
        // 
        // pnlSidebar
        // 
        pnlSidebar.BackColor = Color.FromArgb(32, 43, 59);
        pnlSidebar.Controls.Add(btnDataPenghuni);
        pnlSidebar.Controls.Add(btnDataKamar);
        pnlSidebar.Controls.Add(btnDataKos);
        pnlSidebar.Controls.Add(btnHome);
        pnlSidebar.Controls.Add(lblAppTitle);
        pnlSidebar.Dock = DockStyle.Left;
        pnlSidebar.Location = new Point(0, 0);
        pnlSidebar.Name = "pnlSidebar";
        pnlSidebar.Size = new Size(220, 681);
        pnlSidebar.TabIndex = 0;
        // 
        // btnDataPenghuni
        // 
        btnDataPenghuni.FlatStyle = FlatStyle.Flat;
        btnDataPenghuni.ForeColor = Color.White;
        btnDataPenghuni.Location = new Point(20, 174);
        btnDataPenghuni.Name = "btnDataPenghuni";
        btnDataPenghuni.Size = new Size(180, 36);
        btnDataPenghuni.TabIndex = 3;
        btnDataPenghuni.Text = "Data Penghuni";
        btnDataPenghuni.UseVisualStyleBackColor = true;
        btnDataPenghuni.Click += btnDataPenghuni_Click;
        // 
        // btnDataKamar
        // 
        btnDataKamar.FlatStyle = FlatStyle.Flat;
        btnDataKamar.ForeColor = Color.White;
        btnDataKamar.Location = new Point(20, 132);
        btnDataKamar.Margin = new Padding(3, 2, 3, 2);
        btnDataKamar.Name = "btnDataKamar";
        btnDataKamar.Size = new Size(158, 27);
        btnDataKamar.TabIndex = 3;
        btnDataKamar.Text = "Data Kamar";
        btnDataKamar.UseVisualStyleBackColor = true;
        btnDataKamar.Click += btnDataKamar_Click;
        // 
        // btnDataKos
        // 
        btnDataKos.FlatStyle = FlatStyle.Flat;
        btnDataKos.ForeColor = Color.White;
        btnDataKos.Location = new Point(20, 132);
        btnDataKos.Name = "btnDataKos";
        btnDataKos.Size = new Size(180, 36);
        btnDataKos.TabIndex = 2;
        btnDataKos.Text = "Data Kos";
        btnDataKos.UseVisualStyleBackColor = true;
        btnDataKos.Click += btnDataKos_Click;
        // 
        // btnHome
        // 
        btnHome.FlatStyle = FlatStyle.Flat;
        btnHome.ForeColor = Color.White;
        btnHome.Location = new Point(20, 90);
        btnHome.Name = "btnHome";
        btnHome.Size = new Size(180, 36);
        btnHome.TabIndex = 1;
        btnHome.Text = "Home";
        btnHome.UseVisualStyleBackColor = true;
        btnHome.Click += btnHome_Click;
        // 
        // lblAppTitle
        // 
        lblAppTitle.AutoSize = true;
        lblAppTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAppTitle.ForeColor = Color.White;
        lblAppTitle.Location = new Point(20, 34);
        lblAppTitle.Name = "lblAppTitle";
        lblAppTitle.Size = new Size(132, 20);
        lblAppTitle.TabIndex = 0;
        lblAppTitle.Text = "Management Kos";
        // 
        // pnlTopBar
        // 
        pnlTopBar.BackColor = Color.FromArgb(240, 244, 248);
        pnlTopBar.Controls.Add(lblCurrentView);
        pnlTopBar.Dock = DockStyle.Top;
        pnlTopBar.Location = new Point(220, 0);
        pnlTopBar.Name = "pnlTopBar";
        pnlTopBar.Size = new Size(964, 56);
        pnlTopBar.TabIndex = 1;
        // 
        // lblCurrentView
        // 
        lblCurrentView.AutoSize = true;
        lblCurrentView.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCurrentView.Location = new Point(20, 18);
        lblCurrentView.Name = "lblCurrentView";
        lblCurrentView.Size = new Size(56, 21);
        lblCurrentView.TabIndex = 0;
        lblCurrentView.Text = "Home";
        // 
        // pnlContent
        // 
        pnlContent.BackColor = Color.White;
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(220, 56);
        pnlContent.Name = "pnlContent";
        pnlContent.Size = new Size(964, 625);
        pnlContent.TabIndex = 2;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1184, 681);
        Controls.Add(pnlContent);
        Controls.Add(pnlTopBar);
        Controls.Add(pnlSidebar);
        Name = "FormMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Management Kos";
        Load += FormMain_Load;
        pnlSidebar.ResumeLayout(false);
        pnlSidebar.PerformLayout();
        pnlTopBar.ResumeLayout(false);
        pnlTopBar.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlSidebar;
    private Button btnDataKos;
    private Button btnHome;
    private Label lblAppTitle;
    private Panel pnlTopBar;
    private Label lblCurrentView;
    private Panel pnlContent;
    private Button btnDataPenghuni;
    private Button btnDataKamar;
}
