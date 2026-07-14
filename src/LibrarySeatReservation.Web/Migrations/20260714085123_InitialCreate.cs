using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySeatReservation.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.CheckConstraint("CK_Reservations_TimeSlot", "[TimeSlot] >= 8 AND [TimeSlot] <= 20");
                    table.ForeignKey(
                        name: "FK_Reservations_DemoUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DemoUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DemoUsers",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { "user1", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(2760), "小王" },
                    { "user2", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(2980), "小李" },
                    { "user3", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(2980), "小张" }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Capacity", "CreatedAt", "Features", "IsAvailable", "Location", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5720), "有电源", true, "一楼靠窗", "A-01", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5820) },
                    { 2, 1, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5910), "有电源", true, "一楼靠窗", "A-02", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5910) },
                    { 3, 1, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5910), "有台灯", true, "一楼靠窗", "A-03", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5910) },
                    { 4, 1, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5910), "有电源", true, "二楼安静区", "B-01", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5910) },
                    { 5, 1, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920), null, true, "二楼安静区", "B-02", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920) },
                    { 6, 1, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920), "有电源", true, "二楼安静区", "B-03", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920) },
                    { 7, 4, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920), "有电源、有白板", true, "三楼讨论区", "C-01", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920) },
                    { 8, 4, new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920), "有电源", true, "三楼讨论区", "C-02", new DateTime(2026, 7, 14, 8, 51, 23, 668, DateTimeKind.Utc).AddTicks(5920) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Date",
                table: "Reservations",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatId_Date_TimeSlot_Status",
                table: "Reservations",
                columns: new[] { "SeatId", "Date", "TimeSlot", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Status",
                table: "Reservations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId_Date",
                table: "Reservations",
                columns: new[] { "UserId", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "DemoUsers");

            migrationBuilder.DropTable(
                name: "Seats");
        }
    }
}
