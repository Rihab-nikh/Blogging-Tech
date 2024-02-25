using DAL.Entity;
using DAL.Repos;
using Models;
using System.Collections.Generic;
using System.Linq;

public class UserService
{
    private readonly UserRepo _userRepo;

    public UserService(UserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public UserVM CreateUser(UserVM userVM)
    {
        User xdb = new User(userVM.Username,userVM.Email, userVM.Password,userVM.ProfileImage);
        var createdUser = _userRepo.Create(xdb);
        UserVM xVM = new UserVM(createdUser.UserId,createdUser.Username,createdUser.Email,createdUser.Password,createdUser.ProfileImage);
        return xVM;
    }

    public IEnumerable<UserVM> GetAllUsers()
    {
        return _userRepo.GetAll().Select(user => new UserVM(
            user.UserId,
            user.Username,
            user.Email,
            user.Password,
            user.ProfileImage
        ));
    }


    public UserVM GetUserById(int userId)
    {
        var user = _userRepo.GetById(userId);
        UserVM xVM = new UserVM(user.UserId,user.Username,user.Email,user.Password,user.ProfileImage);
        return xVM;
    }


    public void UpdateUser(UserVM userVM)
    {
        User userEntity = new User(userVM.Username,userVM.Email,userVM.Password,userVM.ProfileImage);
        _userRepo.Update(userEntity);
    }
    public void UpdatePassword(int userId, string newPassword)
    {
        User user = _userRepo.GetById(userId);
        if (user != null)
        {user.Password = newPassword;}
        _userRepo.Update(user);
    }
    public void DeleteUser(int userId)
    {
        _userRepo.Delete(userId);
    }

    public void ChangeEmail(int userId, string newEmail)
    {
        User user = _userRepo.GetById(userId);
        if (user != null)
        { user.Email = newEmail; }
        _userRepo.Update(user);
        
    }
}
