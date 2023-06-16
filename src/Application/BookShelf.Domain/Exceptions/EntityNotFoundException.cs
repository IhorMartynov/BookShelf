using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace BookShelf.Domain.Exceptions
{
    /// <summary>
    /// The entity isn't found in the storage.
    /// </summary>
    [Serializable]
    public sealed class EntityNotFoundException : Exception
    {
        public string EntityName { get; private set; }

        public EntityNotFoundException() { }

        public EntityNotFoundException(string entityName)
        {
            EntityName = entityName;
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        private EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            EntityName = info.GetString(nameof(EntityName));
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(EntityName), EntityName);
        }
    }
}
