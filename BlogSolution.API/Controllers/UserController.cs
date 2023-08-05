using BlogSolution.Application.User;
using BlogSolution.Utilities.Common;
using BlogSolution.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<BaseResultModel> Login(LoginRequest request)
        {
            try
            {
                if(request.Password.Length < 6)
                {
                    return new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = "Password must be greater than or equal to 6"
                    };
                }
                var response = await _userService.Login(request);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("register")]
        public async Task<BaseResultModel> Register(RegisterRequest request)
        {
            try
            {
                if (request.Password.Length < 6)
                {
                    return new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = "Password must be greater than or equal to 6"
                    };
                }
                if (request.Password != request.ConfirmPassword)
                {
                    return new BaseResultModel
                    {
                        IsSuccess = false,
                        Code = 400,
                        Message = "Confirm password not matched"
                    };
                }
                var response = await _userService.Register(request);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-me")]
        public async Task<BaseResultModel> GetInfoCurrent()
        {
            try
            {
                var response = await _userService.GetInfoCurrent();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
