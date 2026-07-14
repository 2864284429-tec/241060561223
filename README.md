# 图书馆座位预约系统

> 让同学们在线上看到哪些座位有空位，提前预约，到了图书馆直接坐下学习。

---

## 项目简介

本项目是一个面向大学生的图书馆座位预约系统，采用 ASP.NET Core MVC 技术栈开发。学生用户可以通过系统浏览座位列表、选择可用座位，管理员可以通过后台管理座位信息、查看预约记录。

**项目性质**：课程实践项目（软件工程方法与实践）

**一句话描述**：让同学们在线上看到哪些座位有空位，提前预约，到了图书馆直接坐下学习。

---

## 技术栈

| 技术 | 版本 | 用途 |
|------|------|------|
| ASP.NET Core MVC | .NET 10* | Web 框架 |
| Razor | - | 视图引擎 |
| Entity Framework Core | 10.x | ORM（Code First） |
| SQLite | - | 数据库（开发环境自动回退） |
| Bootstrap | 5.x | CSS 框架 |
| Playwright | 1.52+ | 自动化测试 |

> \* 文档原定 .NET 8，因开发环境仅安装 .NET 10 SDK，功能无差异。

---

## 功能清单

### 已实现

| 功能 | 入口 | 说明 |
|------|------|------|
| 用户首页 | `/` | 欢迎页，导航到座位列表 |
| 座位列表 | `/Seat/List` | 展示全部 8 个座位，含位置、容量、特色标签 |
| 座位详情 | `/Seat/Detail/{id}` | 单个座位详细信息 |
| 管理员登录 | `/Admin/Login` | 硬编码校验（admin / 123456），Session 鉴权 |
| 预约管理（查看） | `/Admin/ReservationList` | 管理端查看全部预约记录 |
| 座位管理（增删改） | `/Admin/SeatList` | 管理端新增、编辑、删除座位 |
| 管理端布局 | `_AdminLayout.cshtml` | 侧边栏导航，响应式折叠 |

### 未实现（Sprint 2 未开发）

| 功能 | 原因 |
|------|------|
| 预约提交（选座→选时段→提交） | Sprint 2 计划项，尚未开发 |
| 我的预约（查看/取消） | Sprint 2 计划项，尚未开发 |
| 统计页（预约数据统计） | Sprint 4 T14-01/T14-02 待开发 |

---

## 页面清单

### 已实现页面（Razor 视图）

| 页面 | 路由 | 对应原型 |
|------|------|----------|
| 用户首页 | `/` | `prototype/static-v1/user-index.html` |
| 座位列表 | `/Seat/List` | `prototype/static-v1/seat-list.html` |
| 座位详情 | `/Seat/Detail/{id}` | `prototype/static-v1/seat-detail.html` |
| 管理员登录 | `/Admin/Login` | `prototype/static-v1/admin-login.html` |
| 预约管理 | `/Admin/ReservationList` | `prototype/static-v1/admin-booking.html` |
| 座位管理 | `/Admin/SeatList` | `prototype/static-v1/admin-seat.html` |
| 公共布局 | `_Layout.cshtml` | — |
| 管理端布局 | `_AdminLayout.cshtml` | — |

### 静态原型（未对接）

| 原型 | 状态 |
|------|------|
| `booking-submit.html`（预约提交） | 仅有静态原型，Sprint 2 未开发 |
| `my-bookings.html`（我的预约） | 仅有静态原型，Sprint 2 未开发 |
| `admin-stats.html`（统计页） | 仅有静态原型，T14-01/T14-02 待开发 |

---

## 运行步骤

```bash
# 1. 克隆仓库
git clone https://github.com/2864284429-tec/241060561223.git
cd 241060561223

# 2. 还原依赖
dotnet restore

# 3. 运行项目（数据库自动建表 + 种子数据自动初始化）
dotnet run --project src/LibrarySeatReservation.Web

# 4. 访问应用
# 用户端：http://localhost:5123
# 管理端：http://localhost:5123/Admin/Login
```

> 数据库建表和种子数据在首次启动时自动完成，无需手动执行任何 SQL 命令。
> 详见 [database/README.md](database/README.md)。

---

## 数据库

| 项目 | 说明 |
|------|------|
| 设计策略 | Code First（EF Core） |
| 数据库引擎 | SQLite（自动回退，无需安装 LocalDB） |
| 数据库文件 | `src/LibrarySeatReservation.Web/LibrarySeatReservation.db`（首次运行自动生成，已 gitignore） |
| 建表方式 | `Program.cs` 中 `EnsureCreated()` 自动执行 |
| 种子数据 | `AppDbContext.cs` 中 `HasData()` 自动写入 |

### 种子数据

| 类型 | 数量 | 内容 |
|------|------|------|
| 座位 | 8 个 | A-01~A-03（一楼靠窗）、B-01~B-03（二楼安静区）、C-01~C-02（三楼讨论区） |
| 体验用户 | 3 个 | user1/小王、user2/小李、user3/小张 |

---

## 演示账号

### 学生用户

| 账号 ID | 姓名 | 说明 |
|---------|------|------|
| user1 | 小王 | 体验账号 |
| user2 | 小李 | 体验账号 |
| user3 | 小张 | 体验账号 |

> 学生端当前无需登录，直接访问座位列表即可浏览座位信息。

### 管理员

| 用户名 | 密码 | 说明 |
|--------|------|------|
| admin | 123456 | 硬编码，不存储在数据库 |

登录地址：`http://localhost:5123/Admin/Login`

---

## 项目目录说明

```
LibrarySeatReservation/
├── README.md                           # 本文件
├── database/                           # 数据库初始化说明
│   └── README.md
├── docs/                               # 项目文档（01-17，共 17 份正文 + 审计报告）
├── prototype/                          # 静态原型
│   ├── static-v1/                      # 第 1 版静态原型（9 个 HTML）
│   └── review-1/                       # 原型评审记录
├── src/LibrarySeatReservation.Web/     # 主项目
│   ├── Controllers/                    # 控制器层
│   │   ├── HomeController.cs           # 用户首页
│   │   ├── SeatController.cs           # 座位列表 + 详情
│   │   └── AdminController.cs          # 管理端（登录/预约管理/座位管理）
│   ├── Services/                       # 业务逻辑层
│   │   ├── ISeatService.cs / SeatService.cs         # 座位 CRUD
│   │   ├── IReservationService.cs / ReservationService.cs  # 预约逻辑
│   │   └── IAdminAuthService.cs / AdminAuthService.cs      # 管理员登录
│   ├── DataAccess/                     # 数据访问层
│   │   └── AppDbContext.cs             # DbContext + 种子数据
│   ├── Models/
│   │   ├── Entities/                   # 实体类（Seat / Reservation / DemoUser）
│   │   └── ViewModels/                 # 视图模型
│   ├── Views/                          # Razor 视图
│   │   ├── Home/                       # 用户首页
│   │   ├── Seat/                       # 座位列表 + 详情
│   │   ├── Admin/                      # 管理端（登录/预约管理/座位管理）
│   │   └── Shared/                     # 公共布局（_Layout / _AdminLayout）
│   ├── Migrations/                     # EF Core 迁移文件
│   ├── wwwroot/css/custom.css          # 自定义样式
│   ├── Program.cs                      # 启动入口
│   └── appsettings.json                # 配置文件
├── tests/                              # Playwright 自动化测试
│   └── smoke.spec.ts                   # 冒烟测试（10 项）
├── playwright.config.ts                # Playwright 配置
├── package.json                        # Node.js 依赖（测试用）
├── library.html                        # 原型展示页
└── LibrarySeatReservation.sln          # 解决方案文件
```

---

## 测试

### 测试结果

| 测试类型 | 框架 | 结果 |
|----------|------|------|
| 脚本冒烟测试 | curl 脚本 | 7/7 通过 |
| Playwright 自动化测试 | @playwright/test | 10/10 通过 |

### 运行测试

```bash
# 还原依赖
npm install

# 启动应用（另一个终端）
dotnet run --project src/LibrarySeatReservation.Web

# 运行 Playwright 测试
npx playwright test
```

> Playwright 使用 Chrome 浏览器（channel: chrome），headless 模式。

---

## 已实现范围

| 阶段 | 已完成内容 | 状态 |
|------|------------|------|
| 文档阶段 | 项目立项、需求分析、PRD、页面树、UI 规范、原型、系统设计、数据库设计、关键链路设计、一致性审计 | ✅ |
| Sprint 0 | 项目骨架、Entity 实体类、DbContext + 种子数据、Service 层、EF Core 迁移、Program.cs 配置 | ✅ |
| Sprint 1 | 用户首页、座位列表、座位详情 | ✅ |
| Sprint 2 | 预约提交、我的预约、取消预约 | ⏳ 待开发 |
| Sprint 3 | 管理员登录、Session 权限守卫、预约管理（查看）、座位管理（增删改） | ✅ |
| Sprint 4（第 1 轮） | 联调测试（17/17）、P0 Bug 修复（3 个）、docs/16 | ✅ |
| Sprint 4（第 2 轮） | 统计页 | ⏳ 待开发 |

---

## 相关文档

| 编号 | 文档 | 说明 |
|------|------|------|
| 01 | [项目立项单](docs/01-项目立项单.md) | 项目背景、目标、范围 |
| 02 | [需求分析与 MVP 确认](docs/02-需求分析与MVP确认.md) | 需求梳理、MVP 范围 |
| 03 | [PRD Lite](docs/03-PRD-Lite.md) | 产品需求文档 |
| 04 | [页面树与业务流程](docs/04-页面树与业务流程.md) | 页面结构、业务流程图 |
| 05 | [页面卡与 UI 规范](docs/05-页面卡与UI规范.md) | UI 设计规范 |
| 06 | [静态原型与原型评审](docs/06-静态原型与原型评审.md) | 原型设计、评审记录 |
| 07 | [系统设计说明](docs/07-系统设计说明.md) | 架构设计、模块划分 |
| 08 | [数据库设计](docs/08-数据库设计.md) | 表结构设计 |
| 09 | [关键链路详细设计](docs/09-关键链路详细设计.md) | 核心业务逻辑设计 |
| 10 | [开发准备与 Sprint 0](docs/10-开发准备与Sprint0.md) | 环境搭建、骨架搭建 |
| 11 | [开发前一致性总审计](docs/11-开发前一致性总审计.md) | 文档-原型-设计一致性审计 |
| 12 | [开发起步与骨架记录](docs/12-开发起步与骨架记录.md) | Sprint 0 开发记录 |
| 13 | [用户端主链路开发记录](docs/13-用户端主链路开发记录.md) | Sprint 1 开发记录 |
| 14 | [管理端与权限开发记录](docs/14-管理端与权限开发记录.md) | Sprint 3 开发记录 |
| 15 | [功能完善与体验优化记录](docs/15-功能完善与体验优化记录.md) | Sprint 3 第 2 轮开发记录 |
| 16 | [联调测试与缺陷闭环](docs/16-联调测试与缺陷闭环.md) | Sprint 4 测试与 Bug 修复 |
| 17 | [交付说明与项目复盘](docs/17-交付说明与项目复盘.md) | 最终交付、复盘总结 |

---

## 已知限制

| 限制 | 说明 |
|------|------|
| 预约提交/取消未实现 | 用户端核心闭环（选座→选时段→提交→查看→取消）尚未开发，Sprint 2 计划项 |
| 统计页未实现 | 管理端统计页尚未开发，Sprint 4 T14-01/T14-02 待开发 |
| 管理员登录无验证码 | 硬编码账号密码，无防暴力破解机制，课堂项目不实现 |
| 用户端无登录态 | 学生端无 Session，首页未显示当前用户名 |
| 预约管理无筛选/删除 | 管理端预约列表仅展示全部记录，无筛选、搜索、删除功能 |
| 响应式布局未充分测试 | 管理端侧边栏有响应式 CSS，但未做 viewport 自动化测试 |
| 仅 SQLite 环境验证 | 原设计为 SQL Server LocalDB，因 macOS 环境回退 SQLite，未在 Windows + LocalDB 上验证 |

---

## 许可证

本项目仅用于课程实践，不涉及商业用途。
