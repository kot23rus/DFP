using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFP.CORE.DBMigrations
{
    /// <inheritdoc />
    public partial class ChangesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_ID",
                table: "Files",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_State",
                table: "Files",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UTime",
                table: "Files",
                column: "UTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
