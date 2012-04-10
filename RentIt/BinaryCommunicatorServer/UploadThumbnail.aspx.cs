using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    using System.Data.Linq;
    using System.IO;

    using RentIt;

    using RentItDatabase;

    public partial class UploadThumbnail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Stream inputStream = Request.InputStream;

            int mediaId = int.Parse(Request.QueryString["mediaId"]);
            // string mediaExtension = Request.QueryString["extension"];
            string userName = Request.QueryString["userName"];
            string password = Request.QueryString["password"];

            // Check if the user name is a publisher account.
            var db = new RentItDatabase.RentItDatabaseDataContext();
            if (!db.Publisher_accounts.Exists(acc => acc.user_name.Equals(userName)))
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "The specified account is not authorized to upload media.";
            }

            // Check if the metadata of the movie has been uploaded.
            if (!db.Medias.Exists(media => media.id == mediaId))
            {
                Response.StatusCode = 400;
                Response.StatusDescription =
                    "Metadata for the specified media has not been uploaded."
                    + "This is needed before it is possible to upload the data.";
                Response.End();
            }

            var client = new RentItClient();
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

            var memoryStream = new MemoryStream();
            byte[] thumbnail;

            try
            {
                inputStream.CopyTo(memoryStream);
                thumbnail = memoryStream.ToArray();
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "Thumbnail upload failed. Please try again.";
                Response.End();
                return;
            }

            RentItDatabase.Media mediaInfo = (from m in db.Medias
                                              where m.id == mediaId
                                              select m).First();
            mediaInfo.thumbnail = thumbnail;
            db.SubmitChanges();
        }
    }
}