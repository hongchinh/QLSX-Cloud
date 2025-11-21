using QLSX.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult UploadCustomer(FileViewModel fileViewModel)
        {
            if (!Request.Headers.ContainsKey("auth-key") || Request.Headers["auth-key"].ToString() != QLSX.Shared.Contansts.AUTH_KEY)
            {
                return Unauthorized();
            }

            if (fileViewModel == null || string.IsNullOrEmpty(fileViewModel.DataBase64)) return BadRequest();

            var fileData = Convert.FromBase64String(fileViewModel.DataBase64);
            if (fileData.Length <= 0) return BadRequest();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/customers", fileViewModel.FileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            System.IO.File.WriteAllBytes(fullPath, fileData);

            return Ok();
        }

    }
}
