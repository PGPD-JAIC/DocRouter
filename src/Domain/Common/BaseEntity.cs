using DocRouter.Domain.Exceptions;
using System;

namespace DocRouter.Domain.Common
{
    /// <summary>
    /// Base class for an entity class object.
    /// </summary>
    public abstract class BaseEntity
    {
        private int _id;
        /// <summary>
        /// The integer Id of the entity.
        /// </summary>
        public int Id => _id;
        private protected string _createdBy;
        /// <summary>
        /// The username of the user who created the submission.
        /// </summary>
        public string CreatedBy => _createdBy;
        private protected DateTime _created;
        /// <summary>
        /// The Date/Time the entity was created.
        /// </summary>
        public DateTime Created => _created;
        private protected string _editedBy;
        /// <summary>
        /// The username that last edited the entity.
        /// </summary>
        public string EditedBy => _editedBy;
        private protected DateTime? _edited;
        /// <summary>
        /// The Date/Time the entity was last edited.
        /// </summary>
        public DateTime? Edited => _edited;
        /// <summary>
        /// Updates the name of the user who is creating the entity.
        /// </summary>
        /// <param name="userName">A string containing the username.</param>
        /// <exception cref="BaseEntityArgumentException">Thrown when the provided parameter is null, whitespace, or more than 100 characters.</exception>
        public void UpdateCreatedBy(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new BaseEntityArgumentException("Parameter cannot be null or whitespace.", nameof(userName));
            }
            else if (userName.Length > 100)
            {
                throw new BaseEntityArgumentException("Parameter must be 100 characters or fewer.", nameof(userName));
            }
            _createdBy = userName;
        }
        /// <summary>
        /// Updates the Date/Time that the entity was created.
        /// </summary>
        /// <param name="newDate">A <see cref="DateTime"/> containing the date/time the entity was created.</param>
        public void UpdateDateCreated(DateTime newDate)
        {
            _created = newDate;
        }
        /// <summary>
        /// Updates the name of the user lst edited the entity.
        /// </summary>
        /// <param name="userName">A string containing the username.</param>
        /// <exception cref="BaseEntityArgumentException">Thrown when the provided parameter is null, whitespace, or more than 100 characters.</exception>
        public void UpdateEditedBy(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new BaseEntityArgumentException("Parameter cannot be null or whitespace.", nameof(userName));
            }
            else if (userName.Length > 100)
            {
                throw new BaseEntityArgumentException("Parameter must be 100 characters or fewer.", nameof(userName));
            }
            _editedBy = userName;
        }
        /// <summary>
        /// Updates the Date/Time that the entity was edited.
        /// </summary>
        /// <param name="newDate">A <see cref="DateTime"/> containing the date/time the entity was edited.</param>
        public void UpdateDateEdited(DateTime newDate)
        {
            _edited = newDate;
        }
    }
}
