namespace CSF.Web.Client.Models.PageModels
{
    using System;
    using BlazorInputFile;
    using CSF.Web.Client.Data.DTO.MTAPI;

    /// <summary>
    /// Saves the uploaded file's properties.
    /// </summary>
    public class UploadedFile
    {
        /// <summary>
        /// Gets or sets the selected file for upload.
        /// </summary>
        public IFileListEntry SelectedFile { get; set; }

        /// <summary>
        /// Gets or sets SelectedFileWithMemoryData.
        /// </summary>
        public MemoryStreamFileListEntryImpl SelectedFileWithMemoryData { get; set; }

        /// <summary>
        /// Gets or sets FileAttachment related to MTOA file upload.
        /// </summary>
        public FileAttachment MtoaFileAttachment { get; set; }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the virus status of the file.
        /// </summary>
        public bool Safe { get; set; }

        /// <summary>
        /// Gets a value indicating whether the file has an error.
        /// </summary>
        public bool HasAnError => !string.IsNullOrEmpty(this.ErrorMessage);

        /// <summary>
        /// Gets or sets the file's error message if it exists.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Override of Object.ToString() method.
        /// </summary>
        /// <returns>List of all uploaded files.</returns>
        public override string ToString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
