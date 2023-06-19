using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShelf.DAL.Migrations
{
    public partial class SeedCategoriesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string addFullTextSearchScript = @"
IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Books]'))
ALTER FULLTEXT INDEX ON [dbo].[Books] DISABLE

GO
IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Books]'))
BEGIN
	DROP FULLTEXT INDEX ON [dbo].[Books]
End

Go
IF EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE [name]='FTCBooks')
BEGIN
	DROP FULLTEXT CATALOG FTCBooks
END

CREATE FULLTEXT CATALOG FTCBooks AS DEFAULT;
CREATE FULLTEXT INDEX ON dbo.Books(Title, Author) KEY INDEX PK_Books ON FTCBooks WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;
";
            migrationBuilder.Sql(addFullTextSearchScript, true);

            const string createStoredProcedureScript = @"
CREATE OR ALTER   PROCEDURE [dbo].[sp_SearchBooksWithPagination]
    @SearchTerm NVARCHAR(50),
    @SortColumn NVARCHAR(50),
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartRow INT
    SET @StartRow = (@PageNumber - 1) * @PageSize

    SELECT *
    FROM (
        SELECT ROW_NUMBER() OVER (ORDER BY CASE WHEN @SortColumn = 'Title' THEN b.Title END,
                                  CASE WHEN @SortColumn = 'Author' THEN b.Author END,
                                  CASE WHEN @SortColumn = 'Category' THEN c.[Name] END) AS RowNumber,
               b.Id, b.Title, b.Author, b.ISBN, b.PublicationYear, b.Quantity, b.CategoryId, c.[Name] as CategoryName
        FROM [dbo].[Books] b INNER JOIN [dbo].[Categories] c ON b.CategoryId = c.[Id]
        WHERE CONTAINS(b.Title, @SearchTerm)
           OR CONTAINS(b.Author, @SearchTerm)
    ) AS Results
    WHERE RowNumber BETWEEN @StartRow + 1 AND @StartRow + @PageSize
END
";
            migrationBuilder.Sql(createStoredProcedureScript);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Fiction", "Fiction" },
                    { 2L, "Fantasy", "Fantasy" },
                    { 3L, "Poem", "Poem" },
                    { 4L, "Classic", "Classic" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropStoredProcedureScript = "DROP PROC [dbo].[sp_SearchBooksWithPagination]";
            migrationBuilder.Sql(dropStoredProcedureScript);

            const string dropFullTextSearchScript = @"
DROP FULLTEXT INDEX on dbo.Books;
DROP FULLTEXT CATALOG FTCBooks;";
            migrationBuilder.Sql(dropFullTextSearchScript, true);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
