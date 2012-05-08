﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientApp
{
	using System.ServiceModel;

	using RentIt;

	public partial class AlsoRentedList : UserControl
	{
		public AlsoRentedList()
		{
			InitializeComponent();
		}
		internal AlsoRentedList(MediaType prioritizedMediaType, int mediaId)
			: this()
		{
			BasicHttpBinding binding = new BasicHttpBinding();
			EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
			var rentIt = new RentItClient(binding, address);
			var medias = rentIt.GetAlsoRentedItems(mediaId);
			MediaInfo mediaInf;

			MediaInfo[] primaryList;
			MediaInfo[] secondaryList;
			MediaInfo[] tertiaryList;
			switch (prioritizedMediaType)
			{
				case MediaType.Album:
					primaryList = medias.Albums;
					secondaryList = medias.Movies;
					tertiaryList = medias.Books;
					mediaInf = rentIt.GetAlbumInfo(mediaId);
					break;
				case MediaType.Book:
					primaryList = medias.Books;
					secondaryList = medias.Movies;
					tertiaryList = medias.Albums;
					mediaInf = rentIt.GetBookInfo(mediaId);
					break;
				case MediaType.Movie:
					primaryList = medias.Movies;
					secondaryList = medias.Books;
					tertiaryList = medias.Albums;
					mediaInf = rentIt.GetMovieInfo(mediaId);
					break;
				default:
					primaryList = new MediaInfo[0];
					secondaryList = new MediaInfo[0];
					tertiaryList = new MediaInfo[0];
					mediaInf = new MediaInfo { Title = "NOT AVAILABLE" };
					break;
			}
			label1.Text = @"Customers who rented" + Environment.NewLine + mediaInf.Title + @" also rented:";
			foreach (var mediaInfo in primaryList)
				listBox1.Items.Add(mediaInfo.Title);
			foreach (var mediaInfo in secondaryList)
				listBox1.Items.Add(mediaInfo.Title);
			foreach (var mediaInfo in tertiaryList)
				listBox1.Items.Add(mediaInfo.Title);
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
