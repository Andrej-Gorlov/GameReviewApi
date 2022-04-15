using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewApi.DAL.Migrations
{
    public partial class AddToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                    table.ForeignKey(
                        name: "FK_Genre_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    ShortStory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName" },
                values: new object[] { 1, "Assassin's Creed" });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName" },
                values: new object[] { 2, "Resident Evil" });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "GameId", "GameName" },
                values: new object[] { 3, "Devil May Cry" });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "GameId", "GenreName" },
                values: new object[,]
                {
                    { 1, 1, "Action" },
                    { 2, 1, "RPG" },
                    { 3, 2, "Action" },
                    { 4, 2, "Horror" },
                    { 5, 2, "RPG" },
                    { 6, 3, "Action" },
                    { 7, 3, "Slasher" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "ReviewId", "GameId", "Grade", "ShortStory" },
                values: new object[,]
                {
                    { 1, 1, 78, "Главная сюжетная ветка разворачивается, как уже говорилось, вокруг Альтаира, не последнего человека в ордене Ассассинов, который на архиважном задании нехило так облажался, в результате чего один напарник погиб, другой стал калекой. И ладно бы, так Альтаир умудрился привести хвост из армии крестоносцев, то бишь тамплиеров, в свою крепость. За такой косяк Аль-Муалим, духовный наставник и глава всего ордена, выносит главному герою выговор, занесением нескольких сантиметров стали в брюшную полость, понижает в ранге и дает шанс исправиться, потому что во-первых, Альтаир хоть и подставил свою семью (читайте орден), но мастерством обделен не был, а во-вторых - ну главный герой же, нельзя его выпиливать в начале игры." },
                    { 2, 1, 85, "Проиграв на боевой системе, разработчики вовсю оторвались на атмосферности и историчности игры. Средними веками, теми событиями веет так, что дух захватывает. Города выполнены детально, со всеми достопримечательностями. Очень красиво. Народ толпится у прилавков, глашатаи дерут глотки, прославляя Саллах - ад - Дина или Ричарда III. Это здорово." },
                    { 3, 1, 93, "Музыкальное сопровождение тоже на высоте. С восточными нотками, так же атмосферно. Про графику тоже следует упомянуть.Она на высоте.Сделанная для того времени, она реалистична: одежда развевается, волны плещут.Красиво, в общем.Даже сейчас она(графика) смотрится достойно.Весьма достойно." },
                    { 4, 2, 59, "Максимально скучная игра. Такой душноты я не ожидал от хоррора. Миллион часов надо бегать по замку, собирая всякие тарелочки. Я бросил это дело и удалил игру после 4 часов игры. Стрельбы никакой не дали. Нужно было убегать от неубиваемой дочери леди Диметреску. После этого снова тарелки. Поставлю 2 балла. Больше совесть не позволяет." },
                    { 5, 2, 98, "Эта часть напрямую продолжает предыдущую. По сюжету как всегда все сначала запутанно, но это не плохо, так и должно быть. Истории боссов получились вообще по кайфу. Единственное, что не зашло, так это количество шутана. Тут прям на каждом шагу тебя кто-то ждет пачками. Хотя обычно Резидент — это больше про сюжет и стелс, а от хоррора тут вообще мало чего. Добавили торговца, что тоже очень удобно в прохождении. Отрываться вообще не хотелось, прям затягивает. Много всего понравилось, но для полной картины лучше пройти 7 часть" },
                    { 6, 2, 71, "Прошёл игру пять раз и почти на 100%. Это не хоррор (кроме одного момента), а скорее шутер от первого лица. Владык не раскрывают, либо ищи одну записки в огромной локации, либо высматривай детали одежды и окружения. Боевая система как в RE7. А атмосфера лучшая часть игры, возможно из-за неё я и прошёл игру пять раз." },
                    { 7, 3, 39, "Когда-то давно играл в Devil May Cry , но не помню какая была часть, играл тогда на Play Station 3 , но совсем не помню про что там было и как это было) Эта серия игры затянула, интересный сюжет, драчульки) Но самый низкий уровень игры, как по мне сделан, ну уж слииишком низко, боссы почти не чувствовались, ну возможно это и наоборот не плохо. В общем если хотите погореть, то самый низкий уровень не для вас)" },
                    { 8, 3, 93, "Игра классная, как слешер. Сюжет вполне себе интересный. Нашёл два минуса довольно существенных - унылость локаций особенно во второй половине и кол-во боссов могло быть больше или проработанней. Эти минусы (особенно локации) кто бы не говорил очень влияют на восприятии игры. Поэтому такие вот дела. Игру рекомендую не только любителям кромсать всё живое, но и просто гомерам" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GameId",
                table: "Genre",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_GameId",
                table: "Review",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
