using StudentManagementApp.Data;
using StudentManagementApp.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp
{
    public partial class AddEditStudentForm : Form
    {
        private readonly StudentRepository _repository;
        private readonly Student? _student;
        private readonly bool _isEditMode;

        public AddEditStudentForm(StudentRepository repository, Student? student = null)
        {
            InitializeComponent();
            _repository = repository;
            _student = student;
            _isEditMode = student != null;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form title
            Text = _isEditMode ? "Edit Student" : "Add New Student";
            lblFormTitle.Text = _isEditMode ? "Edit Student" : "Add New Student";

            // Initialize gender combo box
            cboGender.Items.AddRange(new string[] { "Male", "Female", "Other" });

            // Initialize year combo box
            for (int year = 1; year <= 6; year++)
            {
                cboYear.Items.Add(year);
            }

            // Set default values
            dtpDateOfBirth.Value = DateTime.Today.AddYears(-18);
            cboYear.SelectedIndex = 0;

            // Load student data if editing
            if (_isEditMode && _student != null)
            {
                LoadStudentData();
            }
        }

        private void LoadStudentData()
        {
            if (_student == null) return;

            txtStudentNumber.Text = _student.StudentNumber;
            txtFirstName.Text = _student.FirstName;
            txtLastName.Text = _student.LastName;
            dtpDateOfBirth.Value = _student.DateOfBirth;
            cboGender.Text = _student.Gender;
            txtEmail.Text = _student.Email;
            txtPhone.Text = _student.Phone;
            txtProgram.Text = _student.Program;
            cboYear.Text = _student.Year.ToString();
            txtAddress.Text = _student.Address;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                var student = new Student
                {
                    Id = _student?.Id ?? 0,
                    StudentNumber = txtStudentNumber.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value.Date,
                    Gender = cboGender.Text,
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Program = txtProgram.Text.Trim(),
                    Year = int.Parse(cboYear.Text),
                    Address = txtAddress.Text.Trim()
                };

                // Check for duplicate student number
                if (_repository.IsStudentNumberExists(student.StudentNumber, student.Id))
                {
                    MessageBox.Show("Student Number already exists. Please enter a unique student number.", 
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStudentNumber.Focus();
                    return;
                }

                bool success;
                if (_isEditMode)
                {
                    success = _repository.UpdateStudent(student);
                    if (success)
                        MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _repository.AddStudent(student);
                    success = true;
                    MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Clear previous error providers
            errorProvider1.Clear();

            bool isValid = true;

            // Validate Student Number
            if (string.IsNullOrWhiteSpace(txtStudentNumber.Text))
            {
                errorProvider1.SetError(txtStudentNumber, "Student Number is required.");
                isValid = false;
            }

            // Validate First Name
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                errorProvider1.SetError(txtFirstName, "First Name is required.");
                isValid = false;
            }

            // Validate Last Name
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                errorProvider1.SetError(txtLastName, "Last Name is required.");
                isValid = false;
            }

            // Validate Email format if provided
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(txtEmail.Text))
                {
                    errorProvider1.SetError(txtEmail, "Please enter a valid email address.");
                    isValid = false;
                }
            }

            // Validate Year
            if (cboYear.SelectedItem == null)
            {
                errorProvider1.SetError(cboYear, "Please select a year.");
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Please correct the validation errors before saving.", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
} 