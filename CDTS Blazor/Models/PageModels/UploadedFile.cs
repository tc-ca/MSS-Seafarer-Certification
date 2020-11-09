namespace CDNApplication.Models.PageModels
{
    using BlazorInputFile;

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
        /// Gets or sets SelectedFileWithMemoryData
        /// </summary>
        public FileListEntryImplExtension SelectedFileWithMemoryData { get; set; }

        /// <summary>
        /// Gets or sets the file description.
        /// </summary>
        public string Description { get; set; }
    }
}
