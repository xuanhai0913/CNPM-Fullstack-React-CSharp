# 🚀 HƯỚNG DẪN CHẠY DỰ ÁN SPRM (Student Project Research Management)

## 📋 Tổng quan
Đây là hướng dẫn chi tiết từng bước để chạy thành công hệ thống SPRM với kiến trúc 3 lớp ASP.NET Core.

## ✅ Yêu cầu hệ thống
Trước khi bắt đầu, hãy đảm bảo máy tính của bạn đã cài đặt:

- **✅ .NET 9.0 SDK** - [Tải về tại đây](https://dotnet.microsoft.com/download/dotnet/9.0)
- **✅ Docker Desktop** - [Tải về tại đây](https://www.docker.com/products/docker-desktop)
- **✅ Visual Studio Code hoặc Visual Studio 2022** (khuyến nghị)
- **✅ Git** (để clone repository)

## 🔧 BƯỚC 1: Chuẩn bị môi trường

### 1.1 Kiểm tra .NET SDK
```bash
dotnet --version
# Kết quả mong đợi: 9.0.x
```

### 1.2 Kiểm tra Docker
```bash
docker --version
# Khởi động Docker Desktop nếu chưa chạy
```

### 1.3 Clone project (nếu từ repository)
```bash
git clone <repository-url>
cd SPRM_3Layer
```

## 🗄️ BƯỚC 2: Thiết lập Database MySQL

### 2.1 Khởi động MySQL Container
```bash
docker run --name mysql-sprm \
  -e MYSQL_ROOT_PASSWORD=password \
  -e MYSQL_DATABASE=SPRM_Database \
  -p 3306:3306 \
  -d mysql:8.0
```

### 2.2 Kiểm tra container đang chạy
```bash
docker ps
# Bạn sẽ thấy container 'mysql-sprm' trong danh sách
```

### 2.3 (Optional) Kết nối database để kiểm tra
```bash
# Sử dụng mysql client
docker exec -it mysql-sprm mysql -uroot -ppassword
# Hoặc sử dụng MySQL Workbench với connection: localhost:3306, user: root, password: password
```

## 📦 BƯỚC 3: Cài đặt Dependencies

### 3.1 Restore tất cả packages
```bash
# Từ thư mục gốc SPRM_3Layer
dotnet restore
```

### 3.2 Kiểm tra các package quan trọng
```bash
# Kiểm tra xem các package đã được cài đặt
dotnet list package --include-transitive | grep -E "(EntityFrameworkCore|MySQL|AutoMapper)"
```

## 🛠️ BƯỚC 4: Entity Framework Migrations

### 4.1 Tạo Migration (nếu chưa có)
```bash
cd SPRM.Data
dotnet ef migrations add InitialCreate --startup-project ../SPRM.WebMVC
```

### 4.2 Áp dụng Migration vào Database
```bash
dotnet ef database update --startup-project ../SPRM.WebMVC
```

### 4.3 Kiểm tra tables đã được tạo
```bash
# Kết nối vào MySQL và kiểm tra
docker exec -it mysql-sprm mysql -uroot -ppassword -e "USE SPRM_Database; SHOW TABLES;"
```

## 🌐 BƯỚC 5: Chạy Applications

### 5.1 Chạy Web MVC Application (Chính)
```bash
# Mở terminal mới và chạy lệnh:
cd SPRM.WebMVC
dotnet run

# Kết quả mong đợi:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:5036
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shutdown.
```

**🎉 Truy cập Web Interface:** http://localhost:5036

### 5.2 Chạy API Application (Optional)
```bash
# Mở terminal mới và chạy lệnh:
cd SPRM.Application
dotnet run

# Kết quả mong đợi:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:5037
```

**🔗 Truy cập Swagger API:** http://localhost:5037/swagger

## 📊 BƯỚC 6: Kiểm tra Seed Data

Hệ thống sẽ tự động tạo dữ liệu mẫu khi chạy lần đầu:

### 6.1 Người dùng mẫu:
- **Admin User**: 
  - Email: admin@sprm.edu
  - Username: admin  
  - Password: admin123
  - Role: Administrator
- **Manager User**:
  - Email: manager@sprm.edu
  - Username: manager
  - Password: manager123
  - Role: Principal Investigator
- **Researcher User**:
  - Email: researcher@sprm.edu
  - Username: researcher
  - Password: researcher123
  - Role: Researcher

**⚠️ Lưu ý:** Dữ liệu mẫu đã được tắt theo mặc định. Để xóa hoàn toàn dữ liệu mẫu (nếu có), hãy:
1. Đăng nhập với tài khoản admin
2. Vào **Quản trị** > **Quản lý Database** 
3. Nhấn **"Xóa dữ liệu mẫu"**

### 6.2 Tính năng mới:
- **✅ Phân quyền cải tiến**: Chỉ Admin/Administrator mới truy cập được khu vực quản trị
- **✅ Xóa dữ liệu mẫu**: Admin có thể xóa dữ liệu mẫu qua giao diện web
- **✅ Nút đăng xuất**: Menu dropdown với thông tin user và nút đăng xuất an toàn
- **✅ Authentication**: Sử dụng Cookie authentication với BCrypt password hashing

## 🔍 BƯỚC 7: Test các chức năng

### 7.1 Test Web MVC (http://localhost:5036)
1. **Đăng nhập Admin**: Sử dụng admin / admin123
2. **Test phân quyền**: Truy cập menu "Quản trị" (chỉ admin thấy được)
3. **Test đăng xuất**: Click vào dropdown user menu và chọn "Đăng xuất"
4. **Test đăng nhập UI**: Bấm "Đăng nhập" ở trang chủ hoặc navbar sẽ chuyển đúng sang trang login (không reload trang). Sau khi đăng nhập, trạng thái đăng nhập sẽ cập nhật ở cả trang chủ và navbar.
5. **Xóa dữ liệu mẫu**: Vào Quản trị > Quản lý Database > Xóa dữ liệu mẫu
6. **Test user thường**: Đăng ký tài khoản mới và kiểm tra không thấy menu "Quản trị"

**Lưu ý:** Nếu bấm "Đăng nhập" mà không chuyển trang, hãy xóa cache trình duyệt và thử lại.

### 7.2 Test API (http://localhost:5037/swagger)
1. **Mở Swagger UI**
2. **Test GET endpoints**: 
   - /api/Project
   - /api/User
   - /api/ResearchTopic
3. **Kiểm tra response data**

## 🛠️ Troubleshooting

### ❌ Lỗi thường gặp và cách xử lý:

#### 1. MySQL Connection Error
```
Error: Unable to connect to any of the specified MySQL hosts
```
**Giải pháp:**
```bash
# Kiểm tra MySQL container
docker ps -a
# Nếu container stopped, khởi động lại
docker start mysql-sprm
```

#### 2. Port đã được sử dụng
```
Error: Address already in use
```
**Giải pháp:**
```bash
# Tìm process đang sử dụng port
netstat -tulpn | grep :5036
# Kill process hoặc thay đổi port trong appsettings.json
```

#### 3. Migration Error
```
Error: No migrations configuration type was found
```
**Giải pháp:**
```bash
# Đảm bảo chạy lệnh từ đúng thư mục
cd SPRM.Data
dotnet ef migrations add InitialCreate --startup-project ../SPRM.WebMVC
```

#### 4. Package Restore Error
```
Error: Package restore failed
```
**Giải pháp:**
```bash
# Clear NuGet cache và restore lại
dotnet nuget locals all --clear
dotnet restore --force
```

## 📁 Cấu trúc Project

```
SPRM_3Layer/
├── SPRM.Data/           # Data Access Layer
├── SPRM.Business/       # Business Logic Layer  
├── SPRM.Application/    # API Layer (REST API)
├── SPRM.WebMVC/         # Presentation Layer (Web MVC)
└── README.md
```

## 🌟 Features đã hoàn thành

- ✅ **Complete 3-Layer Architecture**
- ✅ **Entity Framework Core với MySQL**
- ✅ **Repository Pattern**
- ✅ **Service Layer**
- ✅ **AutoMapper Integration**
- ✅ **Modern Responsive UI**
- ✅ **REST API với Swagger**
- ✅ **Database Seeding**
- ✅ **Dependency Injection**

## 📞 Hỗ trợ

Nếu gặp vấn đề trong quá trình chạy project, hãy:

1. **Kiểm tra logs** trong terminal
2. **Xem database connection** có thành công không
3. **Kiểm tra tất cả containers** đang chạy
4. **Đảm bảo tất cả ports** (5036, 5037, 3306) không bị conflict

---

**🎉 Chúc bạn chạy project thành công!** 

Sau khi hoàn thành các bước trên, bạn sẽ có một hệ thống quản lý dự án nghiên cứu sinh viên hoàn chỉnh với giao diện web hiện đại và API documentation đầy đủ.
