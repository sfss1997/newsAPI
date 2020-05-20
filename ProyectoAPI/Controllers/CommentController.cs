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
        public ActionResult GetCommentsByIdNews(int id)
        {
            var CommentID = new SqlParameter("@NewsId", id);
            var news = _context.News
                 .FromSqlRaw($"SelectCommentById" + CommentID)
                 .AsEnumerable().ToList();

            return Ok(news);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertComment(Comment comment)
        {

            var commentResult = _context.Database
                 .ExecuteSqlRaw("InsertComment {0}, {1}, {2}, {3}, {4}" ,
                 comment.AuthorId,
                 comment.AuthorName,
                 comment.DateTime,
                 comment.Text,
                 comment.NewsId);

            if (commentResult == 0)
            {
                return null;
            }
            return Ok(commentResult);
        }

    }
}