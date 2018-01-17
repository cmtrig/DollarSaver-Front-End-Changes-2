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

namespace DollarSaver.Web.controls {
    public partial class NavMenu : System.Web.UI.UserControl {
        

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);


            StationPageBase parent = (StationPageBase)Page;

            MenuItem homeItem = new MenuItem("Home", "0", null, parent.GetUrl("~/Default.aspx"));
            navMenu.Items.Add(homeItem);

            foreach (DollarSaverDB.CategoryRow category in parent.Station.PrimaryCategories) {
                MenuItem item = new MenuItem(category.Name, category.CategoryId.ToString(), null, "~/Category.aspx?category_id=" + category.CategoryId);

                foreach (DollarSaverDB.CategoryRow child in category.SubCategories) {
                    MenuItem childItem = new MenuItem(child.Name, child.CategoryId.ToString(), null, "~/Category.aspx?category_id=" + child.CategoryId);

                    item.ChildItems.Add(childItem);
                }

                navMenu.Items.Add(item);

            }
            
        }

    }
}
