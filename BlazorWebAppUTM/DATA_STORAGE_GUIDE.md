# ?? Data Storage Guide - User Management CQRS Implementation

## ??? Database Structure

### **Database Name:** `PracticaDB`
### **Connection:** `localhost\SQLEXPRESS`

## ?? Where Your Data is Saved

When you fill out the **User Management** form and click "Create User", the information is saved in the following table:

### **Primary Table: `AspNetUsers`**

| **Form Field** | **Database Column** | **Data Type** | **Description** |
|---|---|---|---|
| **Email** | `Email` | nvarchar(256) | User's email address |
| **Email** | `UserName` | nvarchar(256) | Same as email (Identity requirement) |
| **Password** | `PasswordHash` | nvarchar(max) | Encrypted/hashed password |
| **First Name** | `FirstName` | nvarchar(max) | ? **NEW COLUMN ADDED** |
| **Last Name** | `LastName` | nvarchar(max) | ? **NEW COLUMN ADDED** |
| *(Auto)* | `Id` | nvarchar(450) | Unique user identifier (GUID) |
| *(Auto)* | `CreatedAt` | datetime2 | ? **NEW COLUMN ADDED** |
| *(Auto)* | `EmailConfirmed` | bit | Set to `True` automatically |
| *(Auto)* | `SecurityStamp` | nvarchar(max) | Security token |
| *(Auto)* | `ConcurrencyStamp` | nvarchar(max) | Concurrency control |

## ?? CQRS Data Flow

### **Create User Command Flow:**
```
User Form ? CreateUserCommand ? CreateUserCommandHandler ? AspNetUsers Table
```

1. **User fills form** with Email, Password, First Name, Last Name
2. **CreateUserCommand** captures the data
3. **CreateUserCommandHandler** processes the command
4. **Data saved** to `AspNetUsers` table in `PracticaDB`

### **Get User Query Flow:**
```
User ID Input ? GetUserByIdQuery ? GetUserByIdQueryHandler ? AspNetUsers Table ? Display
```

1. **User enters** User ID
2. **GetUserByIdQuery** searches for the user
3. **GetUserByIdQueryHandler** retrieves data from database
4. **UserDto** returns the formatted data for display

## ??? Related Tables (Created by Identity)

Your database also contains these additional Identity tables:

- `AspNetRoles` - User roles
- `AspNetUserRoles` - User-role relationships
- `AspNetUserClaims` - User claims
- `AspNetUserLogins` - External login providers
- `AspNetUserTokens` - User tokens
- `AspNetRoleClaims` - Role claims
- `__EFMigrationsHistory` - Migration tracking

## ?? How to Verify Data in SQL Server Management Studio

### **Connect to Database:**
1. **Server:** `localhost\SQLEXPRESS`
2. **Database:** `PracticaDB`
3. **Authentication:** Windows Authentication

### **Query to See Your Data:**
```sql
SELECT 
    Id,
    UserName,
    Email,
    FirstName,
    LastName,
    CreatedAt,
    EmailConfirmed
FROM AspNetUsers
ORDER BY CreatedAt DESC
```

### **Sample Data Output:**
```
Id                                   | UserName        | Email           | FirstName | LastName | CreatedAt           | EmailConfirmed
-------------------------------------|-----------------|-----------------|-----------|----------|---------------------|---------------
a1b2c3d4-e5f6-7890-abcd-ef1234567890 | test@example.com| test@example.com| John      | Doe      | 2025-01-04 13:45:30 | 1
```

## ? What Was Fixed

### **Before (Issues):**
- ? First Name and Last Name were **NOT saved**
- ? Only Email and Password were stored
- ? No creation timestamp

### **After (Fixed):**
- ? **First Name** saved to `FirstName` column
- ? **Last Name** saved to `LastName` column  
- ? **Creation timestamp** saved to `CreatedAt` column
- ? **Complete user profile** stored in database
- ? **CQRS pattern** properly implemented with full data persistence

## ?? Testing Your Implementation

1. **Run the application:** `dotnet run`
2. **Navigate to:** User Management page
3. **Create a user** with all fields filled
4. **Check database** with the SQL query above
5. **Verify** all data is saved correctly

Your CQRS implementation now properly saves all user data to the `AspNetUsers` table in your `PracticaDB` database! ??