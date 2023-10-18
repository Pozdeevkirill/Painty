using Painty.BAL.ModelsDTO;
using Painty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painty.BAL.MapperProfiles
{
    public class UserMapper
    {
        #region User => UserDTO

        public UserDTO Map(User user)
        {
            UserDTO _user = new()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
            };

            if(user.Friends != null)
            {
                List<UserDTO> friends = new();
                foreach (var item in user.Friends)
                {
                    friends.Add(new()
                    {
                        Id = item.FriendId,
                        Login = item.FriendName,
                    });
                }

                _user.Friends = friends;
            }

            if(user.Requests != null)
            {
                List<UserDTO> requests = new();
                foreach (var item in user.Requests)
                {
                    requests.Add(new()
                    {
                        Id = item.SenderId,
                        Login = item.UserName,
                    });
                }

                _user.FriendsRequest = requests;
            }

            if (user.Images != null)
            {
                List<ImageDTO> imgs = new();
                foreach (var item in user.Images)
                {
                    imgs.Add(new()
                    {
                        Id = item.Id,
                        Path = item.Path,
                        UserId = item.UserId,
                    });
                }

                _user.Images = imgs;
            }

            return _user;
        }

        public List<UserDTO> Map(List<User> users)
        {
            List<UserDTO> mapped = new();

            foreach (var item in users)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }

        #endregion

        #region UserDTO => User

        public User Map(UserDTO user)
        {
            User _user = new()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
            };

            if(user.Friends != null)
            {
                List<Friend> friends = new();
                foreach (var item in user.Friends)
                {
                    friends.Add(new()
                    {
                        FriendId = item.Id,
                        FriendName = item.Login,
                    });
                }
                _user.Friends = friends;
            }

            if(user.FriendsRequest != null)
            {
                List<Request> requests = new();
                foreach (var item in user.FriendsRequest)
                {
                    requests.Add(new()
                    {
                        SenderId = item.Id,
                        UserName = item.Login,
                    });
                }
                _user.Requests = requests;
            }

            if (user.Images != null)
            {
                List<Image> imgs = new();
                foreach (var item in user.Images)
                {
                    imgs.Add(new()
                    {
                        Id = item.Id,
                        Path = item.Path,
                        UserId = item.UserId,
                    });
                }
                _user.Images = imgs;
            }

            return _user;
        }

        public List<User> Map(List<UserDTO> users)
        {
            List<User> mapped = new();
            foreach (var item in users)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }

        #endregion
    }
}
