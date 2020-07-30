﻿using Job_Bookings.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class UserService : BaseService<UserService>, IUserService
    {
        readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo, ILogger<UserService> logger)
        {
            _userRepo = userRepo;
            _logger = logger;
        }

        public async Task<ReturnDto<bool>> AddUser(User user)
        {
            var rtn = new ReturnDto<bool>();

            if (user == null)
            {
                rtn.ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED;
                rtn.ReturnObject = false;
                
                return rtn;
            }

            try { 
                rtn.ReturnObject = await _userRepo.AddUser(user);
            }
            catch (Exception e)
            {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = false;

                _logger.LogError($"An error occured in - {typeof(UserService)} - Add User -  Message: {e.Message} - U: {user.UserGuid}");
            }

            return rtn;
        }

        public async Task<ReturnDto<User>> GetUser(Guid userGuid)
        {
            var rtn = new ReturnDto<User>();

            if (userGuid == null || userGuid == Guid.Empty)
            {
                rtn.ErrorCode = ErrorCodes.USER_GUID_NOT_PROVIDED;
                rtn.ReturnObject = null;

                return rtn;
            }

            try { 
                rtn.ReturnObject = await _userRepo.GetUser(userGuid);
            }
            catch (Exception e)
            {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {nameof(UserService)} - Get User - Message: {e.Message} - U: {userGuid}");
            }

            return rtn;
        }

        public async Task<ReturnDto<List<User>>> GetUsers()
        {
            var rtn = new ReturnDto<List<User>>();

            try { 
                rtn.ReturnObject = await _userRepo.GetUsers();
            }
            catch (Exception e)
            {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {nameof(UserService)} - Get Users - Message: {e.Message}");
            }

            return rtn;
        }

        public async Task<ReturnDto<User>> UpdateUser(User user)
        {
            var rtn = new ReturnDto<User>();

            if (user == null)
            {
                rtn.ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED;
                rtn.ReturnObject = null;

                return rtn;
            }

            try
            {
                rtn.ReturnObject = await _userRepo.UpdateUser(user);
            }
            catch (Exception e)
            {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {nameof(UserService)} - Update User - Message: {e.Message} - U: {user.UserGuid}");
            }

            return rtn;
        }
    }
}
