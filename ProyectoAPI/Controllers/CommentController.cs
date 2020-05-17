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
        public ActionResult GetComment(int id)
        {
            var CommentID = new SqlParameter("@id", id);
            var news = _context.News
                 .FromSqlRaw($"SelectCommentById" + CommentID)
                 .AsEnumerable();

            return Ok(news);
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult InsertComment(Comment comment)
        {
            var NewsId = new SqlParameter("@NewsId", comment.NewsId);
            var AuthorId = new SqlParameter("@AuthorId", comment.AuthorId);
            var AuthorName = new SqlParameter("@AuthorName", comment.AuthorName);
            var DateTime = new SqlParameter("@DateTime", comment.DateTime);
            var Text = new SqlParameter("@Text", comment.Text);


            var newsResult = _context.Database
                 .ExecuteSqlRaw($"InsertComment" + Text + DateTime + AuthorName + AuthorId + NewsId);
                 

            return Ok(newsResult);
        }

    }
}