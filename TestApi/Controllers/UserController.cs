using Core.Utils;
using DataModel.DTO;
using DataModel.Models;
using Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Property

        private IUserService _userService { get; }

        #endregion

        #region Constructor

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        #endregion

        #region Count User For Register

        [Route("CountUserForRegister/{userName}")]
        [HttpGet]
        public int CountUserForRegister(string userName)
        {
            return _userService.CountUserByUsername(userName);
        }

        #endregion

        #region Find Deleted User

        [Route("FindDeletedUser/{userName}")]
        [HttpGet]
        public Sysuser FindDeletedUser(string userName)
        {
            return _userService.SelectDeletedUser(userName);
        }

        #endregion

        #region Update Deleted User

        [HttpPost("UpdateDeletedUser")]
        public string UpdateDeletedUser(JObject jsonData)
        {
            dynamic json = jsonData;
            JObject jUserData = json.UserData;
            string jStatus = json.status;

            return _userService.UpdateDeletedUser(jUserData.ToObject<Sysuser>(), jStatus);
        }

        #endregion

        #region Register New User

        [HttpPost("RegisterUser")]
        public int RegisterUser(JObject jsonData)
        {
            dynamic json = jsonData;
            JObject jUserData = json.UserData;

            return _userService.InsertNewUser(jUserData.ToObject<Sysuser>());
        }

        #endregion

        #region Get User List
        [HttpPost("GetUserList")]
        public IEnumerable<object> GetUserList(JObject jsonData)
        {
            dynamic json = jsonData;
            int startIndex = json.StartIndex;
            int pageSize = json.PageSize;

            JArray jorders = json.Orders;
            var orders = jorders.ToObject<DTOrder[]>();

            JArray jcolumns = json.Columns;
            var columns = jcolumns.ToObject<DTColumn[]>();

            JObject jsearch = json.Search;
            var search = jsearch.ToObject<DTSearch>();

            return _userService.SelectUserList(startIndex, pageSize, orders, columns, search);
        }

        #endregion

        #region Delete User

        [Route("DeleteUser/{deleteUserId}/{updatedUserId}")]
        [HttpGet]
        public int DeleteUser(string deleteUserId, string updatedUserId)
        {
            return _userService.DeleteUser(deleteUserId, updatedUserId);
        }

        #endregion

        #region Get User By Id

        [HttpGet("GetUserById/{Id}")]
        public SysUserDTO GetUserById(string Id)
        {
            return _userService.SelectUserDtoById(Id);
        }

        #endregion

        #region Get Username and Password

        [HttpPost("Login")]
        public SysUserDTO Login([FromBody]LoginData login)
        {
            login.password = StringCrypto.Encrypt(login.password, StringCrypto.encryptKey);
            return _userService.SelectUserByUsernamePassword(login.username, login.password);
        }

        #endregion

        #region Get User by Username

        [Route("getUserByUsername/{username}")]
        [HttpGet]
        public Sysuser GetUserByUsername(string username)
        {
            return _userService.SelectUserByUsername(username);
        }

        #endregion

        #region Change Password

        [HttpPost("ChangePassword")]
        public int ChangePassword(JObject jsonData)
        {
            dynamic json = jsonData;
            JObject jUserData = json.UserData;
            return _userService.ChangePassword(jUserData.ToObject<Sysuser>());
        }

        #endregion

        #region Add SysLogin Log

        //[HttpPost("AddSysLogin")]
        //public string AddSysLogin(JObject jsonData)
        //{
        //    dynamic json = jsonData;
        //    JObject jLoginData = json.SysLogin;
        //    string userId = json.LoginId;
        //    return _userService.InsertSysLoginLog(userId, jLoginData.ToObject<Syslogin>());
        //}

        #endregion

        #region Get All User Roles

        [HttpGet("GetUserRoleList")]
        public List<Sysrole> GetUserRoleList()
        {
            return _userService.SelectUserRoles();
        }

        #endregion

        #region Logout

        //[HttpPost("UpdateSysLogin")]
        //public int UpdateSysLogin(JObject jsonData)
        //{
        //    dynamic json = jsonData;
        //    string loginId = json.loginId;
        //    string updateUser = json.updatedUser;
        //    return _userService.UpdateSysLogin(loginId, updateUser);
        //}

        #endregion

        #region Api Test
        [HttpGet]
        [Route("GetUsers")]
        public List<Sysuser> GetUsers()
        {
            return _userService.GetUserList();
        }
        #endregion
    }

    public class LoginData
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}