using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSB_RadioForum.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserPost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserPost");
        }
    }
}
