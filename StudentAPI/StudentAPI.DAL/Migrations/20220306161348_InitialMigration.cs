using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentAPI.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CourseCode = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TeachingMethod = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationMaster_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationMasterId = table.Column<int>(type: "int", nullable: false),
                    CourseDetailsId = table.Column<int>(type: "int", nullable: false),
                    CoursePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationDetails_ApplicationMaster_ApplicationMasterId",
                        column: x => x.ApplicationMasterId,
                        principalTable: "ApplicationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationDetails_CourseUnits_CourseDetailsId",
                        column: x => x.CourseDetailsId,
                        principalTable: "CourseUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseUnits",
                columns: new[] { "Id", "CourseCode", "CourseName", "Price" },
                values: new object[,]
                {
                    { -1, "ICT001", "Foundamental Of Computing", 30000m },
                    { -2, "ICT002", "Programming", 2000m },
                    { -3, "ENG223", "English Language", 1500m }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DateOfBirth", "StudentName" },
                values: new object[,]
                {
                    { -1, "Kampala", new DateTime(1990, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jone Daniel" },
                    { -2, "Kampala", new DateTime(1998, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jules Willis" },
                    { -3, "Kampala", new DateTime(2000, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Victoria Elisabeth" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_ApplicationMasterId",
                table: "ApplicationDetails",
                column: "ApplicationMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_CourseDetailsId",
                table: "ApplicationDetails",
                column: "CourseDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMaster_StudentId",
                table: "ApplicationMaster",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationDetails");

            migrationBuilder.DropTable(
                name: "ApplicationMaster");

            migrationBuilder.DropTable(
                name: "CourseUnits");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
