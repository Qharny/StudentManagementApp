using System.Drawing.Drawing2D;

namespace StudentManagementApp
{
    public static class UIHelper
    {
        // Color Theme
        public static Color PrimaryColor = Color.FromArgb(76, 175, 80); // #4CAF50
        public static Color SecondaryColor = Color.FromArgb(33, 150, 243); // #2196F3
        public static Color AccentColor = Color.FromArgb(255, 193, 7); // #FFC107
        public static Color BackgroundColor = Color.FromArgb(248, 249, 250); // #F8F9FA
        public static Color SurfaceColor = Color.White;
        public static Color TextPrimaryColor = Color.FromArgb(33, 37, 41); // #212529
        public static Color TextSecondaryColor = Color.FromArgb(108, 117, 125); // #6C757D
        public static Color BorderColor = Color.FromArgb(222, 226, 230); // #DEE2E6
        public static Color HoverColor = Color.FromArgb(240, 248, 255); // Light blue
        public static Color SelectionColor = Color.FromArgb(173, 216, 230); // Light blue

        // Fonts
        public static Font DefaultFont = new Font("Segoe UI", 10f);
        public static Font HeaderFont = new Font("Segoe UI", 12f, FontStyle.Bold);
        public static Font TitleFont = new Font("Segoe UI", 16f, FontStyle.Bold);
        public static Font MenuFont = new Font("Segoe UI", 10f);

        // Sizes
        public static int NavigationWidth = 200;
        public static int ButtonHeight = 40;
        public static int CornerRadius = 8;

        public static void ApplyModernStyle(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.BackColor = BackgroundColor;
            form.Font = DefaultFont;
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        public static void ApplyButtonStyle(Button button, bool isPrimary = false)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = isPrimary ? PrimaryColor : SurfaceColor;
            button.ForeColor = isPrimary ? Color.White : TextPrimaryColor;
            button.Font = DefaultFont;
            button.Height = ButtonHeight;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;

            // Add hover effects
            button.MouseEnter += (sender, e) =>
            {
                if (isPrimary)
                {
                    button.BackColor = Color.FromArgb(69, 160, 73); // Darker green
                }
                else
                {
                    button.BackColor = HoverColor;
                }
            };

            button.MouseLeave += (sender, e) =>
            {
                button.BackColor = isPrimary ? PrimaryColor : SurfaceColor;
            };
        }

        public static void ApplyDataGridViewStyle(DataGridView dgv)
        {
            dgv.BackgroundColor = SurfaceColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = BorderColor;
            dgv.Font = DefaultFont;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = HeaderFont;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.BackColor = SurfaceColor;
            dgv.DefaultCellStyle.ForeColor = TextPrimaryColor;
            dgv.DefaultCellStyle.SelectionBackColor = SelectionColor;
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimaryColor;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // #F5F5F5
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimaryColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = SelectionColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextPrimaryColor;
        }

        public static void ApplyPanelStyle(Panel panel, bool isRounded = true)
        {
            panel.BackColor = SurfaceColor;
            if (isRounded)
            {
                panel.Region = GetRoundedRegion(panel.Width, panel.Height, CornerRadius);
            }
        }

        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = SurfaceColor;
            textBox.ForeColor = TextPrimaryColor;
            textBox.Font = DefaultFont;
            textBox.Height = 35;
        }

        public static void ApplyComboBoxStyle(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = SurfaceColor;
            comboBox.ForeColor = TextPrimaryColor;
            comboBox.Font = DefaultFont;
            comboBox.Height = 35;
        }

        public static void ApplyDateTimePickerStyle(DateTimePicker dateTimePicker)
        {
            dateTimePicker.Format = DateTimePickerFormat.Short;
            dateTimePicker.Font = DefaultFont;
            dateTimePicker.Height = 35;
        }

        public static void ApplyLabelStyle(Label label, bool isHeader = false)
        {
            label.Font = isHeader ? HeaderFont : DefaultFont;
            label.ForeColor = isHeader ? TextPrimaryColor : TextSecondaryColor;
            label.BackColor = Color.Transparent;
        }

        public static Region GetRoundedRegion(int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(width - radius, 0, radius, radius, 270, 90);
            path.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            path.AddArc(0, height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return new Region(path);
        }

        public static void CreateRoundedButton(Button button, int radius = 8)
        {
            button.Region = GetRoundedRegion(button.Width, button.Height, radius);
        }

        public static void CreateRoundedPanel(Panel panel, int radius = 8)
        {
            panel.Region = GetRoundedRegion(panel.Width, panel.Height, radius);
        }

        public static void ApplyNavigationButtonStyle(Button button, bool isActive = false)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = isActive ? PrimaryColor : Color.Transparent;
            button.ForeColor = isActive ? Color.White : TextPrimaryColor;
            button.Font = MenuFont;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(20, 10, 10, 10);
            button.Height = 50;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;

            button.MouseEnter += (sender, e) =>
            {
                if (!isActive)
                {
                    button.BackColor = HoverColor;
                }
            };

            button.MouseLeave += (sender, e) =>
            {
                button.BackColor = isActive ? PrimaryColor : Color.Transparent;
            };
        }
    }
} 