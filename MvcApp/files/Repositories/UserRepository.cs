using System;
using System.Collections.Generic;
using NetCoreWebGoat.Config;
using NetCoreWebGoat.Helpers;
using NetCoreWebGoat.Models;

namespace NetCoreWebGoat.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(AppConfig appConfig) : base(appConfig) { }

        public UserModel Login(UserLoginModel loginModel)
        {
            using (var dataReader = Query($"SELECT Id, Email, Name, LastName, Password, Photo, Role, CreatedAt, UpdatedAt FROM \"User\" WHERE Email = '{loginModel.Email}' AND Password = '{HashHelper.Md5(loginModel.Password)}'"))
            {
                if (dataReader.HasRows && dataReader.Read())
                    return new UserModel(dataReader);
            }
            return null;
        }

        public UserModel FindByEmail(string email)
        {
            using (var dataReader = Query($"SELECT Id, Email, Name, LastName, Password, Photo, Role, CreatedAt, UpdatedAt FROM \"User\" WHERE Email = '{email}'"))
            {
                if (dataReader.HasRows && dataReader.Read())
                    return new UserModel(dataReader);
            }
            return null;
        }

        public UserModel FindById(string id)
        {
            using (var dataReader = Query($"SELECT Id, Email, Name, LastName, Password, Photo, Role CreatedAt, UpdatedAt FROM \"User\" WHERE Id = '{id}'"))
            {
                if (dataReader.HasRows && dataReader.Read())
                    return new UserModel(dataReader);
            }
            return null;
        }

        public List<UserModel> FindAll()
        {
            var list = new List<UserModel>();
            using (var dataReader = Query("SELECT Id, Email, Name, LastName, Password, Photo, Role, CreatedAt, UpdatedAt FROM \"User\""))
                if (dataReader.HasRows)
                    while (dataReader.HasRows && dataReader.Read())
                        list.Add(new UserModel(dataReader));
            return list;
        }

        public void Register(UserRegisterModel model)
        {
            var sql = $"INSERT INTO \"User\" (Email, Name, LastName, Password, CreatedAt) VALUES (@Email, @Name, @LastName, @Password, @CreatedAt)";
            model.CreatedAt = DateTime.Now;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@Email", model.Email);
            dic.Add("@Name", model.Name);
            dic.Add("@LastName", model.LastName);
            dic.Add("@Password", HashHelper.Md5(model.Password));
            dic.Add("@CreatedAt", model.CreatedAt);
            ExecuteNonQuery(sql, dic);
        }

        public void UpdatePhoto(int userId, string photo)
        {
            var sql = $"UPDATE \"User\" SET Photo = @Photo, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@Photo", photo);
            dic.Add("@Id", userId);
            dic.Add("@UpdatedAt", DateTime.Now);
            ExecuteNonQuery(sql, dic);
        }
    }
}