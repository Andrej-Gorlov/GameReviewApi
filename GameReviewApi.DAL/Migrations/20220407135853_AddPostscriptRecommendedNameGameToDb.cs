using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewApi.DAL.Migrations
{
    public partial class AddPostscriptRecommendedNameGameToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE Game SET GameName=CONCAT(GameName, N' рекомендовано') 
                                   WHERE EXISTS (SELECT 1 FROM Genre 
                                                 WHERE Genre.GameId = Game.GameId AND Genre.GenreName ='RPG')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE Game SET GameName=REPLACE(GameName, N'рекомендовано', '') 
                                   WHERE EXISTS (SELECT 1 FROM Genre 
                                                 WHERE Genre.GameId = Game.GameId AND Genre.GenreName ='RPG')");
        }
    }
}
