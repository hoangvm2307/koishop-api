namespace KoishopBusinessObjects;
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public bool? isDeleted { get; set; } = false;
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}