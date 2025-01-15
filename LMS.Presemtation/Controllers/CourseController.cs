using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Presemtation.Controllers;

[Route("api/courses")]
[ApiController]
public class CourseController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult GetDemoAuth()
    {
        return Ok(new CourseDto[]{ new CourseDto { Id = 1, Name = "Kalle" },
                                  new  CourseDto{ Id = 2, Name = "Anka" },
                                  new  CourseDto{ Id = 3, Name = "Nisse" },
                                  new  CourseDto{ Id = 4, Name = "Pelle" }});
    }
}
