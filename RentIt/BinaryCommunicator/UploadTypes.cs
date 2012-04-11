
namespace BinaryCommunicator
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// This enum represents the available media types of the service.
    /// </summary>
    public enum MediaTypeUpload
    {
        Book = 1,

        Movie = 2,

        Album = 3,

        Song = 4,
    }

    #region Media metadata types for upload

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// MediaFileUpload represents all the metadata that must be registered about
    /// a particular media item before it can be uploaded. MediaFileUpload represents
    /// all the metadata-properties that all media types (Book, Movie, Album and Song)
    /// have in common.
    /// </summary>
    public abstract class MediaInfoUpload
    {
        private string title;

        private MediaTypeUpload mediaType;

        private string genre;

        private int price;

        private DateTime releaseDate;

        private string publisher;

        private System.Drawing.Image thumbnail;

        /// <summary>
        /// Gets or sets the title of the media file to be uploaded.
        /// </summary>
        [CategoryAttribute("Base data")]
        [DescriptionAttribute("The title of the media")]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets or sets the media type of the media file to be uploaded.
        /// </summary>
        [Browsable(false)]
        public MediaTypeUpload MediaType
        {
            get
            {
                return this.mediaType;
            }
            set
            {
                this.mediaType = value;
            }
        }

        /// <summary>
        /// Gets or sets the genre of the media file to be uploaded.
        /// </summary>
        [CategoryAttribute("Base data")]
        [DescriptionAttribute("The genre of the media")]
        public string Genre
        {
            get
            {
                return this.genre;
            }
            set
            {
                this.genre = value;
            }
        }

        /// <summary>
        /// Gets or sets the genre of the media file to be uploaded
        /// </summary>
        [CategoryAttribute("Base data")]
        [DescriptionAttribute("The price of the media")]
        public int Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }

        /// <summary>
        /// Gets or sets the release date of the media file to be uploaded.
        /// </summary>
        [CategoryAttribute("Base data")]
        [DescriptionAttribute("The release day of the media")]
        public DateTime ReleaseDate
        {
            get
            {
                return this.releaseDate;
            }
            set
            {
                this.releaseDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the publisher name of the media file to be uploaded.
        /// </summary>
        [CategoryAttribute("Base data")]
        [DescriptionAttribute("The publisher of the media")]
        public string Publisher
        {
            get
            {
                return this.publisher;
            }
            set
            {
                this.publisher = value;
            }
        }

        /// <summary>
        /// Gets or sets the thumbnail of the media file to be uploaded.
        /// </summary>
        [CategoryAttribute("Base data")]
        [DescriptionAttribute("The thumbnail of the media")]
        public Image Thumbnail
        {
            get
            {
                return this.thumbnail;
            }
            set
            {
                this.thumbnail = value;
            }
        }
    }

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// BookInfoUpload holds all the metadata of a book. It holds all the base metadata
    /// (the metadata that are common for all media types) as well as the metadata that
    /// are specific for books.
    /// </summary>
    public class BookInfoUpload : MediaInfoUpload
    {
        private string filePath;

        private string author;

        private string summary;

        private int pages;

        public BookInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Book;
        }

        /// <summary>
        /// Gets or sets the author of the book to be uploaded.
        /// </summary>
        [CategoryAttribute("Book specific data")]
        [DescriptionAttribute("The author of the book")]
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }

        /// <summary>
        /// Gets or sets the summary of the book to be uploaded.
        /// </summary>
        [CategoryAttribute("Book specific data")]
        [DescriptionAttribute("A summary of the book")]
        public string Summary
        {
            get
            {
                return this.summary;
            }
            set
            {
                this.summary = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of pages of the book to be uploaded.
        /// </summary>
        [CategoryAttribute("Book specific data")]
        [DescriptionAttribute("The number of pages of the book")]
        public int Pages
        {
            get
            {
                return this.pages;
            }
            set
            {
                this.pages = value;
            }
        }

        /// <summary>
        /// Gets or sets of file path of the media file to be uploaded.
        /// </summary>
        [CategoryAttribute("Media file"), DescriptionAttribute("The media file")]
        [System.ComponentModel.Editor(typeof(BookFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }
    }

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// AlbumInfoUpload holds all the metadata of an album. It holds all the base metadata
    /// (the metadata that are common for all media types) as well as the metadata that
    /// are specific for albums.
    /// An AlbumInfoUpload object does not contain any data about what songs are a part of this album.
    /// </summary>
    public class AlbumInfoUpload : MediaInfoUpload
    {
        private string albumArtist;

        private string description;

        public AlbumInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Album;
        }

        /// <summary>
        /// Gets or sets the album artist of the album to be uploaded.
        /// </summary>
        [CategoryAttribute("Album specific data")]
        [DescriptionAttribute("The album artist of the album")]
        public string AlbumArtist
        {
            get
            {
                return this.albumArtist;
            }
            set
            {
                this.albumArtist = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of the album to be uploaded.
        /// </summary>
        [CategoryAttribute("Album specific data")]
        [DescriptionAttribute("The description of the album")]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
    }

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// MovieInfoUpload holds all the metadata of a movie. It holds all the base metadata
    /// (the metadata that are common for all media types) as well as the metadata that
    /// are specific for movies.
    /// </summary>
    public class MovieInfoUpload : MediaInfoUpload
    {
        private string filePath;

        private string summary;

        private string director;

        private TimeSpan duration;

        public MovieInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Movie;
        }

        /// <summary>
        /// Get or sets the summary of the movie to be uploaded.
        /// </summary>
        [CategoryAttribute("Movie specific data")]
        [DescriptionAttribute("The summary of the movie. May contain a listing of the actors in the movie.")]
        public string Summary
        {
            get
            {
                return this.summary;
            }
            set
            {
                this.summary = value;
            }
        }

        /// <summary>
        /// Gets or sets the director of the movie to be uploaded.
        /// </summary>
        [CategoryAttribute("Movie specific data")]
        [DescriptionAttribute("The director of the movie")]
        public string Director
        {
            get
            {
                return this.director;
            }
            set
            {
                this.director = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the movie to be uploaded.
        /// </summary>
        [CategoryAttribute("Movie specific data")]
        [DescriptionAttribute("The duration of the movie")]
        public TimeSpan Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the file path of the movie to be uploaded.
        /// </summary>
        [CategoryAttribute("Media file"), DescriptionAttribute("The media file")]
        [System.ComponentModel.Editor(typeof(MovieFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }
    }

    /// <author>Kenneth Søhrmann</author>
    /// <summary>
    /// SongInfoUpload hold all the metadata of a song. It holds all the base metadata 
    /// (the metadata that are common for all media types) as well as the metadata that
    /// are specific for songs.
    /// </summary>
    public class SongInfoUpload : MediaInfoUpload
    {
        private string filePath;

        private string artist;

        private TimeSpan duration;

        private string summary;

        public SongInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Song;
        }

        /// <summary>
        /// Gets or sets the artist of the song to be uploaded.
        /// </summary>
        [CategoryAttribute("Song specific data")]
        [DescriptionAttribute("The artist of the song")]
        public string Artist
        {
            get
            {
                return this.artist;
            }
            set
            {
                this.artist = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the song to be uploaded.
        /// </summary>
        [CategoryAttribute("Song specific data")]
        [DescriptionAttribute("The duration of the song")]
        public TimeSpan Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the summary of the song to be uploaded.
        /// </summary>
        [CategoryAttribute("Song specific data")]
        [DescriptionAttribute("The summary of the song")]
        public string Summary
        {
            get
            {
                return this.summary;
            }
            set
            {
                this.summary = value;
            }
        }

        /// <summary>
        /// Gets or sets the file path of the song file to be uploaded.
        /// </summary>
        [CategoryAttribute("Media file"), DescriptionAttribute("The media file")]
        [System.ComponentModel.Editor(typeof(SongFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }
    }

    #endregion

    #region File Name Editors

    /// <summary>
    /// The classes implementation has been found at this page.
    /// http://social.msdn.microsoft.com/Forums/en-US/winforms/thread/07ad29f2-3040-4f1d-81c6-d55e0522afe7/
    /// This class is used to define the properties of the 
    /// Open File-dialog that appears when the user is to select
    /// the movie file to be uploaded. It only allows selection
    /// of files with the extension .mp4
    /// </summary>
    public class MovieFileNameEditor : UITypeEditor
    {
        private string movieFilter = "Videos(*.mp4)|*.mp4";

        public MovieFileNameEditor()
        {
        }

        [DefaultValue("Videos(*.mp4)|*.mp4")]
        public string CustomFilter
        {
            get
            {
                return movieFilter;
            }
            set
            {
                movieFilter = value;
            }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // We'll show modal dialog (OpenFileDialog)
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = this.CustomFilter;
                if (dialog.ShowDialog() == DialogResult.OK) value = dialog.FileName;
            }
            return value;
        }
    }

    /// <summary>
    /// This class is used to define the properties of the 
    /// Open File-dialog that appears when the user is to select
    /// the book file to be uploaded. It only allows selection
    /// of files with the extension .pdf
    /// </summary>
    public class BookFileNameEditor : UITypeEditor
    {
        private string bookFilter = "Books(*.pdf)|*.pdf";

        public BookFileNameEditor()
        {
        }

        [DefaultValue("Books(*.pdf)|*.pdf")]
        public string CustomFilter
        {
            get
            {
                return bookFilter;
            }
            set
            {
                bookFilter = value;
            }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // We'll show modal dialog (OpenFileDialog)
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = this.CustomFilter;
                if (dialog.ShowDialog() == DialogResult.OK) value = dialog.FileName;
            }
            return value;
        }
    }

    /// <summary>
    /// This class is used to define the properties of the 
    /// Open File-dialog that appears when the user is to select
    /// the song file to be uploaded. It only allows selection
    /// of files with the extension .mp3
    /// </summary>
    public class SongFileNameEditor : UITypeEditor
    {
        private string songFilter = "Songs(*.mp3)|*.mp3";

        public SongFileNameEditor()
        {
        }

        [DefaultValue("Songs(*.mp3)|*.mp3")]
        public string CustomFilter
        {
            get
            {
                return songFilter;
            }
            set
            {
                songFilter = value;
            }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // We'll show modal dialog (OpenFileDialog)
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = this.CustomFilter;
                if (dialog.ShowDialog() == DialogResult.OK) value = dialog.FileName;
            }
            return value;
        }
    }

    #endregion
}