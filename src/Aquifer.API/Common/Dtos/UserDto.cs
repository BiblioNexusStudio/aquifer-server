using Aquifer.Data.Entities;

namespace Aquifer.API.Common.Dtos;

public class UserDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public static UserDto? FromUserEntity(UserEntity? user)
    {
        if (user is null)
        {
            return null;
        }

        return new UserDto { Id = user.Id, Name = $"{user.FirstName} {user.LastName}" };
    }
}