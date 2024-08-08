using EventBooking.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Domain.Entities
{
    public class User : IdentityUser, IBaseEntity
    {
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ResetToken { get; set; }
        public DateTimeOffset? ResetTokenExpires { get; set; }
        public string? VerificationToken { get; set; }
        public DateTimeOffset? VerificationTokenExpires { get; set; }
        public bool IsConfirmed { get; set; }
        public string? ImagePath { get; set; }
        public string? EmailCode { get; set; }
        public ICollection<Event> Events { get; set; } 
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<EventInvitation> EventInvitations { get; set; }
        public ICollection<EventTicket> EventTickets { get; set; }
        public ICollection<EventPost> EventPosts { get; set; }
        // Base entity
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? DeletedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public string CreatedBy { get; set; } = string.Empty;
        public string LastUpdatedBy { get; set; } = string.Empty;
        public string? DeletedBy { get; set; }

    }
}
