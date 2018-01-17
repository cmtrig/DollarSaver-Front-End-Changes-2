using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Web.Admin {

    public partial class StationEdit : ManagerPageBase {

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (ReadOnly) {
                Response.Redirect("~/admin/Default.aspx");
            }
        }


        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            stationControl.Station = Station;
        
        }

    }
}