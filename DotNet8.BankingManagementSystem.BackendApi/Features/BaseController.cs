﻿using DotNet8.BankingManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.BankingManagementSystem.BackendApi.Features
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected IActionResult InternalServerError(Exception exception)
        {
            return Ok(new
            {
                Response = new MessageResponseModel(false, exception)
            });
        }
    }
}