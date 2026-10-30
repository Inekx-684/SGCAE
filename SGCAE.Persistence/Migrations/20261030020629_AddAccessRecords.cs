using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGCAE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CredentialCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Movement = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Decision = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlockedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credentials_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessPoints_Name",
                table: "AccessPoints",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_AccessPointId_OccurredAt",
                table: "AccessRecords",
                columns: new[] { "AccessPointId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_Decision_OccurredAt",
                table: "AccessRecords",
                columns: new[] { "Decision", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRecords_EmployeeId_OccurredAt",
                table: "AccessRecords",
                columns: new[] { "EmployeeId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_Code",
                table: "Credentials",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_EmployeeId",
                table: "Credentials",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessPoints");

            migrationBuilder.DropTable(
                name: "AccessRecords");

            migrationBuilder.DropTable(
                name: "Credentials");
        }
    }
}
