using Data_Layer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            SetSeedData(builder);
        }

        private void SetSeedData(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User { Id = 4, UserName="user" , Password = "user1234", Role = "user"  , RefreshToken = "" , RefreshTokenExpiryTime = DateTime.Now});
            builder.HasData(new User { Id = 5, UserName="admin" , Password = "admin1234" , Role = "admin", RefreshToken = "", RefreshTokenExpiryTime = DateTime.Now });
            builder.HasData(new User { Id = 6, UserName = "siteadmin", Password = "siteadmin1234" , Role = "siteadmin", RefreshToken = "", RefreshTokenExpiryTime = DateTime.Now });

        }
    }
}
