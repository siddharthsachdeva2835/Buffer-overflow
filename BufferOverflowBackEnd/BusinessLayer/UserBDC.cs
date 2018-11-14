using Repo;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserBDC
    {
        private UserRepository _userRepository;
        public UserBDC()
        {
            _userRepository = new UserRepository();
        }

        public UserDTO getUsers(int id)
        {
            return _userRepository.getUsers(id);
        }

        public UserDTO LoginUser(UserDTO loginUserDTO)
        {

            UserDTO user =  _userRepository.getUserByEmailID(loginUserDTO.EmailID);

            if(user == null)
            {
                throw new Exception("EmailID not registed.");
            }
            else if (user.Password != loginUserDTO.Password)
            {
                throw new Exception("Incorrect Password");
            }

            return user;
        }

        public UserDTO RegisterUser(UserDTO userDTO, string imageURL)
        {
            return _userRepository.SaveUser(userDTO, imageURL);
        }

        public UserDTO getUserByToken(string nameFilter)
        {
            return _userRepository.getUserByToken(nameFilter);
        }
    }
}
