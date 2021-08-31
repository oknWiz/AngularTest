using Core.Utils;
using DataModel.DTO;
using DataModel.Models;
using System;
using System.Collections.Generic;

namespace Interface.Service
{
    public interface IUserService : IDisposable
    {
        SysUserDTO SelectUserByUsernamePassword(string username, string password);
        Sysuser SelectUserByUsername(string username);
        int CountUserByUsername(string username);
        int InsertNewUser(Sysuser newUser);
        int UpdateUser(Sysuser updateUser);
        string UpdateDeletedUser(Sysuser userDM, string status);
        int DeleteUser(string deleteUserId, string updatedUser);
        int ChangePassword(Sysuser cUser);
        IEnumerable<Object> SelectUserList(int startIndex, int pageSize, DTOrder[] orders, DTColumn[] columns,
            DTSearch searchExp);
        Sysuser SelectUserById(string id);
        SysUserDTO SelectUserDtoById(string id);
        Sysuser SelectDeletedUser(string username);
        List<Sysrole> SelectUserRoles();
        List<Sysuser> GetUserList();
        #region sysLogin
        string InsertSysLoginLog(string userId, Syslogin userLog);
        int UpdateSysLogin(string loginId, string updatedUser);
        Syslogin GetSysLogin(string loginId);
        #endregion

    }
}
