# 图书馆座位预约系统

> 让同学们在线上看到哪些座位有空位，提前预约，到了图书馆直接坐下学习。

---

## 项目简介

本项目是一个面向大学生的图书馆座位预约系统，采用 ASP.NET Core MVC 技术栈开发。学生用户可以通过系统浏览座位列表、选择可用座位、预约特定时段，并在"我的预约"中查看和取消预约。管理员可以通过后台管理座位信息和查看预约记录。

**一句话描述**：让同学们在线上看到哪些座位有空位，提前预约，到了图书馆直接坐下学习。

---

## 技术栈

| 技术 | 版本 | 用途 |
|------|------|------|
| ASP.NET Core MVC | .NET 8 | Web 框架 |
| Razor | - | 视图引擎 |
| Entity Framework Core | 8.x | ORM |
| SQL Server LocalDB | - | 数据库 |
| Bootstrap | 5.x | CSS 框架 |
| jQuery | 3.x | JS 库（可选） |

---

## 目录结构

### 当前已存在

```
LibrarySeatReservation/
├── docs/                              # 项目文档
│   ├── 01-项目立项单.md
│   ├── 02-需求分析与MVP确认.md
│   ├── 03-PRD-Lite.md
│   ├── 04-页面树与业务流程.md
│   ├── 05-页面卡与UI规范.md
│   ├── 06-静态原型与原型评审.md
│   ├── 07-系统设计说明.md
│   ├── 08-数据库设计.md
│   ├── 09-关键链路详细设计.md
│   └── 10-开发准备与Sprint0.md
│
├── prototype/                         # 静态原型
│   └── static-v1/                     # 第一版静态原型
│       ├── css/custom.css
│       ├── user-index.html
│       ├── seat-list.html
│       ├── seat-detail.html
│       ├── booking-submit.html
│       ├── my-bookings.html
│       ├── admin-login.html
│       ├── admin-booking.html
│       ├── admin-seat.html
│       └── admin-stats.html
│
└── README.md                          # 本文件
```

### 后续计划 / 待生成

```
LibrarySeatReservation/
├── Controllers/                       # 控制器层
│   ├── HomeController.cs              # 用户首页
│   ├── SeatController.cs              # 座位列表、详情
│   ├── ReservationController.cs       # 预约提交、我的预约、取消
│   └── Admin/
│       ├── AdminAuthController.cs     # 管理员登录
│       ├── AdminHomeController.cs     # 预约管理
│       └── AdminSeatController.cs     # 座位管理
│
├── Services/                          # 业务逻辑层
│   ├── ISeatService.cs
│   ├── SeatService.cs
│   ├── IReservationService.cs
│   ├── ReservationService.cs
│   ├── IAdminAuthService.cs
│   └── AdminAuthService.cs
│
├── DataAccess/                        # 数据访问层
│   ├── AppDbContext.cs
│   └── SeedData.cs
│
├── Models/
│   ├── Entities/
│   │   ├── Seat.cs
│   │   ├── Reservation.cs
│   │   └── DemoUser.cs
│   └── ViewModels/
│       ├── SeatListViewModel.cs
│       ├── SeatDetailViewModel.cs
│       ├── ReservationCreateViewModel.cs
│       ├── MyBookingsViewModel.cs
│       ├── BookingItem.cs
│       └── Admin/
│           ├── AdminSeatListViewModel.cs
│           ├── AdminSeatItem.cs
│           └── AdminReservationListViewModel.cs
│
├── Views/
│   ├── Home/Index.cshtml
│   ├── Seat/List.cshtml
│   ├── Seat/Detail.cshtml
│   ├── Reservation/Create.cshtml
│   ├── Reservation/My.cshtml
│   ├── Admin/Login.cshtml
│   ├── Admin/ReservationList.cshtml
│   ├── Admin/SeatList.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _AdminLayout.cshtml
│       └── _ViewStart.cshtml
│
├── wwwroot/
│   ├── css/custom.css
│   └── js/site.js
│
├── Program.cs
├── appsettings.json
└── LibrarySeatReservation.csproj
```

---

## 运行前提

### 开发环境

| 工具 | 版本要求 | 说明 |
|------|----------|------|
| .NET SDK | 8.0+ | [下载地址](https://dotnet.microsoft.com/download/dotnet/8.0) |
| SQL Server LocalDB | - | Visual Studio 自带，或单独安装 |
| IDE | - | Visual Studio 2022 / VS Code + C# 扩展 |

### 验证环境

```bash
# 检查 .NET SDK 版本
dotnet --version

# 检查 SQL Server LocalDB
sqlcmd -S "(localdb)\mssqllocaldb" -Q "SELECT 1"
```

---

## 快速开始

> ⚠️ 以下步骤需要在"开发起步与项目骨架"阶段完成后才能执行。

```bash
# 1. 克隆仓库
git clone https://github.com/your-username/LibrarySeatReservation.git
cd LibrarySeatReservation

# 2. 还原依赖
dotnet restore

# 3. 数据库迁移
dotnet ef migrations add InitialCreate
dotnet ef database update

# 4. 运行项目
dotnet run

# 5. 访问应用
# 用户端：http://localhost:5000
# 管理端：http://localhost:5000/Admin/Login
```

---

## 已实现范围

> 📝 本节将在每个 Sprint 完成后更新。

| 阶段 | 已完成内容 | 状态 |
|------|------------|------|
| 文档阶段 | 项目立项、需求分析、PRD、页面树、UI 规范、原型、系统设计、数据库设计、关键链路设计 | ✅ |
| Sprint 0 | 项目骨架、数据库迁移、种子数据 | ⏳ 待开发 |
| Sprint 1 | 用户首页、座位列表、座位详情 | ⏳ 待开发 |
| Sprint 2 | 预约提交、我的预约、取消预约 | ⏳ 待开发 |
| Sprint 3 | 管理员登录、预约管理、座位管理 | ⏳ 待开发 |
| Sprint 4 | 统计页、联调测试 | ⏳ 待开发 |

---

## 数据库初始化

> ⚠️ 需要先执行数据库迁移。

```bash
# 创建迁移
dotnet ef migrations add InitialCreate

# 应用迁移到数据库
dotnet ef database update
```

种子数据会在首次运行时自动初始化（通过 `Program.cs` 中的 `SeedData.Initialize()` 方法）。

---

## 演示账号

### 学生用户（体验账号）

| 账号 ID | 姓名 | 说明 |
|---------|------|------|
| user1 | 小王 | 体验账号 |
| user2 | 小李 | 体验账号 |
| user3 | 小张 | 体验账号 |

> 切换账号通过页面上的下拉框或按钮实现，数据存储在 Session 中。

### 管理员

| 用户名 | 密码 | 说明 |
|--------|------|------|
| admin | 123456 | 硬编码，不存储在数据库 |

---

## 已知限制

| 项目 | 说明 |
|------|------|
| 并发处理 | 单人课堂项目，不考虑高并发 |
| 用户认证 | 体验账号切换，无注册登录体系 |
| 数据导出 | 不支持 Excel/CSV 导出 |
| 消息通知 | 不支持短信/邮件通知 |
| 移动端适配 | 以 PC 端演示为主 |

---

## 相关文档

- [项目立项单](docs/01-项目立项单.md)
- [需求分析与 MVP 确认](docs/02-需求分析与MVP确认.md)
- [PRD Lite](docs/03-PRD-Lite.md)
- [页面树与业务流程](docs/04-页面树与业务流程.md)
- [页面卡与 UI 规范](docs/05-页面卡与UI规范.md)
- [静态原型与原型评审](docs/06-静态原型与原型评审.md)
- [系统设计说明](docs/07-系统设计说明.md)
- [数据库设计](docs/08-数据库设计.md)
- [关键链路详细设计](docs/09-关键链路详细设计.md)
- [开发准备与 Sprint 0](docs/10-开发准备与Sprint0.md)

---

## 许可证

本项目仅用于课程实践，不涉及商业用途。
