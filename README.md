```markdown
# 🍽️ Restaurant Order Management System (Restoran Sipariş Uygulaması)

A comprehensive Windows Forms-based point-of-sale and restaurant management system built with C# .NET Framework, designed to handle the complete restaurant workflow from order creation through payment processing, kitchen operations, and reporting.

## 🚀 Features

### User Authentication & Role-Based Access
- **Manager**: Staff management, table configuration, daily reporting
- **Chef**: Menu editing, product management, price updates  
- **Cashier**: Order completion, payment processing, revenue reports
- **Waiter**: Table status management, order taking and editing

### Order Management System
- Table-based order creation, updates, and cancellation
- Real-time order status tracking (*Preparing, Completed, Cancelled*)
- Kitchen display system for order preparation
- Waiter assignment and table selection

### Menu & Product Management
- Category-based menu organization
- Product inventory with stock control
- Automatic removal of out-of-stock items
- Price and availability management

### Payment & Billing
- Multiple payment methods (Cash, Card)
- Professional receipt generation with Crystal Reports
- Comprehensive transaction tracking

### Reporting & Analytics
- Daily and detailed business reports
- Staff performance analytics
- Revenue and expense tracking
- Crystal Reports integration for professional reporting

## 🏗️ System Architecture

### Technology Stack
- **Framework**: .NET Framework 4.7.2
- **UI Framework**: Windows Forms + Guna.UI2.WinForms
- **Database**: SQL Server (`DbRestoranSiparis`)
- **Reporting**: Crystal Reports
- **Architecture**: Multi-Document Interface (MDI)

### Core Components
- **Authentication**: `frmGiris` - User login and role validation
- **Main Dashboard**: `frmAnasayfa` - Central navigation hub
- **Order Processing**: `frmSiparis` - Order creation and management
- **Kitchen Display**: `frmMutfakSayfa` - Kitchen order tracking
- **Payment System**: `frmOdeme` - Payment processing
- **Entity Management**: Product, Category, Table, and Staff management forms
- **Reporting**: `frmRapor` with Crystal Reports integration

## 🗃️ Database Design

**Primary Tables:**
- **Personel** → Staff information (roles, credentials, salary, contact)
- **Masa** → Table capacity and status (*Available, Occupied, Cleaning Required*)
- **Ürün** → Food and beverage information (price, stock, category)
- **Sipariş** → Order basic information (table, waiter, timestamp)
- **Sipariş Detay** → Detailed order items and quantities
- **Ödeme** → Payment information linked to orders
- **Sipariş Durumu** → Order status tracking
- **Menü & Menü-Ürün İlişkisi** → Active menu management

## 💻 Technical Implementation

### Form Inheritance Pattern
- **`sayfaModeli`**: Base template for data management pages
- **`ekleModeli`**: Base template for add/edit forms with standardized CRUD operations

### Resource Management
- Centralized icon and image management through `Properties.Resources`
- Icons8 library integration for consistent UI elements
- Localized interface support

### Database Access
- `AnaSinif` class provides centralized database utilities
- Connection management and common CRUD operations
- User authentication and session management

## 🖥️ User Interface

### Main Application Flow
1. **Login Screen** → Role-based authentication
2. **Dashboard** → Navigation based on user permissions
3. **Order Management** → Table selection, product selection, order processing
4. **Kitchen Display** → Real-time order tracking for kitchen staff
5. **Payment Processing** → Multiple payment methods and receipt generation
6. **Management Interfaces** → Product, table, and staff administration

### Key Screens
- **Table Management** → Visual table layout with occupancy status
- **Menu Interface** → Category-based product selection
- **Payment Terminal** → Professional checkout experience
- **Kitchen Display** → Order queue management
- **Reporting Dashboard** → Business analytics and insights

## ⚡ Installation & Setup

### Prerequisites
- Visual Studio 2017 or later
- .NET Framework 4.7.2
- SQL Server (LocalDB or full instance)
- Crystal Reports Runtime

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/harunabkn/Restoran_Siparis_Uygulamasi.git
   ```

2. **Database Setup**
   - Create SQL Server database named `DbRestoranSiparis`
   - Run database scripts to create tables and relationships
   - Update connection string in `app.config`

3. **Dependencies**
   - Install Guna.UI2.WinForms NuGet package
   - Ensure Crystal Reports runtime is installed

4. **Build and Run**
   - Open solution in Visual Studio
   - Build the project
   - Run in Debug mode for development

## 📁 Project Structure

```
Restoran_Siparis_Uygulamasi/
├── 📂 Sayfalar/              # Application forms and pages
├── 📂 Properties/            # Application properties and resources
├── 📂 Resources/             # Icons, images, and static assets
├── 📂 bin/                   # Compiled binaries
├── 📂 obj/                   # Build artifacts
├── Program.cs                # Application entry point
├── AnaSinif.cs              # Core database utilities
├── CrystalReport1.rpt       # Report template
└── *.csproj                 # Project configuration
```

## 🔧 Configuration

### Database Connection
Update the connection string in `app.config`:
```xml
<connectionStrings>
  <add name="DbRestoranSiparis" 
       connectionString="Server=.;Database=DbRestoranSiparis;Integrated Security=true;" />
</connectionStrings>
```

### User Roles
Default user roles can be configured through the staff management interface:
- Manager (Full access)
- Chef (Menu and kitchen access)
- Cashier (Order and payment access)
- Waiter (Order taking access)

## 🤝 Contributing

This project was developed as an educational initiative. Contributions are welcome for:
- UI/UX improvements
- Additional reporting features
- Performance optimizations
- Database schema enhancements

## 👨‍💻 Development Team

- **Harun Abukan** → Database & Data Management
- **Backend Development** → Business logic and system architecture
- **UI/UX Design** → Form design and user experience

## 📜 License

This project is developed for educational purposes.

**MIT License** - See LICENSE file for details

## 🔗 Additional Resources

- [System Architecture Documentation](wiki/System-Architecture)
- [Database Schema](wiki/Database-Design)
- [User Manual](wiki/User-Guide)
- [API Documentation](wiki/API-Reference)

---

*For technical support or questions, please open an issue in the GitHub repository.*
