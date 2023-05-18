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
        public SubmissionItem(string itemName, string itemUri)
        {
            UpdateItemName(itemName);
            UpdateItemUri(itemUri);
        }
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
