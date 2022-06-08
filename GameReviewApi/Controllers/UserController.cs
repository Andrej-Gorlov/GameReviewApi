using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Paging;
using GameReviewApi.Middleware.CustomAuthorization;
using GameReviewApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameReviewApi.Controllers
{
    [Authorization(Role.Admin)]
    [Route("api")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        /// <summary>
        /// Список всех пользователей.
        /// </summary>
        /// <returns>Вывод всех пользователей.</returns>
        /// <remarks>
        /// Образец запроса:
        ///
        ///     GET /users
        ///     
        ///        PageNumber: Номер страницы   // Введите номер страницы, которую нужно показать с списоком всех пользователей.
        ///        PageSize: Размер страницы    // Введите размер страницы, какое количество пользователей нужно показать.
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="401"> Пользователь [Admin] не авторизован. </response>
        [HttpGet]
        [Route("/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
        {
            var users = await _userService.GetAsyncService(userParameters);
            var metadata = new
            {
                users.TotalCount,
                users.PageSize,
                users.CurrentPage,
                users.TotalPages,
                users.HasNext,
                users.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        } 
        
        /// <summary>
        /// Вывод пользователя по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных пользователя.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /user/{id:int}
        ///     
        ///        userId: 0   // Введите id пользователя, данные которого нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Пользователь не найден. </response>
        /// <response code="401"> Пользователь [Admin] не авторизован. </response>
        /// <response code="401"> Пользователь не найден. </response>
        [HttpGet]
        [Route("/user/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            if (id <= 0) 
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю.");
            } 
            var review = await _userService.GetByIdAsyncService(id);
            if (review == null) 
            {
                return NotFound(review);
            }
            return Ok(review);
        }
        
        /// <summary>
        /// Удаление пользователя по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Пользователь удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /user/{id}
        ///     
        ///        userId: 0 // Введите id пользователя, которого нужно удалить из базы данных.
        ///     
        /// </remarks>
        /// <response code="204"> Пользователь удалён. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="401"> Пользователь [Admin] не авторизован. </response>
        /// <response code="404"> Пользователь c указанным id не найден. </response>
        [HttpDelete]
        [Route("/user/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю.");
            }
            var review = await _userService.DeleteAsyncService(id);
            if (!review)
            {
                return NotFound(review);
            }
            return NoContent();
        }
    }
}
