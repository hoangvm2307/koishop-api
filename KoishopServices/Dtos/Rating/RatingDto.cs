using DTOs.AccountDtos;
using DTOs.KoiFish;

namespace KoishopServices.Dtos.Rating
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public virtual UserDto? UserDto { get; set; }
        public int KoiFishId { get; set; }
        public virtual KoiFishDto? KoiFishDto { get; set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool? isDeleted { get; set; } = false;
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
