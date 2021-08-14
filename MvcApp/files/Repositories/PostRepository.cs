using System;
using System.Collections.Generic;
using NetCoreWebGoat.Config;
using NetCoreWebGoat.Models;

namespace NetCoreWebGoat.Repositories
{
    public class PostRepository : BaseRepository
    {
        public PostRepository(AppConfig appConfig) : base(appConfig) { }

        public List<PostModelList> GetAll(string filter = "")
        {
            List<PostModelList> list = new List<PostModelList>();
            var sql = $"SELECT p.Id Id, Text, p.Photo Photo, UserId, Name, LastName, u.Photo UserPhoto, p.CreatedAt CreatedAt, p.UpdatedAt UpdatedAt FROM \"Post\" p join \"User\" u on u.Id = p.UserId WHERE p.Text LIKE '%{filter}%' ORDER BY p.CreatedAt DESC";
            var result = Query(sql);
            if (result.HasRows)
                while (result.Read())
                    list.Add(new PostModelList(result));
            return list;
        }

        public void Add(PostModel post)
        {
            var sql = $"INSERT INTO \"Post\" (Text, Photo, UserId, CreatedAt) VALUES (@Text, @Photo, @UserId, @CreatedAt)";
            post.CreatedAt = DateTime.Now;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@Id", post.Id);
            dic.Add("@Text", post.Text);
            dic.Add("@Photo", post.Photo);
            dic.Add("@UserId", post.UserId);
            dic.Add("@CreatedAt", post.CreatedAt);
            ExecuteNonQuery(sql, dic);
        }

        public void Update(PostModel post)
        {
            var sql = $"UPDATE \"Post\" SET Text = @Text, Photo = @Photo, UserId = @UserId, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            post.UpdatedAt = DateTime.Now;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@Id", post.Id);
            dic.Add("@Text", post.Text);
            dic.Add("@Photo", post.Photo);
            dic.Add("@UserId", post.UserId);
            dic.Add("@UpdatedAt", post.UpdatedAt);
            ExecuteNonQuery(sql, dic);
        }

        public void Delete(int id)
        {
            var sql = $"DELETE FROM \"Post\" WHERE Id = @Id";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@Id", id);
            ExecuteNonQuery(sql, dic);
        }
    }
}