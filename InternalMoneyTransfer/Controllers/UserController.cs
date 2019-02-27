using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using InternalMoneyTransfer.ActionFilters;
using InternalMoneyTransfer.Core.DataModel;
using InternalMoneyTransfer.Core.Dtos;
using InternalMoneyTransfer.Core.Helpers;
using InternalMoneyTransfer.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InternalMoneyTransfer.Controllers
{
    [LogAction]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Constructor

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        #endregion

        #region Fields

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        #endregion

        #region Methods

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginUserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = JwtHelper.GetSecurityKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.Id,
                Username = user.FullName,
                user.Email,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto userDto)
        {
            if (!userDto.Password.Equals(userDto.PasswordConfirm))
                return BadRequest(new {message = "Password and confirm password do not match."});

            var user = _mapper.Map<User>(userDto);

            try
            {
                _userService.CreateUser(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var usersDtoList = _mapper.Map<IList<UserDto>>(users);
            return Ok(usersDtoList);
        }

        #endregion
    }
}