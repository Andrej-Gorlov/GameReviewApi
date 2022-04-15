using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Middleware.CustomAuthorization;
using GameReviewApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApi.Controllers
{
    [Authorization]
    [Route("api/")]
    [Produces("application/json")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService) => _reviewService = reviewService;

        /// <summary>
        /// Вывод рецензии по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных рецензии.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /review/{id:int}
        ///     
        ///        reviewId: 0   // Введите id рецензии, данные которой нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Рецензия не найдена. </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        [HttpGet]
        [Route("review/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByIdReview(int id)
        {
            if (id <= 0) 
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю.");
            } 
            var review = await _reviewService.GetByIdAsyncService(id);
            if (review == null) 
            {
                return NotFound();
            }
            return Ok(review);
        }
        /// <summary>
        /// Удаление рецензии по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Рецензия удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /review/{id}
        ///     
        ///        reviewId: 0 // Введите id рецензии, которую нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Рецензия удалёна. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        /// <response code="404"> Рецензия c указанным id не найдена. </response>
        [HttpDelete]
        [Route("review/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (id <= 0) 
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю.");
            }
            var review = await _reviewService.DeleteAsyncService(id);
            if (!review) 
            {
                return NotFound(review);
            }
            return NoContent();
        }
        /// <summary>
        /// Создание новой рецензии.
        /// </summary>
        /// <param name="reviewDto"></param>
        /// <returns>Создаётся рецензия.</returns>
        /// <remarks>
        /// 
        ///     Свойство ["ReviewId"] указывать не обязательно.
        ///     Диапазон оценки равен от 1 до 100.
        /// 
        /// Образец ввовда данных:
        ///
        ///     POST /review
        ///     
        ///     {
        ///       "reviewId": 0,              // id рецензии.
        ///       "gameId": 0,                // id игры, которой принадлежит рецензия.
        ///       "shortStory": "string",     // Краткий рассказ.
        ///       "grade": 1                  // Оцена.
        ///     }
        ///      
        /// </remarks>
        /// <response code="201"> Рецензия создана. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        [HttpPost]
        [Route("review")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewDto)
        {
            var review = await _reviewService.CreateAsyncService(reviewDto);
            if (review == null) 
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetByIdReview), reviewDto);
        }
        /// <summary>
        /// Редактирование рецензии.
        /// </summary>
        /// <param name="reviewDto"></param>
        /// <returns>Обновление игры.</returns>
        /// <remarks>
        /// 
        ///     Диапазон оценки равен от 1 до 100. 
        /// 
        /// Образец ввовда данных:
        ///
        ///     PUT /review
        ///     
        ///     {
        ///       "reviewId": 0,              // id рецензии.
        ///       "gameId": 0,                // id игры, которой принадлежит рецензия.
        ///       "shortStory": "string",     // Краткий рассказ.
        ///       "grade": 1                  // Оцена.
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        /// <response code="404"> Рецензия не найдена. </response>
        [HttpPut]
        [Route("review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewDto reviewDto)
        {
            var review = await _reviewService.UpdateAsyncService(reviewDto);
            if (review == null) 
            {
                return NotFound();
            }
            return Ok(review);
        }
    }
}
