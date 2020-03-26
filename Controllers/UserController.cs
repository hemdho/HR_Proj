using HR.CommonUtility;
using HR.WebApi.Interfaces;
using HR.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ActionFilters.Log]

    public class UserController : ControllerBase
    {
        public IUserService<User> userRepository { get; set; }
        public IUser_PasswordReset user_PasswordResetRepository { get; set; }

        public UserController(IUserService<User> commonRepository, IUser_PasswordReset user_PasswordResetRepository)
        {
            userRepository = commonRepository;
            this.user_PasswordResetRepository = user_PasswordResetRepository;
        }

        //Get All User Data
        [HttpGet]
        [HttpGet("{recordLimit}")]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> GetAll(int RecordLimit)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                var vList = userRepository.GetAll(RecordLimit);
                if (vList == null)
                {
                    objResHelper.Status = StatusCodes.Status404NotFound;
                    objResHelper.Message = "Get Empty Data";
                }
                else
                {
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Get Successfully";
                    objResHelper.Data = vList;
                }
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> Get(int id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                var vList = userRepository.GetUserById(id);
                if (vList == null)
                {
                    objResHelper.Status = StatusCodes.Status404NotFound;
                    objResHelper.Message = "Get Empty Data";
                }
                else
                {
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Get Successfully";
                    objResHelper.Data = vList;
                }
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        // GET: api/User/
        [HttpGet("{pageIndex},{pageSize},{searchValue}")]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> FindPaginated(int pageIndex, int pageSize, string searchValue)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                var vList = userRepository.FindPaginated(pageIndex, pageSize, searchValue);
                if (vList == null)
                {
                    objResHelper.Status = StatusCodes.Status404NotFound;
                    objResHelper.Message = "Get Empty Data";
                }
                else
                {
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Get Successfully";
                    objResHelper.Data = vList;
                }
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //New User Creation
        // POST: api/User
        [ActionFilters.AuditLog]
        [HttpPost]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> Add(User user)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objResHelper.Status = StatusCodes.Status424FailedDependency;
                objResHelper.Message = "Invalid Model State";
                return BadRequest(objResHelper);
            }

            try
            {
                if (userRepository.UserExist(user))
                {
                    objResHelper.Status = StatusCodes.Status208AlreadyReported;
                    objResHelper.Message = "Data already available";
                    return Ok(objResHelper);
                }

                userRepository.UserCreate(user);
                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";

                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Edit User
        // PUT: api/User/5
        [ActionFilters.AuditLog]
        [HttpPut]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> Edit(User user)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objResHelper.Status = StatusCodes.Status424FailedDependency; ;
                objResHelper.Message = "Invalid Model State";
                return BadRequest(objResHelper);
            }

            try
            {
                if (userRepository.UserExist(user))
                {
                    objResHelper.Status = StatusCodes.Status208AlreadyReported;
                    objResHelper.Message = "Data already available";
                    return Ok(objResHelper);
                }

                userRepository.UserUpdate(user);
                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";

                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //User StatusChange - active/inactive
        [ActionFilters.AuditLog]
        [HttpPut("{id},{isActive}")]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> StatusChange(int id, short isActive)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                if (isActive == 1)
                    userRepository.ActivateUser(id);
                else
                    userRepository.InactivateUser(id);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Inactivate User
        // DELETE: api/User/5
        [ActionFilters.AuditLog]
        [HttpDelete("{id}")]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                userRepository.InactivateUser(id);
                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //User Password change
        [ActionFilters.AuditLog]
        [HttpPut("{user_Id},{oldPassword},{newPassword}")]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> ChangePassword(int user_Id, string oldPassword, string newPassword)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                string strMessage = userRepository.ChangePassword(user_Id, oldPassword, newPassword);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = strMessage;

                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Admin forcefully reset password with custom password
        [ActionFilters.AuditLog]
        [HttpPost("{login_Id},{password}")]
        [ActionFilters.TokenVerify]
        public async Task<IActionResult> AdminChangePassword(string login_Id, string password)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                userRepository.AdminChangePassword(login_Id, password);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        //Forgot Password
        [ActionFilters.AuditLog]
        [HttpPost("{login_Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string login_Id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                user_PasswordResetRepository.ForgotPassword(login_Id);
                //userRepository.ForgotPassword(login_Id);

                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Saved Successfully";
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Get Unsuccessful";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionFilters.AuditLog]
        public async Task<IActionResult> SignIn(User_Request user_Request)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            if (!ModelState.IsValid)
            {
                objResHelper.Status = StatusCodes.Status424FailedDependency;
                objResHelper.Message = "User Name / Password Not Available";
                return BadRequest(objResHelper);
            }

            try
            {
                var vList = userRepository.SignIn(user_Request);
                if (vList != null && vList.User_Id > 0)
                {
                    objResHelper.Status = StatusCodes.Status200OK;
                    objResHelper.Message = "Request Completed Successfully";
                    objResHelper.Data = vList;
                }
                else
                {
                    objResHelper.Status = StatusCodes.Status204NoContent;
                    objResHelper.Message = "Data Not Available";
                }
                return Ok(objResHelper);
            }
            catch (Exception ex)
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }

        [HttpPost]
        [ActionFilters.AuditLog]
        public IActionResult SignOut(int id)
        {
            ResponseHelper objResHelper = new ResponseHelper();
            try
            {
                userRepository.SignOut(id);
                objResHelper.Status = StatusCodes.Status200OK;
                objResHelper.Message = "Sign Out";
                return Ok(objResHelper);
            }
            catch
            {
                objResHelper.Status = StatusCodes.Status500InternalServerError;
                objResHelper.Message = "Sign Out Fail";
                return StatusCode(StatusCodes.Status500InternalServerError, objResHelper);
            }
        }
    }
}