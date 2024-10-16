﻿using DataAccess.DaoClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApi.Controllers
{

    /// <summary>
    /// This controller provides login functionality
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {

        #region Repository and constructor
        IAuthorDAO _authorRepository;

        public LoginsController(IAuthorDAO repository) => _authorRepository = repository;
        #endregion

        // POST api/<LoginController>
        [HttpPost] 
        public async Task<ActionResult<int>> Post([FromBody] AuthorDto loginvalues) =>
            await _authorRepository.LoginAsync(loginvalues.Email, loginvalues.Password);
    }
}