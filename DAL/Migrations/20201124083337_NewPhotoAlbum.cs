using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class NewPhotoAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Items");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Items",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
