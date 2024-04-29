using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BringMe.Migrations
{
    /// <inheritdoc />
    public partial class roads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ways",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLocalityA = table.Column<int>(type: "integer", nullable: false),
                    IdLocalityB = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Distance = table.Column<int>(type: "integer", nullable: false),
                    Transport = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ways", x => x.Id);
                    table.ForeignKey(
                    name: "FK_Ways_AspNetUsers_IdUser", // Имя внешнего ключа
                    column: x => x.IdUser,
                    principalTable: "AspNetUsers", // Таблица, с которой связываем
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                    name: "FK_Ways_Localities_IdLocalityA", // Имя внешнего ключа
                    column: x => x.IdLocalityA,
                    principalTable: "Localities", // Таблица, с которой связываем
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                    name: "FK_Ways_Localities_IdLocalityB", // Имя внешнего ключа
                    column: x => x.IdLocalityB,
                    principalTable: "Localities", // Таблица, с которой связываем
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ways");
        }
    }
}
