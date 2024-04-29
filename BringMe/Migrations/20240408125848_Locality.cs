using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BringMe.Migrations
{
    /// <inheritdoc />
    public partial class Locality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Warehouse = table.Column<bool>(type: "boolean", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Id);
                    table.ForeignKey(
                    name: "FK_Localities_AspNetUsers_IdUser", // Имя внешнего ключа
                    column: x => x.IdUser,
                    principalTable: "AspNetUsers", // Таблица, с которой связываем
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localities");
        }
    }
}
