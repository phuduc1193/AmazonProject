﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AmazonProjectAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public CredentialSchema Login([FromBody] UserCredential userCredential)
        {
            return _userService.Login(userCredential.Username, userCredential.Password);
        }

        [HttpPost("[action]")]
        public CredentialSchema RefreshToken([FromBody] string token)
        {
            return _userService.LoginWithRefreshToken(token);
        }
    }
}