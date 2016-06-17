using Diplom.BusinessLogic;
using Diplom.DataAccess;
using Diplom.DataAccess.Repositories;
using Diplom.Domain;
using Diplom.Interfaces;
using Diplom.ViewModels.SocialSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public class ProfileService : IProfileService
    {
        private UserRepository UserRepository;
        private ProfileRepository ProfileRepository;
        private TeacherRepository TeacherRepository;
        private FriendRequestRepository FriendRequestRepository;
        private ApplicationContext Context;
        private List<FriendRequest> _Connections;
        private List<FriendRequest> Connections(string username)
        {
            if (_Connections == null)
            {
                _Connections = FriendRequestRepository.Get(includeProperties: "From,To").Where(x => x.From.UserName == username || x.To.UserName == username).ToList();
            }
            return _Connections;
        }
        public ProfileService(IApplicationContext context)
        {
            UserRepository = new UserRepository(context);
            ProfileRepository = new ProfileRepository(context);
            FriendRequestRepository = new FriendRequestRepository(context);
            TeacherRepository = new TeacherRepository(context);
            Context = (ApplicationContext)context;
        }
        public bool CheckSubscription(string userName, string id)
        {
            return FriendRequestRepository.Get(includeProperties: "From,To")
                .Where(r => (r.From.UserName == userName && r.FromResult == FriendRequestResult.Confirmed && r.ToResult == FriendRequestResult.Ignore && r.To.Id == id) ||
                (r.To.UserName == userName && r.ToResult == FriendRequestResult.Confirmed && r.FromResult == FriendRequestResult.Ignore && r.From.Id == id)).Any();
        }
        public List<User> GetFriends(string userName)
        {
            return Connections(userName)
                .Where(r => (r.From.UserName == userName || r.To.UserName == userName) && (r.FromResult == FriendRequestResult.Confirmed && r.ToResult == FriendRequestResult.Confirmed))
                .Select(r =>
                {
                    if (r.From.UserName == userName)
                    {
                        return r.To;
                    }
                    else
                    {
                        return r.From;
                    }

                }).OrderByDescending(r => r.CreationDate).ToList();
        }
        public List<User> GetSubscriptions(string userName)
        {
            return Connections(userName)
                .Where(r => (r.From.UserName == userName && r.FromResult == FriendRequestResult.Confirmed && r.ToResult == FriendRequestResult.Ignore) ||
                (r.To.UserName == userName && r.ToResult == FriendRequestResult.Confirmed && r.FromResult == FriendRequestResult.Ignore))
                .Select(r =>
                {
                    if (r.From.UserName == userName)
                    {
                        return r.To;
                    }
                    else
                    {
                        return r.From;
                    }

                }).OrderByDescending(r => r.CreationDate).ToList();
        }
        public List<User> GetSubscribers(string userName)
        {
            return Connections(userName)
                .Where(r => (r.From.UserName == userName && r.FromResult == FriendRequestResult.Ignore && r.ToResult == FriendRequestResult.Confirmed) ||
                (r.To.UserName == userName && r.ToResult == FriendRequestResult.Ignore && r.FromResult == FriendRequestResult.Confirmed))
                .Select(r =>
                {
                    if (r.From.UserName == userName)
                    {
                        return r.To;
                    }
                    else
                    {
                        return r.From;
                    }

                }).OrderByDescending(r => r.CreationDate).ToList();
        }
        public bool AddToFriends(string userName, string id)
        {
            var request = FriendRequestRepository.Get(includeProperties: "From,To")
                .FirstOrDefault(r => (r.From.UserName == userName && r.To.Id == id) || (r.From.Id == id && r.To.UserName == userName));
            if (request == null)
            {
                var from = UserRepository.Get().FirstOrDefault(u => u.UserName == userName);
                var to = UserRepository.GetByID(id);
                FriendRequestRepository.Insert(new FriendRequest()
                {
                    From = from,
                    To = to,
                    FromResult = FriendRequestResult.Confirmed,
                    ToResult = FriendRequestResult.Ignore
                });
                FriendRequestRepository.Save();
                return true;
            }

            if (request.From.UserName == userName && request.FromResult == FriendRequestResult.Ignore)
            {
                request.FromResult = FriendRequestResult.Confirmed;
                FriendRequestRepository.Update(request);
                FriendRequestRepository.Save();
                return true;
            }
            if (request.To.UserName == userName && request.ToResult == FriendRequestResult.Ignore)
            {
                request.ToResult = FriendRequestResult.Confirmed;
                FriendRequestRepository.Update(request);
                FriendRequestRepository.Save();
                return true;
            }
            return false;
        }
        public bool DeleteFromFriends(string userName, string id)
        {
            var request = FriendRequestRepository.Get(includeProperties: "From,To")
               .FirstOrDefault(r => (r.From.UserName == userName && r.To.Id == id) || (r.From.Id == id && r.To.UserName == userName));
            if (request == null)
            {
                return false;
            }

            if (request.From.UserName == userName && request.FromResult == FriendRequestResult.Confirmed)
            {
                request.FromResult = FriendRequestResult.Ignore;
                FriendRequestRepository.Update(request);
                FriendRequestRepository.Save();
                return true;
            }
            if (request.To.UserName == userName && request.ToResult == FriendRequestResult.Confirmed)
            {
                request.ToResult = FriendRequestResult.Ignore;
                FriendRequestRepository.Update(request);
                FriendRequestRepository.Save();
                return true;
            }
            return false;
        }
        public void SendFriendRequest(string from, string to)
        {
            var friendRequest = FriendRequestRepository.Get(r => (r.From.Id == from || r.To.Id == from) && (r.From.Id == to || r.To.Id == to), includeProperties: "From,To").FirstOrDefault();
            if (friendRequest == null)
            {
                friendRequest = new FriendRequest();
                friendRequest.From = UserRepository.GetByID(from);
                friendRequest.To = UserRepository.GetByID(to);
                friendRequest.FromResult = FriendRequestResult.Confirmed;
                friendRequest.ToResult = FriendRequestResult.Ignore;
                FriendRequestRepository.Insert(friendRequest);
                FriendRequestRepository.Save();
                return;
            }
            if (from == friendRequest.From.Id)
            {
                friendRequest.FromResult = FriendRequestResult.Confirmed;
                FriendRequestRepository.Update(friendRequest);
                FriendRequestRepository.Save();
                return;
            }
            friendRequest.ToResult = FriendRequestResult.Confirmed;
            FriendRequestRepository.Update(friendRequest);
            FriendRequestRepository.Save();
        }
        public void DeclineFriendRequest(string from, string to)
        {
            var friendRequest = FriendRequestRepository.Get(r => (r.From.Id == from || r.To.Id == from) && (r.From.Id == to || r.To.Id == to), includeProperties: "From,To").FirstOrDefault();
            if (friendRequest == null)
            {
                return;
            }
            if (from == friendRequest.From.Id)
            {
                friendRequest.FromResult = FriendRequestResult.Ignore;
                FriendRequestRepository.Update(friendRequest);
                FriendRequestRepository.Save();
                return;
            }
            friendRequest.ToResult = FriendRequestResult.Ignore;
            FriendRequestRepository.Update(friendRequest);
            FriendRequestRepository.Save();
        }
        public User GetUser(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                return null;
            }
            return UserRepository.Get(includeProperties: "Profile,Role,Teacher,Role.Permission").FirstOrDefault(u => u.UserName == username);
        }
        public void EditProfile(IndexProfileViewModel model, string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return;
            }
            var user = UserRepository.Get(u => u.UserName == userName, includeProperties: "Profile,Teacher").FirstOrDefault();
            if (user == null)
            {
                return;
            }
            user.Profile.BirthDate = DateTimeProvider.ParseDate(model.BirthDate);
            user.Profile.Sex = model.Sex;
            user.Profile.Skype = model.Skype;
            user.Profile.Phone = model.Phone;
            ProfileRepository.Update(user.Profile);
            ProfileRepository.Save();
        }
        public void EditTeacherProfile(IndexProfileViewModel model, string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return;
            }
            var user = UserRepository.Get(u => u.UserName == userName, includeProperties: "Teacher").FirstOrDefault();
            if (user == null)
            {
                return;
            }
            user.Teacher.Post = model.User.Teacher.Post;
            user.Teacher.InfoScientificTitle = model.User.Teacher.InfoScientificTitle;
            user.Teacher.ScientificInterests = model.User.Teacher.ScientificInterests;
            user.Teacher.ScientificTitle = model.User.Teacher.ScientificTitle;
            user.Teacher.AcademicDegree = model.User.Teacher.AcademicDegree;
            user.Teacher.Contacts = model.User.Teacher.Contacts;
            user.Teacher.Discipline = model.User.Teacher.Discipline;
            TeacherRepository.Update(user.Teacher);
            TeacherRepository.Save();
        }
        public void UpdatePhoto(Profile profile)
        {
            ProfileRepository.Update(profile);
            ProfileRepository.Save();
        }
        public void FillSearch(string searchQuery, SearchProfileViewModel view)
        {
            searchQuery = searchQuery.ToLower();
            if (String.IsNullOrEmpty(searchQuery))
            {
                view.Users = UserRepository.Get(x=>x.isActive,includeProperties: "Profile").Select(x => new SearchProfileItemViewMOdel()
                {
                    FullName = x.Profile.FullName,
                    Photo_200 = x.Profile.Photo_200,
                    Photo_Max = x.Profile.Photo_max,
                    Username = x.UserName
                }).ToList();
                return;
            }
            view.Users = UserRepository.Get(x=>x.isActive,includeProperties: "Profile")
                .Where(x=>x.UserName.ToLower()==searchQuery || x.Profile.FullName.ToLower()==searchQuery)
                .Select(x => new SearchProfileItemViewMOdel()
                {
                    FullName = x.Profile.FullName,
                    Photo_200 = x.Profile.Photo_200,
                    Photo_Max = x.Profile.Photo_max,
                    Username = x.UserName
                }).ToList();
        }
    }
}
