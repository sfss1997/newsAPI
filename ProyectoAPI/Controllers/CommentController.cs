using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Data.SqlClient;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        _2020_IF4101PICES_B2Context _context = new _2020_IF4101PICES_B2Context();

        [Route("[action]/{id}")]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var CommentID = new SqlParameter("@id", id);
            var news = _context.News
                 .FromSqlRaw($"SelectCommentById" + CommentID)
                 .AsEnumerable();

            return Ok(news);
        }


    }
}