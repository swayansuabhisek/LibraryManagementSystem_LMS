# 🚀 Library Management System (.NET + EF Core + SQL Server)


This document explains how to set up and run the project locally.

---

## ✅ Prerequisites

Make sure you have the following installed:

- .NET SDK (Recommended: .NET 8 or project target version)
- SQL Server (LocalDB / SQL Server Express / Docker SQL Server)
- Git
- IDE (Visual Studio or Rider)

You can verify .NET installation using:

```bash
dotnet --version
```

---

## 🚀 Setup & Run Instructions

### 🔹 Step 1: Clone the Repository

```bash
git clone https://github.com/swayansuabhisek/LibraryManagementSystem.git
cd LibraryManagementSystem/LMS
```

---

### 🔹 Step 2: Build the Solution

```bash
dotnet restore
dotnet build
```

OR

Open the solution in your IDE and build it.  
Make sure `LMS.API` is set as the Startup Project.

---

### 🔹 Step 3: Configure Database Connection

Open:

```
LMS.API/appsettings.json
```

Update the connection string parameter:

```
"LMSConnString"
```

Modify the Server name and Database name according to your local setup.

---

### 🔹 Step 4: Apply Database Migration

```bash
dotnet ef database update \
  --project LMS.Infrastructure \
  --startup-project LMS.API
```

This will:
- Create the database (if it does not exist)
- Apply all existing migrations

---

### 🔹 Step 5: Run the Project

Run using CLI:

```bash
dotnet run --project LMS.API
```

OR

Run the `LMS.API` project directly from your IDE.

Then open in browser:

```
https://localhost:<port>/swagger/index.html
```

---

## 🎯 You're Ready!

The API should now be running locally with Swagger UI available.
