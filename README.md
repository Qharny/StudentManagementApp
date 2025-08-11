# Student Management Application

A modern, professional C# Windows Forms application for managing student records using SQLite database with a sleek, flat design interface.

## ✨ Features

- **Modern UI Design**: Flat, borderless window with custom title bar and rounded corners
- **Navigation Panel**: Left-side navigation with icons and smooth hover effects
- **Add Students**: Add new student records with comprehensive information
- **View Students**: Display all students in a modern DataGridView with alternating row colors
- **Edit Students**: Modify existing student information
- **Delete Students**: Remove student records with confirmation
- **Search Students**: Search by student number, name, or email
- **Data Validation**: Comprehensive validation for required fields and data formats
- **SQLite Database**: Local file-based database for data persistence
- **Responsive Design**: Layout adapts to window resizing

## 🎨 Design Features

- **Custom Title Bar**: Draggable window with minimize/close buttons
- **Navigation Panel**: 200px left panel with Dashboard, Add Student, and Exit options
- **Color Theme**: Professional green (#4CAF50) primary color with blue (#2196F3) secondary
- **Modern Typography**: Segoe UI 10pt throughout the application
- **Rounded Corners**: All panels and buttons feature rounded corners
- **Hover Effects**: Smooth hover animations on buttons and navigation items
- **DataGridView Styling**: 
  - Green header with white text
  - Alternating row colors (white and #F5F5F5)
  - Light blue selection highlight
  - Clean, modern appearance

## 📋 Student Information Fields

- Student Number (unique identifier)
- First Name (required)
- Last Name (required)
- Date of Birth
- Gender (Male/Female/Other)
- Email (with format validation)
- Phone Number
- Program/Study Program
- Year Level (1-6)
- Address

## 🛠️ Technical Details

- **Framework**: .NET 8.0 Windows Forms
- **Database**: SQLite (Microsoft.Data.Sqlite)
- **Architecture**: Repository pattern with data access layer
- **UI Framework**: Custom UIHelper class for consistent styling
- **Validation**: Data annotations and custom validation
- **Design**: Modern flat design with custom controls

## 📁 Project Structure

```
StudentManagementApp/
├── Models/
│   └── Student.cs                 # Student entity model
├── Data/
│   └── StudentRepository.cs       # Data access layer
├── UIHelper.cs                    # Modern UI styling and theme management
├── CustomTitleBar.cs              # Custom title bar control
├── CustomTitleBar.Designer.cs     # Title bar designer
├── MainForm.cs                    # Main application form (modern design)
├── MainForm.Designer.cs           # Main form designer
├── AddEditStudentForm.cs          # Add/Edit student form (modern design)
├── AddEditStudentForm.Designer.cs # Add/Edit form designer
├── Program.cs                     # Application entry point
└── StudentManagementApp.csproj    # Project file
```

## 🎯 UI Components

### MainForm
- **Custom Title Bar**: Draggable with minimize/close buttons
- **Navigation Panel**: Left-side menu with icons
- **Dashboard View**: Search panel, action buttons, and DataGridView
- **Responsive Layout**: Adapts to window resizing

### AddEditStudentForm
- **Modern Styling**: Consistent with main form design
- **Form Validation**: Real-time validation with error indicators
- **Rounded Controls**: All input controls feature rounded corners

### UIHelper Class
- **Centralized Styling**: All UI styles managed in one place
- **Color Theme**: Consistent color palette throughout
- **Control Styling**: Methods for styling all control types
- **Rounded Regions**: Helper methods for creating rounded corners

## 🗄️ Database

The application automatically creates a SQLite database file named `StudentManagement.db` in the application directory on first run. The database includes:

- **Students Table**: Stores all student information
- **Auto-incrementing ID**: Primary key for each student
- **Unique Student Number**: Ensures no duplicate student numbers
- **Proper Data Types**: Appropriate SQLite data types for each field

## 🚀 Getting Started

1. **Prerequisites**: 
   - .NET 8.0 SDK
   - Visual Studio 2022 or later (recommended)

2. **Build and Run**:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

3. **First Run**: The application will automatically create the database and table structure.

## 📖 Usage

1. **Navigation**: Use the left-side panel to navigate between Dashboard and Add Student
2. **Adding Students**: Click "Add Student" in navigation or "Add New Student" button
3. **Editing Students**: Select a student and click "Edit Student" or double-click the row
4. **Deleting Students**: Select a student and click "Delete Student" (confirmation required)
5. **Searching**: Use the search box to find students by name, number, or email
6. **Refreshing**: Click "Refresh" to reload the student list

## ✅ Validation Rules

- **Required Fields**: Student Number, First Name, Last Name
- **Unique Student Number**: No duplicate student numbers allowed
- **Email Format**: Valid email format when provided
- **Year Level**: Must be selected from dropdown (1-6)

## 🛡️ Error Handling

- **User-Friendly Messages**: Clear error messages for validation failures
- **Database Errors**: Proper exception handling with informative messages
- **Confirmation Dialogs**: Delete confirmations to prevent accidental deletions

## 🎨 Design Principles

This application demonstrates:
- **Modern UI Design**: Flat design with rounded corners and smooth animations
- **Consistent Theming**: Centralized styling through UIHelper class
- **Responsive Layout**: Adapts to different window sizes
- **Professional Appearance**: Clean, modern interface suitable for business use
- **Windows Forms Development**: Advanced Windows Forms techniques with .NET 8.0
- **SQLite Database Integration**: Local database with proper data management
- **Repository Pattern**: Clean separation of data access logic
- **Data Validation**: Comprehensive input validation and error handling

## 📄 License

This project is licensed under the MIT License - see the LICENSE.txt file for details.