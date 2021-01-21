using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CSF.MPDIS.API.Data.Entity
{
    public class DocumentType
    {

        /// <summary>
        /// Gets or sets the Index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets English Name.
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// Gets or sets French Name.
        /// </summary>
        public string FrenchName { get; set; }

        /// <summary>
        /// This method is used for getting Document types.
        /// </summary>
        /// <returns>All available Document types.</returns>
        public static List<DocumentType> GetAllDocumentTypes()
        {
            string[,] Documents =
                #pragma warning disable CA1814 //The rule wants us to use jagged arrays. We prefer multi dimensional array at this point.
                {
                    //{ "0", "   ", "     " },
                    { "1", "Passport", "Passeport" },
                    { "2", "Drivers License", "Permis de conduire" },
                    { "3", "ADVANCED FIRE FIGHTING", "TECHNIQUES AVANCÉES DE LUTTE CONTRE L'INCENDIE" }
                };
#pragma warning restore CA1814 // The rule wants us to use jagged arrays. We prefer multi dimensional array at this point.

            List<DocumentType> DocumentTypes = new List<DocumentType>();
            var rows = Documents.GetLength(0);
            int columns = 3;
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NegativeSign = "neg ";

            for (int r = 0; r < rows; r++)
            {
                DocumentType DocumentType = new DocumentType();
                for (int c = 0; c < columns; c++)
                {
                    if (c == 0)
                    {
                        DocumentType.Index = Convert.ToInt16(Documents[r, c], provider);
                    }

                    if (c == 1)
                    {
                        DocumentType.EnglishName = Documents[r, c];
                    }

                    if (c == 2)
                    {
                        DocumentType.FrenchName = Documents[r, c];
                    }
                }

                DocumentTypes.Add(DocumentType);
            }

            return DocumentTypes;
        }

    }
}
