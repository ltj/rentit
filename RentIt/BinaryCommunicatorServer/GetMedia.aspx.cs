using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    using System.Data.Linq;

    using RentIt;

    using RentItDatabase;

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Request.QueryString["userName"];
            string password = Request.QueryString["password"];
            int mediaId = int.Parse(Request.QueryString["mediaId"]);

            //Response.Write("Hello " + userName + "! :D");
            //Response.End();

            RentItClient client = new RentItClient();
            /*
            try
            {
                client.ValidateCredentials(
                    new AccountCredentials() { UserName = userName, HashedPassword = password });
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "Incorrect credentials";
                Response.End();
            }
            */
            RentItDatabaseDataContext db = new RentItDatabaseDataContext();
            /*
            // Check if the requested media exists  
            if (!db.Medias.Exists(media => media.id == mediaId))
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "Requested media does not exist.";
                Response.End();
            }

            // Check if the requested media is rented by the user.
            if (!db.Rentals.Exists(
                rental => rental.user_name.Equals(userName) && rental.media_id == mediaId && rental.end_time > DateTime.Now))
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "The specified user has not currently an active rental of the requested media.";
                Response.End();
            }
            */
            // Get the media from the database.
            //var mediaDataa = (from media in db.Medias where media.id.Equals(mediaId) select media);

            var mediaData = (from media in db.Medias
                             where media.id == mediaId
                             select media).First().Media_file;

            //select media.Media_file).First();
            //int count = mediaDataa.Count();
            //var mediaData = mediaDataa.First().Media_file;
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", "media" + mediaData.extension));
            Response.AddHeader("Content-Length", mediaData.data.Length.ToString());
            Response.BinaryWrite(mediaData.data);
            //Response.ContentType = "video/mpeg";
            Response.End();


            /*
            String filePath = @"C:\RentItServices\RentIt01\test.bin";

            var fileInfo = new System.IO.FileInfo(filePath);
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", filePath));
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.WriteFile(filePath);
            //Response.WriteFile(filePath);
            Response.End();
             * */
        }
    }
}