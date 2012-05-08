using System;
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

	public partial class ListOfMedia : UserControl
	{
		public ListOfMedia()
		{
			InitializeComponent();
		}
		internal ListOfMedia(MediaType type, string listTitle, MediaCriteria criteria, bool numbering)
			: this()
		{
			BasicHttpBinding binding = new BasicHttpBinding();
			EndpointAddress address = new EndpointAddress("http://rentit.itu.dk/rentit01/RentItService.svc");
			var rentIt = new RentItClient(binding, address);
			var medias = rentIt.GetMediaItems(criteria);
			label1.Text = listTitle;

			MediaInfo[] list;
			switch (type)
			{
				case MediaType.Album:
					list = medias.Albums;
					break;
				case MediaType.Book:
					list = medias.Books;
					break;
				case MediaType.Song:
					list = medias.Songs;
					break;
				case MediaType.Movie:
					list = medias.Movies;
					break;
				default:
					list = new MediaInfo[0];
					break;
			}
			if (list.Count() > 0 && !numbering)
				for (int i = 0; i < list.Count() - 1; i++)
					listBox1.Items.Add(list[i].Title);

			if (list.Count() > 0 && numbering)
				for (int i = 0; i < list.Count() - 1; i++)
					listBox1.Items.Add((i + 1) + ". " + list[i].Title);
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
