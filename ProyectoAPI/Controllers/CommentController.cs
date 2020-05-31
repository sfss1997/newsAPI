﻿using System.Linq;
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
            var CommentID = new SqlParameter("@id", id);
            var news = _context.Comment
                 .FromSqlRaw($"SelectCommentById @id" , CommentID)
                 .AsEnumerable().ToList();

            return Ok(news);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult PostComment(Comment comment)
        {

            var commentResult = _context.Database
                 .ExecuteSqlRaw("InsertComment {0}, {1}, {2}, {3}, {4}" ,
                 comment.AuthorId,
                 comment.AuthorName,
                 comment.Text,
                 comment.DateTime,
                 comment.NewsId);

            if (commentResult == 0)
            {
                return null;
            }
            return Ok(commentResult);
        }

        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public ActionResult DeleteComment(String id)
        {
            var CommentId = new SqlParameter("@id", id);
            var result = _context.Database.ExecuteSqlRaw($"DeleteComment @id" ,CommentId);
            if (result == 0)
            {
                return null;
            }
            return Ok(result);
        }

    }
}