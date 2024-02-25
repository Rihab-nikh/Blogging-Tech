using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Blog2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_T_Post_PostsPostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_T_Tag_TagsTagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameColumn(
                name: "TagsTagId",
                table: "PostTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "PostsPostId",
                table: "PostTag",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagsTagId",
                table: "PostTag",
                newName: "IX_PostTag_TagId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostTag",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_PostId",
                table: "PostTag",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_T_Post_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "T_Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_T_Tag_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "T_Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_T_Post_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_T_Tag_TagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.DropIndex(
                name: "IX_PostTag_PostId",
                table: "PostTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostTag");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "PostTag",
                newName: "TagsTagId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostTag",
                newName: "PostsPostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                newName: "IX_PostTag_TagsTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                columns: new[] { "PostsPostId", "TagsTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_T_Post_PostsPostId",
                table: "PostTag",
                column: "PostsPostId",
                principalTable: "T_Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_T_Tag_TagsTagId",
                table: "PostTag",
                column: "TagsTagId",
                principalTable: "T_Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
