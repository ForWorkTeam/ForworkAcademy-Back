using ForworkAcademy.Models;

namespace ForworkAcademy.Interfaces
{
    public interface IUserRepository
    {
        Task<int> SendUserPopup(UserPopup userPopup);
    }
}
