# 开发准备与 Sprint 0

> 本阶段目标：搭好开发节奏，明确分支策略、提交规范、Sprint 划分，为正式编码做好准备。

---

## 1. 仓库结构

### 1.1 当前仓库

```
LibrarySeatReservation/
├── docs/                    # 项目文档（01-09 已完成）
├── prototype/               # 静态原型
│   └── static-v1/           # 9 个 HTML 页面 + CSS
├── README.md                # 项目说明
├── .gitignore               # Git 忽略文件
└── .gitattributes           # Git 属性配置
```

### 1.2 后续阶段将生成

```
LibrarySeatReservation/
├── Controllers/             # 控制器层
├── Services/                # 业务逻辑层
├── DataAccess/              # 数据访问层
├── Models/                  # 实体模型 + ViewModel
├── Views/                   # Razor 视图
├── wwwroot/                 # 静态资源
├── Program.cs               # 入口文件
├── appsettings.json         # 配置文件
└── LibrarySeatReservation.csproj  # 项目文件
```

---

## 2. 分支策略

### 2.1 分支模型

采用简化的 Git Flow 模型，适合单人课堂项目：

| 分支 | 用途 | 生命周期 | 保护规则 |
|------|------|----------|----------|
| `main` | 稳定版本，可演示 | 永久 | 禁止直接 push |
| `dev` | 开发集成分支 | 永久 | 禁止直接 push |
| `feat/xxx` | 功能分支 | 开发完成合并到 dev | 无 |
| `fix/xxx` | 修复分支 | 修复完成合并到 dev | 无 |

### 2.2 分支命名规范

| 类型 | 格式 | 示例 |
|------|------|------|
| 功能 | `feat/<模块>-<简述>` | `feat/user-home`、`feat/seat-list`、`feat/admin-login` |
| 修复 | `fix/<问题>-<简述>` | `fix/booking-timezone` |
| 文档 | `docs/<简述>` | `docs/sprint-0` |
| 重构 | `refactor/<模块>-<简述>` | `refactor/service-layer` |

### 2.3 分支工作流

```
main ←──────── dev ←──────── feat/xxx
                  ↑
                  └── feat/yyy
```

1. **开发新功能**：从 `dev` 创建 `feat/xxx`
2. **开发完成**：合并回 `dev`
3. **阶段完成**：`dev` 合并到 `main`，打 tag
4. **紧急修复**：从 `main` 创建 `fix/xxx`，修复后合并回 `dev`，再合并到 `main`

---

## 3. 提交规范

### 3.1 Commit Message 格式

```
<type>(<scope>): <subject>

<body>
```

### 3.2 Type 类型

| Type | 说明 | 示例 |
|------|------|------|
| `feat` | 新功能 | `feat(seat): 实现座位列表页` |
| `fix` | 修复 | `fix(booking): 修复时段判断逻辑` |
| `docs` | 文档 | `docs: 更新 Sprint 1 任务卡` |
| `style` | 格式调整 | `style: 统一代码缩进` |
| `refactor` | 重构 | `refactor(service): 提取通用查询方法` |
| `test` | 测试 | `test(reservation): 添加预约创建测试` |
| `chore` | 构建/工具 | `chore: 更新 .gitignore` |

### 3.3 Scope 范围

| Scope | 对应模块 |
|-------|----------|
| `user` | 用户首页、通用 |
| `seat` | 座位列表、详情 |
| `booking` | 预约提交、我的预约 |
| `admin` | 管理端通用 |
| `admin-seat` | 管理端座位管理 |
| `admin-booking` | 管理端预约管理 |
| `db` | 数据库相关 |
| `ui` | 前端样式 |

### 3.4 提交粒度建议

| 时机 | 是否提交 |
|------|----------|
| 完成一个页面的 Controller + Service + View | ✅ |
| 完成一个功能的完整闭环 | ✅ |
| 修复一个已知 bug | ✅ |
| 调整一处样式 | ✅ |
| 改了半截代码，还没跑通 | ❌（继续工作） |
| 只是临时测试 | ❌（用 stash） |

---

## 4. Sprint 0 目标

### 4.1 Sprint 0 概述

| 项目 | 内容 |
|------|------|
| Sprint 名称 | Sprint 0 - 开发准备 |
| 目标 | 搭建项目骨架，确保环境可运行 |
| 预计时长 | 1-2 天 |
| 验收标准 | `dotnet build` 通过 + `dotnet run` 能启动 + 数据库已建表 + 种子数据已初始化 |

### 4.2 Sprint 0 任务清单

| 编号 | 任务 | 验收标准 | 状态 |
|------|------|----------|------|
| T10-01 | 创建 `.sln` 解决方案 | `dotnet new sln` 成功，文件存在 | ⏳ |
| T10-02 | 创建 Web 项目 `.csproj` | `dotnet new mvc` 成功，项目文件存在 | ⏳ |
| T10-03 | 首次 `dotnet build` | 编译成功，无错误 | ⏳ |
| T10-04 | 首次 `dotnet run` | 能启动，浏览器可访问 `localhost:5000` | ⏳ |
| T10-05 | 创建 Entity 实体类 | Seat.cs、Reservation.cs、DemoUser.cs 创建完成 | ⏳ |
| T10-06 | 创建 DbContext | AppDbContext.cs 创建完成，包含三个 DbSet | ⏳ |
| T10-07 | 首次 EF 迁移 | `dotnet ef migrations add InitialCreate` 成功 | ⏳ |
| T10-08 | 首次数据库更新 | `dotnet ef database update` 成功，三张表已创建 | ⏳ |
| T10-09 | 种子数据初始化 | Seats 表 8 条记录，DemoUsers 表 3 条记录 | ⏳ |
| T10-10 | 创建基础目录结构 | Controllers、Services、DataAccess、Models 文件夹创建 | ⏳ |
| T10-11 | 验证 `dotnet run` 启动后数据库可用 | 启动后数据库已初始化，种子数据存在 | ⏳ |

### 4.3 Sprint 0 最低完成线

必须完成以下任务才能进入 Sprint 1：

- [x] `.sln` 和 `.csproj` 创建
- [x] `dotnet build` 通过
- [x] `dotnet run` 能启动
- [x] 数据库已建表
- [x] 种子数据已初始化

---

## 5. Sprint 1-4 主 Sprint 粗计划

> ⚠️ 每个主 Sprint 允许多轮推进，不要求一次性完成所有任务。

### 5.1 Sprint 1 - 用户首页与座位模块

| 项目 | 内容 |
|------|------|
| Sprint 名称 | Sprint 1 - 用户首页与座位模块 |
| 目标 | 完成用户首页、座位列表、座位详情三个页面 |
| 预计时长 | 2-3 天 |
| 最低完成线 | 座位列表页能显示种子数据中的座位 |
| 关联页面 | 用户首页（user-index）、座位列表（seat-list）、座位详情（seat-detail） |
| 关联文档 | docs/04（页面树）、docs/07（系统设计）、docs/09（关键链路） |

**Sprint 1 任务卡：**

| 编号 | 任务 | 关联页面 | 验收标准 |
|------|------|----------|----------|
| T11-01 | 实现 HomeController.Index | user-index | 显示欢迎语和导航 |
| T11-02 | 实现 SeatController.List | seat-list | 显示座位列表，显示当前时段状态 |
| T11-03 | 实现 SeatService.GetAllSeatsWithStatus | seat-list | 查询座位 + 预约状态，无 N+1 |
| T11-04 | 实现 SeatController.Detail | seat-detail | 显示座位详情 |
| T11-05 | 实现座位列表页 Razor 视图 | seat-list | 显示卡片列表，设备标签 |
| T11-06 | 实现座位详情页 Razor 视图 | seat-detail | 显示详细信息和预约按钮 |

**Sprint 1 里程碑：** 用户能浏览座位列表，查看座位详情。

---

### 5.2 Sprint 2 - 预约核心链路

| 项目 | 内容 |
|------|------|
| Sprint 名称 | Sprint 2 - 预约核心链路 |
| 目标 | 完成预约提交、我的预约、取消预约三个页面 |
| 预计时长 | 2-3 天 |
| 最低完成线 | 用户能完成"选座 → 选时段 → 提交 → 查看 → 取消"完整闭环 |
| 关联页面 | 预约提交（booking-submit）、我的预约（my-bookings） |
| 关联文档 | docs/04（页面树）、docs/07（系统设计）、docs/09（关键链路） |

**Sprint 2 任务卡：**

| 编号 | 任务 | 关联页面 | 验收标准 |
|------|------|----------|----------|
| T12-01 | 实现 ReservationController.Create（GET） | booking-submit | 显示日期选择器和时段列表 |
| T12-02 | 实现 ReservationController.Create（POST） | booking-submit | 预约成功后跳转"我的预约" |
| T12-03 | 实现 ReservationService.CreateReservation | booking-submit | 检查时段可用性，创建预约记录 |
| T12-04 | 实现 ReservationController.My | my-bookings | 显示当前用户预约列表 |
| T12-05 | 实现 ReservationService.GetUserBookings | my-bookings | 查询预约记录，显示时段文本 |
| T12-06 | 实现取消预约功能 | my-bookings | 弹出确认框，取消后刷新列表 |
| T12-07 | 实现预约提交页 Razor 视图 | booking-submit | 显示表单，处理错误提示 |
| T12-08 | 实现我的预约页 Razor 视图 | my-bookings | 显示预约列表，取消按钮 |

**Sprint 2 里程碑：** 用户能完成预约、查看、取消完整闭环。

---

### 5.3 Sprint 3 - 管理端核心功能

| 项目 | 内容 |
|------|------|
| Sprint 名称 | Sprint 3 - 管理端核心功能 |
| 目标 | 完成管理员登录、预约管理、座位管理三个页面 |
| 预计时长 | 2-3 天 |
| 最低完成线 | 管理员能登录，查看预约列表，管理座位 |
| 关联页面 | 管理员登录（admin-login）、预约管理（admin-booking）、座位管理（admin-seat） |
| 关联文档 | docs/04（页面树）、docs/07（系统设计）、docs/09（关键链路） |

**Sprint 3 任务卡：**

| 编号 | 任务 | 关联页面 | 验收标准 |
|------|------|----------|----------|
| T13-01 | 实现 AdminAuthController.Login（GET） | admin-login | 显示登录表单 |
| T13-02 | 实现 AdminAuthController.Login（POST） | admin-login | 硬编码校验 admin/123456 |
| T13-03 | 实现 Session 管理 | admin-login | 登录成功设置 Session，未登录跳转 |
| T13-04 | 实现 AdminHomeController.ReservationList | admin-booking | 显示所有预约记录 |
| T13-05 | 实现 SeatService（增删改） | admin-seat | 实现 CreateSeat、UpdateSeat、DeleteSeat |
| T13-06 | 实现 AdminSeatController.SeatList | admin-seat | 显示座位列表 |
| T13-07 | 实现 AdminSeatController.Create/Edit/Delete | admin-seat | 新增、编辑、删除座位 |
| T13-08 | 实现管理端 Razor 视图 | admin-login, admin-booking, admin-seat | 显示表单和列表 |

**Sprint 3 里程碑：** 管理员能登录，查看预约，管理座位。

---

### 5.4 Sprint 4 - 统计页与收尾

| 项目 | 内容 |
|------|------|
| Sprint 名称 | Sprint 4 - 统计页与收尾 |
| 目标 | 完成统计页，联调测试，修复 bug |
| 预计时长 | 1-2 天 |
| 最低完成线 | 统计页能显示座位数量和预约统计 |
| 关联页面 | 统计页（admin-stats） |
| 关联文档 | docs/04（页面树）、docs/07（系统设计） |

**Sprint 4 任务卡：**

| 编号 | 任务 | 关联页面 | 验收标准 |
|------|------|----------|----------|
| T14-01 | 实现统计页功能 | admin-stats | 显示座位总数、可用数、预约数 |
| T14-02 | 实现统计页 Razor 视图 | admin-stats | 显示统计卡片 |
| T14-03 | 全流程联调 | 全部 | 用户端和管理端流程跑通 |
| T14-04 | Bug 修复 | 全部 | 修复已知问题 |
| T14-05 | UI 细节调整 | 全部 | 样式统一，交互优化 |
| T14-06 | 编写答辩 PPT / 演示脚本 | - | 准备最终演示 |

**Sprint 4 里程碑：** 项目完整可演示，准备答辩。

---

## 6. 里程碑节点

| 里程碑 | 内容 | 验收标准 | 预计完成时间 |
|--------|------|----------|--------------|
| M1：开发环境就绪 | Sprint 0 完成 | `dotnet run` 能启动，数据库已初始化 | 第 1 天 |
| M2：用户端核心链路 | Sprint 1 + Sprint 2 完成 | 用户能完成预约、查看、取消完整闭环 | 第 4 天 |
| M3：管理端核心功能 | Sprint 3 完成 | 管理员能登录，查看预约，管理座位 | 第 6 天 |
| M4：项目交付 | Sprint 4 完成 | 项目完整可演示，准备答辩 | 第 7-8 天 |

---

## 7. 默认补足项 / 当前假设

| 序号 | 内容 | 来源 | 假设依据 |
|------|------|------|----------|
| 1 | Sprint 预计时长基于每天 3-4 小时有效编码时间 | 本阶段补足 | 实习生日常工作安排 |
| 2 | 每个主 Sprint 允许多轮推进（例如 Sprint 1 分 2 轮完成） | 本阶段补足 | 符合敏捷迭代精神 |
| 3 | 分支策略采用简化 Git Flow，适合单人项目 | 本阶段补足 | 不需要复杂的分支保护规则 |
| 4 | 提交粒度建议基于功能模块，不是文件 | 本阶段补足 | 符合功能原子性原则 |
| 5 | 里程碑数量控制在 4 个以内 | 本阶段补足 | 符合用户要求 |
| 6 | Sprint 0 的 .sln 和 .csproj 创建保留在本阶段文档中 | 本阶段补足 | 用户要求不直接生成，保留到下一步执行 |

---

## 8. 进入下一步的检查清单

进入"开发前一致性总审计"前，确认以下内容：

- [ ] README.md 已创建，包含项目简介、技术栈、目录结构
- [ ] docs/10-开发准备与Sprint0.md 已创建，包含分支策略、提交规范、Sprint 计划
- [ ] docs/项目任务板与迭代记录.md 已创建，包含任务卡和迭代记录
- [ ] Sprint 0 任务清单已明确
- [ ] Sprint 1-4 任务卡已初步定义
- [ ] 里程碑节点已设定
- [ ] 所有文档与前序文档保持命名和范围一致

---

*创建时间：2026-07-14*
