
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
    public class NewsController : ControllerBase
    {
        _2020_IF4101PICES_B2Context _context = new _2020_IF4101PICES_B2Context();


        [Route("[action]")]
        [HttpGet]
        public IActionResult GetNews()
        {
            var news = _context.News
                .FromSqlRaw($"SelectNews")
                .AsEnumerable().ToList();

            return Ok(news);
        }

        [Route("[action]/{title}")]
        [HttpGet("{title}")]
        public ActionResult GetNewsByTitle(String title)
        {
            var NewsTitle = new SqlParameter("@title", title);
            var news = _context.News
                 .FromSqlRaw($"SelectNewsByTitle" + NewsTitle)
                 .AsEnumerable().Single();

            return Ok(news);
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult InsertNews(News news)
        {
            var AuthorId = new SqlParameter("@AuthorId", news.AuthorId);
            var AuthorName = new SqlParameter("@AuthorName", news.AuthorName);
            var DateTime = new SqlParameter("@DateTime", news.DateTime);
            var Text = new SqlParameter("@Text", news.Text);
            var Title = new SqlParameter("@Title", news.Title);

            var newsResult = _context.News
                 .FromSqlRaw($"InsertNews" + Title, Text, DateTime, AuthorName, AuthorId)
                 .AsEnumerable();

            return Ok(newsResult);
        }
    }
}