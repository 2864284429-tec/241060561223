using LibrarySeatReservation.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySeatReservation.Web.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<DemoUser> DemoUsers => Set<DemoUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seats 表
        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Features).HasMaxLength(200);
        });

        // Reservations 表
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);

            // 外键
            entity.HasOne(r => r.Seat)
                  .WithMany()
                  .HasForeignKey(r => r.SeatId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.DemoUser)
                  .WithMany()
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            // 唯一索引：同一座位同一日期同一时段不能有重复"已预约"记录
            // 注意：EF Core 不直接支持带过滤条件的唯一索引，这里用普通索引替代
            // 应用层做冲突检测
            entity.HasIndex(r => new { r.SeatId, r.Date, r.TimeSlot, r.Status });
            entity.HasIndex(r => new { r.UserId, r.Date });
            entity.HasIndex(r => r.Date);
            entity.HasIndex(r => r.Status);

            // TimeSlot 范围约束
            entity.ToTable(t => t.HasCheckConstraint("CK_Reservations_TimeSlot", "[TimeSlot] >= 8 AND [TimeSlot] <= 20"));
        });

        // DemoUsers 表
        modelBuilder.Entity<DemoUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        });

        // 种子数据：体验账号
        modelBuilder.Entity<DemoUser>().HasData(
            new DemoUser { Id = "user1", Name = "小王" },
            new DemoUser { Id = "user2", Name = "小李" },
            new DemoUser { Id = "user3", Name = "小张" }
        );

        // 种子数据：初始座位
        // 注意：HasData 不允许动态值（如 DateTime.UtcNow），必须使用固定值
        var seedTime = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Seat>().HasData(
            new Seat { Id = 1, Name = "A-01", Location = "一楼靠窗", Capacity = 1, Features = "有电源", IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime },
            new Seat { Id = 2, Name = "A-02", Location = "一楼靠窗", Capacity = 1, Features = "有电源", IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime },
            new Seat { Id = 3, Name = "A-03", Location = "一楼靠窗", Capacity = 1, Features = "有台灯", IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime },
            new Seat { Id = 4, Name = "B-01", Location = "二楼安静区", Capacity = 1, Features = "有电源", IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime },
            new Seat { Id = 5, Name = "B-02", Location = "二楼安静区", Capacity = 1, Features = null, IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime },
            new Seat { Id = 6, Name = "B-03", Location = "二楼安静区", Capacity = 1, Features = "有电源", IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime },
            new Seat { Id = 7, Name = "C-01", Location = "三楼讨论区", Capacity = 4, Features = "有电源、有白板", IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime },
            new Seat { Id = 8, Name = "C-02", Location = "三楼讨论区", Capacity = 4, Features = "有电源", IsAvailable = true, CreatedAt = seedTime, UpdatedAt = seedTime }
        );
    }
}
