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

using System.Data.SqlClient;
using System.Net.Mail;

using DollarSaver.Core.Common;
using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;
using DollarSaver.Core.Encryption;

namespace DollarSaver.Web.Admin {

    public partial class CertificateEdit : SalesPersonPageBase {

        private int certificateId = 0;
        private DollarSaverDB.CertificateRow certificate;

        protected void Page_Load(object sender, EventArgs e) {
            saveButton.Click += new EventHandler(saveButton_Click);
            cancelButton.Click += new EventHandler(cancelButton_Click);
            deleteButton.Click += new EventHandler(deleteButton_Click);
            deleteButton.Attributes["onclick"] = "javascript: return confirm('Are you sure want to delete this item?');";

            emailButton.Click += new EventHandler(emailButton_Click);
            addNumbersButton.Click += new EventHandler(addNumbersButton_Click);
            removeNumbersButton.Click += new EventHandler(removeNumbersButton_Click);

            createHistoryRepeater.ItemDataBound += new RepeaterItemEventHandler(createHistoryRepeater_ItemDataBound);
            availableNumberGrid.RowDataBound += new GridViewRowEventHandler(availableNumberGrid_RowDataBound);

            certificateId = GetIdFromQueryString();

            int advertiserId = 0;

            
            CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();

            if (certificateId > 0) {

                DollarSaverDB.CertificateDataTable certificateTable = certificateAdapter.GetCertificate(certificateId);

                if (certificateTable.Count != 1) {
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                certificate = certificateTable[0];

                advertiserId = certificate.AdvertiserId;

            } else {
                advertiserId = GetValueFromQueryString("advertiserId");
            }

            if (!Page.IsPostBack) {


                if (Station.StationSiteType == SiteType.DealOfTheWeek) {
                    standardDateHolder.Visible = false;
                    onSaleDateBoxRFV.Enabled = false;
                    onSaleDateList.Visible = true;

                    ArrayList onSaleDates = new ArrayList();
                    onSaleDates = certificateAdapter.GetDealDates(StationId);

                    onSaleDateList.DataSource = onSaleDates;
                    onSaleDateList.DataTextFormatString = "{0:MM/dd/yyyy hh:mm:ss tt}";
                    onSaleDateList.DataBind();

                    DateTime onSaleNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, Station.StationTimeZoneInfo);
                    onSaleNow = onSaleNow.AddDays(-14);

                    onSaleDateList.Items.Add(new ListItem("-- On Sale Now --", onSaleNow.ToString("MM/dd/yyyy hh:mm:ss tt")));

                } else {
                    standardDateHolder.Visible = true;
                    onSaleDateBoxRFV.Enabled = true;
                    onSaleDateList.Visible = false;

                    for (int i = 0; i <= 23; i++) {

                        String hour;

                        if (i % 12 == 0) {
                            hour = "12";
                        } else {
                            hour = (i % 12).ToString("00");
                        }

                        onSaleHourList.Items.Add(new ListItem(hour + " " + (i < 12 ? "AM" : "PM"), i.ToString("00")));
                    }

                    for (int i = 0; i <= 59; i++) {
                        onSaleMinuteList.Items.Add(new ListItem(i.ToString("00"), i.ToString("00")));
                    }
                }

                AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                DollarSaverDB.AdvertiserDataTable advertiserTable = advertiserAdapter.GetAdvertiser(advertiserId);

                if (advertiserTable.Count != 1) {
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                DollarSaverDB.AdvertiserRow advertiser = advertiserTable[0];

                if (advertiser.StationId != StationId) {
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                if (advertiser.IsDeleted) {
                    InfoMessage = "Sorry, this advertiser has been deleted";
                    Response.Redirect("~/admin/AdvertiserList.aspx");
                }

                discountList.Items.Add(new ListItem("Percentage", "1"));
                discountList.Items.Add(new ListItem("Flat Amount", "2"));


                advertiserNameLabel.Text = advertiser.Name;
                advertiserIdHidden.Value = advertiserId.ToString();

                if (certificateId > 0) {
                    createEditLabel.Text = "Edit";
                    newCertHolder.Visible = false;

                    idHidden.Value = certificateId.ToString();

                    nameBox.Text = certificate.ShortName;
                    descriptionLengthLabel.Text = certificate.Description.Length.ToString();
                    descriptionBox.Text = certificate.Description;
                    minPurchaseQtyBox.Text = certificate.MinPurchaseQty.ToString();
                    maxPurchaseQtyBox.Text = certificate.MaxPurchaseQty.ToString();

                    faceValueBox.Text = certificate.FaceValue.ToString("0.00");
                    discountBox.Text = certificate.Discount.ToString("0.00");
                    discountList.SelectedValue = certificate.DiscountTypeId.ToString();

                    if (CurrentUser.Role == AdminRole.Manager || CurrentUser.Role == AdminRole.SalesRep) {
                        faceValueBox.Enabled = false;
                        discountBox.Enabled = false;
                        discountList.Enabled = false;
                    }


                    deliveryList.SelectedValue = certificate.DeliveryTypeId.ToString();
                    deliveryNoteBox.Text = certificate.DeliveryNote;
                    isActiveBox.Checked = certificate.IsActive;
                    lowStockAmountBox.Text = certificate.LowStockAmount.ToString();
                    
                    if(numberLengthList.Items.FindByValue(certificate.NumberLength.ToString()) != null) {
                        numberLengthList.SelectedValue = certificate.NumberLength.ToString();
                    }

                    if (Station.StationSiteType == SiteType.DealOfTheWeek) {

                        ListItem dateItem = onSaleDateList.Items.FindByText(certificate.AdjustedOnSaleDate.ToString("MM/dd/yyyy hh:mm:ss tt}"));
                        if(dateItem != null) {
                            onSaleDateList.SelectedIndex = onSaleDateList.Items.IndexOf(dateItem);
                        } else {
                            onSaleDateList.Items.Insert(0, new ListItem(certificate.AdjustedOnSaleDate.ToString("MM/dd/yyyy hh:mm:ss tt"), 
                                certificate.AdjustedOnSaleDate.ToString("M/d/yyyy h:mm:ss tt")));
                        }

                    } else {
                        onSaleDateBox.Text = certificate.AdjustedOnSaleDate.ToString("MM/dd/yyyy");
                        onSaleHourList.SelectedValue = certificate.AdjustedOnSaleDate.Hour.ToString("00");
                        onSaleMinuteList.SelectedValue = certificate.AdjustedOnSaleDate.Minute.ToString("00");
                    }

                    CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();

                    ArrayList createDates = certificateNumberAdapter.GetDates(certificateId);

                    createDateList.DataSource = createDates;
                    createDateList.DataTextFormatString = "{0:MM/dd/yyyy}";
                    createDateList.DataBind();

                    createDateList.Items.Insert(0, new ListItem("-- All Dates --", ""));
                    
                    certificateNumberHolder.Visible = true;

                    createHistoryRepeater.DataSource = createDates;
                    createHistoryRepeater.DataBind();

                    if (certificate.DeliveryType == DeliveryType.Print) {
                        printableHolder.Visible = true;
                        notPrintableHolder.Visible = false;

                        String queryString = "sample_id=" + certificate.CertificateId;

                        String encryptedString = Server.UrlEncode(Cipher.Encrypt(queryString));

                        viewSampleLink.NavigateUrl = "~/ViewCertificate.aspx?x=" + encryptedString;
                         
                        BindNumbers();
                    } else {
                        printableHolder.Visible = false;
                        notPrintableHolder.Visible = true;

                        availableCountLabel.Text = certificate.AvailableNumbers.Count.ToString();
                        usedCountLabel.Text = certificate.UsedNumbers.Count.ToString();
                    }


                } else {
                    deleteButton.Visible = false;
                    saveButton.Text = "Create";
                    createEditLabel.Text = "Create";
                    certificateNumberHolder.Visible = false;
                    newCertHolder.Visible = true;
                    onSaleDateBox.Text = DateTime.Now.ToString("MM/dd/yyyy");

                }



            }

        }


        private void BindNumbers() {


            if (certificate.AvailableNumbers.Count > 0) {
                availableNumberGrid.Visible = true;
                noAvailableHolder.Visible = false;
                availableNumberGrid.Columns[0].HeaderText = "Available (" + certificate.AvailableNumbers.Count + ")";
                availableNumberGrid.DataSource = certificate.AvailableNumbers.Rows;
                availableNumberGrid.DataBind();

            } else {
                availableNumberGrid.Visible = false;
                noAvailableHolder.Visible = true;
            }

            if (certificate.UsedNumbers.Count > 0) {

                noneUsedLabel.Visible = false;
                usedLink.Visible = true;

                usedLink.NavigateUrl = "~/admin/CertificateUsed.aspx?id=" + certificate.CertificateId;
                usedLink.Text = "Sold (" + certificate.UsedNumbers.Count + ")";

            } else {
                noneUsedLabel.Visible = true;
                usedLink.Visible = false;
            }
        }


        void createHistoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                DateTime createDate = (DateTime)e.Item.DataItem;

                Label createDateLabel = (Label)e.Item.FindControl("createDateLabel");
                Label quantityLabel = (Label)e.Item.FindControl("quantityLabel");

                CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();

                int quantity = certificateNumberAdapter.GetByCertificateAndCreateDate(certificateId, createDate).Count;

                createDateLabel.Text = createDate.ToString("MM/dd/yyyy");
                quantityLabel.Text = quantity.ToString();
            }
        }


        void availableNumberGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DollarSaverDB.CertificateNumberRow certNumber = (DollarSaverDB.CertificateNumberRow)e.Row.DataItem;

                /*
                if (CurrentUser.Role == AdminRole.Root) {
                    HyperLink certificateNumberLink = (HyperLink)e.Row.FindControl("certificateNumberLink");
                    certificateNumberLink.Text = certNumber.Number;
                    //certificateNumberLink.NavigateUrl = "~/admin/ViewCertificate.aspx?id=" + certNumber.CertificateNumberId;

                    String queryString = "id=" + certNumber.CertificateNumberId + "&void=true";
                    String encryptedString = Server.UrlEncode(Cipher.Encrypt(queryString));
                    certificateNumberLink.NavigateUrl = "~/ViewCertificate.aspx?x=" + encryptedString;
                    
                    certificateNumberLink.Visible = true;
                } else {
                 * */
                    Label certificateNumberLabel = (Label)e.Row.FindControl("certificateNumberLabel");
                    certificateNumberLabel.Text = certNumber.Number;
                    certificateNumberLabel.Visible = true;
                //}

            }
        }

        void saveButton_Click(object sender, EventArgs e) {

            if (Page.IsValid) {

                int advertiserId = Int32.Parse(advertiserIdHidden.Value);
                String shortName = nameBox.Text.Trim();
                String description = descriptionBox.Text.Trim();

                if (shortName == String.Empty) {
                    ErrorMessage = "Name is required";
                    return;
                }

                if (shortName.Length > 100) {
                    shortName = shortName.Substring(0, 100);
                }

                if (description == String.Empty) {
                    ErrorMessage = "Description is required";
                    return;
                }

                if (description.Length > 500) {
                    description = description.Substring(0, 500);
                }


                int minPurchaseQty = 0;

                try {
                    minPurchaseQty = Int32.Parse(minPurchaseQtyBox.Text);
                } catch {
                    ErrorMessage = "Min Purchase Qty must be an integer";
                    return;
                }

                if (minPurchaseQty <= 0) {
                    ErrorMessage = "Min Purchase Qty must be a postive number";
                    return;
                }


                int maxPurchaseQty = 0;

                try {
                    maxPurchaseQty = Int32.Parse(maxPurchaseQtyBox.Text);
                } catch {
                    ErrorMessage = "Max Purchase Qty must be an integer";
                    return;
                }

                if (maxPurchaseQty < 0) {
                    ErrorMessage = "Min Purchase Qty must be greater than or equal to 0";
                    return;
                }





                decimal faceValue = 0;
                try {
                    faceValue = Decimal.Parse(faceValueBox.Text);
                } catch {
                    ErrorMessage = "Face Value must be a decimal";
                    return;
                }

                if (faceValue <= 0) {
                    ErrorMessage = "Face Value be a postive";
                    return;
                }

                decimal discount = 0;
                try {
                    discount = Decimal.Parse(discountBox.Text);
                } catch {
                    ErrorMessage = "Discount must be a decimal";
                    return;
                }

                if (discount <= 0) {
                    ErrorMessage = "Discount be a postive";
                    return;
                }

                int discountTypeId = Int32.Parse(discountList.SelectedValue);

                String onSaleDateStr = String.Empty;

                if (Station.StationSiteType == SiteType.DealOfTheWeek) {
                    onSaleDateStr = onSaleDateList.SelectedValue;
                } else {
                    onSaleDateStr = onSaleDateBox.Text.Trim();
                }

                if (onSaleDateStr == String.Empty) {
                    ErrorMessage = "On Sale Date is required";
                    return;
                }

                if (Station.StationSiteType == SiteType.Standard) {
                    onSaleDateStr += " " + onSaleHourList.SelectedValue + ":" + onSaleMinuteList.SelectedValue + ":00 ";
                }

                DateTime onSaleDate;
                try {
                    onSaleDate = Convert.ToDateTime(onSaleDateStr);
                } catch {
                    ErrorMessage = "On Sale Date must by in the format MM/DD/YYYY";
                    return;
                }

                onSaleDate = TimeZoneInfo.ConvertTime(onSaleDate, Station.StationTimeZoneInfo, TimeZoneInfo.Local);

                int lowStockAmount = 0;

                try {
                    lowStockAmount = Int32.Parse(lowStockAmountBox.Text);
                } catch {
                    ErrorMessage = "Low Stock Amount must be an integer";
                    return;
                }

                if (lowStockAmount < 0) {
                    ErrorMessage = "Low Stock Amount must be greater than or equal to 0";
                    return;
                }

                int numberLength = Convert.ToInt32(numberLengthList.SelectedValue);

                int deliveryTypeId = Convert.ToInt32(deliveryList.SelectedValue);
                string deliveryNote = deliveryNoteBox.Text.Trim();


                if (deliveryNote.Length > 500) {
                    deliveryNote = deliveryNote.Substring(0, 500);
                }

                bool isActive = isActiveBox.Checked;

                CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();

                if (idHidden.Value != String.Empty) {

                    certificateId = Int32.Parse(idHidden.Value);

                    certificate = certificateAdapter.GetCertificate(certificateId)[0];

                    certificate.ShortName = shortName;
                    certificate.Description = description;
                    certificate.MinPurchaseQty = minPurchaseQty;
                    certificate.MaxPurchaseQty = maxPurchaseQty;

                    certificate.FaceValue = faceValue;
                    certificate.Discount = discount;
                    certificate.DiscountTypeId = discountTypeId;
                    certificate.OnSaleDate = onSaleDate;
                    certificate.LowStockAmount = lowStockAmount;
                    certificate.NumberLength = numberLength;
                    certificate.DeliveryTypeId = deliveryTypeId;
                    certificate.DeliveryNote = deliveryNote;
                    certificate.IsActive = isActive;

                    certificateAdapter.Update(certificate);

                    CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();

                    InfoMessage = "Certificate updated";
                } else {

                    int quantity = 0;

                    try {
                        quantity = Int32.Parse(qtyBox.Text.Trim());
                    } catch {
                        ErrorMessage = "Quantity must be an integer";
                        return;
                    }

                    if (quantity <= 0) {
                        ErrorMessage = "Quantity must be a postive";
                        return;
                    }

                    certificateId = Convert.ToInt32(certificateAdapter.InsertPK(advertiserId, shortName, description,
                        minPurchaseQty, maxPurchaseQty, faceValue, discount, discountTypeId, deliveryTypeId, deliveryNote, isActive, 
                        onSaleDate, lowStockAmount, numberLength));


                    CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();
                    certificateNumberAdapter.Generate(certificateId, quantity);

                    InfoMessage = "Certificate created";
                }

                Response.Redirect("~/admin/AdvertiserEdit.aspx?id=" + advertiserId);
            }

        }

        void cancelButton_Click(object sender, EventArgs e) {
            int advertiserId = Int32.Parse(advertiserIdHidden.Value);
            Response.Redirect("~/admin/AdvertiserEdit.aspx?id=" + advertiserId);
        }

        void deleteButton_Click(object sender, EventArgs e) {

            int advertiserId = Int32.Parse(advertiserIdHidden.Value);

            if (idHidden.Value != String.Empty) {

                certificateId = Int32.Parse(idHidden.Value);

                try {

                    CertificateNumberTableAdapter certNumberAdapter = new CertificateNumberTableAdapter();
                    certNumberAdapter.DeleteByCertificate(certificateId);

                    CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                    certificateAdapter.Delete(certificateId);
                    Response.Redirect("~/admin/AdvertiserEdit.aspx?id=" + advertiserId);

                } catch (SqlException ex) {
                    if (ex.Number == 547) {
                        ErrorMessage = "Certificate cannot be deleted due to database constraints. De-activate it instead.";
                    } else {
                        throw ex;
                    }
                }
            }


        }


        void addNumbersButton_Click(object sender, EventArgs e) {
            int qty = 0;
            certificateId = Int32.Parse(idHidden.Value);

            try {
                qty = Int32.Parse(newQtyBox.Text);
            } catch { }

            if (qty > 0) {

                CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();
                certificateNumberAdapter.Generate(certificateId, qty);

                InfoMessage = qty + " numbers added";
                Response.Redirect("~/admin/CertificateEdit.aspx?id=" + certificateId);
            } else {
                InfoMessage = "Quantity must be a postitive integer";
            }
        }

        void removeNumbersButton_Click(object sender, EventArgs e) {
            int qty = 0;
            certificateId = Int32.Parse(idHidden.Value);

            try {
                qty = Int32.Parse(newQtyBox.Text);
            } catch { }

            if (qty > 0) {

                CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();
                int qtyRemoved = certificateNumberAdapter.Remove(certificateId, qty);

                InfoMessage = qtyRemoved + " numbers removed";
                Response.Redirect("~/admin/CertificateEdit.aspx?id=" + certificateId);
            } else {
                InfoMessage = "Quantity must be a postitive integer";
            }
        }

        void emailButton_Click(object sender, EventArgs e) {

            String emailAddress = emailBox.Text.Trim();

            if (emailAddress == String.Empty) {
                return;
            }


            RegexStringValidator regex = new RegexStringValidator(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            try {
                regex.Validate(emailAddress);
            } catch {
                ErrorMessage = "E-mail address is not valid";
                return;
            }


            String createDateStr = String.Empty;

            createDateStr = createDateList.SelectedValue;

            DollarSaverDB.CertificateNumberDataTable certificateNumbers;
            if (createDateStr == String.Empty) {
                certificateNumbers = certificate.AllNumbers;
                createDateStr = "All Dates";
            } else {

                DateTime createDate = DateTime.Now;
                try {
                    createDate = Convert.ToDateTime(createDateStr);
                    createDateStr = createDate.ToString("MM/dd/yyyy");
                } catch {
                    ErrorMessage = "Create Date must by in the format MM/DD/YYYY";
                    return;
                }

                CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();
                certificateNumbers = certificateNumberAdapter.GetByCertificateAndCreateDate(certificateId, createDate);


            }

            //MailMessage message = new MailMessage("matthewherro@gmail.com", "matthewherro@gmail.com"); 
            MailMessage message = new MailMessage("\"DollarSaver\" <auto-confirm@dollarsavershow.com>", emailAddress);


            message.IsBodyHtml = true;


            message.Subject = "Certificate Numbers For " + certificate.AdvertiserName;

            String body = "<html><body><span style=\"font-family: Verdana; font-size: 12pt;\">";
/*
            body += "<table cellpadding=\"3\" cellspacing=\"0\" border=\"0\" width=\"550px\">" + Environment.NewLine +
                "<tr><td align=\"center\" style=\"font-size: 18pt;\">Advertiser: " + certificate.Advertiser.Name + "</td></tr>" + Environment.NewLine +
                "<tr><td align=\"center\"><HR width=\"100%\" ></td></tr>" + Environment.NewLine +
                "<tr><td align=\"left\">Station: " + Station.Name + "</td></tr>" + Environment.NewLine +
                "<tr><td align=\"left\">Certificate: " + certificate.ShortName + "</td></tr>" + Environment.NewLine +
                "<tr><td align=\"left\">Create Date: " + createDateStr + "</td></tr>" + Environment.NewLine +
                "<tr><td align=\"left\">Quantity: " + certificateNumbers.Count + "</td></tr>" + Environment.NewLine +
                "<tr><td align=\"center\"><HR width=\"100%\" ></td></tr>" + Environment.NewLine +
                "<tr><td align=\"center\" style=\"font-size: 18pt;\"><U>CERTIFICATE NUMBERS</U></td></tr>" + Environment.NewLine +
                "<tr><td align=\"center\"><B>Cross out number when certificate is redeemed</B></td></tr>" + Environment.NewLine +
                "<tr><td align=\"center\"><B>--&gt; Read numbers Left to Right --&gt;</B></td></tr>" +
                "<tr><td align=\"center\"><HR width=\"100%\" ></td></tr>" + Environment.NewLine;
*/
            body += "<table cellpadding=\"3\" cellspacing=\"0\" border=\"0\" width=\"550px\">" + Environment.NewLine +
                "<tr><td align=\"center\" style=\"font-size: 14pt;\">" + certificateNumbers.Count + " Certificate Numbers for "  + certificate.Advertiser.Name + "</td></tr>" + Environment.NewLine +
                "<tr><td align=\"center\" style=\"font-size: 10pt;\">Certificate: " + certificate.ShortName + "</td></tr>" + Environment.NewLine +
                "<tr><td align=\"center\" style=\"font-size: 8pt;\"><B>Cross out number when certificate is redeemed</B> --&gt; Read numbers Left to Right --&gt;</B></td></tr>" +
                "<tr><td align=\"center\"><HR width=\"100%\" ></td></tr>" + Environment.NewLine;

            body += "<tr><td><table cellpadding=\"3\" cellspacing=\"0\" border=\"0\" width=\"100%\">";
            for (int i = 0; i < certificateNumbers.Count; i+=3) {
                body += "<tr><td style=\"font-size: 20pt\" align=\"center\">" + certificateNumbers[i].Number + "</td>" + Environment.NewLine;

                if (i + 1 < certificateNumbers.Count) {
                    body += "<td style=\"font-size: 20pt\" align=\"center\">" + certificateNumbers[i + 1].Number + "</td>" + Environment.NewLine;
                } else {
                    body += "<td style=\"font-size: 20pt\" align=\"center\">&nbsp;</td>" + Environment.NewLine;
                }
                if (i + 2 < certificateNumbers.Count) {
                    body += "<td style=\"font-size: 20pt\" align=\"center\">" + certificateNumbers[i + 2].Number + "</td></tr>" + Environment.NewLine;
                } else {
                    body += "<td style=\"font-size: 20pt\" align=\"center\">&nbsp;</td></tr>" + Environment.NewLine;
                }
            }
            body += "</table></tr></td>";

            body += "<tr><td align=\"center\"><HR width=\"100%\" ></td></tr></table></body></html>";

            // end message with a <CRLF>.<CRLF> ??

            message.Body = body;

            bool success = false;
            try {
                //SmtpClient smtp = new SmtpClient("localhost", 25);
                //smtp.Send(message);

                Mailer mailer = new Mailer();
                mailer.Send(message);
                
                InfoMessage = "Certificate Numbers sent to: " + emailAddress;
                success = true;
            } catch (Exception ex) {
                ErrorMessage = "Error Sending E-mail: " + ex.Message;
            }

            if (success) {
                Response.Redirect("~/admin/CertificateEdit.aspx?id=" + certificateId);
            }
        }
    }
}