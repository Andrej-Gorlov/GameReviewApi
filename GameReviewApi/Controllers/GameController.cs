﻿using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApi.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService) => _gameService = gameService;

        /// <summary>
        /// Список всех игр и их средних оценок по убыванию.
        /// </summary>
        /// <returns>Вывод всех игр и их средних оценок по убыванию.</returns>
        /// <remarks>
        ///
        ///     GET /games
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("games")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGames() => Ok(await _gameService.GamesAvgGradeAsyncService());

        /// <summary>
        /// Игра и все её рецензии и оценки.
        /// </summary>
        /// <param name="nameGame"></param>
        /// <returns>Вывод игры и списоков всех её рецензий и оценок.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /game/{nameGame}
        ///     
        ///        nameGame: название игры   // Введите название игры, которой нужно вывести все рецензии и оценки.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Рецензии и оценки введенной игры не найдены. </response>
        [HttpGet]
        [Route("game/{nameGame}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGameStoriesAndGrades(string nameGame)
        {
            var game = await _gameService.GameStoriesAndGradesAsyncService(nameGame);
            if (game == null) 
            {
                return NotFound(game);
            }
            return Ok(game);
        }

        /// <summary>
        /// Список названий игр по жанру.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>Вывод списка игр по жанру.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /games/{genre}
        ///     
        ///        nameGame: название жанра   // Введите название жанра, для вывода списка игр которому они соответствуют.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список игр не найден. </response>
        [HttpGet]
        [Route("games/{genre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGamesByGenre(string genre)
        {
            var games = await _gameService.GamesByGenreAsyncService(genre);
            if (games == null) 
            {
                return NotFound(games);
            }
            return Ok(games);
        }

        /// <summary>
        /// Создание новой игры.
        /// </summary>
        /// <param name="gameDto"></param>
        /// <returns>Создаётся игра.</returns>
        /// <remarks>
        /// 
        ///     Свойство ["GameId", "Reviews" и "Genres"] указывать не обязательно.
        /// 
        /// Образец ввовда данных:
        ///
        ///     POST /game
        ///     
        ///     {
        ///       "gameId": 0,                    // id игры.
        ///       "gameName": "string",           // Название игры.
        ///       "Reviews": [                    // Данные рецензии.
        ///         {
        ///           "reviewId": 0,              // id рецензии.
        ///           "gameId": 0,                // id игры, которой принадлежит рецензия.
        ///           "shortStory": "string",     // Краткий рассказ.
        ///           "grade": 0                  // Оцена.
        ///         }         
        ///       ],                            
        ///       "Genres": [                     // Данные жанра.
        ///         {
        ///           "reviewId": 0,              // id жанра.
        ///           "gameId": 0,                // id игры, которой принадлежит жанр.
        ///           "genreName": "string"       // Название жанра.
        ///         }         
        ///       ], 
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Игра создана. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [Route("game")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGame([FromBody] GameDto gameDto)
        {
            var game = await _gameService.CreateAsyncService(gameDto);
            if (game is null) 
            {
                return BadRequest(game);
            }
            return CreatedAtAction(nameof(GetGames), gameDto);
        }

        /// <summary>
        /// Редактирование игры.
        /// </summary>
        /// <param name="gameDto"></param>
        /// <returns>Обновление игры.</returns>
        /// <remarks>
        /// 
        /// Образец ввовда данных:
        ///
        ///     PUT /game
        ///     
        ///     {
        ///       "gameId": 0,                    // id игры.
        ///       "gameName": "string",           // Название игры.
        ///       "Reviews": [                    // Данные рецензии.
        ///         {
        ///           "reviewId": 0,              // id рецензии.
        ///           "gameId": 0,                // id игры, которой принадлежит рецензия.
        ///           "shortStory": "string",     // Краткий рассказ.
        ///           "grade": 0                  // Оцена.
        ///         }         
        ///       ],                            
        ///       "Genres": [                     // Данные жанра.
        ///         {
        ///           "reviewId": 0,              // id жанра.
        ///           "gameId": 0,                // id игры, которой принадлежит жанр.
        ///           "genreName": "string"       // Название жанра.
        ///         }         
        ///       ], 
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Игра не найдена. </response>
        [HttpPut]
        [Route("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGame([FromBody] GameDto gameDto)
        {
            var game = await _gameService.UpdateAsyncService(gameDto);
            if (game is null) 
            {
                return NotFound(game);
            }
            return Ok(game);
        }

        /// <summary>
        /// Удаление игры по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Игра удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /game/{id}
        ///     
        ///        gameId: 0 // Введите id игры, которую нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Игра удалёна. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Игра c указанным id не найден. </response>
        [HttpDelete]
        [Route("user/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGame(int id)
        {
            if (id <= 0) 
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var game = await _gameService.DeleteAsyncService(id);
            if (!game) 
            {
                return NotFound(game);
            } 
            return NoContent();
        }

        /// <summary>
        /// Вывод игры по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных игры.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /game/{id:int}
        ///     
        ///        gameId: 0   // Введите id игры, данные которой нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Игра не найдена. </response>
        [HttpGet]
        [Route("game/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdGame(int id)
        {
            if (id <= 0) 
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            var game = await _gameService.GetByIdAsyncService(id);
            if (game == null) 
            {
                return NotFound(game);
            }
            return Ok(game);
        }
    }
}
