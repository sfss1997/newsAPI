
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

        [Route("[action]/{id}")]
        [HttpGet("{id}")]
        public ActionResult GetNewsById(int id)
        {
            var NewsId = new SqlParameter("@Id", id);
            var news = _context.News
                 .FromSqlRaw($"GetNewsById @Id" , NewsId)
                 .AsEnumerable().Single();

            return Ok(news);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult PostNews(News news)
        {

            var newsResult = _context.Database.ExecuteSqlRaw("InsertUpdateNews {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                news.Id,
                news.Title,
                news.Text,
                news.DateTime,
                news.AuthorName,
                news.AuthorId,
                "Insert");

            if (newsResult == 0)
            {
                return null;
            }

            return Ok(newsResult);
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult PutNews(News news)
        {
            var newsResult = _context.Database.ExecuteSqlRaw("InsertUpdateNews {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                news.Id,
                news.Title,
                news.Text,
                news.DateTime,
                news.AuthorName,
                news.AuthorId,
                "Update");

            if (newsResult == 0)
            {
                return null;
            }

            return Ok(newsResult);
        }

        [Route("[action]/{id}")]
        [HttpDelete("{id}")]
        public ActionResult DeleteNews(int id)
        {
            var NewsId = new SqlParameter("@Id", id);
            var news = _context.News.FromSqlRaw($"DeleteNews @Id", NewsId);
            
            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }
    }
}