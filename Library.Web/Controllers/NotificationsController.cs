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
            var user = await _accountManager.GetUserByUsernameAsync(User.Identity.Name);

            
            var listVM = new ListNotificationsViewModel();

            var notifications = user.Notifications;

            if (User.IsInRole("admin"))
            {
                foreach (var notification in notifications)
                {
                    string username = notification.Message.Substring(0, notification.Message.IndexOf(" "));
                    var userWhenAdmin = await _accountManager.GetUserByUsernameAsync(username);
                    var vm = new NotificationsViewModel();

                    vm.Id = notification.Id.ToString();
                    vm.Message = notification.Message;
                    vm.UserId = notification.User.Id.ToString();
                    vm.IsSeen = notification.IsSeen;
                    vm.DateSent = notification.SentOn;
                    vm.User = userWhenAdmin;

                    listVM.NotificationsList.Add(vm);
                }
            }
            else if (User.IsInRole("user"))
            {                
                foreach (var notification in notifications)
                {
                    var vm = new NotificationsViewModel();

                    vm.Id = notification.Id.ToString();
                    vm.Message = notification.Message;
                    vm.UserId = notification.User.Id.ToString();
                    vm.IsSeen = notification.IsSeen;
                    vm.DateSent = notification.SentOn;                    

                    listVM.NotificationsList.Add(vm);
                }
            }
            return View(listVM);
        }

        public async Task<IActionResult> MarkSeen(string id)
        {
            await _system.MarkNotificationSeen(id);

            return RedirectToAction("Index");
        }
    }
}