using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.Contracts;
using Library.Web.Models.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly ILibrarySystem _system;

        public NotificationsController(IAccountManager accountManager, ILibrarySystem system)
        {
            _accountManager = accountManager;
            _system = system;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new NotificationsViewModel();
            if (User.IsInRole("admin"))
            {
                var allNotifications = await _system.GetAllNotificationsAsync();

                foreach (var notification in allNotifications)
                {
                    vm.Id = notification.Id.ToString();
                    vm.Message = notification.Message;
                    vm.UserId = notification.User.Id.ToString();
                    vm.IsSeen = notification.IsSeen;
                    vm.DateSent = notification.SentOn;
                    vm.User = notification.User;

                    vm.NotificationsList.Add(vm);
                }
            }
            else
            {
                var user = await _accountManager.GetUserByUsernameAsync(User.Identity.Name);

                foreach (var notification in user.Notifications)
                {
                    vm.Id = notification.Id.ToString();
                    vm.Message = notification.Message;
                    vm.UserId = notification.User.Id.ToString();
                    vm.IsSeen = notification.IsSeen;
                    vm.DateSent = notification.SentOn;
                    vm.User = notification.User;

                    vm.NotificationsList.Add(vm);
                }
            }
            return View(vm);
        }
    }
}