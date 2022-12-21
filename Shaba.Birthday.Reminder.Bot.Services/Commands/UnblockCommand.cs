﻿using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
    public class UnblockCommand : ICommand
    {
        private readonly IUserRepository _userRepository;

        public UnblockCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Execute(Update update, User user, string? arg = null)
        {
            await _userRepository.UnBlockUser(user);
        }
    }
}
