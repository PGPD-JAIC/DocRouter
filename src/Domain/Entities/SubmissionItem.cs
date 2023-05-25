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
        /// <param name="itemUri">The path of the item.</param>
        public SubmissionItem(string itemName, string itemUri)
        {
            UpdateItemName(itemName);
            UpdateItemUri(itemUri);
        }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="itemUri">The path of the item.</param>
        /// <param name="itemId">The id of the item.</param>
        public SubmissionItem(string itemName, string itemUri, string itemId)
        {
            UpdateItemName(itemName);
            UpdateItemUri(itemUri);
            UpdateUniqueId(itemId);
        }
        private string _itemName;
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string ItemName => _itemName;
        private string _uniqueId;
        /// <summary>
        /// The unique Identifier for the item.
        /// </summary>
        public string UniqueId => _uniqueId;
        private string _itemUri;
        /// <summary>
        /// The path to the item.
        /// </summary>
        public string ItemUri => _itemUri;
        private int _submissionId;
        public int SubmissionId => _submissionId;
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
        /// Updates the unique identifier of the item.
        /// </summary>
        /// <param name="newId">A string containing the unique Id</param>
        /// <exception cref="SubmissionItemArgumentException">Thrown when the provided parameter is null, whitespace, or more than 500 characters.</exception>
        public void UpdateUniqueId(string newId)
        {
            if (string.IsNullOrWhiteSpace(newId))
            {
                throw new SubmissionItemArgumentException("Parameter cannot be null or whitespace.", nameof(newId));
            }
            else if (newId.Length > 500)
            {
                throw new SubmissionItemArgumentException("Parameter cannot be more than 100 characters.", nameof(newId));
            }
            _uniqueId = newId;
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
    }
}
