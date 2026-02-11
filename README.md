# Order Management System

ASP.NET Core Web API with **Onion Architecture** for managing products, orders, customers, and invoices.

---

## 🚀 Features

- Product Management (CRUD)
- Order Processing (Stock validation, Tiered discounts, Invoice generation)
- Customer Management
- JWT Authentication & Role-Based Authorization
- Email Notifications
- Payment Methods: CreditCard, PayPal, BankTransfer, Cash

---

## 🔧 Tech Stack

- .NET 8 | SQL Server | EF Core
- JWT Authentication | AutoMapper | Autofac
- Swagger/OpenAPI

---

## ⚙️ Setup

1. **Clone**
```bash
   git clone 
```

2. **Update Connection String** in `appsettings.json`
```json
   "ConnectionStrings": {
     "cs": "Server=YOUR_SERVER;Database=OrderManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
```

3. **Run**
```bash
   cd OrderManagementSystem.Api
   dotnet run
```
   Database auto-migrates and seeds on first run.

4. **Swagger**: `https://localhost:7XXX/swagger`

---

## 🔐 Test Credentials

**Admin**: `admin` / `Admin@123`  
**Customer**: `customer1` / `Customer@123`

---

## 📊 Seeded Data

- 20 Products
- 4 Customers
- 8 Orders with Invoices

---
## Email Notifications

Email notifications are configured using Ethereal Email (test service).
Emails are sent when order status changes but are not delivered to real recipients.
To view sent emails, check: https://ethereal.email/messages

## 🔗 Key Endpoints
```
POST   /api/Auth/Login
POST   /api/Orders/CreateOrder
GET    /api/Orders/GetAllOrders (Admin)
PUT    /api/Orders/UpdateOrderStatus/{id}/status (Admin)
GET    /api/Products/GetAllProducts
```

---

## 🎯 Business Rules

- **Discounts**: 5% over $100, 10% over $200
- **Stock Validation**: Checks before order
- **Auto Invoice**: Generated on order creation
- **Email**: Sent on status change

---
