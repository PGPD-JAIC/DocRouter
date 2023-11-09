using DocRouter.Domain.Common;
using DocRouter.Domain.Exceptions.Entities;
using System.Collections.Generic;

namespace DocRouter.Domain.Entities
{
    /// <summary>
    /// Entity class that represents a document submission.
    /// </summary>
    public class Submission : BaseEntity
    {
        /// <summary>
        /// Empty, parameterless constructor used by EF.
        /// </summary>
        private Submission() { }
        /// <summary>
        /// Constructor used to create a new submission.
        /// </summary>
        /// <remarks>
        /// The parameters in this constructor are required to ensure a valid object to create a valid submission in the storage infrastructure. 
        /// </remarks>
        /// <param name="title">A string containing the Title of the submission.</param>
        /// <param name="description">A string containing a description of the submission.</param>
        /// <param name="driveId">A string containing a drive Id of the submission.</param>
        /// <param name="listId">A string containing a list Id of the submission.</param>
        /// <param name="initialTransaction">A <see cref="SubmissionTransaction"/> object representing the initial routing transaction.</param>
        public Submission(string title, string description, string driveId, string listId, SubmissionTransaction initialTransaction)
        {
            UpdateTitle(title);
            UpdateDescription(description);
            UpdateDriveId(driveId);
            UpdateListId(listId);
            _transactions = new List<SubmissionTransaction>();
            _items = new List<SubmissionItem>();
            AddTransaction(initialTransaction);
        }
        private string _title;
        /// <summary>
        /// The title of the submission.
        /// </summary>
        public string Title => _title;
        private string _description;
        /// <summary>
        /// The Description of the Submission.
        /// </summary>
        public string Description => _description;
        private string _folderUri;
        /// <summary>
        /// The path to the submission folder.
        /// </summary>
        public string FolderUri => _folderUri;
        private string _itemId;
        /// <summary>
        /// The Id of the list associated with the folder item.
        /// </summary>
        public string ItemId => _itemId;
        private string _driveId;
        /// <summary>
        /// The Drive Id of the drive in which the submission is stored.
        /// </summary>
        public string DriveId => _driveId;
        private string _listId;
        /// <summary>
        /// The List Id of the list in which the submission is stored.
        /// </summary>
        public string ListId => _listId;
        private List<SubmissionItem> _items;
        /// <summary>
        /// A readonly list of <see cref="SubmissionItem"/> objects associated with the Case.
        /// </summary>
        public virtual ICollection<SubmissionItem> Items => _items.AsReadOnly();
        private List<SubmissionTransaction> _transactions;
        /// <summary>
        /// A readonly list of <see cref="SubmissionItem"/> objects associated with the Case.
        /// </summary>
        public virtual ICollection<SubmissionTransaction> Transactions => _transactions.AsReadOnly();
        /// <summary>
        /// Updates the title of the submission.
        /// </summary>
        /// <param name="newTitle">A string containing the new name.</param>
        /// <exception cref="SubmissionArgumentException">Thrown when the provided parameter is empty, whitespace, or longer than 100 characters.</exception>
        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new SubmissionArgumentException("Parameter cannot be empty or whitespace.", nameof(newTitle));
            }
            else if (newTitle.Length > 100)
            {
                throw new SubmissionArgumentException("Parameter must be 100 characters or fewer.", nameof(newTitle));
            }
            _title = newTitle;
        }
        /// <summary>
        /// Updates the Description of the submission.
        /// </summary>
        /// <param name="newDescription">A string containing the new Description.</param>
        /// <exception cref="SubmissionArgumentException">Thrown when the provided parameter is empty, whitespace, or longer than 100 characters.</exception>
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new SubmissionArgumentException("Parameter cannot be empty or whitespace.", nameof(newDescription));
            }
            else if (newDescription.Length > 1000)
            {
                throw new SubmissionArgumentException("Parameter must be 1000 characters or fewer.", nameof(newDescription));
            }
            _description = newDescription;
        }
        /// <summary>
        /// Updates the submission's Drive Id.
        /// </summary>
        /// <param name="newId">A string containing the new Id.</param>
        /// <exception cref="SubmissionArgumentException">Thrown when the provided Id is empty, whitespace, or longer than 100 characters.</exception>
        public void UpdateDriveId(string newId)
        {
            if (string.IsNullOrWhiteSpace(newId))
            {
                throw new SubmissionArgumentException("Parameter cannot be empty or whitespace.", nameof(newId));
            }
            else if (newId.Length > 100)
            {
                throw new SubmissionArgumentException("Parameter has a maximum length of 100 characters.", nameof(newId));
            }
            _driveId = newId;
        }
        /// <summary>
        /// Updates the submission's List Id.
        /// </summary>
        /// <param name="newId">A string containing the new Id.</param>
        /// <exception cref="SubmissionArgumentException">Thrown when the provided Id is longer than 100 characters.</exception>
        public void UpdateListId(string newId)
        {
            if (newId.Length > 100)
            {
                throw new SubmissionArgumentException("Parameter has a maximum length of 100 characters.", nameof(newId));
            }
            _listId = newId;
        }
        /// <summary>
        /// Updates the Submission's folder Uri.
        /// </summary>
        /// <param name="newFolderUri">A string representing the path to the submission folder.</param>
        /// <exception cref="SubmissionArgumentException">Thrown when the provided parameter is empty, whitespace, or longer than 500 characters.</exception>
        public void UpdateFolderUri(string newFolderUri)
        {
            if (string.IsNullOrWhiteSpace(newFolderUri))
            {
                throw new SubmissionArgumentException("Parameter cannot be empty or whitespace.", nameof(newFolderUri));
            }
            else if (newFolderUri.Length > 500)
            {
                throw new SubmissionArgumentException("Parameter must be 500 characers or fewer.", nameof(newFolderUri));
            }
            _folderUri = newFolderUri;
        }
        /// <summary>
        /// Updates the submission's List Id.
        /// </summary>
        /// <param name="newId">A string containing the new Id.</param>
        /// <exception cref="SubmissionArgumentException">Thrown when the provided Id is empty or whitespace.</exception>
        public void UpdateItemId(string newId)
        {
            if (string.IsNullOrWhiteSpace(newId))
            {
                throw new SubmissionArgumentException("Parameter cannot be empty or whitespace.", nameof(newId));
            }
            _itemId = newId;
        }
        /// <summary>
        /// Adds an item to the Submission's Item Collection
        /// </summary>
        /// <param name="newItem">The Item to add.</param>
        public void AddItem(SubmissionItem newItem)
        {
            _items.Add(newItem);
        }
        /// <summary>
        /// Removes an item from the item's collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(SubmissionItem item)
        {
            _items.Remove(item);
        }
        /// <summary>
        /// Adds a transaction to the submission's transaction list.
        /// </summary>
        /// <param name="newTransaction"></param>
        public void AddTransaction(SubmissionTransaction newTransaction)
        {
            _transactions.Add(newTransaction);
        }
        /// <summary>
        /// Removes a transaction from the submission's transaction list.
        /// </summary>
        /// <param name="toRemove"></param>
        public void RemoveTransaction(SubmissionTransaction toRemove)
        {
            _transactions.Remove(toRemove);
        }
    }
}
