// <copyright file="IUserDetailService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.ViewModels;

    /// <summary>
    /// IUserDetailService
    /// </summary>
    public interface IUserDetailService
    {
        /// <summary>
        /// check if mobile number already exists.
        /// </summary>
        /// <param name="mobile">The user detail.</param>
        /// <returns>true or false</returns>
        Task<bool> IsDuplicateMobileAsync(string mobile);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>Insert Record Async</returns>
        Task<int> InsertAsync(UserDetailModel userDetail);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>Update Record Async</returns>
        Task<int> UpdateAsync(UserDetailModel userDetail);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>Delete Recored Async</returns>
        Task<int> DeleteAsync(UserDetailModel userDetail);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="userDetailId">The user detail identifier.</param>
        /// <returns>
        /// Get Record By Id
        /// </returns>
        Task<UserDetailModel> GetByIdAsync(int userDetailId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Get All List</returns>
        Task<IList<UserDetailModel>> GetAllAsync();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Get All PaggedList</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// send otp.
        /// </summary>
        /// <param name="mobileNo">Themobile number.</param>
        /// <param name="from">from.</param>
        /// <returns>
        /// UserDetailModel
        /// </returns>
        Task<UserDetailModel> CheckMobile(string mobileNo, string from);

        /// <summary>
        /// update otp.
        /// </summary>
        /// <param name='id'>The Mobile Number.</param>
        /// <param name="otp">The otp.</param>
        /// <returns>Record Id</returns>
        Task<int> InsertOTP(int id, int otp);

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="mobile">The email.</param>
        /// <param name="otp">The password.</param>
        /// <param name="checkotp">if set to <c>true</c> [is guest].</param>
        /// <returns>
        /// Login User Async
        /// </returns>
        Task<UserDetailModel> LoginOTPAsync(string mobile, string otp, bool checkotp = true);

        /// <summary>
        /// Logins via otp asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="from">from.</param>
        /// <param name="checkPassword">if set to <c>true</c> [check password].</param>
        /// <returns>
        /// LoginAsync
        /// </returns>
        Task<UserDetailModel> LoginAsync(string email, string password, string from, bool checkPassword = true);

        /// <summary>
        /// Duplicates the EmailId.
        /// </summary>
        /// <param name="emailId">The emailId.</param>
        /// <returns>Login User Async</returns>
        Task<bool> IsDuplicateAsync(string emailId);

        /// <summary>
        /// Chnages the user password.
        /// </summary>
        /// <param name="userid">The user detail.</param>
        /// <returns>GetUserProfileByEmailId</returns>
        Task<MyInformationViewModel> GetUserProfileByEmailId(string userid);

        /// <summary>
        /// Changes the user password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>
        /// ChangeUserPassword
        /// </returns>
        Task<bool> UserChangePassword(string userId, string password, string newPassword);

        /// <summary>
        /// Users the set password.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>true or false</returns>
        Task<bool> UserSetPassword(string emailId, string newPassword);

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <returns>Total Number of users</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Activates the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActivateUser</returns>
        Task<UserDetailModel> ActivateUser(int id);

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <returns>Password</returns>
        Task<UserDetailModel> GetPassword(string emailId);

        /// <summary>
        /// Gets the user identifier asynchronous.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <returns>int</returns>
        Task<int> GetUserIdAsync(string emailId);

        /// <summary>
        /// Logins via otp asynchronous.
        /// </summary>
        /// <param name="emailId">The email ID.</param>
        /// <returns>
        /// LoginAsync
        /// </returns>
        Task<UserDetailModel> GetUserRecordByEmailId(string emailId);

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="userId">The User identifier.</param>
        /// <returns>Password</returns>
        Task<List<MyBookingsListViewModel>> GetDealBookingsByUserId(int userId);

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="bookingId">The Booking identifier.</param>
        /// <returns>Password</returns>
        Task<MyBookingDescriptionViewModel> GetMyBookingDescriptionByBookingId(int bookingId);

        /// <summary>
        /// Logins via otp asynchronous.
        /// </summary>
        /// <param name="mobile">The Mobile Number.</param>
        /// <returns>
        /// LoginAsync
        /// </returns>
        Task<UserDetailModel> GetUserRecordByMobile(string mobile);
    }
}
