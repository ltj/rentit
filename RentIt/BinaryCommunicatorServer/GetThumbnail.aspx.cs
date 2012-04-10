using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    using System.IO;

    using RentIt;

    using RentItDatabase;

    public partial class GetThumbnail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int mediaId = int.Parse(Request.QueryString["mediaId"]);

            var db = new RentItDatabaseDataContext();
            if (!db.Medias.Exists(media => media.id == mediaId))
            {
                Response.StatusCode = 400;
                Response.StatusDescription = "The requested media does not exist.";
                Response.End();
            }

            byte[] thumbnail = db.Medias.Where(media => media.id == mediaId).First().thumbnail;

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", "thumbnail.jpg"));
            Response.AddHeader("Content-Length", thumbnail.Length.ToString());
            Response.BinaryWrite(thumbnail);
            Response.End();
        }
    }
}