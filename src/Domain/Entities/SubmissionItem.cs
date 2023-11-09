using DocRouter.Domain.Common;
using DocRouter.Domain.Exceptions.Entities.SubmissionItem;

namespace DocRouter.Domain.Entities
{
    /// <summary>
    /// Entity class that represents an item submitted as part of a submission.
    /// </summary>
    public class SubmissionItem : BaseEntity
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        private SubmissionItem() { }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="itemId">The Id of the Item.</param>
        /// <param name="itemUri">The Uri of the item.</param>
        /// <param name="driveId">The Id of the Drive that containing the submission.</param>
        public SubmissionItem(string itemName, string itemUri, string itemId, string driveId)
        {
            UpdateItemName(itemName);
            UpdateItemUri(itemUri);
            UpdateItemId(itemId);
            UpdateDriveId(driveId);
        }
        private int _submissionId;
        /// <summary>
        /// The Id of the submission.
        /// </summary>
        public int SubmissionId => _submissionId;
        private string _itemName;
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string ItemName => _itemName;
        private string _itemUri;
        /// <summary>
        /// The path to the item.
        /// </summary>
        public string ItemUri => _itemUri;
        private int _displayOrder;
        /// <summary>
        /// An integer representingt the order the item should appear among it's sibling items.
        /// </summary>
        public int DisplayOrder => _displayOrder;
        private string _driveId;
        /// <summary>
        /// The Id of the Drive associated with the submission item.
        /// </summary>
        public string DriveId => _driveId;
        private string _itemId;
        /// <summary>
        /// The unique Identifier for the item.
        /// </summary>
        public string ItemId => _itemId;
        /// <summary>
        /// Navigation property to the <see cref="Entities.Submission"/> associated with the item.
        /// </summary>
        public Submission Submission;

        /// <summary>
        /// Updates the name of the item.
        /// </summary>
        /// <param name="newName">A string containing the item's name.</param>
        /// <exception cref="SubmissionItemArgumentException">Thrown when the provided parameter is null, whitespace, or longer than 100 characters.</exception>
        public void UpdateItemName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new SubmissionItemArgumentException("Parameter cannot be null or whitespace.", nameof(newName));
            }
            else if (newName.Length > 100)
            {
                throw new SubmissionItemArgumentException("Parameter cannot be more than 100 characters.", nameof(newName));
            }
            _itemName = newName;
        }
        /// <summary>
        /// Updates the path to the item.
        /// </summary>
        /// <param name="newPath">A string containing the path to the item.</param>
        /// <exception cref="SubmissionItemArgumentException">Thrown when the provided parameter is null, whitespace, or longer than 1000 characters.</exception>
        public void UpdateItemUri(string newPath)
        {
            if (string.IsNullOrWhiteSpace(newPath))
            {
                throw new SubmissionItemArgumentException("Parameter cannot be null or whitespace.", nameof(newPath));
            }
            else if (newPath.Length > 1000)
            {
                throw new SubmissionItemArgumentException("Parameter cannot be more than 1000 characters.", nameof(newPath));
            }
            _itemUri = newPath;
        }
        /// <summary>
        /// Updates the Drive Id associated with the item.
        /// </summary>
        /// <param name="newId">The new Id</param>
        /// <exception cref="SubmissionItemArgumentException">Thrown when the provided parameter is null or whitespace.</exception>
        public void UpdateDriveId(string newId)
        {
            if (string.IsNullOrWhiteSpace(newId))
            {
                throw new SubmissionItemArgumentException("Parameter cannot be null or whitespace.", nameof(newId));
            }
            _driveId = newId;
        }
        /// <summary>
        /// Updates the unique identifier of the item.
        /// </summary>
        /// <param name="newId">A string containing the unique Id</param>
        /// <exception cref="SubmissionItemArgumentException">Thrown when the provided parameter is null, whitespace, or more than 500 characters.</exception>
        public void UpdateItemId(string newId)
        {
            if (string.IsNullOrWhiteSpace(newId))
            {
                throw new SubmissionItemArgumentException("Parameter cannot be null or whitespace.", nameof(newId));
            }
            else if (newId.Length > 500)
            {
                throw new SubmissionItemArgumentException("Parameter cannot be more than 100 characters.", nameof(newId));
            }
            _itemId = newId;
        }
        /// <summary>
        /// Updates the display order of the item.
        /// </summary>
        /// <param name="newPosition">An integer representing the item's new Position.</param>
        /// <exception cref="SubmissionItemArgumentException">Throw when the parameter is less than zero.</exception>
        public void UpdateDisplayOrder(int newPosition)
        {
            if (newPosition < 0) 
            {
                throw new SubmissionItemArgumentException("Parameter cannot be less than zero.", nameof(newPosition));
            }
            _displayOrder = newPosition;
        }
    }
}
