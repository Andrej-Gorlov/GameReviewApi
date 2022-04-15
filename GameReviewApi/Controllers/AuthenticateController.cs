using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApi.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(IAuthenticateService authenticateService) => _authenticateService = authenticateService;

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Пользователь зарегистрировался.</returns>
        /// <remarks>
        /// 
        ///     Свойство ["userId"] указывать не обязательно.
        /// 
        /// Образец ввовда данных:
        ///
        ///     POST /register
        ///     
        ///     {
        ///       "userName": "string",       // Имя пользователя.
        ///       "password": "string",       // Пароль.
        ///       "email": "string"           // Электронная почта.
        ///     }
        ///      
        /// </remarks>
        /// <response code="200"> Пользователь зарегистрирован. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="500"> Пользователь уже существует. </response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var register = await _authenticateService.RegisterAsyncService(model);
            if (register == null) 
            {
                return BadRequest();
            }
            if (register.StatusCode == 500) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, register);
            }
            return Ok(register);
        }
        /// <summary>
        /// Регистрация пользователя [Admin].
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Пользователь [Admin] зарегистрировался.</returns>
        /// <remarks>
        /// 
        ///     Свойство ["userId"] указывать не обязательно.
        /// 
        /// Образец ввовда данных:
        ///
        ///     POST /register
        ///     
        ///     {
        ///       "userName": "string",       // Имя пользователя.
        ///       "password": "string",       // Пароль.
        ///       "email": "string"           // Электронная почта.
        ///     }
        ///      
        /// </remarks>
        /// <response code="200"> Пользователь [Admin] зарегистрирован. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="500"> Пользователь [Admin] уже существует. </response>
        [HttpPost]
        [Route("registerAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            var register = await _authenticateService.RegisterAdminAsyncService(model);
            if (register == null) 
            {
                return BadRequest();
            }
            if (register.StatusCode == 500) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, register);
            }
            return Ok(register);
        }
        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Пользователь авторизован.</returns>
        /// <remarks>
        /// 
        /// Образец ввовда данных:
        ///
        ///     POST /login
        ///     
        ///     {
        ///       "userName": "string",      // Имя пользователя.
        ///       "Password": "string"       // Пароль.
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="401"> Пользователь не авторизован. </response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var register = await _authenticateService.LoginAsyncService(model);
            if (register.StatusCode == 401) 
            {
                return Unauthorized(register);
            }
            return Ok(register);
        }
    }
}
