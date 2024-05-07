using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class removeCommentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsId",
                table: "Forums");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentsId",
                table: "Forums",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
