using Meteors;
using Microsoft.AspNetCore.Identity;
using System;

namespace MrIdentityUser.IntIndex
{

}


namespace MrIdentityUser.GuidIndex
{
 
}


namespace Meteors.AspNetCore.Infrastructure.ModelEntity.Securty
{

    public interface IMrIdentity { }
    public interface IMrIdentity<TIndex> : IMrIdentity where TIndex: struct, IEquatable<TIndex> { }

    public class MrIdentityUser<TIndex> : IdentityUser<TIndex>, IMrIdentity, IBaseEntity<TIndex> where TIndex : struct, IEquatable<TIndex>
    {
        public DateTime? DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }

    public class MrIdentityRole<TIndex> : IdentityRole<TIndex>, IMrIdentity, IBaseEntity<TIndex> where TIndex : struct, IEquatable<TIndex>
    {
        public DateTime? DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }

    public class MrIdentityUserRole<TIndex> : IdentityUserRole<TIndex>, IDeletable<TIndex>, ICreatable<TIndex>, IUpdatable<TIndex> where TIndex : struct, IEquatable<TIndex>
    {
        public DateTime? DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }

    public class MrIdentityUserClaim<TIndex> : IdentityUserClaim<TIndex>, IMrIdentity, IDeletable<TIndex>, ICreatable<TIndex>, IUpdatable<TIndex> where TIndex : struct, IEquatable<TIndex>
    {
        public DateTime? DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }

    public class MrIdentityUserLogin<TIndex> : IdentityUserLogin<TIndex>, IMrIdentity, IDeletable<TIndex>, ICreatable<TIndex>, IUpdatable<TIndex> where TIndex : struct, IEquatable<TIndex>
    {
        public DateTime? DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }

    public class MrIdentityRoleClaim<TIndex> : IdentityRoleClaim<TIndex>, IDeletable<TIndex>, ICreatable<TIndex>, IUpdatable<TIndex> where TIndex : struct, IEquatable<TIndex>
    {
        public DateTime? DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }

    public partial class MrIdentityUserToken<TIndex> : IdentityUserToken<TIndex>, IMrIdentity, IDeletable<TIndex>, ICreatable<TIndex>, IUpdatable<TIndex> where TIndex : struct, IEquatable<TIndex>
    {
        public DateTime? DateDeleted { get; set; }
        public Nullable<TIndex> DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<TIndex> CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Nullable<TIndex> UpdatedBy { get; set; }
    }
}



