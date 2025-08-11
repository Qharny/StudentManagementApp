using StudentManagementApp.Data;
using StudentManagementApp.Models;

namespace StudentManagementApp
{
    public partial class MainForm : Form
    {
        private readonly StudentRepository _repository;
        private List<Student> _students;

        public MainForm()
        {
            InitializeComponent();
            _repository = new StudentRepository();
            _students = new List<Student>();
            
            InitializeDataGridView();
            LoadStudents();
        }

        private void InitializeDataGridView()
        {
            // Configure DataGridView
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.RowHeadersVisible = false;

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
            lblStatus.Text = $"Total Students: {_students.Count}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedStudent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedStudent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStudents();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SearchStudents();
            }
        }

        private void DgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void SearchStudents()
        {
            var searchTerm = txtSearch.Text.Trim();
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
    }
} 