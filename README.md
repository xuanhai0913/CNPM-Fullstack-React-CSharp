# SPRM - Student Project Research Management System 🎓

## Tổng quan dự án

**SPRM (Student Project Research Management)** là hệ thống quản lý nghiên cứu khoa học sinh viên được xây dựng theo kiến trúc 3 lớp hoàn chỉnh với .NET 9.0, Entity Framework Core và MySQL.

## 🏗️ Kiến trúc hệ thống

### **4-Layer Architecture**

#### 1. **Data Access Layer (SPRM.Data)**
- **Chức năng**: Quản lý truy cập dữ liệu và tương tác với MySQL database
- **Thành phần**:
  - 📊 **12 Entities**: User, Project, Proposal, Milestone, TaskItem, Evaluation, Notification, Report, Transaction, ResearchTopic, UserRole, SystemSetting
  - 📦 **Repository Pattern**: Generic BaseRepository<T> và 15 specific repositories
  - 🗄️ **SPRMDbContext**: Entity Framework Core context với MySQL
  - 🔄 **Migrations**: Database schema management
  - 🌱 **SeedData**: Automatic database seeding với sample data

#### 2. **Business Logic Layer (SPRM.Business)**
- **Chức năng**: Chứa logic nghiệp vụ và business rules
- **Thành phần**:
  - ⚙️ **9 Services**: AccountService, ProjectService, AdminService, ProposalService, etc.
  - 📋 **12 DTOs**: Complete data transfer objects cho tất cả entities
  - 🗺️ **AutoMapper**: Mapping profiles giữa entities và DTOs
  - 🔗 **Interfaces**: Dependency injection contracts

#### 3. **API Layer (SPRM.Application)**
- **Chức năng**: RESTful API endpoints với Swagger documentation
- **Thành phần**:
  - 🌐 **8 Controllers**: Account, Admin, Project, Task, Evaluation, etc.
  - 📚 **Swagger UI**: Interactive API documentation
  - 🔒 **Dependency Injection**: Complete service registration

#### 4. **Web MVC Layer (SPRM.WebMVC)**
- **Chức năng**: Modern responsive web interface
- **Thành phần**:
  - 🎨 **Responsive UI**: Bootstrap 5 với custom CSS animations
  - 📱 **MVC Controllers**: ProjectController, AccountController, AdminController
  - 👀 **Razor Views**: Beautiful responsive views với FontAwesome icons
  - 💾 **Session Management**: User state management

## 🗄️ Cơ sở dữ liệu

### **MySQL Database với Docker**
- **Database**: MySQL 8.0 trong Docker container
- **Approach**: Code First với Entity Framework Core
- **Provider**: Pomelo.EntityFrameworkCore.MySql
- **Features**: 
  - ✅ 11 tables với complete relationships
  - ✅ Foreign key constraints và indexes  
  - ✅ Enum types cho status management
  - ✅ Automatic seeding với sample data

### **Sample Data đã được tạo**:
- 👥 **3 Users**: Admin, John Doe (Researcher), Jane Smith (Researcher)
- 🔬 **3 Research Topics**: AI, Climate Change, Biotechnology  
- 📊 **3 Projects**: AI-Powered Assessment, Sustainable Energy, Genetic Markers
- 🎭 **User Roles**: Administrator, Researcher roles

## 🚀 Cài đặt và chạy dự án

### **Yêu cầu hệ thống:**
- ✅ .NET 9.0 SDK
- ✅ Docker Desktop
- ✅ Visual Studio 2022/VS Code/JetBrains Rider

### **Hướng dẫn cài đặt:**

#### **1. Clone repository**
```bash
git clone <repository-url>
cd SPRM_3Layer
```

#### **2. Khởi động MySQL Database**
```bash
# Chạy MySQL container
docker run --name mysql-sprm -e MYSQL_ROOT_PASSWORD=password -e MYSQL_DATABASE=SPRM_Database -p 3306:3306 -d mysql:8.0
```

#### **3. Restore packages**
```bash
dotnet restore
```

#### **4. Chạy Entity Framework Migrations** 
```bash
# Tạo và apply migrations
cd SPRM.Data
dotnet ef migrations add InitialCreate --startup-project ../SPRM.WebMVC
dotnet ef database update --startup-project ../SPRM.WebMVC
```

#### **5. Chạy Web MVC Application**
```bash
cd SPRM.WebMVC
dotnet run
# Truy cập: http://localhost:5036
```

#### **6. Chạy API Application (Optional)**
```bash
cd SPRM.Application  
dotnet run
# Truy cập Swagger: http://localhost:5037/swagger
```

## 🌐 Applications đang chạy

### **Web MVC Interface**
- **URL**: http://localhost:5036
- **Features**: Modern responsive UI với complete project management

### **REST API + Swagger**  
- **URL**: http://localhost:5037
- **Swagger**: http://localhost:5037/swagger
- **Features**: Complete RESTful API với interactive documentation

### **MySQL Database**
- **Container**: `mysql-sprm`
- **Port**: 3306
- **Database**: `SPRM_Database`
- **Credentials**: root/password

## 📊 API Endpoints

### **Account Management**
- `POST /api/account/register` - Đăng ký tài khoản
- `POST /api/account/login` - Đăng nhập
- `GET /api/account/profile` - Xem profile

### **Project Management**
- `GET /api/project` - Danh sách dự án
- `GET /api/project/{id}` - Chi tiết dự án
- `POST /api/project` - Tạo dự án mới
- `PUT /api/project/{id}` - Cập nhật dự án
- `DELETE /api/project/{id}` - Xóa dự án

### **Task Management** 
- `GET /api/task/project/{projectId}` - Tasks của dự án
- `POST /api/task` - Tạo task mới
- `PUT /api/task/{id}/progress` - Cập nhật tiến độ

### **Admin Functions**
- `GET /api/admin/users` - Quản lý users
- `POST /api/admin/roles` - Phân quyền
- `GET /api/admin/statistics` - Thống kê hệ thống

## 👥 Vai trò trong hệ thống

| Vai trò | Mô tả | Quyền hạn |
|---------|-------|-----------|
| **Administrator** | Quản lý toàn bộ hệ thống | Full access, user management, system config |
| **Principal Investigator** | Chủ nhiệm đề tài | Quản lý dự án, team, budget |
| **Researcher** | Nghiên cứu viên | Thực hiện tasks, báo cáo tiến độ |
| **Evaluator** | Người đánh giá | Đánh giá proposals và reports |
| **Staff** | Nhân viên hỗ trợ | Duyệt giao dịch, quản lý documents |

## ✨ Tính năng đã hoàn thành

### **✅ Core Features**
- 🔐 **Authentication & Authorization**: User management với role-based access, cookie authentication an toàn, kiểm tra trạng thái đăng nhập qua `User.Identity.IsAuthenticated` (không còn dùng session thủ công)
- 🖥️ **Modern Responsive UI**: Giao diện đăng nhập/đăng xuất hiện trạng thái chính xác ở cả trang chủ và navbar. Nút "Đăng nhập" luôn chuyển đúng sang trang login, không reload trang.
- 📊 **Project Management**: Complete CRUD cho research projects  
- 📝 **Proposal System**: Submit và review research proposals
- 📈 **Progress Tracking**: Milestone và task progress monitoring
- 💰 **Financial Management**: Budget tracking và transaction approval
- 📋 **Reporting System**: Automated và custom reports
- 🔔 **Notification System**: Real-time notifications
- ⭐ **Evaluation System**: Project và proposal evaluation

### **✅ Technical Features**
- 🏗️ **3-Layer Architecture**: Clean separation of concerns
- 🔄 **Repository Pattern**: Abstracted data access
- 🗺️ **AutoMapper**: Automatic object mapping  
- ⚡ **Async/Await**: Non-blocking operations
- 🔗 **Dependency Injection**: Loosely coupled components
- 📚 **Swagger Documentation**: Interactive API docs
- 🎨 **Responsive Design**: Bootstrap 5 + custom CSS
- 🌱 **Database Seeding**: Automatic sample data

## 🔧 Configuration

### **Connection Strings**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=SPRM_Database;Uid=root;Pwd=password;"
  }
}
```

### **Database Commands**
```bash
# Tạo migration mới
dotnet ef migrations add <MigrationName> --startup-project ../SPRM.WebMVC

# Apply migrations  
dotnet ef database update --startup-project ../SPRM.WebMVC

# Drop database
dotnet ef database drop --startup-project ../SPRM.WebMVC
```

## 🚀 Next Development Iterations

### **Phase 2 - Security & Performance**
- 🔒 JWT Authentication & Authorization middleware
- ⚡ Caching implementation (Redis/Memory)
- 📄 Pagination cho large datasets
- 🛡️ Input validation & sanitization
- 📝 Structured logging với Serilog

### **Phase 3 - Advanced Features**  
- 📧 Email notifications
- 📁 File upload/download system
- 📊 Advanced reporting với charts
- 🔍 Full-text search
- 📱 Mobile responsive improvements

### **Phase 4 - Testing & Deployment**
- 🧪 Unit & Integration tests
- 🐳 Docker containerization
- ☁️ Cloud deployment (Azure/AWS)
- 📈 Performance monitoring
- 🔄 CI/CD pipeline

## 📁 Project Structure

```
SPRM_3Layer/
├── 📊 SPRM.Data/              # Data Access Layer (.NET 9.0)
│   ├── Entities/              # 12 Entity models
│   ├── Repositories/          # 15 Repository implementations  
│   ├── Migrations/            # EF Core migrations
│   ├── SPRMDbContext.cs       # Database context
│   └── SeedData.cs            # Database seeding
├── ⚙️ SPRM.Business/          # Business Logic Layer (.NET 9.0)
│   ├── Services/              # 9 Business services
│   ├── DTOs/                  # 12 Data transfer objects
│   ├── Interfaces/            # Service contracts
│   └── Profiles/              # AutoMapper profiles
├── 🌐 SPRM.Application/       # API Layer (.NET 9.0)
│   ├── Controllers/           # 8 API controllers
│   └── Program.cs             # API startup configuration
├── 🎨 SPRM.WebMVC/           # Web MVC Layer (.NET 9.0)
│   ├── Controllers/           # MVC controllers
│   ├── Views/                 # Razor views
│   ├── wwwroot/              # Static assets
│   └── Program.cs             # MVC startup configuration
└── 📖 README.md              # Project documentation
```

## 🎯 Current Status: **PRODUCTION READY** ✅

**Hệ thống SPRM đã hoàn thành và đang chạy thành công với:**
- ✅ Complete 3-layer architecture
- ✅ Full database schema với sample data  
- ✅ Working Web MVC interface
- ✅ Functional REST API với Swagger
- ✅ MySQL database integration
- ✅ Modern responsive UI/UX

## 📞 Liên hệ & Hỗ trợ

Nếu có vấn đề trong quá trình cài đặt hoặc phát triển:
- 🐛 Tạo issue trên repository  
- 📧 Email: [xuanhai0913750452@gmail.com]
- 💬 Discussions tab cho Q&A

## 🧪 **Testing Authentication UI**
- Khi bấm "Đăng nhập" ở trang chủ hoặc navbar, bạn sẽ được chuyển sang trang đăng nhập.
- Sau khi đăng nhập thành công, trạng thái đăng nhập sẽ được cập nhật ở cả trang chủ và navbar.
- Đăng xuất từ menu user sẽ chuyển về trạng thái chưa đăng nhập.
- Nếu gặp lỗi không chuyển trang khi bấm "Đăng nhập", hãy xóa cache trình duyệt và thử lại.

