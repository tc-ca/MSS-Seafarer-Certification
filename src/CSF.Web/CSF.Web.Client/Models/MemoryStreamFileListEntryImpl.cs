namespace CSF.Web.Client.Models
{
    using System.IO;
    using BlazorInputFile;

    /// <summary>
    /// Overload of <see cref="FileListEntryImpl"/> class that stores file data in memory using <see cref="MemoryStream"/>.
    /// </summary>
    public class MemoryStreamFileListEntryImpl : FileListEntryImpl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryStreamFileListEntryImpl"/> class.
        /// </summary>
        /// <param name="memoryStreamData">Memory stream instance data for the file.</param>
        public MemoryStreamFileListEntryImpl(MemoryStream memoryStreamData)
        {
            this.MemoryStreamData = memoryStreamData;
        }

        /// <summary>
        /// Gets or sets the file's data stream in-memory.
        /// </summary>
        public MemoryStream MemoryStreamData { get; set; }
    }
}