using DocRouter.Domain.Common;
using DocRouter.Domain.Exceptions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocRouter.Domain.Entities
{
    public class Submission : BaseEntity
    {
        /// <summary>
        /// Empty, parameterless constructor used by EF.
        /// </summary>
        private Submission() { }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="title">A string containing the title of the submission.</param>
        public Submission(
            string title,
            string folderUri
            )
        {
            UpdateTitle(title);
            UpdateFolderUri(folderUri);
            _items = new List<SubmissionItem>();
            _transactions = new List<SubmissionTransaction>();
        }
        private string _title;
        /// <summary>
        /// The title of the submission.
        /// </summary>
        public string Title => _title;
        private string _folderUri;
        /// <summary>
        /// The path to the submission folder.
        /// </summary>
        public string FolderUri => _folderUri;
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
