using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShelf.DAL.Migrations
{
    public partial class SeedBooksMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "ISBN", "PublicationYear", "Quantity", "Title" },
                values: new object[,]
                {
                    { new Guid("ded9e342-ef0d-4c2a-bdd0-8e5ff2a117ed"), "L. Tolstoy", 4L, "123456", 2001, 10, "War and Peace (tom 1)" },
                    { new Guid("36022756-536a-4099-b0d2-65a099d2714f"), "L. Tolstoy", 4L, "123457", 2001, 10, "War and Peace (tom 2)" },
                    { new Guid("d6f5cd92-6fd3-472f-a3b8-0aa15faa8a83"), "L. Tolstoy", 4L, "123458", 2001, 10, "War and Peace (tom 3)" },
                    { new Guid("567289db-d2f4-41e7-b417-c292b0378913"), "Zelazny", 1L, "123459", 1998, 20, "Amber Chronicles" },
                    { new Guid("08ac84b7-62be-40ee-9128-02b3f8fdf967"), "J.R.R. Tolkien", 2L, "123460", 1983, 5, "Hobbit" },
                    { new Guid("1b7652f5-be71-48e9-9493-fe54976db20a"), "W. Shakespeare", 3L, "123461", 1985, 15, "Hamlet" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("08ac84b7-62be-40ee-9128-02b3f8fdf967"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("1b7652f5-be71-48e9-9493-fe54976db20a"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("36022756-536a-4099-b0d2-65a099d2714f"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("567289db-d2f4-41e7-b417-c292b0378913"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d6f5cd92-6fd3-472f-a3b8-0aa15faa8a83"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ded9e342-ef0d-4c2a-bdd0-8e5ff2a117ed"));
        }
    }
}
