using Aquifer.Data.Entities;

namespace Aquifer.API.Common.Dtos;

public class UserDto(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;

    public UserDto(int id, string firstName, string lastName)
        : this(id, $"{firstName} {lastName}")
    {
    }

    public UserDto((int Id, string FirstName, string LastName) user)
        : this(user.Id, user.FirstName, user.LastName)
    {
    }

    public static UserDto? FromUserEntity(UserEntity? user)
    {
        return user is null ? null : new UserDto(user.Id, user.FirstName, user.LastName);
    }
}