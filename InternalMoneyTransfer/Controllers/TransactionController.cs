using System;
using System.Collections.Generic;
using AutoMapper;
using InternalMoneyTransfer.ActionFilters;
using InternalMoneyTransfer.Core.DataModel;
using InternalMoneyTransfer.Core.Dtos;
using InternalMoneyTransfer.Services.Transaction;
using InternalMoneyTransfer.Services.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternalMoneyTransfer.Controllers
{
    [LogAction]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        #region Constructor

        public TransactionController(ITransactionService transactionService,
            IUserAccountService userAccountService, IMapper mapper)
        {
            _transactionService = transactionService;
            _userAccountService = userAccountService;
            _mapper = mapper;
        }

        #endregion

        #region Fields

        private readonly ITransactionService _transactionService;

        private readonly IUserAccountService _userAccountService;

        private readonly IMapper _mapper;

        #endregion

        #region Methods

        [Authorize]
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            var accountDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(accountDto);
        }

        [Authorize]
        [HttpGet("getall/{id}")]
        public IActionResult GetAll(int id)
        {
            var transactions = _transactionService.GetAllTransactionByAccountId(id);
            var transactionDtos = _mapper.Map<IList<TransactionDto>>(transactions);
            return Ok(transactionDtos);
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddTransaction([FromBody] TransactionDto transactionDto)
        {
            if (!_transactionService.CanMakeTransaction(transactionDto.CreditorId, transactionDto.Amount))
                return BadRequest(new {message = "Error during transaction."});

            try
            {
                var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
                var creditor = _userAccountService.GetAccountById(transactionDto.CreditorId);
                var debtor = _userAccountService.GetAccountById(transactionDto.DebtorId);
                transaction.Creditor = creditor;
                transaction.Debtor = debtor;
                _transactionService.CreateTransaction(transaction);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        #endregion
    }
}