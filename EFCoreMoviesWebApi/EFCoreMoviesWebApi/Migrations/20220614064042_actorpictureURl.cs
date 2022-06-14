using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMoviesWebApi.Migrations
{
    public partial class actorpictureURl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureURL",
                table: "Actors",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureURL",
                table: "Actors");
        }
    }
}
