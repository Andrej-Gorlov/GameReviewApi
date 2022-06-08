using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Middleware.CustomAuthorization;
using GameReviewApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApi.Controllers
{
    [Authorization]
    [Route("api/")]
    [Produces("application/json")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService) => _genreService = genreService;

        /// <summary>
        /// Вывод жанра по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных жанра.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /genre/{id:int}
        ///     
        ///        genreId: 0   // Введите id жанра, данные которого нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода. </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        /// <response code="404"> Жанр не найден. </response>
        [HttpGet]
        [Route("genre/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdGenre(int id)
        {
            if (id <= 0) 
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю.");
            }
            var genre = await _genreService.GetByIdAsyncService(id);
            if (genre is null) 
            {
                return NotFound(genre);
            }
            return Ok(genre);
        }

        /// <summary>
        /// Удаление жанра по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Жанр удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /genre/{id}
        ///     
        ///        genreId: 0 // Введите id жанра, которого нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Жанр удалён. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        /// <response code="404"> Жанр c указанным id не найден. </response>
        [HttpDelete]
        [Route("genre/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (id <= 0) 
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю.");
            }
            var genre = await _genreService.DeleteAsyncService(id);
            if (!genre) 
            {
                return NotFound(genre);
            }
            return NoContent();
        }

        /// <summary>
        /// Создание нового жанра.
        /// </summary>
        /// <param name="genreDto"></param>
        /// <returns>Создаётся жанр.</returns>
        /// <remarks>
        /// 
        ///     Свойство ["GenreId"] указывать не обязательно.
        /// 
        /// Образец ввовда данных:
        ///
        ///     POST /genre
        ///     
        ///     {
        ///       "genreId": 0,               // id жанра.
        ///       "gameId": 0,                // id игры, которой принадлежит жанр.
        ///       "genreName": "string",      // Название жанра.
        ///     }
        ///      
        /// </remarks>
        /// <response code="201"> Жанр создан. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        [HttpPost]
        [Route("genre")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDto genreDto)
        {
            var genre = await _genreService.CreateAsyncService(genreDto);
            if (genre is null) 
            {
                return BadRequest(genre);
            }
            return CreatedAtAction(nameof(GetByIdGenre), genreDto);
        }

        /// <summary>
        /// Редактирование жанра.
        /// </summary>
        /// <param name="genreDto"></param>
        /// <returns>Обновление жанра.</returns>
        /// <remarks>
        /// 
        /// Образец ввовда данных:
        ///
        ///     PUT /genre
        ///     
        ///     {
        ///       "genreId": 0,               // id жанра.
        ///       "gameId": 0,                // id игры, которой принадлежит жанр.
        ///       "genreName": "string",      // Название жанра.
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Жанр не найден. </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        [HttpPut]
        [Route("genre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreDto genreDto)
        {
            var genre = await _genreService.UpdateAsyncService(genreDto);
            if (genre is null) 
            {
                return NotFound(genre);
            } 
            return Ok(genre);
        }
    }
}
