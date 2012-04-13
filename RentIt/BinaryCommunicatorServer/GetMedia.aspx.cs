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

            RentItClient client = new RentItClient();

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

            RentItDatabaseDataContext db = new RentItDatabaseDataContext();

            // Check if the requested media exists  
            if (!db.Medias.Exists(media => media.id == mediaId))
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "Requested media does not exist.";
                Response.End();
            }

            // Get the media metadata from the database.
            var mediaMetadata = (from media in db.Medias
                                 where media.id == mediaId
                                 select media).First();

            // Does any active rentals exist for the specified user?
            bool existsRentals = db.Rentals.Exists(
             rental => rental.user_name.Equals(userName) && rental.media_id == mediaId && rental.end_time > DateTime.Now);

            // Check if the requested media is rented by the user.
            if (mediaMetadata.price != 0 && !existsRentals)
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "The specified user has not currently an active rental of the requested media.";
                Response.End();
            }

            // Get the media data from the database.
            var mediaData = mediaMetadata.Media_file;

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", mediaMetadata.title + mediaData.extension));
            Response.AddHeader("Content-Length", mediaData.data.Length.ToString());
            Response.BinaryWrite(mediaData.data);
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