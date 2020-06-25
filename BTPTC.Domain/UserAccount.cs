using System;

namespace BTPTC.Domain
{
    public class UserAccount : Entity<Int64>
    {

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public Int64 TotalCount { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public string SecurityCode { get; set; }

        public bool LastLoginStatus { get; set; }
        public Guid SessionId { get; set; }

    }
}
