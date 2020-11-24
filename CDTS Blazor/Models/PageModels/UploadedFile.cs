namespace CDNApplication.Models.PageModels
{
    using BlazorInputFile;
    using CDNApplication.Data.DTO.MTAPI;

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
        public FileListEntryImplExtension SelectedFileWithMemoryData { get; set; }

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
    }
}
