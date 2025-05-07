using NewsPortal.EF;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace NewsPortal.Controllers
{
    public class NewsController : ApiController
    {
        private readonly Context db = new Context();

        // GET All News
        [HttpGet]
        [Route("api/news/getall")]
        public IHttpActionResult GetAll()
        {
            var data = db.News.ToList();
            if (data.Any())
            {
                return Ok(new { Success = true, Message = "News retrieved successfully.", Data = data });
            }
            else
            {
                return NotFound();
            }
        }



        // GET News by ID
        [HttpGet]
        [Route("api/news/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var news = db.News.Find(id);
            if (news != null)
            {
                return Ok(new { Success = true, Message = "News retrieved successfully.", Data = news });
            }
            else
            {
                return NotFound();
            }
        }



        // CREATE News
        [HttpPost]
        [Route("api/news/create")]
        public IHttpActionResult Create(News news)
        {
            if (string.IsNullOrEmpty(news.Title) || string.IsNullOrEmpty(news.Category))
            {
                return BadRequest("Invalid news data.");
            }

            db.News.Add(news);
            db.SaveChanges();
            return Ok(new { Success = true, Message = "News created successfully.", Data = news });
        }



        // UPDATE News
        [HttpPut]
        [Route("api/news/update/{id}")]
        public IHttpActionResult Update(int id, News updatedNews)
        {
            var existingNews = db.News.Find(id);
            if (existingNews != null)
            {
                existingNews.Title = updatedNews.Title;
                existingNews.Category = updatedNews.Category;
                existingNews.Date = updatedNews.Date;
                db.SaveChanges();
                return Ok(new { Success = true, Message = "News updated successfully.", Data = existingNews });
            }
            else
            {
                return NotFound();
            }
        }



        // DELETE News
        [HttpDelete]
        [Route("api/news/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var news = db.News.Find(id);
            if (news != null)
            {
                db.News.Remove(news);
                db.SaveChanges();
                return Ok(new { Success = true, Message = "News deleted successfully." });
            }
            else
            {
                return NotFound();
            }
        }



        // Feature APIs

        // 1. GET News by Date
        [HttpGet]
        [Route("api/news/bydate")]
        public IHttpActionResult GetByDate(DateTime date)
        {
            var data = db.News.Where(n => DbFunctions.TruncateTime(n.Date) == DbFunctions.TruncateTime(date)).ToList();
            return Ok(new { Success = true, Message = "News by date retrieved successfully.", Data = data });
        }



        // 2. GET News by Category
        [HttpGet]
        [Route("api/news/bycategory")]
        public IHttpActionResult GetByCategory(string category)
        {
            var data = db.News.Where(n => n.Category == category).ToList();
            return Ok(new { Success = true, Message = "News by category retrieved successfully.", Data = data });
        }



        // 3. GET News by Title
        [HttpGet]
        [Route("api/news/bytitle")]
        public IHttpActionResult GetByTitle(string title)
        {
            var data = db.News.Where(n => n.Title.Contains(title)).ToList();
            return Ok(new { Success = true, Message = "News by title retrieved successfully.", Data = data });
        }



        // 4. GET News by Date & Category
        [HttpGet]
        [Route("api/news/bydateandcategory")]
        public IHttpActionResult GetByDateAndCategory(DateTime date, string category)
        {
            var data = db.News.Where(n => DbFunctions.TruncateTime(n.Date) == DbFunctions.TruncateTime(date) && n.Category == category).ToList();
            return Ok(new { Success = true, Message = "News by date and category retrieved successfully.", Data = data });
        }




        // 5. GET News by Title & Category
        [HttpGet]
        [Route("api/news/bytitleandcategory")]
        public IHttpActionResult GetByTitleAndCategory(string title, string category)
        {
            var data = db.News.Where(n => n.Title.Contains(title) && n.Category == category).ToList();
            return Ok(new { Success = true, Message = "News by title and category retrieved successfully.", Data = data });
        }



        // 6. GET News by Title, Date & Category
        [HttpGet]
        [Route("api/news/byall")]
        public IHttpActionResult GetByAll(string title, DateTime date, string category)
        {
            var data = db.News.Where(n =>
                n.Title.Contains(title) &&
                DbFunctions.TruncateTime(n.Date) == DbFunctions.TruncateTime(date) &&
                n.Category == category)
            .ToList();

            return Ok(new { Success = true, Message = "News by all filters retrieved successfully.", Data = data });
        }




        //7: Any category & title matches input
        [HttpGet]
        [Route("api/news/matches")]
        public IHttpActionResult GetMatches(string title, string category)
        {
            var data = db.News.Where(n => n.Title == title || n.Category == category).ToList();
            return Ok(new { Success = true, Message = "News matches category or title retrieved successfully.", Data = data });
        }
    }
}

