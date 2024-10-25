# Role-Based Authorization API in .NET Core

This project demonstrates a **Role-Based Authorization** system using **.NET Core Web API**. It manages user authentication and access control by defining Admin and User roles and restricting access to certain API endpoints based on these roles. It also includes encryption and decryption for sensitive data handling.

## ✨ Features

- 🔐 **User Authentication**: Token-based authentication using **JWT** (JSON Web Tokens).
- 🛡️ **Role-Based Access Control**: Define roles (e.g., Admin and User) to control access to specific API resources.
- 🔒 **Encryption/Decryption**: Ensures sensitive data is encrypted during storage and transmission using AES (Advanced Encryption Standard).
- 🔍 **Secure API Endpoints**: Prevent unauthorized access by securing endpoints.
- 📄 **API Documentation**: Comprehensive documentation with **Swagger**.

## 🔧 Technologies Used

- **ASP.NET Core 6.0**
- **Entity Framework Core** for database interaction
- **JWT Bearer Tokens** for secure authentication
- **SQL Server** for the database
- **Swagger** for API documentation

## 🚀 Installation

Follow these steps to set up the project locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/kirtanp04/RoleBasedAuthApi.git
