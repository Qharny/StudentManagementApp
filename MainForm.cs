using StudentManagementApp.Data;
using StudentManagementApp.Models;

namespace StudentManagementApp
{
    public partial class MainForm : Form
    {
        private readonly StudentRepository _repository;
        private List<Student> _students;
        private Panel? contentPanel;
        private Button? activeMenuButton;

        public MainForm()
        {
            InitializeComponent();
            _repository = new StudentRepository();
            _students = new List<Student>();
            
            ApplyModernDesign();
            InitializeNavigation();
            InitializeContentPanel();
            LoadStudents();
        }

        private void ApplyModernDesign()
        {
            // Apply modern form style
            UIHelper.ApplyModernStyle(this);
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(1000, 600);

            // Add custom title bar
            CustomTitleBar titleBar = new CustomTitleBar();
            this.Controls.Add(titleBar);

            // Create rounded form region
            this.Region = UIHelper.GetRoundedRegion(this.Width, this.Height, UIHelper.CornerRadius);
        }

        private void InitializeNavigation()
        {
            // Navigation panel
            Panel navPanel = new Panel
            {
                Width = UIHelper.NavigationWidth,
                Dock = DockStyle.Left,
                BackColor = UIHelper.SurfaceColor
            };
            UIHelper.CreateRoundedPanel(navPanel);
            this.Controls.Add(navPanel);

            // Logo/Title area
            Label logoLabel = new Label
            {
                Text = "Student Management",
                Font = UIHelper.TitleFont,
                ForeColor = UIHelper.PrimaryColor,
                AutoSize = true,
                Location = new Point(20, 60)
            };
            navPanel.Controls.Add(logoLabel);

            // Navigation buttons
            Button btnDashboard = CreateNavButton("📊 Dashboard", 120);
            Button btnAddStudent = CreateNavButton("➕ Add Student", 170);
            Button btnExit = CreateNavButton("🚪 Exit", 220);

            navPanel.Controls.Add(btnDashboard);
            navPanel.Controls.Add(btnAddStudent);
            navPanel.Controls.Add(btnExit);

            // Set dashboard as active initially
            SetActiveMenuButton(btnDashboard);

            // Event handlers
            btnDashboard.Click += (s, e) => ShowDashboard();
            btnAddStudent.Click += (s, e) => ShowAddStudentForm();
            btnExit.Click += (s, e) => this.Close();
        }

        private Button CreateNavButton(string text, int yPosition)
        {
            Button button = new Button
            {
                Text = text,
                Size = new Size(UIHelper.NavigationWidth - 20, 50),
                Location = new Point(10, yPosition),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = UIHelper.TextPrimaryColor,
                Font = UIHelper.MenuFont,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;

            // Hover effects
            button.MouseEnter += (s, e) =>
            {
                if (button != activeMenuButton)
                {
                    button.BackColor = UIHelper.HoverColor;
                }
            };
            button.MouseLeave += (s, e) =>
            {
                if (button != activeMenuButton)
                {
                    button.BackColor = Color.Transparent;
                }
            };

            return button;
        }

        private void SetActiveMenuButton(Button? button)
        {
            if (activeMenuButton != null)
            {
                activeMenuButton.BackColor = Color.Transparent;
                activeMenuButton.ForeColor = UIHelper.TextPrimaryColor;
            }

            activeMenuButton = button;
            if (button != null)
            {
                button.BackColor = UIHelper.PrimaryColor;
                button.ForeColor = Color.White;
            }
        }

        private void InitializeContentPanel()
        {
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UIHelper.BackgroundColor,
                Padding = new Padding(20)
            };
            this.Controls.Add(contentPanel);
            contentPanel.BringToFront();
        }

        private void ShowDashboard()
        {
            SetActiveMenuButton(GetNavButtonByText("📊 Dashboard"));
            if (contentPanel != null)
            {
                contentPanel.Controls.Clear();
                CreateDashboardView();
            }
        }

        private void ShowAddStudentForm()
        {
            SetActiveMenuButton(GetNavButtonByText("➕ Add Student"));
            if (contentPanel != null)
            {
                contentPanel.Controls.Clear();
                CreateAddStudentView();
            }
        }

        private Button GetNavButtonByText(string text)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel navPanel && navPanel.Dock == DockStyle.Left)
                {
                    foreach (Control navControl in navPanel.Controls)
                    {
                        if (navControl is Button button && button.Text == text)
                        {
                            return button;
                        }
                    }
                }
            }
            return null;
        }

        private void CreateDashboardView()
        {
            if (contentPanel == null) return;

            // Header
            Label headerLabel = new Label
            {
                Text = "Student Dashboard",
                Font = UIHelper.TitleFont,
                ForeColor = UIHelper.TextPrimaryColor,
                AutoSize = true,
                Location = new Point(0, 0)
            };
            contentPanel.Controls.Add(headerLabel);

            // Search panel
            Panel searchPanel = new Panel
            {
                Size = new Size(contentPanel!.Width - 40, 80),
                Location = new Point(0, 50),
                BackColor = UIHelper.SurfaceColor
            };
            UIHelper.CreateRoundedPanel(searchPanel);
            contentPanel.Controls.Add(searchPanel);

            // Search controls
            Label searchLabel = new Label
            {
                Text = "Search Students:",
                Font = UIHelper.DefaultFont,
                ForeColor = UIHelper.TextSecondaryColor,
                Location = new Point(20, 25),
                AutoSize = true
            };
            searchPanel.Controls.Add(searchLabel);

            TextBox searchBox = new TextBox
            {
                Size = new Size(300, 35),
                Location = new Point(150, 20),
                Font = UIHelper.DefaultFont
            };
            UIHelper.ApplyTextBoxStyle(searchBox);
            searchPanel.Controls.Add(searchBox);

            Button searchButton = new Button
            {
                Text = "Search",
                Size = new Size(100, 35),
                Location = new Point(470, 20)
            };
            UIHelper.ApplyButtonStyle(searchButton, true);
            searchButton.Click += (s, e) => SearchStudents(searchBox.Text);
            searchPanel.Controls.Add(searchButton);

            Button refreshButton = new Button
            {
                Text = "Refresh",
                Size = new Size(100, 35),
                Location = new Point(580, 20)
            };
            UIHelper.ApplyButtonStyle(refreshButton);
            refreshButton.Click += (s, e) => LoadStudents();
            searchPanel.Controls.Add(refreshButton);

            // Action buttons panel
            Panel actionPanel = new Panel
            {
                Size = new Size(contentPanel!.Width - 40, 60),
                Location = new Point(0, 150),
                BackColor = UIHelper.SurfaceColor
            };
            UIHelper.CreateRoundedPanel(actionPanel);
            contentPanel.Controls.Add(actionPanel);

            Button addButton = new Button
            {
                Text = "➕ Add New Student",
                Size = new Size(150, 40),
                Location = new Point(20, 10)
            };
            UIHelper.ApplyButtonStyle(addButton, true);
            addButton.Click += btnAdd_Click;
            actionPanel.Controls.Add(addButton);

            Button editButton = new Button
            {
                Text = "✏️ Edit Student",
                Size = new Size(150, 40),
                Location = new Point(190, 10)
            };
            UIHelper.ApplyButtonStyle(editButton);
            editButton.Click += btnEdit_Click;
            actionPanel.Controls.Add(editButton);

            Button deleteButton = new Button
            {
                Text = "🗑️ Delete Student",
                Size = new Size(150, 40),
                Location = new Point(360, 10)
            };
            UIHelper.ApplyButtonStyle(deleteButton);
            deleteButton.Click += btnDelete_Click;
            actionPanel.Controls.Add(deleteButton);

            // DataGridView
            DataGridView dgv = new DataGridView
            {
                Size = new Size(contentPanel!.Width - 40, contentPanel.Height - 280),
                Location = new Point(0, 230),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            UIHelper.ApplyDataGridViewStyle(dgv);
            contentPanel.Controls.Add(dgv);

            // Store reference to DataGridView
            this.dgvStudents = dgv;
            InitializeDataGridView();

            // Status label
            Label statusLabel = new Label
            {
                Text = "Total Students: 0",
                Font = UIHelper.DefaultFont,
                ForeColor = UIHelper.TextSecondaryColor,
                AutoSize = true,
                Location = new Point(0, contentPanel!.Height - 30)
            };
            contentPanel.Controls.Add(statusLabel);
            this.lblStatus = statusLabel;

            // Load data
            LoadStudents();
        }

        private void CreateAddStudentView()
        {
            // This will be handled by the AddEditStudentForm
            var addForm = new AddEditStudentForm(_repository);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                ShowDashboard(); // Return to dashboard after adding
            }
        }

        private void InitializeDataGridView()
        {
            // Configure DataGridView
            dgvStudents.AutoGenerateColumns = false;

            // Add columns
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentNumber",
                HeaderText = "Student Number",
                Width = 120
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Full Name",
                Width = 200
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DateOfBirth",
                HeaderText = "Date of Birth",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "MM/dd/yyyy" }
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Gender",
                HeaderText = "Gender",
                Width = 80
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 200
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Width = 120
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Program",
                HeaderText = "Program",
                Width = 150
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Year",
                HeaderText = "Year",
                Width = 60
            });

            // Bind double-click event
            dgvStudents.CellDoubleClick += DgvStudents_CellDoubleClick;
        }

        private void LoadStudents()
        {
            try
            {
                _students = _repository.GetAllStudents();
                dgvStudents.DataSource = null;
                dgvStudents.DataSource = _students;
                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusLabel()
        {
            if (lblStatus != null)
            {
                lblStatus.Text = $"Total Students: {_students.Count}";
            }
        }

        private void SearchStudents(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadStudents();
                return;
            }

            try
            {
                _students = _repository.SearchStudents(searchTerm);
                dgvStudents.DataSource = null;
                dgvStudents.DataSource = _students;
                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handlers (keeping existing functionality)
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                var addForm = new AddEditStudentForm(_repository);
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object? sender, EventArgs e)
        {
            EditSelectedStudent();
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            DeleteSelectedStudent();
        }

        private void DgvStudents_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedStudent();
            }
        }

        private void EditSelectedStudent()
        {
            if (dgvStudents.CurrentRow == null)
            {
                MessageBox.Show("Please select a student to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var student = (Student)dgvStudents.CurrentRow.DataBoundItem;
                var editForm = new AddEditStudentForm(_repository, student);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedStudent()
        {
            if (dgvStudents.CurrentRow == null)
            {
                MessageBox.Show("Please select a student to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var student = (Student)dgvStudents.CurrentRow.DataBoundItem;
            var result = MessageBox.Show(
                $"Are you sure you want to delete student '{student.FullName}' (ID: {student.StudentNumber})?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_repository.DeleteStudent(student.Id))
                    {
                        MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudents();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
} 