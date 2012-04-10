

namespace BinaryCommunicator
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;

    public enum MediaTypeUpload
    {
        Book = 1,

        Movie = 2,

        Album = 3,

        Song = 4,
    }

    public abstract class MediaInfoUpload
    {
        private string filePath;

        private string title;

        private MediaTypeUpload mediaType;

        private string genre;

        private int price;

        private DateTime releaseDate;

        private string publisher;

        private System.Drawing.Image thumbnail;

        [CategoryAttribute("Media file"), DescriptionAttribute("The media file")]
        [System.ComponentModel.Editor(typeof(CustomFileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string File
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

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

    public class BookInfoUpload : MediaInfoUpload
    {
        private string author;

        private string summary;

        private int pages;

        public BookInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Book;
        }

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
    }

    public class AlbumInfoUpload : MediaInfoUpload
    {
        private string albumArtist;

        private string description;

        public AlbumInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Album;
        }

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

    public class MovieInfoUpload : MediaInfoUpload
    {
        private string summary;

        private string director;

        private TimeSpan duration;

        public MovieInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Movie;
        }

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
    }

    //[DefaultPropertyAttribute("Name")]
    public class SongInfoUpload : MediaInfoUpload
    {
        private string artist;

        private TimeSpan duration;

        private string summary;

        public SongInfoUpload()
        {
            this.MediaType = MediaTypeUpload.Song;
        }

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
    }




    /// <summary>
    /// http://social.msdn.microsoft.com/Forums/en-US/winforms/thread/07ad29f2-3040-4f1d-81c6-d55e0522afe7/
    /// </summary>
    public class CustomFileNameEditor : UITypeEditor
    {
        private string _customFilter = "Videos(*.mp4)|*.mp4";

        public CustomFileNameEditor()
        {
        }

        [DefaultValue("Videos(*.mp4)|*.mp4")]
        public string CustomFilter
        {
            get
            {
                return _customFilter;
            }
            set
            {
                _customFilter = value;
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
}