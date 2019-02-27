using System.Collections.Generic;
using AutoMapper;
using InternalMoneyTransfer.ActionFilters;
using InternalMoneyTransfer.Core.Dtos;
using InternalMoneyTransfer.Services.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternalMoneyTransfer.Controllers
{
    [LogAction]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Constructor

        public AccountController(IUserAccountService userAccountService, IMapper mapper)
        {
            _userAccountService = userAccountService;
            _mapper = mapper;
        }

        #endregion

        #region Fields

        private readonly IUserAccountService _userAccountService;

        private readonly IMapper _mapper;

        #endregion

        #region Methods

        [Authorize]
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var account = _userAccountService.GetAccountById(id);
            var accountDto = _mapper.Map<UserAccountDto>(account);
            return Ok(accountDto);
        }

        [Authorize]
        [HttpGet("getall/{id}")]
        public IActionResult GetAll(int id)
        {
            var accounts = _userAccountService.GetAllAccountsWithoutExcludedId(id);
            var accountDtos = _mapper.Map<IList<UserAccountDto>>(accounts);
            return Ok(accountDtos);
        }

        #endregion
    }
}