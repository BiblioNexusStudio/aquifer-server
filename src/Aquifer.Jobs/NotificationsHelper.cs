﻿using Aquifer.Common.Services;
using Aquifer.Data.Entities;

namespace Aquifer.Jobs;

public static class NotificationsHelper
{
    public static readonly EmailAddress NotificationSenderEmailAddress = new("notifications@aquifer.bible", "Aquifer Notifications");
    public static readonly EmailAddress NotificationToEmailAddress = new("no-reply@aquifer.bible", "Aquifer");

    public static EmailAddress GetEmailAddress(UserEntity userEntity)
    {
        return new EmailAddress(userEntity.Email, GetUserFullName(userEntity));
    }

    public static string GetUserFullName(UserEntity userEntity)
    {
        return $"{userEntity.FirstName} {userEntity.LastName}";
    }
}