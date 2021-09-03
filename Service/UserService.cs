using Core.Utils;
using DataModel.DTO;
using DataModel.Models;
using Interface.Service;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(MyFirstAngularContext db) : base(db)
        {
        }

        public SysUserDTO SelectUserByUsernamePassword(string username, string password)
        {
            SysUserDTO result;
            try
            {
                var query = dbContext.Sysusers.LeftJoin(dbContext.Sysroles,
                        u => u.RoleId,
                        r => r.RoleId,
                        (u, r) => new { u, r })
                    .Where(w => w.u.IsDelete == false
                                && w.u.Username == username && w.u.Password == password)
                    .Select(s => new
                    {
                        s.u.UserId,
                        s.u.FullName,
                        s.u.Username,
                        s.u.Password,
                        s.r.RoleCode,
                        s.r.RoleName,
                        s.r.RoleId,
                        s.u.Contact,
                        s.u.Email,
                        s.u.IsDelete,
                        s.u.CreatedDate,
                        s.u.CreatedUser,
                        s.u.UpdatedDate,
                        s.u.UpdatedUser
                    });

                var obj = query.SingleOrDefault();
                if (obj == null) return null;
                result = new SysUserDTO()
                {
                    UserId = obj.UserId,
                    FullName = obj.FullName,
                    Username = obj.Username,
                    RoleName = obj.RoleName,
                    RoleId = obj.RoleId,
                    Contact = obj.Contact,
                    Email = obj.Email,
                    CreatedDate = obj.CreatedDate,
                    CreatedUser = obj.CreatedUser
                };
            }
            catch (Exception ex)
            {
                LogUtilities.LogErrorMessage(ex.Message, ex);
                throw;
            }

            return result;
        }

        public Sysuser SelectUserByUsername(string username)
        {
            return dbContext.Sysusers.FirstOrDefault(w => w.IsDelete == false && w.Username == username);
        }

        public int CountUserByUsername(string username)
        {
            var count = 0;
            count += dbContext.Sysusers.Count(w => w.Username == username && w.IsDelete == false);
            return count;
        }

        public int InsertNewUser(Sysuser newUser)
        {
            var count = 0;
            using (var context = dbContext)
            {
                try
                {
                    var checkUser = SelectUserById(newUser.UserId) ?? new Sysuser
                    {
                        UserId = Guid.NewGuid().ToString(),
                        Password = newUser.Password,
                        CreatedUser = newUser.CreatedUser,
                        CreatedDate = newUser.CreatedDate
                    };

                    checkUser.FullName = newUser.FullName;
                    checkUser.Username = newUser.Username;
                    checkUser.RoleId = newUser.RoleId;
                    checkUser.Contact = newUser.Contact;
                    checkUser.Email = newUser.Email;
                    checkUser.IsDelete = false;
                    //Save User Table
                    dbContext.Sysusers.Add(checkUser);
                    count += dbContext.SaveChanges();
                    if (count == 1)
                        LogUtilities.LogInfoMessage("INSERT User Success in DB...",
                            ConstantValues.GetCurrentMethod());
                }
                catch (Exception e)
                {
                    LogUtilities.LogErrorMessage(e.Message, e);
                    throw;
                }
            }
            return count;
        }

        public int UpdateUser(Sysuser updateUser)
        {
            var count = 0;
            using (var context = dbContext)
            {
                try
                {
                    var checkUser = SelectUserById(updateUser.UserId);
                    if (checkUser != null)
                    {
                        checkUser.UpdatedUser = updateUser.UpdatedUser;
                        checkUser.UpdatedDate = ConstantValues.GetDateTime(ConstantValues.SG_Timezone);
                        checkUser.FullName = updateUser.FullName;
                        checkUser.Username = updateUser.Username;
                        checkUser.RoleId = updateUser.RoleId;
                        checkUser.Contact = updateUser.Contact;
                        checkUser.Email = updateUser.Email;
                        checkUser.IsDelete = false;
                    }
                    //Update User Table
                    count += dbContext.SaveChanges();
                    if (count == 1)
                        LogUtilities.LogInfoMessage("UPDATE User Success in DB...",
                            ConstantValues.GetCurrentMethod());
                }
                catch (Exception e)
                {
                    LogUtilities.LogErrorMessage(e.Message, e);
                    throw;
                }
            }
            return count;
        }

        public int DeleteUser(string deleteUserId, string updatedUser)
        {
            var i = new int();

            try
            {
                var deleteUser =
                    dbContext.Sysusers.FirstOrDefault(w => w.UserId == deleteUserId && w.IsDelete == false);
                if (deleteUser == null) return 0;
                deleteUser.IsDelete = true;
                deleteUser.UpdatedDate = ConstantValues.GetDateTime(ConstantValues.SG_Timezone);
                deleteUser.UpdatedUser = updatedUser;
                i += dbContext.SaveChanges();
                if (i == 1)
                    LogUtilities.LogInfoMessage("DELETE User Success in DB...", ConstantValues.GetCurrentMethod());
            }
            catch (Exception e)
            {
                LogUtilities.LogErrorMessage(e.Message, e);
            }

            return i;
        }

        public Sysuser SelectUserById(string id)
        {
            return dbContext.Sysusers.FirstOrDefault(w => w.IsDelete == false && w.UserId == id);
        }

        public string InsertSysLoginLog(string userId, Syslogin loginLog)
        {
            var count = 0;
            var loginId = string.Empty;
            using (var context = dbContext)
            {
                try
                {
                    var newLogin = new Syslogin
                    {
                        Id = Guid.NewGuid().ToString(),
                        Username = loginLog.Username,
                        IsLogin = loginLog.IsLogin,
                        IsDelete = false,
                        LoginTime = loginLog.LoginTime,
                        UserId = userId,
                        CreatedUser = userId,
                        CreatedDate = ConstantValues.GetDateTime(ConstantValues.SG_Timezone)
                    };

                    dbContext.Syslogins.Add(newLogin);
                    count += dbContext.SaveChanges();
                    loginId = newLogin.Id;
                }
                catch (Exception e)
                {
                    LogUtilities.LogErrorMessage(e.Message, e);
                    throw;
                }
                return loginId;
            }
        }

        public int ChangePassword(Sysuser cUser)
        {
            var count = 0;
            using (var context = dbContext)
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        var updateUser = dbContext.Sysusers.Find(cUser.UserId);
                        if (updateUser != null)
                        {
                            updateUser.Password = cUser.Password;
                            updateUser.UpdatedDate = cUser.UpdatedDate;
                            updateUser.UpdatedUser = cUser.UpdatedUser;
                            count += dbContext.SaveChanges();
                        }

                        scope.Complete();
                    }
                    catch (Exception e)
                    {
                        LogUtilities.LogErrorMessage(e.Message, e);
                        throw;
                    }
                }
            }

            return count;
        }

        public int UpdateSysLogin(string loginId, string updatedUser)
        {
            var count = 0;
            using (var context = dbContext)
            {
                try
                {
                    var login = GetSysLogin(loginId) ?? throw new ArgumentNullException(nameof(loginId));
                    login.UpdatedUser = updatedUser;
                    login.UpdatedDate = ConstantValues.GetDateTime(ConstantValues.SG_Timezone);
                    login.IsLogin = false;
                    login.LogoutTime = ConstantValues.GetDateTime(ConstantValues.SG_Timezone);
                    count += dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    LogUtilities.LogErrorMessage(e.Message, e);
                    throw;
                }
            }

            return count;
        }

        public Syslogin GetSysLogin(string loginId)
        {
            return dbContext.Syslogins
                .FirstOrDefault(w => w.IsDelete == false && w.IsLogin && w.Id == loginId);
        }

        public IEnumerable<object> SelectUserList(int startIndex, int pageSize, DTOrder[] orders, DTColumn[] columns,
            DTSearch searchExp)
        {
            var query = QueryableExtensions.LeftJoin(dbContext.Sysusers, dbContext.Sysroles,
                u => u.RoleId,
                r => r.RoleId,
                (u, r) => new { u, r }).Where(w => w.u.IsDelete == false).Select(s => new
                {
                    s.u.UserId,
                    s.u.FullName,
                    s.u.Username,
                    s.u.Password,
                    s.r.RoleCode,
                    s.r.RoleName,
                    s.r.RoleId,
                    s.u.Contact,
                    s.u.Email
                });

            if (!string.IsNullOrEmpty(searchExp.Value))
                query = query.Where(w => w.FullName.ToLower().Contains(searchExp.Value) ||
                                         w.Username.ToLower().Contains(searchExp.Value) ||
                                         w.RoleName.ToLower().Contains(searchExp.Value));

            var totalCount = query.Count();

            for (var i = 0; i < orders.Count(); i++)
                query = query.OrderBy(columns[orders[i].Column].Data + " " + orders[i].Dir);

            var list = query.Skip(pageSize * (startIndex - 1))
                .Take(pageSize)
                .ToList();

            IEnumerable<object> resultList = list.Select(s => new
            {
                s.UserId,
                s.FullName,
                s.Username,
                s.Password,
                s.RoleCode,
                s.RoleName,
                s.RoleId,
                s.Contact,
                s.Email,
                TotalCount = totalCount
            }).ToList();
            return resultList;
        }

        public List<Sysrole> SelectUserRoles()
        {
            return dbContext.Sysroles.Where(w => w.IsDelete == false).ToList();
        }

        #region Find Deleted users

        public Sysuser SelectDeletedUser(string username)
        {
            return dbContext.Sysusers.FirstOrDefault(w => w.Username.Equals(username) && w.IsDelete);
        }

        #endregion

        #region Register Deleted users

        public string UpdateDeletedUser(Sysuser userDM, string status)
        {
            var count = 0;
            using (var context = dbContext)
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var selectedUser = dbContext.Sysusers.FirstOrDefault(w => w.UserId == userDM.UserId);
                        if (selectedUser != null)
                        {
                            if (status == "Register")
                            {
                                selectedUser.CreatedUser = userDM.CreatedUser;
                                selectedUser.CreatedDate = userDM.CreatedDate;
                            }
                            else
                            {
                                userDM.UpdatedDate = ConstantValues.GetDateTime(ConstantValues.SG_Timezone);
                                userDM.UpdatedUser = userDM.UpdatedUser;
                            }
                            selectedUser.FullName = userDM.FullName;
                            selectedUser.Username = userDM.Username;
                            selectedUser.Password = userDM.Password;
                            selectedUser.RoleId = userDM.RoleId;
                            selectedUser.Contact = userDM.Contact;
                            selectedUser.Email = userDM.Email;
                            selectedUser.IsDelete = false;
                        }
                        count += dbContext.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        LogUtilities.LogErrorMessage(e.Message, e);
                    }
                }
            }
            return count > 0 ? "success" : "fail";
        }

        #endregion

        public List<Sysuser> GetUserList()
        {
            return dbContext.Sysusers.Where(w => w.IsDelete == false).ToList();
        }

        public SysUserDTO SelectUserDtoById(string id)
        {
            var query = QueryableExtensions.LeftJoin(dbContext.Sysusers, dbContext.Sysroles,
                u => u.RoleId,
                r => r.RoleId,
                (u, r) => new { u, r })
                .Where(w => w.u.IsDelete == false && w.u.UserId == id)
                .Select(s => new
                {
                    s.u.UserId,
                    s.u.FullName,
                    s.u.Username,
                    s.u.Password,
                    s.r.RoleCode,
                    s.r.RoleName,
                    s.r.RoleId,
                    s.u.Contact,
                    s.u.Email
                });
            var obj = query.Single();
            SysUserDTO userDto = new SysUserDTO()
            {
                UserId = obj.UserId,
                FullName = obj.FullName,
                Username = obj.Username,
                Password = obj.Password,
                RoleName = obj.RoleName,
                RoleId = obj.RoleId,
                Contact = obj.Contact,
                Email = obj.Email
            };
            return userDto;
        }
    }
}