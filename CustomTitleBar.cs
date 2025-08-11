using System.Drawing.Drawing2D;

namespace StudentManagementApp
{
    public partial class CustomTitleBar : UserControl
    {
        private bool isDragging = false;
        private Point lastCursor;
        private Form parentForm;

        public CustomTitleBar()
        {
            InitializeComponent();
            this.Height = 40;
            this.BackColor = UIHelper.PrimaryColor;
            this.Dock = DockStyle.Top;
            this.Cursor = Cursors.SizeAll;

            // Add title label
            Label titleLabel = new Label
            {
                Text = "Student Management System",
                ForeColor = Color.White,
                Font = UIHelper.TitleFont,
                AutoSize = true,
                Location = new Point(15, 10)
            };
            this.Controls.Add(titleLabel);

            // Add minimize button
            Button btnMinimize = new Button
            {
                Text = "─",
                Size = new Size(45, 30),
                Location = new Point(this.Width - 90, 5),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += (s, e) => parentForm?.WindowState = FormWindowState.Minimized;
            this.Controls.Add(btnMinimize);

            // Add close button
            Button btnClose = new Button
            {
                Text = "×",
                Size = new Size(45, 30),
                Location = new Point(this.Width - 45, 5),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => parentForm?.Close();
            this.Controls.Add(btnClose);

            // Add hover effects
            btnMinimize.MouseEnter += (s, e) => btnMinimize.BackColor = Color.FromArgb(69, 160, 73);
            btnMinimize.MouseLeave += (s, e) => btnMinimize.BackColor = Color.Transparent;
            btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.FromArgb(220, 53, 69);
            btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.Transparent;

            // Mouse events for dragging
            this.MouseDown += CustomTitleBar_MouseDown;
            this.MouseMove += CustomTitleBar_MouseMove;
            this.MouseUp += CustomTitleBar_MouseUp;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            parentForm = this.FindForm();
        }

        private void CustomTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = e.Location;
            }
        }

        private void CustomTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && parentForm != null)
            {
                Point currentCursor = e.Location;
                Point cursorOffset = new Point(currentCursor.X - lastCursor.X, currentCursor.Y - lastCursor.Y);
                parentForm.Location = new Point(parentForm.Location.X + cursorOffset.X, parentForm.Location.Y + cursorOffset.Y);
            }
        }

        private void CustomTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw rounded corners for the title bar
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, UIHelper.CornerRadius, UIHelper.CornerRadius, 180, 90);
                path.AddArc(this.Width - UIHelper.CornerRadius, 0, UIHelper.CornerRadius, UIHelper.CornerRadius, 270, 90);
                path.AddLine(this.Width, UIHelper.CornerRadius, this.Width, this.Height);
                path.AddLine(0, this.Height, 0, UIHelper.CornerRadius);
                path.CloseFigure();

                using (SolidBrush brush = new SolidBrush(UIHelper.PrimaryColor))
                {
                    g.FillPath(brush, path);
                }
            }
        }
    }
} 