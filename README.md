# 🛒 MyShop

MyShop is a simple online store built with ASP.NET Core.  
It includes user authentication, product browsing, category filtering, a shopping cart, and an admin panel for managing products and users.

## ✨ Features

- User registration
- User login and logout
- Cookie-based authentication
- Form validation
- Product listing
- Product details page
- Browse products by category
- Add products to shopping cart
- Remove products from shopping cart
- Admin panel for product management
- User management section in the admin area

## 🏗️ Project Structure

The solution is organized into three projects:

- **MyShop**  
  Main web application with controllers, views, Razor Pages, and static files

- **DataLayer**  
  Database context and repository classes

- **Models**  
  Entity models, view models, and shared classes

## 🛠️ Technologies

- ASP.NET Core MVC
- Razor Pages
- Entity Framework Core
- SQL Server
- Cookie Authentication

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/alibm1383/MyShop.git
```

### 2. Open the solution

Open the `MyShop.sln` file in Visual Studio.

### 3. Configure the database

Update the connection string in `appsettings.json` based on your local SQL Server configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MyShopDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 4. Apply migrations

Migrations are stored in the `Migration` folder.

You can update the database using one of the following commands:

**Package Manager Console**

```bash
Update-Database
```

**.NET CLI**

```bash
dotnet ef database update
```

### 5. Run the application

Run the project from Visual Studio.

## 🔐 Admin Area

The admin panel is available under `/admin`

The user management page is available under `/admin/manageusers`

## 🛍️ Shopping Cart

Authenticated users can add products to their shopping cart and remove them when needed.
