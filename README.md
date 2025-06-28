# E-commerce Application

A basic e-commerce application built with .NET 9 using ASP.NET Core MVC with Razor Pages.

## About the Project

This is a web-based e-commerce application that provides basic functionality for an online sales website. The project was developed to demonstrate fundamental web development concepts using Microsoft technologies.

The UI/UX design and layout inspiration was taken from [Hutche - Furniture Store Template](https://rundemo.hasthemes.com/hutche/) to provide a modern and professional e-commerce interface.

## Technologies Used

- **.NET 9.0** - Core framework
- **ASP.NET Core MVC** - Model-View-Controller architecture
- **Razor Pages** - Template engine for views
- **Entity Framework Core** - ORM for data access
- **SQLite** - Database (configured for Linux compatibility)
- **Bootstrap** - CSS framework for responsiveness
- **Authentication & Authorization** - Cookie-based authentication system

## Features

### Public Area
- **Contact Page** - Only page accessible without authentication
- **Login/Register System** - User authentication
- **Automatic redirection** to login for protected pages

### Client Area (Login Required)
- **Home Page** - Main dashboard with banners
- **About Us** - Company information
- **Shop** - Product catalog
- **Blog** - Articles and news
- **My Account** - Profile management
- **Shopping Cart** - Purchase functionality
- **Search** - Product search system

### Administrative Area
- **Admin Panel** - Separate management interface
- **Banner Management** - Full CRUD operations
- **Menu Management** - Navigation configuration
- **Image Upload** - Upload system for banners

## Project Structure

```
Ecommerce/
├── Areas/Admin/          # Administrative area
├── Controllers/          # MVC Controllers
├── Models/Database/      # Entity Framework models
├── Views/               # Razor views
├── Content/             # Static assets and uploads
│   ├── css/            # Stylesheets
│   ├── js/             # JavaScript files
│   ├── fonts/          # Web fonts
│   ├── images/         # Static images (logo, icons)
│   ├── lib/            # Third-party libraries
│   └── uploads/        # Dynamic uploads (banners)
└── Program.cs           # Main configuration
```

## How to Run

```bash
# Clone the repository
git clone [repository-url]

# Navigate to directory
cd ecommerce/Ecommerce

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

## Database Configuration

The project uses SQLite with Entity Framework Core:
- Database file: `ecommerce.db`
- Configuration: Code-First with automatic migrations
- Automatic table creation on first run

## Authentication

- **Cookie System** - Cookie-based authentication
- **30-minute session** - Configurable timeout
- **Automatic redirection** - To login pages when required
- **Route protection** - Authorization middleware

## Notes

- Project configured for Linux/WSL development environment
- Responsive interface with Bootstrap
- Clear separation between public, client, and administrative areas
- Code optimized for .NET 9 with modern features