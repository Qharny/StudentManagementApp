# Student Management Application

A comprehensive C# Windows Forms application for managing student records using SQLite database.

## Features

- **Add Students**: Add new student records with comprehensive information
- **View Students**: Display all students in a DataGridView with sorting capabilities
- **Edit Students**: Modify existing student information
- **Delete Students**: Remove student records with confirmation
- **Search Students**: Search by student number, name, or email
- **Data Validation**: Comprehensive validation for required fields and data formats
- **SQLite Database**: Local file-based database for data persistence

## Student Information Fields

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

## Technical Details

- **Framework**: .NET 8.0 Windows Forms
- **Database**: SQLite (Microsoft.Data.Sqlite)
- **Architecture**: Repository pattern with data access layer
- **Validation**: Data annotations and custom validation
- **UI**: Modern Windows Forms interface with proper error handling

## Project Structure

```
StudentManagementApp/
├── Models/
│   └── Student.cs                 # Student entity model
├── Data/
│   └── StudentRepository.cs       # Data access layer
├── MainForm.cs                    # Main application form
├── MainForm.Designer.cs           # Main form designer
├── AddEditStudentForm.cs          # Add/Edit student form
├── AddEditStudentForm.Designer.cs # Add/Edit form designer
├── Program.cs                     # Application entry point
└── StudentManagementApp.csproj    # Project file
```

## Database

The application automatically creates a SQLite database file named `StudentManagement.db` in the application directory on first run. The database includes:

- **Students Table**: Stores all student information
- **Auto-incrementing ID**: Primary key for each student
- **Unique Student Number**: Ensures no duplicate student numbers
- **Proper Data Types**: Appropriate SQLite data types for each field

## Getting Started

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

## Usage

1. **Adding Students**: Click the "Add" button to open the student entry form
2. **Editing Students**: Select a student and click "Edit" or double-click the row
3. **Deleting Students**: Select a student and click "Delete" (confirmation required)
4. **Searching**: Use the search box to find students by name, number, or email
5. **Refreshing**: Click "Refresh" to reload the student list

## Validation Rules

- **Required Fields**: Student Number, First Name, Last Name
- **Unique Student Number**: No duplicate student numbers allowed
- **Email Format**: Valid email format when provided
- **Year Level**: Must be selected from dropdown (1-6)

## Error Handling

- **User-Friendly Messages**: Clear error messages for validation failures
- **Database Errors**: Proper exception handling with informative messages
- **Confirmation Dialogs**: Delete confirmations to prevent accidental deletions

## Development

This application demonstrates:
- Windows Forms development with .NET 8.0
- SQLite database integration
- Repository pattern implementation
- Data validation and error handling
- Modern UI design principles

## License

This project is licensed under the MIT License - see the LICENSE.txt file for details.