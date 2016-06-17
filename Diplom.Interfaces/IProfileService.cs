using Diplom.Domain;
using Diplom.ViewModels.SocialSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Interfaces
{
    public interface IProfileService
    {
        void SendFriendRequest(string from,string to);
        List<User> GetFriends(string id);
        void DeclineFriendRequest(string from, string to);
        User GetUser(string username);
        void EditProfile(IndexProfileViewModel model, string userName);
        void UpdatePhoto(Profile profile);
        bool AddToFriends(string userName, string id);
        bool DeleteFromFriends(string userName, string id);
        bool CheckSubscription(string userName, string id);
        List<User> GetSubscriptions(string userName);
        List<User> GetSubscribers(string userName);
        void EditTeacherProfile(IndexProfileViewModel model, string userName);
        void FillSearch(string searchQuery, SearchProfileViewModel view);
    }
}
