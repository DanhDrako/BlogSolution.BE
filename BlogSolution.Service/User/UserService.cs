using BlogSolution.Data.Entities;
using BlogSolution.Data.Enums;
using BlogSolution.Data.Repositories.User;
using BlogSolution.Utilities.Common;
using BlogSolution.Utilities.Constants;
using BlogSolution.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogSolution.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;

        }

        //hash password
        private readonly byte[] saltInput = Encoding.ASCII.GetBytes(SystemConstants.SaltHashPassWord);
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        string HashPasword(string password,byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// compare password (password hash)
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns>boolean</returns>
        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="request"></param>
        /// <returns>token and refreshToken</returns>
        public async Task<BaseResultModel> Login(LoginRequest request)
        {
            var userData = await _userRepository.GetSingleByCondition(x => x.UserName == request.UserName);
            if (userData == null)
                return new BaseResultModel
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Error username or password"
                };

            var checkPassword = VerifyPassword(request.Password, userData.Password, saltInput);
            if (checkPassword != true)
            {
                return new BaseResultModel
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Error username or password"
                };
            }
            var claims = new[]
            {
                new Claim("Id", userData.Id.ToString()),
                new Claim("UserName",userData.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var refreshToken = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: creds);

            var responseData = new GuardVm
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken)
            };

            return new BaseResultModel
            {
                IsSuccess = true,
                Code = 200,
                Message = "Login success",
                Data = responseData
            };
        }

        /// <summary>
        /// register new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseResultModel> Register(RegisterRequest request)
        {
            var findUser = await _userRepository.GetSingleByCondition(x => x.UserName == request.UserName);
            if (findUser != null)
                return new BaseResultModel
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Exited username"
                };

            var passwordHash = HashPasword(request.Password, saltInput);

            var userInput = new UserEntity()
            {
                //Id = Guid.NewGuid(),
                //UserName = request.UserName,
                Password = passwordHash,
                Role = UserRole.Customer
            };

            var userNew = await _userRepository.Create(userInput);

            var userOutPut = new UserVm
            {
                Id = userNew.Id,
                UserName = userNew.UserName,
                Role = userNew.Role
            };
            return new BaseResultModel
            {
                IsSuccess = true,
                Code = 200,
                Message = "Register success",
                Data = userOutPut
            };
        }

        /// <summary>
        /// get info current node (after authentication success)
        /// </summary>
        /// <returns>id and username of user</returns>
        public async Task<BaseResultModel> GetInfoCurrent()
        {
            var userContext = _httpContextAccessor.HttpContext.Items["UserContext"] as ClaimsIdentity;
            var idDecode = userContext.Claims.First(claim => claim.Type == "Id").Value;

            var findUser = await _userRepository.GetSingleById(new Guid(idDecode));
            if (findUser == null)
                return new BaseResultModel
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "Not found"
                };

            var dataResult = new UserVm
            {
                Id = findUser.Id,
                UserName = findUser.UserName,
            };
            return new BaseResultModel
            {
                IsSuccess = true,
                Code = 200,
                Message = "Get user success",
                Data = dataResult
            };
        }
    }
}
