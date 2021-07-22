using NewsTrackingSite.Models;
using NewsTrackingSite.Models.HelperModels;
using NewsTrackingSite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Repositories
{
    public class MembershipRepository
    {
        private UserRepository userRepo;
        public MembershipRepository()
        {
            userRepo = new UserRepository();
        }

        public NResult<int> Register(User user, MemberType role = MemberType.User)
        {
            // Bu kullanıcı email ile sisteme kayıt olmuş mu bakılır.
            var userControl = userRepo.GetUser(user.EMail);
            if (userControl.Data != null)
                return new NResult<int>() { IsSuccessful = false, Message = "Bu kişi sisteme kayıtlıdır." };

            if (userRepo.SaveUser(user).IsSuccessful)
            {
                return new NResult<int>() { IsSuccessful = true, Message = "Sisteme başarıyla kayıt oldunuz.", Data = userRepo.GetUser(user.EMail, user.Password).Data.UserID };
            }
            else
            {
                return new NResult<int>() { IsSuccessful = false, Message = "Sisteme kayıt olunurken hata ile karşılaşıldı." };
            }
        }

        public NResult<NSession> Login(NLoginModel user)
        {
            NSession session = new NSession();
            var member = userRepo.GetUser(user.EMail, user.Password);

            if (!member.IsSuccessful)
                return new NResult<NSession>() { IsSuccessful = false, Message = "Kullanıcı adı veya şifrenizi kontrol ediniz!" };

            session.ID = member.Data.UserID;

            return new NResult<NSession>() { IsSuccessful = true, Message = "Sisteme başarıyla giriş yapıldı.", Data = session };
        }
    }
}