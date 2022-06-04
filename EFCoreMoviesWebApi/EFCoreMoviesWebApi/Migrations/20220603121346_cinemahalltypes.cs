using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMoviesWebApi.Migrations
{
    public partial class cinemahalltypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaHallType",
                table: "CinemaHalls",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CinemaHallType",
                table: "CinemaHalls");
        }
    }
}
