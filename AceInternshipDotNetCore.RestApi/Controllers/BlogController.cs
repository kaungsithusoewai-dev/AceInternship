using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace AceInternshipDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        public BlogController()
        {
            // _connectionStringBuilder = new SqlConnectionStringBuilder();
            //   _connectionStringBuilder.DataSource = ".";//Sever  NAme
            // _connectionStringBuilder.InitialCatalog = "AceInternship";
            //_connectionStringBuilder.UserID = "sa";
            // _connectionStringBuilder.Password = "sa@123";

            _connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AceInternship",
                UserID = "sa",
                Password = "sa@123",
            };
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var lst = db.Query<TblBlog>(Quries.BlogList).ToList();
            return Ok(lst);
        }

        [HttpGet ("{id}")]
        public IActionResult GetBlog(int id)
        {

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<TblBlog>(Quries.BlogById, new {BlogId = id}).FirstOrDefault();
            if (item is null)
            
                return NotFound("No data Found");
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Quries.BlogCreate, blog);
            string message = result > 0 ? "Saving Success" : "Saving failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            blog.BlogId = id;
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result2 = db.Execute(Quries.BlogUpdate, blog);
            string message2 = result2 > 0 ? "Updating Success" : "Update failed";
            return Ok(message2);
            
        }
        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok("PatchBlog");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result1 = db.Execute(Quries.BlogDelete, new { BlogId = id });
            string message1 = result1 > 0 ? "Delete Success" : "Delete failed";
            return Ok(message1);
        }
    }



    public class TblBlog
    {

        public int BlogId { get; set; }

        public string BlogTitle { get; set; }

        public string BlogAuthor { get; set; }

        public string BlogContent { get; set; }
    }
    public static class Quries
    {
        public static string BlogList { get; } = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]";

        public static string BlogById { get; } = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog] Where BlogId = @BlogId";

        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
		  @BlogAuthor,
		   @BlogContent
        )";

        public static string BlogUpdate { get; } = @"
UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE Blogid = @BlogId ";

        public static string BlogDelete { get; } = @"
DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";
    }
}



