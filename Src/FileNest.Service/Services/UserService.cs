using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileNest.Model.Models;
using MongoDB.Driver;

namespace FileNest.Service
{
    public class UserService
    {
        private readonly IMongoCollection<UserClass> _users;
        public UserService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("FileNest");
            _users = database.GetCollection<UserClass>("Users");
        }
        public async Task CreateUserAsync(UserClass user)
        {
            await _users.InsertOneAsync(user);
        }
        public async Task<List<UserClass>> GetUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }
    }
}