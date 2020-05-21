
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
        [HttpPost]
        public ActionResult InsertNews(News news)
        {

            var newsResult = _context.Database.ExecuteSqlRaw("InsertNews {0}, {1}, {2}, {3}, {4}",
                news.AuthorId,
                news.AuthorName,
                news.DateTime,
                news.Text,
                news.Title);

            if (newsResult == 0)
            {
                return null;
            }

            return Ok(newsResult);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult UpdateNews(News news)
        {

            var newsResult = _context.Database.ExecuteSqlRaw("UpdateNews {0}, {1}, {2}, {3}, {4}, {5}",
                news.Id,
                news.AuthorId,
                news.AuthorName,
                news.DateTime,
                news.Text,
                news.Title);

            if (newsResult == 0)
            {
                return null;
            }

            return Ok(newsResult);
        }

        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public ActionResult DeleteNewsById(String id)
        {
            var NewsId = new SqlParameter("@id", id);
            var result = _context.Database.ExecuteSqlRaw($"DeleteNews" + NewsId);
            if (result == 0)
            {
                return null;
            }
            return Ok(result);
        }
    }
}