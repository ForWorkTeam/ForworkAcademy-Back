using ForworkAcademy.Data;
using ForworkAcademy.Interfaces;
using ForworkAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace ForworkAcademy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ForworkDbContext _dbContext;

        public UserRepository(ForworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> SendUserPopup(UserPopup userPopup)
        {
            var find = await _dbContext.UserPopups.FirstOrDefaultAsync(x=> x.Email == userPopup.Email);

            if (find != null)
            {
                var newPopup = new UserPopup()
                {
                    Name = userPopup.Name,
                    LastName = userPopup.LastName,
                    Email = userPopup.Email,
                    PhoneNumber = userPopup.PhoneNumber,
                    BornDate = userPopup.BornDate,
                    Direction = userPopup.Direction,
                };
               await _dbContext.UserPopups.AddAsync(newPopup);
               await _dbContext.SaveChangesAsync();
                return newPopup.Id;
            }
            else
            {
                return -1;
            }
        }
    }
}
