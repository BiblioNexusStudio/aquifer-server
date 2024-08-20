using System.ComponentModel.DataAnnotations.Schema;

namespace Aquifer.Data.Entities;

public class CompanyEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    // TODO: REMOVE THIS PROPERTY AFTER COMPANY REVIEWER IS DEPLOYED
    public int? DefaultReviewerUserId { get; set; }

    public ICollection<UserEntity> Users { get; set; } = [];

    [InverseProperty(nameof(UserEntity.CompaniesAsDefaultReviewer))]
    public UserEntity? DefaultReviewerUser { get; set; }
}