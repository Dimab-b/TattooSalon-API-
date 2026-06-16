#if DEBUG || Testing // Ця директива каже: код існує тільки при розробці або тестах
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApiArchitecture.Controllers
{
    [ApiController]
    [Route("api/test-exception")]
    public class controllerForTesting : ControllerBase
    {
        [HttpGet]
        public IActionResult ThrowError()
        {
          
            throw new InvalidOperationException("Тестовий вибух сервера!");
        }
    }
}
#endif