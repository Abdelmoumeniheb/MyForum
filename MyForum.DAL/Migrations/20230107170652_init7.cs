using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyForum.DAL.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsercreated",
                table: "Forums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUsercreated",
                table: "Forums",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
