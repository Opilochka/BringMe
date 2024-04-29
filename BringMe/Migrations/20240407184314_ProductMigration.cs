using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BringMe.Migrations
{
    /// <inheritdoc />
    public partial class ProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                ImageUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                Price = table.Column<string>(type: "integer", nullable: true),
                Size = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                Mass = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                IdUser = table.Column<string>(type: "text", nullable: true) // Связь с таблицей AspNetUsers
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                name: "FK_Products_AspNetUsers_IdUser", // Имя внешнего ключа
                column: x => x.IdUser,
                principalTable: "AspNetUsers", // Таблица, с которой связываем
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
