using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RentIt
{
    using System.IO;

    using BinaryCommunicator;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This class provides convenient conversion methods of
    /// the two enums MediaType and Rating.
    /// </summary>
    internal static class Util
    {
        /// <auhtor>Kenneth Søhrmann</auhtor>
        /// <summary>
        /// Converts the image to the .jpeg-format and returns its bytes.
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Converts the mediaType to its string representation.
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string StringValueOfMediaType(MediaTypeUpload mediaType)
        {
            switch (mediaType)
            {
                case MediaTypeUpload.Book:
                    return "Book";
                case MediaTypeUpload.Movie:
                    return "Movie";
                case MediaTypeUpload.Album:
                    return "Album";
                case MediaTypeUpload.Song:
                    return "Song";
                default:
                    return "Any";
            }
        }
    }
}