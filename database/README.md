# 数据库初始化说明

## 设计策略

本项目采用 **Entity Framework Core Code First** 方式管理数据库。所有表结构和种子数据均通过 C# 代码定义，无需手写 SQL 脚本。

| 项目 | 说明 |
|------|------|
| ORM | Entity Framework Core 10.x |
| 策略 | Code First（代码优先） |
| 迁移方式 | `EnsureCreated()`（SQLite）/ `Migrate()`（SQL Server） |
| 数据库文件 | 首次运行自动生成 `LibrarySeatReservation.db`（SQLite） |

---

## 自动初始化流程

应用启动时，`Program.cs` 自动执行以下步骤：

```
1. 检测数据库引擎
   ├── 有 SqliteConnection → 使用 SQLite
   └── 无 SqliteConnection → 使用 SQL Server LocalDB

2. 建库建表
   ├── SQLite → db.Database.EnsureCreated()
   └── SQL Server → db.Database.Migrate()

3. 种子数据
   └── EF Core HasData() 在 EnsureCreated/Migrate 时自动写入
```

**用户无需执行任何手动命令。** 只要 `dotnet run` 成功，数据库即已就绪。

---

## 种子数据来源

种子数据定义在 `src/LibrarySeatReservation.Web/DataAccess/AppDbContext.cs` 的 `OnModelCreating` 方法中，使用 EF Core 的 `HasData()` API。

### 座位数据（Seats 表）

| ID | 编号 | 位置 | 容量 | 特色 | 状态 |
|----|------|------|------|------|------|
| 1 | A-01 | 一楼靠窗 | 1 人 | 有电源 | 可用 |
| 2 | A-02 | 一楼靠窗 | 1 人 | 有电源 | 可用 |
| 3 | A-03 | 一楼靠窗 | 1 人 | 有台灯 | 可用 |
| 4 | B-01 | 二楼安静区 | 1 人 | 有电源 | 可用 |
| 5 | B-02 | 二楼安静区 | 1 人 | — | 可用 |
| 6 | B-03 | 二楼安静区 | 1 人 | 有电源 | 可用 |
| 7 | C-01 | 三楼讨论区 | 4 人 | 有电源、有白板 | 可用 |
| 8 | C-02 | 三楼讨论区 | 4 人 | 有电源 | 可用 |

共 8 个座位，覆盖单人座位（6 个）和小组讨论座位（2 个），三种楼层区域。

### 体验用户（DemoUsers 表）

| 用户 ID | 姓名 |
|---------|------|
| user1 | 小王 |
| user2 | 小李 |
| user3 | 小张 |

共 3 个体验账号，用于学生端功能演示。

---

## 演示账号

### 学生用户

| 账号 ID | 姓名 | 使用方式 |
|---------|------|----------|
| user1 | 小王 | 在座位详情页输入此 ID 预约 |
| user2 | 小李 | 同上 |
| user3 | 小张 | 同上 |

> 学生端当前无需登录（Sprint 2 未实现），直接访问座位列表即可浏览。

### 管理员

| 用户名 | 密码 | 说明 |
|--------|------|------|
| admin | 123456 | 硬编码在 `AdminAuthService.cs`，不存储在数据库 |

登录地址：`http://localhost:5123/Admin/Login`

---

## 数据库文件位置

| 引擎 | 文件路径 | 说明 |
|------|----------|------|
| SQLite | `src/LibrarySeatReservation.Web/LibrarySeatReservation.db` | 首次运行自动创建 |
| SQL Server | `(localdb)\mssqllocaldb` | 需 LocalDB 环境 |

> SQLite 数据库文件已在 `.gitignore` 中排除（`*.db`），不会提交到仓库。每次 `dotnet run` 首次启动时自动重建。

---

## 重置数据库

如需清空数据重新初始化：

```bash
# 删除 SQLite 数据库文件
rm src/LibrarySeatReservation.Web/LibrarySeatReservation.db

# 重新启动，自动重建
dotnet run --project src/LibrarySeatReservation.Web
```
