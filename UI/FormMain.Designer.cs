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
        btnDataKos = new Button();
        btnHome = new Button();
        lblAppTitle = new Label();
        pnlTopBar = new Panel();
        lblCurrentView = new Label();
        pnlContent = new Panel();
        btnDataKamar = new Button();
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
        pnlSidebar.Margin = new Padding(3, 4, 3, 4);
        pnlSidebar.Name = "pnlSidebar";
        pnlSidebar.Size = new Size(251, 908);
        pnlSidebar.TabIndex = 0;
        // 
        // btnDataPenghuni
        // 
        btnDataPenghuni.FlatStyle = FlatStyle.Flat;
        btnDataPenghuni.ForeColor = Color.White;
        btnDataPenghuni.Location = new Point(23, 232);
        btnDataPenghuni.Margin = new Padding(3, 4, 3, 4);
        btnDataPenghuni.Name = "btnDataPenghuni";
        btnDataPenghuni.Size = new Size(206, 48);
        btnDataPenghuni.TabIndex = 3;
        btnDataPenghuni.Text = "Data Penghuni";
        btnDataPenghuni.UseVisualStyleBackColor = true;
        btnDataPenghuni.Click += btnDataPenghuni_Click;
        // 
        // btnDataKos
        // 
        btnDataKos.FlatStyle = FlatStyle.Flat;
        btnDataKos.ForeColor = Color.White;
        btnDataKos.Location = new Point(23, 176);
        btnDataKos.Margin = new Padding(3, 4, 3, 4);
        btnDataKos.Name = "btnDataKos";
        btnDataKos.Size = new Size(206, 48);
        btnDataKos.TabIndex = 2;
        btnDataKos.Text = "Data Kos";
        btnDataKos.UseVisualStyleBackColor = true;
        btnDataKos.Click += btnDataKos_Click;
        // 
        // btnHome
        // 
        btnHome.FlatStyle = FlatStyle.Flat;
        btnHome.ForeColor = Color.White;
        btnHome.Location = new Point(23, 120);
        btnHome.Margin = new Padding(3, 4, 3, 4);
        btnHome.Name = "btnHome";
        btnHome.Size = new Size(206, 48);
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
        lblAppTitle.Location = new Point(23, 45);
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
        pnlTopBar.Location = new Point(251, 0);
        pnlTopBar.Margin = new Padding(3, 4, 3, 4);
        pnlTopBar.Name = "pnlTopBar";
        pnlTopBar.Size = new Size(1102, 75);
        pnlTopBar.TabIndex = 1;
        // 
        // lblCurrentView
        // 
        lblCurrentView.AutoSize = true;
        lblCurrentView.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCurrentView.Location = new Point(23, 24);
        lblCurrentView.Name = "lblCurrentView";
        lblCurrentView.Size = new Size(56, 21);
        lblCurrentView.TabIndex = 0;
        lblCurrentView.Text = "Home";
        // 
        // pnlContent
        // 
        pnlContent.BackColor = Color.White;
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(251, 75);
        pnlContent.Margin = new Padding(3, 4, 3, 4);
        pnlContent.Name = "pnlContent";
        pnlContent.Size = new Size(1102, 833);
        pnlContent.TabIndex = 2;
        // 
        // btnDataKamar
        // 
        btnDataKamar.FlatStyle = FlatStyle.Flat;
        btnDataKamar.ForeColor = Color.White;
        btnDataKamar.Location = new Point(20, 174);
        btnDataKamar.Name = "btnDataKamar";
        btnDataKamar.Size = new Size(180, 36);
        btnDataKamar.TabIndex = 3;
        btnDataKamar.Text = "Data Kamar";
        btnDataKamar.UseVisualStyleBackColor = true;
        btnDataKamar.Click += btnDataKamar_Click;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1353, 908);
        Controls.Add(pnlContent);
        Controls.Add(pnlTopBar);
        Controls.Add(pnlSidebar);
        Margin = new Padding(3, 4, 3, 4);
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
