namespace Aquifer.Data.Entities;

public class CompanyEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<UserEntity> Users { get; set; } = [];
    public ICollection<CompanyReviewerEntity> CompanyReviewers { get; set; } = [];
}