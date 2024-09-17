ASP.NET Web App Dashboard
Project Overview
This project is an ASP.NET Web Application designed to provide an administrative dashboard where users can manage categories, products, and roles. The application includes user role-based authentication, allowing different access levels and actions based on user roles.

Key Features:
Category Management: Add, edit, delete, and view categories.
Product Management: Manage products (add, update, delete, view), with the ability to filter and sort by various criteria.
User Role Management: Role-based access control where admins can assign roles (e.g., Admin, Manager, Editor, etc.) and manage user permissions within the dashboard.
Sorting and Filtering: Customize the view of categories and products with sorting by different fields and filtering by various attributes.
Search Functionality: Search categories or products by name or other attributes.
Responsive UI: The dashboard is optimized for all devices, providing a smooth experience on desktops, tablets, and mobile devices.

Table of Contents
Technologies Used
Usage
Authentication and Authorization


1) Technologies Used
ASP.NET Core 8.x (MVC Pattern)
Entity Framework Core for database management
SQL Server (or any supported database provider)
Bootstrap 5.x for responsive design
Mailtrap for handling email services (for actions like registration confirmation, etc.)
LINQ for sorting and filtering data
Git for version control
JavaScript for interactive features
HTML/CSS for frontend design

2) Usage
Categories Management
Create New Category: Navigate to Categories -> Add New Category.
Edit Category: Choose a category and click "Edit".
Delete Category: You can delete a category, but make sure no products are associated with it.
Products Management
Create New Product: Navigate to Products -> Add New Product to add a new product with details such as name, price, category, etc.
Edit Product: Click on the product's "Edit" button to modify its details.
Delete Product: Ensure that a product has no active orders associated before deleting it.
Sorting and Filtering
Use the dropdown in the product or category list to choose the column to sort by (e.g., name, price).
Use the search bar to filter items by name, price, or category.


3) Role-Based Access
Admin: Can manage categories, products, and users.
Product Manager: Can manage categories and products but cannot manage user roles.
Category Manager: Can only Add,view,Delete and edit categories 
