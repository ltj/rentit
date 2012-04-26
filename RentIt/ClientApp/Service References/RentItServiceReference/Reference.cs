﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientApp.RentItServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MediaInfo", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClientApp.RentItServiceReference.AlbumInfo))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClientApp.RentItServiceReference.SongInfo))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClientApp.RentItServiceReference.MovieInfo))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClientApp.RentItServiceReference.BookInfo))]
    public partial class MediaInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PublisherField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ClientApp.RentItServiceReference.MediaRating RatingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ReleaseDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ClientApp.RentItServiceReference.MediaType TypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Genre {
            get {
                return this.GenreField;
            }
            set {
                if ((object.ReferenceEquals(this.GenreField, value) != true)) {
                    this.GenreField = value;
                    this.RaisePropertyChanged("Genre");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Publisher {
            get {
                return this.PublisherField;
            }
            set {
                if ((object.ReferenceEquals(this.PublisherField, value) != true)) {
                    this.PublisherField = value;
                    this.RaisePropertyChanged("Publisher");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ClientApp.RentItServiceReference.MediaRating Rating {
            get {
                return this.RatingField;
            }
            set {
                if ((object.ReferenceEquals(this.RatingField, value) != true)) {
                    this.RatingField = value;
                    this.RaisePropertyChanged("Rating");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ReleaseDate {
            get {
                return this.ReleaseDateField;
            }
            set {
                if ((this.ReleaseDateField.Equals(value) != true)) {
                    this.ReleaseDateField = value;
                    this.RaisePropertyChanged("ReleaseDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ClientApp.RentItServiceReference.MediaType Type {
            get {
                return this.TypeField;
            }
            set {
                if ((this.TypeField.Equals(value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MediaRating", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    [System.SerializableAttribute()]
    public partial class MediaRating : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float AverageRatingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RatingsCountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ClientApp.RentItServiceReference.MediaReview[] ReviewsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float AverageRating {
            get {
                return this.AverageRatingField;
            }
            set {
                if ((this.AverageRatingField.Equals(value) != true)) {
                    this.AverageRatingField = value;
                    this.RaisePropertyChanged("AverageRating");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RatingsCount {
            get {
                return this.RatingsCountField;
            }
            set {
                if ((this.RatingsCountField.Equals(value) != true)) {
                    this.RatingsCountField = value;
                    this.RaisePropertyChanged("RatingsCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ClientApp.RentItServiceReference.MediaReview[] Reviews {
            get {
                return this.ReviewsField;
            }
            set {
                if ((object.ReferenceEquals(this.ReviewsField, value) != true)) {
                    this.ReviewsField = value;
                    this.RaisePropertyChanged("Reviews");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlbumInfo", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    [System.SerializableAttribute()]
    public partial class AlbumInfo : ClientApp.RentItServiceReference.MediaInfo {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AlbumArtistField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ClientApp.RentItServiceReference.SongInfo[] SongsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan TotalDurationField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AlbumArtist {
            get {
                return this.AlbumArtistField;
            }
            set {
                if ((object.ReferenceEquals(this.AlbumArtistField, value) != true)) {
                    this.AlbumArtistField = value;
                    this.RaisePropertyChanged("AlbumArtist");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ClientApp.RentItServiceReference.SongInfo[] Songs {
            get {
                return this.SongsField;
            }
            set {
                if ((object.ReferenceEquals(this.SongsField, value) != true)) {
                    this.SongsField = value;
                    this.RaisePropertyChanged("Songs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan TotalDuration {
            get {
                return this.TotalDurationField;
            }
            set {
                if ((this.TotalDurationField.Equals(value) != true)) {
                    this.TotalDurationField = value;
                    this.RaisePropertyChanged("TotalDuration");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SongInfo", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    [System.SerializableAttribute()]
    public partial class SongInfo : ClientApp.RentItServiceReference.MediaInfo {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AlbumIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ArtistField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan DurationField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AlbumId {
            get {
                return this.AlbumIdField;
            }
            set {
                if ((this.AlbumIdField.Equals(value) != true)) {
                    this.AlbumIdField = value;
                    this.RaisePropertyChanged("AlbumId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Artist {
            get {
                return this.ArtistField;
            }
            set {
                if ((object.ReferenceEquals(this.ArtistField, value) != true)) {
                    this.ArtistField = value;
                    this.RaisePropertyChanged("Artist");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MovieInfo", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    [System.SerializableAttribute()]
    public partial class MovieInfo : ClientApp.RentItServiceReference.MediaInfo {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DirectorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan DurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SummaryField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Director {
            get {
                return this.DirectorField;
            }
            set {
                if ((object.ReferenceEquals(this.DirectorField, value) != true)) {
                    this.DirectorField = value;
                    this.RaisePropertyChanged("Director");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Summary {
            get {
                return this.SummaryField;
            }
            set {
                if ((object.ReferenceEquals(this.SummaryField, value) != true)) {
                    this.SummaryField = value;
                    this.RaisePropertyChanged("Summary");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BookInfo", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    [System.SerializableAttribute()]
    public partial class BookInfo : ClientApp.RentItServiceReference.MediaInfo {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PagesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SummaryField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Pages {
            get {
                return this.PagesField;
            }
            set {
                if ((this.PagesField.Equals(value) != true)) {
                    this.PagesField = value;
                    this.RaisePropertyChanged("Pages");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Summary {
            get {
                return this.SummaryField;
            }
            set {
                if ((object.ReferenceEquals(this.SummaryField, value) != true)) {
                    this.SummaryField = value;
                    this.RaisePropertyChanged("Summary");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MediaType", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    public enum MediaType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Any = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Book = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Movie = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Album = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Song = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MediaReview", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    [System.SerializableAttribute()]
    public partial struct MediaReview : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MediaIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ClientApp.RentItServiceReference.Rating RatingField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReviewTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MediaId {
            get {
                return this.MediaIdField;
            }
            set {
                if ((this.MediaIdField.Equals(value) != true)) {
                    this.MediaIdField = value;
                    this.RaisePropertyChanged("MediaId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ClientApp.RentItServiceReference.Rating Rating {
            get {
                return this.RatingField;
            }
            set {
                if ((this.RatingField.Equals(value) != true)) {
                    this.RatingField = value;
                    this.RaisePropertyChanged("Rating");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReviewText {
            get {
                return this.ReviewTextField;
            }
            set {
                if ((object.ReferenceEquals(this.ReviewTextField, value) != true)) {
                    this.ReviewTextField = value;
                    this.RaisePropertyChanged("ReviewText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Timestamp {
            get {
                return this.TimestampField;
            }
            set {
                if ((this.TimestampField.Equals(value) != true)) {
                    this.TimestampField = value;
                    this.RaisePropertyChanged("Timestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Rating", Namespace="http://schemas.datacontract.org/2004/07/RentIt")]
    public enum Rating : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        One = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Two = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Three = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Four = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Five = 5,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    public class FaultExceptionOfInvalidCredentialsExceptionFe9b7uG0 {
    }
}
