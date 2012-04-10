using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    using System.Data.Linq;
    using System.Diagnostics;
    using System.IO;

    using RentIt;

    using RentItDatabase;

    public partial class UploadControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int mediaId = int.Parse(Request.QueryString["mediaId"]);
            string mediaExtension = Request.QueryString["extension"];
            string userName = Request.QueryString["userName"];
            string password = Request.QueryString["password"];

            Debug.WriteLine("Query string accepted.");

            // Check if the user name is a publisher account.
            var db = new RentItDatabase.RentItDatabaseDataContext();
            if (!db.Publisher_accounts.Exists(acc => acc.user_name.Equals(userName)))
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "The specified account is not authorized to upload media.";
                Response.End();
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

            // Get the uploaded file and.
            HttpPostedFile uploadedFile = Request.Files.Get(0);
            var memoryStream = new MemoryStream();
            byte[] binary;

            try
            {
                uploadedFile.InputStream.CopyTo(memoryStream);
                binary = memoryStream.ToArray();
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "Media upload failed. Please try again.";
                Response.End();
                return;
            }

            // Create a new Media_file entry in the database and upload the file to
            // the database.
            RentItDatabase.Media_file mediaFile = new Media_file()
                {
                    name = mediaId.ToString(),
                    extension = mediaExtension,
                    data = binary
                };
            db.Media_files.InsertOnSubmit(mediaFile);
            db.SubmitChanges();

            RentItDatabase.Media mediaInfo = (from m in db.Medias
                                              where m.id == mediaId
                                              select m).First();
            mediaInfo.Media_file = mediaFile;
            db.SubmitChanges();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(@"C:\RentItServices\RentIt01\" + FileUpload1.FileName);
                UploadLabel.Text = "File uploaded: " + FileUpload1.FileName;

            }
            else
            {
                UploadLabel.Text = "No file oploaded.";
            }
        }
    }
}