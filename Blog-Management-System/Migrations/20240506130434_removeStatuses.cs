using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class removeStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forums",
                columns: table => new
                {
                    ForumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriesId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusesId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.ForumId);
                    table.ForeignKey(
                        name: "FK_Forums_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryForum",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    ForumsForumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryForum", x => new { x.CategoriesId, x.ForumsForumId });
                    table.ForeignKey(
                        name: "FK_CategoryForum_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryForum_Forums_ForumsForumId",
                        column: x => x.ForumsForumId,
                        principalTable: "Forums",
                        principalColumn: "ForumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ForumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Forums_ForumId",
                        column: x => x.ForumId,
                        principalTable: "Forums",
                        principalColumn: "ForumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ForumStatus",
                columns: table => new
                {
                    ForumsForumId = table.Column<int>(type: "int", nullable: false),
                    StatusesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumStatus", x => new { x.ForumsForumId, x.StatusesId });
                    table.ForeignKey(
                        name: "FK_ForumStatus_Forums_ForumsForumId",
                        column: x => x.ForumsForumId,
                        principalTable: "Forums",
                        principalColumn: "ForumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumStatus_Status_StatusesId",
                        column: x => x.StatusesId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "alien" },
                    { 2, "ufo" },
                    { 3, "dog" },
                    { 4, "cat" },
                    { 5, "nasa" },
                    { 6, "zombie" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created_at", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 6, 20, 4, 34, 397, DateTimeKind.Local).AddTicks(6237), "admin", "chukka" },
                    { 2, new DateTime(2024, 5, 6, 20, 4, 34, 397, DateTimeKind.Local).AddTicks(6238), "user", "otto" },
                    { 3, new DateTime(2024, 5, 6, 20, 4, 34, 397, DateTimeKind.Local).AddTicks(6239), "user", "Juijui" }
                });

            migrationBuilder.InsertData(
                table: "Forums",
                columns: new[] { "ForumId", "Body", "CategoriesId", "Created_at", "Like", "StatusesId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "hello", null, new DateTime(2024, 5, 6, 20, 4, 34, 397, DateTimeKind.Local).AddTicks(6207), 3, null, "I found alien last year", 1 },
                    { 2, "...", null, new DateTime(2024, 5, 6, 20, 4, 34, 397, DateTimeKind.Local).AddTicks(6216), 18, null, "Hello from another world", 2 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Body", "ForumId", "Like", "UserId" },
                values: new object[] { 1, "wtf..", 1, 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryForum_ForumsForumId",
                table: "CategoryForum",
                column: "ForumsForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ForumId",
                table: "Comments",
                column: "ForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Forums_UserId",
                table: "Forums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumStatus_StatusesId",
                table: "ForumStatus",
                column: "StatusesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryForum");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ForumStatus");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Forums");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
