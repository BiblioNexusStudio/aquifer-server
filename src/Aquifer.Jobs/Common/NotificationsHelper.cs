using Aquifer.Common.Messages.Models;
using Aquifer.Data.Entities;

namespace Aquifer.Jobs.Common;

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
        return GetUserFullName(userEntity.FirstName, userEntity.LastName);
    }

    public static string GetUserFullName(string firstName, string lastName)
    {
        return $"{firstName} {lastName}";
    }
}