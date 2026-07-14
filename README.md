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
| ASP.NET Core MVC | .NET 10* | Web 框架 |
| Razor | - | 视图引擎 |
| Entity Framework Core | 10.x | ORM |
| SQL Server LocalDB / SQLite | - | 数据库（macOS 自动回退 SQLite） |
| Bootstrap | 5.x | CSS 框架 |

> \* 文档原定 .NET 8，因开发环境仅安装 .NET 10 SDK，功能无差异。

---

## 目录结构

```
LibrarySeatReservation/
├── docs/                              # 项目文档（16 份）
├── prototype/static-v1/              # 静态原型（9 个 HTML 页面）
├── src/LibrarySeatReservation.Web/   # 主项目
│   ├── Controllers/                   # 控制器层
│   │   ├── HomeController.cs          ✅ Sprint 1
│   │   ├── SeatController.cs          ✅ Sprint 1
│   │   ├── AdminController.cs            ✅ Sprint 3+4
│   ├── Services/                      # 业务逻辑层 ✅
│   │   ├── ISeatService.cs
│   │   ├── SeatService.cs
│   │   ├── IReservationService.cs
│   │   ├── ReservationService.cs
│   │   ├── IAdminAuthService.cs
│   │   └── AdminAuthService.cs
│   ├── DataAccess/                    # 数据访问层 ✅
│   │   └── AppDbContext.cs
│   ├── Models/
│   │   ├── Entities/                  # 实体类 ✅
│   │   │   ├── Seat.cs
│   │   │   ├── Reservation.cs
│   │   │   └── DemoUser.cs
│   │   └── ViewModels/               # 视图模型 ✅
│   │       ├── SeatListViewModel.cs
│   │       ├── SeatDetailViewModel.cs
│   │       ├── ReservationCreateViewModel.cs
│   │       ├── MyBookingsViewModel.cs
│   │       └── Admin/
│   │           ├── AdminReservationListViewModel.cs
│   │           └── AdminSeatListViewModel.cs
│   ├── Views/                         # Razor 视图
│   │   ├── Home/Index.cshtml          ✅ Sprint 1
│   │   ├── Seat/List.cshtml           ✅ Sprint 1
│   │   ├── Seat/Detail.cshtml         ✅ Sprint 1
│   │   ├── Admin/Login.cshtml         ✅ Sprint 3
│   │   ├── Admin/ReservationList.cshtml ✅ Sprint 3
│   │   ├── Admin/SeatList.cshtml       ✅ Sprint 3
│   │   └── Shared/
│   │       ├── _Layout.cshtml         ✅ Sprint 1
│   │       └── _AdminLayout.cshtml    ✅ Sprint 3
│   ├── Migrations/                    # EF Core 迁移 ✅
│   ├── wwwroot/                       # 静态资源
│   │   └── css/custom.css             ✅ Sprint 1
│   ├── Program.cs                     # 启动入口 ✅
│   ├── appsettings.json
│   └── LibrarySeatReservation.Web.csproj
├── LibrarySeatReservation.sln
└── README.md
```

> ✅ = Sprint 0 已创建 | 空白 = 对应 Sprint 实现时填充

---

## 运行前提

| 工具 | 版本要求 | 说明 |
|------|----------|------|
| .NET SDK | 8.0+ | [下载地址](https://dotnet.microsoft.com/download) |
| SQL Server LocalDB | - | Visual Studio 自带，或单独安装 |
| IDE | - | Visual Studio 2022 / VS Code + C# 扩展 |

```bash
# 检查 .NET SDK 版本
dotnet --version

# 检查 SQL Server LocalDB
sqlcmd -S "(localdb)\mssqllocaldb" -Q "SELECT 1"
```

---

## 快速开始

```bash
# 1. 克隆仓库
git clone https://github.com/2864284429-tec/241060561223.git
cd 241060561223

# 2. 还原依赖
dotnet restore

# 3. 运行项目（数据库自动迁移 + 种子数据自动初始化）
dotnet run --project src/LibrarySeatReservation.Web

# 4. 访问应用
# 用户端：http://localhost:5123
# 管理端：http://localhost:5123/Admin/Login
```

> 数据库建表和种子数据在首次启动时通过 `Program.cs` 中的 `db.Database.EnsureCreated()` + `SeedData.Initialize()` 自动完成。

---

## 演示账号

### 学生用户

| 账号 ID | 姓名 | 说明 |
|---------|------|------|
| user1 | 小王 | 体验账号 |
| user2 | 小李 | 体验账号 |
| user3 | 小张 | 体验账号 |

### 管理员

| 用户名 | 密码 | 说明 |
|--------|------|------|
| admin | 123456 | 硬编码，不存储在数据库 |

---

## 已实现范围

| 阶段 | 已完成内容 | 状态 |
|------|------------|------|
| 文档阶段 | 项目立项、需求分析、PRD、页面树、UI 规范、原型、系统设计、数据库设计、关键链路设计、一致性审计 | ✅ |
| Sprint 0 | 项目骨架（sln + csproj）、Entity 实体类、DbContext + 种子数据、Service 接口与实现（含 SeatService CRUD + ReservationService）、EF Core 迁移、Program.cs 配置 | ✅ |
| Sprint 1 | 用户首页、座位列表、座位详情（Controller + Razor 视图 + 自定义样式） | ✅ |
| Sprint 2 | 预约提交、我的预约、取消预约 | ⏳ 待开发 |
| Sprint 3（第 1 轮） | 管理员登录（GET/POST）、Session 权限守卫、预约管理页（最小可用版）、管理端布局（侧边栏） | ✅ |
| Sprint 3（第 2 轮） | 座位管理（AdminSeatController + SeatList 视图 + CRUD + 管理端布局 CSS） | ✅ |
| Sprint 4（第 1 轮） | 联调测试（脚本冒烟 7/7 + Playwright 自动化 10/10）、P0 Bug 修复（3 个全部闭环）、UI 细节调整、docs/16 联调测试与缺陷闭环记录 | ✅ |
| Sprint 4（第 2 轮） | 统计页功能 | ⏳ 待开发 |

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
- [开发前一致性总审计](docs/11-开发前一致性总审计.md)
- [开发起步与骨架记录](docs/12-开发起步与骨架记录.md)
- [用户端主链路开发记录](docs/13-用户端主链路开发记录.md)
- [管理端与权限开发记录](docs/14-管理端与权限开发记录.md)
- [功能完善与体验优化记录](docs/15-功能完善与体验优化记录.md)
- [联调测试与缺陷闭环记录](docs/16-联调测试与缺陷闭环.md)

---

## 许可证

本项目仅用于课程实践，不涉及商业用途。

---

## 当前已知限制

| 限制 | 说明 | 计划处理 |
|------|------|----------|
| 管理员登录无验证码 | 硬编码账号密码，无防暴力破解 | 课堂项目不实现 |
| 预约管理无筛选/删除 | 仅显示全部记录 | Sprint 3 第 2 轮 |
| ~~座位管理未实现~~ | ~~无法新增/编辑/删除座位~~ | ✅ Sprint 3 第 2 轮完成 |
| 统计页未实现 | 无预约统计数据 | Sprint 4 |
| 用户端无 Session | 首页未显示当前用户名 | Sprint 2 |
| 预约提交/取消未实现 | 用户端闭环未完成 | Sprint 2 |
