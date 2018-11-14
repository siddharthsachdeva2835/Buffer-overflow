using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Entity;
using Entity.Models;
using Repo.Config;

namespace Repo
{
    public class UserRepository
    {
        public UserDTO getUsers(int id)

        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    User user = db.Users.SingleOrDefault(x => x.UserID == id);

                    return MapConfig.mapper.Map<User, UserDTO>(user);
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException)
                    throw new Exception("Our database is currently experiencing problems. Please try again later.");

                else if (ex is NullReferenceException)
                    throw new Exception("EmailID not registed.");

                else if (ex is ArgumentException)
                    throw new Exception("Incorrect Password");

                else
                    throw new Exception("Our website is facing some problem");
            }
        }

        public UserDTO getUserByEmailID(string emailID)

        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    User user = db.Users.SingleOrDefault(x => x.EmailID == emailID);
                    return MapConfig.mapper.Map<User, UserDTO>(user);
                }
            }
            catch (Exception ex)
            {
               throw new Exception("Our website is facing some problem");
            }
        }

        public UserDTO getUserByToken(string nameFilter)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    User user = db.Tokens.SingleOrDefault(x =>  x.TokenString.ToString() == nameFilter).User;
                    return MapConfig.mapper.Map<User, UserDTO>(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Our website is facing some problem");
            }
        }

        public UserDTO SaveUser(UserDTO RegisterUserDTO, string imageURL)
        {
            try
            {
                using (var db = new DataBaseContext())
                {

                    User user = db.Users.Add(MapConfig.mapper.Map<UserDTO, User>(RegisterUserDTO));
                    user.Votings = new List<Voting>();
                    user.ImageURL = imageURL;
                    Token token = new Token();
                    token.TokenString = (user.EmailID + ':' + user.Password).GetHashCode();
                    user.Token = token;
                    db.SaveChanges();
                    return MapConfig.mapper.Map<User, UserDTO>(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Our website is facing some problem");
            }
        }
    }
}
