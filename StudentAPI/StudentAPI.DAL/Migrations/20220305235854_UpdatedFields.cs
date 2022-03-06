using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentAPI.DAL.Migrations
{
    public partial class UpdatedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDetails_ApplicationMaster_ApplicationMasterId1",
                table: "ApplicationDetails");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationDetails_ApplicationMasterId1",
                table: "ApplicationDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationMasterId1",
                table: "ApplicationDetails");

            migrationBuilder.RenameColumn(
                name: "StudentRegisterNumber",
                table: "ApplicationMaster",
                newName: "ApplicationNumber");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationMasterId",
                table: "ApplicationDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_ApplicationMasterId",
                table: "ApplicationDetails",
                column: "ApplicationMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDetails_ApplicationMaster_ApplicationMasterId",
                table: "ApplicationDetails",
                column: "ApplicationMasterId",
                principalTable: "ApplicationMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationDetails_ApplicationMaster_ApplicationMasterId",
                table: "ApplicationDetails");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationDetails_ApplicationMasterId",
                table: "ApplicationDetails");

            migrationBuilder.RenameColumn(
                name: "ApplicationNumber",
                table: "ApplicationMaster",
                newName: "StudentRegisterNumber");

            migrationBuilder.AlterColumn<long>(
                name: "ApplicationMasterId",
                table: "ApplicationDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationMasterId1",
                table: "ApplicationDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDetails_ApplicationMasterId1",
                table: "ApplicationDetails",
                column: "ApplicationMasterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationDetails_ApplicationMaster_ApplicationMasterId1",
                table: "ApplicationDetails",
                column: "ApplicationMasterId1",
                principalTable: "ApplicationMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
