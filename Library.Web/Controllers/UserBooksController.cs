﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class UserBooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}