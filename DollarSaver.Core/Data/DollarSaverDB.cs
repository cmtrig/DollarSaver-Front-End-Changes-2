using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using System.IO;
using System.Text.RegularExpressions;

using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Core.Data
{


    partial class DollarSaverDB
    {

        partial class IssueRow
        {

            private AdvertiserRow _advertiser = null;
            private StationRow _station = null;

            public AdvertiserRow Advertiser
            {
                get
                {
                    if (_advertiser == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _advertiser = advertiserAdapter.GetAdvertiser(AdvertiserId)[0];
                    }

                    return _advertiser;
                }
            }

            public StationRow Station
            {
                get
                {
                    if (_station == null)
                    {
                        StationTableAdapter stationAdapter = new StationTableAdapter();
                        _station = stationAdapter.GetStation(StationId)[0];
                    }

                    return _station;
                }
            }

            public String StationName
            {
                get { return Station.Name; }
            }

            public String StationShortName
            {
                get { return Station.ShortName; }
            }

            public String FullName
            {
                get
                {
                    return FirstName + " " + LastName;
                }
            }

        }

        partial class StationContentDataTable
        {
        }

        partial class StationContentRow
        {
            private StationRow _station = null;

            public StationRow Station
            {
                get
                {
                    if (_station == null)
                    {
                        StationTableAdapter stationAdapter = new StationTableAdapter();
                        _station = stationAdapter.GetStation(StationId)[0];
                    }
                    return _station;
                }
            }


        }

        partial class CertificateDataTable
        {
        }

        partial class ParameterRow
        {

            private ParameterTypeRow _parameterType = null;

            public ParameterTypeRow ParameterType
            {
                get
                {
                    if (_parameterType == null)
                    {
                        ParameterTypeTableAdapter parameterTypeAdapter = new ParameterTypeTableAdapter();
                        _parameterType = parameterTypeAdapter.GetParameterType(ParameterTypeId)[0];
                    }
                    return _parameterType;
                }
            }


            private SqlParameter _runParameter;

            public SqlParameter RunParameter
            {
                get { return _runParameter; }
                set { _runParameter = value; }
            }
        }


        partial class ReportTypeRow
        {

            private AdminRoleDataTable _roles = null;

            public AdminRoleDataTable Roles
            {
                get
                {
                    if (_roles == null)
                    {
                        AdminRoleTableAdapter adapter = new AdminRoleTableAdapter();
                        _roles = adapter.GetByReportType(ReportTypeId);
                    }

                    return _roles;
                }
            }

        }


        partial class ReportRow
        {


            private ParameterDataTable _parameters = null;
            private ReportTypeRow _typeOfReport = null;

            public ParameterDataTable Parameters
            {
                get
                {
                    if (_parameters == null)
                    {

                        ParameterTableAdapter parameterAdapter = new ParameterTableAdapter();
                        _parameters = parameterAdapter.GetByReport(ReportId);
                    }
                    return _parameters;
                }
            }

            public ReportTypeRow TypeOfReport
            {
                get
                {
                    if (_typeOfReport == null)
                    {
                        ReportTypeTableAdapter adapter = new ReportTypeTableAdapter();
                        _typeOfReport = adapter.GetReportType(ReportTypeId)[0];
                    }

                    return _typeOfReport;
                }
            }

            public DataTable Execute(ArrayList parameters)
            {

                ReportTableAdapter reportAdapter = new ReportTableAdapter();

                SqlCommand cmd = reportAdapter.Connection.CreateCommand();

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = StoredProcedureName;

                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter();

                try
                {
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                    throw new Exception("Run query error: " + cmd.CommandText + " [" + ex.Message + "]");
                }
                finally
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                        {
                            cmd.Connection.Close();
                        }
                        cmd.Connection.Dispose();
                    }
                }

                return dt;
            }
        }



        partial class AdminDataTable
        {
        }

        partial class AdminRow
        {

            public AdminRole Role
            {
                get { return (AdminRole)AdminRoleId; }
            }

            public string RoleName
            {
                get
                {
                    return ((AdminRole)AdminRoleId).ToString();
                }
            }


        }

        partial class StationDataTable
        {
        }

        /// <summary>
        /// OrderLineItem
        /// </summary>
        partial class OrderLineItemDataTable
        {

            public OrderLineItemRow GetLineItem(int certificateId)
            {

                foreach (OrderLineItemRow lineItem in this.Rows)
                {
                    if (lineItem.CertificateId == certificateId)
                    {
                        return lineItem;
                    }
                }
                return null;


            }


            public decimal SubTotal
            {
                get
                {
                    decimal subTotal = 0.0M;

                    foreach (OrderLineItemRow item in this)
                    {
                        subTotal += item.Total;
                    }

                    return subTotal;
                }
            }

            public bool Contains(int id)
            {
                foreach (OrderLineItemRow item in this.Rows)
                {
                    if (item.OrderLineItemId == id)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        partial class OrderLineItemRow
        {

            private OrderRow _order = null;
            private CertificateRow _certificate = null;
            private CertificateNumberDataTable _numbers = null;

            public OrderRow Order
            {
                get
                {
                    if (_order == null)
                    {
                        OrderTableAdapter orderAdapter = new OrderTableAdapter();
                        _order = orderAdapter.GetOrder(OrderId)[0];
                    }
                    return _order;
                }
            }

            public CertificateRow Certificate
            {
                get
                {
                    if (_certificate == null)
                    {
                        CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                        _certificate = certificateAdapter.GetCertificate(CertificateId)[0];
                    }
                    return _certificate;
                }
            }

            public CertificateNumberDataTable Numbers
            {
                get
                {
                    if (_numbers == null)
                    {
                        CertificateNumberTableAdapter numberAdapter = new CertificateNumberTableAdapter();
                        _numbers = numberAdapter.GetByLineItem(OrderLineItemId);
                    }
                    return _numbers;
                }
            }

            public decimal DiscountValue
            {
                get
                {
                    if (DiscountTypeId == 1)
                    {
                        // percentage
                        return FaceValue - Math.Round(FaceValue * (Discount / 100), 2);
                    }
                    else
                    {
                        // flat rate
                        return FaceValue - Discount;
                    }
                }
            }

            public decimal Total
            {
                get
                {
                    return DiscountValue * Quantity;
                }
            }


            public DeliveryType DeliveryType
            {
                get
                {
                    return (DeliveryType)DeliveryTypeId;
                }
            }

        }
        /*
        partial class ReturnedOrderLineItem1DataTable {

            public ReturnedOrderLineItemRow GetReturnedItem(int certificateId) {

                foreach (ReturnedOrderLineItemRow returnedItem in this.Rows) {
                    if (returnedItem.CertificateId == certificateId) {
                        return returnedItem;
                    }
                }
                return null;
            }
        }

        partial class ReturnedOrderLineItemRow {

            private OrderRow _order = null;
            private CertificateRow _certificate = null;

            public OrderRow Order {
                get {
                    if (_order == null) {
                        OrderTableAdapter orderAdapter = new OrderTableAdapter();
                        _order = orderAdapter.GetOrder(OrderId)[0];
                    }
                    return _order;
                }
            }

            public CertificateRow Certificate {
                get {
                    if (_certificate == null) {
                        CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                        _certificate = certificateAdapter.GetCertificate(CertificateId)[0];
                    }
                    return _certificate;
                }
            }

            public decimal DiscountValue {
                get {
                    if (DiscountTypeId == 1) {
                        // percentage
                        return FaceValue - (FaceValue * (Discount / 100));
                    } else {
                        // flat rate
                        return FaceValue - Discount;
                    }
                }
            }

            public decimal Total {
                get {
                    return DiscountValue * Quantity;
                }
            }

        }
        */

        /// <summary>
        /// Order
        /// </summary>
        partial class OrderDataTable
        {
        }


        partial class OrderRow
        {

            private OrderLineItemDataTable _lineItems = null;
            private OrderLineItemDataTable _returnedLineItems = null;
            private StationRow _station = null;

            public OrderLineItemDataTable LineItems
            {
                get
                {
                    if (_lineItems == null)
                    {
                        OrderLineItemTableAdapter lineItemAdapter = new OrderLineItemTableAdapter();
                        _lineItems = lineItemAdapter.GetByOrder(OrderId);
                    }

                    return _lineItems;
                }
            }

            public OrderLineItemDataTable ReturnedLineItems
            {
                get
                {
                    if (_returnedLineItems == null)
                    {
                        OrderLineItemTableAdapter lineItemAdapter = new OrderLineItemTableAdapter();
                        _returnedLineItems = lineItemAdapter.GetReturned(OrderId);
                    }

                    return _returnedLineItems;
                }
            }

            public StationRow Station
            {
                get
                {
                    if (_station == null)
                    {
                        StationTableAdapter stationAdapter = new StationTableAdapter();
                        _station = stationAdapter.GetStation(StationId)[0];
                    }

                    return _station;
                }
            }

            public String StationName
            {
                get { return Station.Name; }
            }

            public String StationShortName
            {
                get { return Station.ShortName; }
            }

            public string BillingName
            {
                get
                {
                    string name = string.Empty;
                    if (!IsBillingFirstNameNull())
                    {
                        name += BillingFirstName + " ";
                    }

                    if (!IsBillingLastNameNull())
                    {
                        name += BillingLastName;
                    }

                    return name.Trim();
                }

            }

            public string BillingAddress
            {
                get
                {
                    string address = string.Empty;
                    if (!IsBillingAddress1Null())
                    {
                        address += BillingAddress1 + Environment.NewLine;
                    }
                    if (!IsBillingAddress2Null())
                    {
                        address += BillingAddress2 + Environment.NewLine;
                    }
                    if (!IsBillingCityNull())
                    {
                        address += BillingCity;
                        if (!IsBillingStateCodeNull())
                        {
                            address += ",";
                        }
                        address += " ";
                    }
                    if (!IsBillingStateCodeNull())
                    {
                        address += BillingStateCode + " ";
                    }
                    if (!IsBillingZipCodeNull())
                    {
                        address += BillingZipCode;
                    }
                    return address;
                }

            }

            public string Status
            {
                get
                {
                    return ((OrderStatus)OrderStatusId).ToString();
                }
            }

            public string DisplayTotal
            {
                get
                {
                    if (IsGrandTotalNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return GrandTotal.ToString("$#,0.00");
                    }
                }
            }

            public String ShippingInfo
            {
                get
                {

                    String shippingAddress = String.Empty;

                    if (!IsShippingFirstNameNull())
                    {
                        shippingAddress += ShippingFirstName;
                    }

                    if (!IsShippingLastNameNull())
                    {
                        if (shippingAddress.Length > 0)
                        {
                            shippingAddress += " ";
                        }

                        shippingAddress += ShippingLastName + Environment.NewLine;
                    }
                    else
                    {
                        shippingAddress += Environment.NewLine;
                    }

                    if (!IsShippingAddress1Null() && ShippingAddress1 != String.Empty)
                    {
                        shippingAddress += ShippingAddress1 + Environment.NewLine;
                    }

                    if (!IsShippingAddress2Null() && ShippingAddress2 != String.Empty)
                    {
                        shippingAddress += ShippingAddress2 + Environment.NewLine;
                    }

                    if (!IsShippingCityNull() && ShippingCity != String.Empty)
                    {
                        shippingAddress += ShippingCity;
                    }

                    if (!IsShippingStateCodeNull())
                    {
                        if (!IsShippingCityNull())
                        {
                            shippingAddress += ", ";
                        }
                        shippingAddress += ShippingStateCode + " ";
                    }

                    if (!IsShippingZipCodeNull())
                    {
                        shippingAddress += ShippingZipCode;
                    }

                    if (!IsShippingPhoneNull())
                    {
                        if (shippingAddress.Length > 0)
                        {
                            shippingAddress += Environment.NewLine;
                        }
                        shippingAddress += ShippingPhone;
                    }


                    return shippingAddress;
                }
            }

            public bool PrintingRequired
            {
                get
                {
                    bool required = false;
                    if (PrintItems.Count > 0)
                    {
                        required = true;
                    }

                    return required;
                }
            }

            public bool ShippingRequired
            {
                get
                {
                    bool required = false;
                    if (ShipItems.Count > 0)
                    {
                        required = true;
                    }

                    return required;
                }
            }

            public bool PickUpRequired
            {
                get
                {
                    bool required = false;
                    if (PickUpItems.Count > 0)
                    {
                        required = true;
                    }

                    return required;
                }
            }

            public OrderLineItemDataTable PrintItems
            {
                get
                {
                    OrderLineItemDataTable printItems = new OrderLineItemDataTable();

                    foreach (OrderLineItemRow item in LineItems)
                    {
                        if (item.DeliveryTypeId == (int)DeliveryType.Print)
                        {
                            printItems.ImportRow(item);
                        }
                    }

                    return printItems;
                }
            }

            public OrderLineItemDataTable ShipItems
            {
                get
                {
                    OrderLineItemDataTable shipItems = new OrderLineItemDataTable();

                    foreach (OrderLineItemRow item in LineItems)
                    {
                        if (item.DeliveryTypeId == (int)DeliveryType.Ship)
                        {
                            shipItems.ImportRow(item);
                        }
                    }

                    return shipItems;
                }
            }


            public OrderLineItemDataTable PickUpItems
            {
                get
                {
                    OrderLineItemDataTable pickUpItems = new OrderLineItemDataTable();

                    foreach (OrderLineItemRow item in LineItems)
                    {
                        if (item.DeliveryTypeId == (int)DeliveryType.PickUp)
                        {
                            pickUpItems.ImportRow(item);
                        }
                    }

                    return pickUpItems;
                }
            }

            private DateTime _adjustedOrderDate = new DateTime();

            public DateTime AdjustedOrderDate
            {
                get
                {
                    if (_adjustedOrderDate == new DateTime())
                    {
                        _adjustedOrderDate = TimeZoneInfo.ConvertTime(OrderDate, TimeZoneInfo.Local, Station.StationTimeZoneInfo);
                    }
                    return _adjustedOrderDate;
                }
            }

        }




        /// <summary>
        /// Sales Person
        /// </summary>
        partial class SalesPersonDataTable
        {
        }

        partial class SalesPersonRow
        {

            public string FullName
            {
                get
                {
                    return FirstName + " " + LastName;
                }
            }

        }


        partial class CertificateNumberDataTable
        {
        }

        partial class CertificateNumberRow
        {
            private CertificateRow _certificate = null;
            private OrderLineItemRow _lineItem = null;

            public CertificateRow Certificate
            {
                get
                {
                    if (_certificate == null)
                    {
                        CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                        _certificate = certificateAdapter.GetCertificate(CertificateId)[0];
                    }
                    return _certificate;
                }

            }

            public OrderLineItemRow LineItem
            {

                get
                {
                    if (!IsOrderLineItemIdNull())
                    {
                        OrderLineItemTableAdapter lineItemAdapter = new OrderLineItemTableAdapter();
                        _lineItem = lineItemAdapter.GetOrderLineItem(OrderLineItemId)[0];

                    }

                    return _lineItem;
                }
            }

            public int OrderId
            {
                get
                {
                    return LineItem.OrderId;
                }
            }

            public string AdvertiserName
            {
                get { return Certificate.Advertiser.Name; }
            }
        }

        partial class CategoryDataTable
        {
        }

        partial class CategoryRow
        {

            private CategoryDataTable _subCategories = null;
            private AdvertiserDataTable _advertisers = null;
            private AdvertiserDataTable _activeAdvertisers = null;
            private AdvertiserDataTable _inactiveAdvertisers = null;


            public CategoryDataTable SubCategories
            {
                get
                {
                    if (_subCategories == null)
                    {

                        CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

                        _subCategories = categoryAdapter.GetByParent(this.CategoryId);

                    }

                    return _subCategories;

                }
            }

            public AdvertiserDataTable Advertisers
            {
                get
                {
                    if (_advertisers == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _advertisers = advertiserAdapter.GetByCategory(CategoryId);
                    }
                    return _advertisers;
                }
            }

            public AdvertiserDataTable ActiveAdvertisers
            {
                get
                {
                    if (_activeAdvertisers == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _activeAdvertisers = advertiserAdapter.GetActiveByCategory(CategoryId);
                    }
                    return _activeAdvertisers;
                }
            }

            public AdvertiserDataTable InactiveAdvertisers
            {
                get
                {
                    if (_inactiveAdvertisers == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _inactiveAdvertisers = advertiserAdapter.GetInactiveByCategory(CategoryId);
                    }
                    return _inactiveAdvertisers;
                }
            }

            public int DisplaySeqNo
            {
                get { return SeqNo + 1; }
            }

        }

        partial class CertificateRow
        {

            private DollarSaverDB.AdvertiserRow _advertiser = null;
            private DollarSaverDB.CertificateNumberDataTable _allNumbers = null;
            private DollarSaverDB.CertificateNumberDataTable _availableNumbers = null;
            private DollarSaverDB.CertificateNumberDataTable _usedNumbers = null;

            public DollarSaverDB.AdvertiserRow Advertiser
            {
                get
                {
                    if (_advertiser == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _advertiser = (DollarSaverDB.AdvertiserRow)advertiserAdapter.GetAdvertiser(this.AdvertiserId).Rows[0];
                    }
                    return _advertiser;
                }
            }

            public String AdvertiserName
            {
                get { return Advertiser.Name; }
            }


            public decimal DiscountValue
            {
                get
                {
                    if (DiscountTypeId == 1)
                    {
                        // percentage
                        return FaceValue - Math.Round(FaceValue * (Discount / 100), 2);
                    }
                    else
                    {
                        // flat rate
                        return FaceValue - Discount;
                    }
                }
            }

            public String Savings
            {

                get
                {
                    if (DiscountTypeId == 1)
                    {
                        // percentage
                        return Math.Floor(Discount) + "%";
                    }
                    else
                    {
                        // flat rate
                        return Discount.ToString("$#,0.00");
                    }
                }
            }

            public int QtyRemaining
            {
                get { return AvailableNumbers.Count; }
            }

            public int QtyUsed
            {
                get { return UsedNumbers.Count; }
            }

            public DeliveryType DeliveryType
            {
                get
                {
                    return (DeliveryType)DeliveryTypeId;
                }
            }

            public DollarSaverDB.CertificateNumberDataTable AllNumbers
            {
                get
                {
                    if (_allNumbers == null)
                    {
                        CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();
                        _allNumbers = certificateNumberAdapter.GetAllByCertificate(CertificateId);
                    }
                    return _allNumbers;
                }
            }


            public DollarSaverDB.CertificateNumberDataTable AvailableNumbers
            {
                get
                {
                    if (_availableNumbers == null)
                    {
                        CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();
                        _availableNumbers = certificateNumberAdapter.GetAvailableByCertificate(CertificateId);
                    }
                    return _availableNumbers;
                }
            }

            public DollarSaverDB.CertificateNumberDataTable UsedNumbers
            {
                get
                {
                    if (_usedNumbers == null)
                    {
                        CertificateNumberTableAdapter certificateNumberAdapter = new CertificateNumberTableAdapter();
                        _usedNumbers = certificateNumberAdapter.GetUsedByCertificate(CertificateId);
                    }
                    return _usedNumbers;
                }
            }

            public string AdvertiserAndCertificate
            {
                get
                {
                    return Advertiser.Name + " - " + ShortName;
                }
            }

            private DateTime _adjustedOnSaleDate = new DateTime();

            public DateTime AdjustedOnSaleDate
            {
                get
                {
                    if (_adjustedOnSaleDate == new DateTime())
                    {
                        _adjustedOnSaleDate = TimeZoneInfo.ConvertTime(OnSaleDate, TimeZoneInfo.Local, Advertiser.Station.StationTimeZoneInfo);
                    }
                    return _adjustedOnSaleDate;
                }
            }

        }


        /// <summary>
        /// Advertiser
        /// </summary>
        partial class AdvertiserDataTable
        {
        }

        partial class AdvertiserRow
        {

            private StationRow _station = null;
            private CategoryRow _category = null;
            private CategoryDataTable _subCategories = null;
            private CertificateDataTable _activeCertificates = null;
            private CertificateDataTable _onSaleCertificates = null;
            private CertificateDataTable _inactiveCertificates = null;
            private CertificateDataTable _allCertificates = null;
            private SalesPersonRow _salesPerson = null;

            public StationRow Station
            {
                get
                {
                    if (_station == null)
                    {
                        StationTableAdapter stationAdapter = new StationTableAdapter();
                        _station = stationAdapter.GetStation(StationId)[0];
                    }
                    return _station;
                }
            }

            public CategoryRow AdvertiserCategory
            {
                get
                {
                    if (_category == null)
                    {
                        CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();

                        _category = categoryAdapter.GetCategory(CategoryId)[0];

                    }

                    return _category;
                }


            }

            public CategoryDataTable SubCategories
            {
                get
                {
                    if (_subCategories == null)
                    {
                        CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                        _subCategories = categoryAdapter.GetSubCategoriesByAdvertiser(AdvertiserId);
                    }
                    return _subCategories;
                }
            }

            public CertificateDataTable ActiveCertificates
            {
                get
                {
                    if (_activeCertificates == null)
                    {
                        CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                        _activeCertificates = certificateAdapter.GetActiveByAdvertiser(AdvertiserId);
                    }
                    return _activeCertificates;
                }
            }

            public CertificateDataTable OnSaleCertificates
            {
                get
                {
                    if (_onSaleCertificates == null)
                    {
                        CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                        _onSaleCertificates = certificateAdapter.GetOnSaleByAdvertiser(AdvertiserId);
                    }
                    return _onSaleCertificates;
                }
            }

            public CertificateDataTable InactiveCertificates
            {
                get
                {
                    if (_inactiveCertificates == null)
                    {
                        CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                        _inactiveCertificates = certificateAdapter.GetInactiveByAdvertiser(AdvertiserId);
                    }
                    return _inactiveCertificates;
                }
            }

            public CertificateDataTable AllCertificates
            {
                get
                {
                    if (_allCertificates == null)
                    {
                        CertificateTableAdapter certificateAdapter = new CertificateTableAdapter();
                        _allCertificates = certificateAdapter.GetByAdvertiser(AdvertiserId);
                    }
                    return _allCertificates;
                }
            }

            public SalesPersonRow SalesPerson
            {
                get
                {
                    if (_salesPerson == null)
                    {
                        if (!IsSalesPersonIdNull())
                        {
                            SalesPersonTableAdapter salesPersonAdapter = new SalesPersonTableAdapter();
                            _salesPerson = salesPersonAdapter.GetSalesPerson(SalesPersonId)[0];
                        }
                    }

                    return _salesPerson;
                }
            }

            public string LogoUrl
            {
                get
                {
                    if (!IsLogoImageNull() && LogoImage != string.Empty)
                    {
                        return "~/" + Station.ImageDirUrl + this.LogoImage;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }

            public string FullAddress
            {
                get
                {
                    string fullAddress = string.Empty;
                    if (!IsAddress1Null() && this.Address1 != string.Empty)
                    {
                        fullAddress += this.Address1 + "<br />";
                    }
                    if (!IsAddress2Null() && this.Address2 != string.Empty)
                    {
                        fullAddress += this.Address2 + "<br />";
                    }
                    if (!IsCityNull() && this.City != string.Empty)
                    {
                        fullAddress += this.City + ", ";
                    }
                    if (!IsStateCodeNull() && this.StateCode != string.Empty)
                    {
                        fullAddress += this.StateCode + " ";
                    }
                    if (!IsZipCodeNull() && this.ZipCode != string.Empty)
                    {
                        fullAddress += this.ZipCode;
                    }

                    return fullAddress;
                }
            }

            public string InlineAddress
            {
                get
                {
                    string inlineAddress = string.Empty;
                    if (!IsAddress1Null() && this.Address1 != string.Empty)
                    {
                        inlineAddress += this.Address1;
                    }
                    if (!IsAddress2Null() && this.Address2 != string.Empty)
                    {
                        if (inlineAddress != string.Empty)
                        {
                            inlineAddress += " ";
                        }
                        inlineAddress += this.Address2;
                    }
                    if (!IsCityNull() && this.City != string.Empty)
                    {
                        if (inlineAddress != string.Empty)
                        {
                            inlineAddress += ", ";
                        }
                        inlineAddress += this.City;
                    }
                    if (!IsStateCodeNull() && this.StateCode != string.Empty)
                    {
                        if (inlineAddress != string.Empty)
                        {
                            inlineAddress += ", ";
                        }
                        inlineAddress += this.StateCode;
                    }
                    if (!IsZipCodeNull() && this.ZipCode != string.Empty)
                    {
                        if (inlineAddress != string.Empty)
                        {
                            inlineAddress += " ";
                        }
                        inlineAddress += this.ZipCode;
                    }

                    return inlineAddress;
                }
            }

            public string CategoryName
            {
                get
                {
                    return AdvertiserCategory.Name;
                }
            }

            public string DisplayAddress1
            {
                get
                {
                    if (IsAddress1Null())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return Address1;
                    }
                }
            }

            public string DisplayAddress2
            {
                get
                {
                    if (IsAddress2Null())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return Address2;
                    }
                }
            }

            public string DisplayCity
            {
                get
                {
                    if (IsCityNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return City;
                    }
                }
            }

            public string DisplayStateCode
            {
                get
                {
                    if (IsStateCodeNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return StateCode;
                    }
                }
            }

            public string DisplayZipCode
            {
                get
                {
                    if (IsZipCodeNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return ZipCode;
                    }
                }
            }

            public string DisplayDescription
            {
                get
                {
                    if (IsDescriptionNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return Description;
                    }
                }
            }

            public string DisplayLogoImage
            {
                get
                {
                    if (IsLogoImageNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return LogoImage;
                    }
                }
            }

            public string DisplayPhoneNumber
            {
                get
                {
                    if (IsPhoneNumberNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return PhoneNumber;
                    }
                }
            }

            public string DisplayWebsiteUrl
            {
                get
                {
                    if (IsWebsiteUrlNull())
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return WebsiteUrl;
                    }
                }
            }

            public String DescriptionPlainText
            {
                get
                {
                    String descriptionPlainText = String.Empty;
                    if (!IsDescriptionNull() && Description != String.Empty)
                    {
                        descriptionPlainText = Regex.Replace(Description, "<[^>]+>", "");
                    }

                    return descriptionPlainText;
                }
            }

        }


        /// <summary>
        /// Station
        /// </summary>
        partial class StationRow
        {

            private CategoryDataTable _primaryCategories = null;
            private SalesPersonDataTable _salesPeople = null;
            private SalesPersonDataTable _activeSalesPeople = null;
            private AdvertiserDataTable _advertisers = null;
            private AdvertiserDataTable _activeAdvertisers = null;
            private AdvertiserDataTable _inactiveAdvertisers = null;
            private TimeZoneRow _timeZone = null;
            private TimeZoneInfo _timeZoneInfo = null;
            private StationContentRow _content = null;
            private AdminDataTable _contacts = null;

            public CategoryDataTable PrimaryCategories
            {
                get
                {
                    if (_primaryCategories == null)
                    {
                        CategoryTableAdapter categoryAdapter = new CategoryTableAdapter();
                        _primaryCategories = categoryAdapter.GetPrimaryCategoriesByStation(StationId);
                    }

                    return _primaryCategories;
                }
            }

            public SalesPersonDataTable SalesPeople
            {
                get
                {
                    if (_salesPeople == null)
                    {
                        SalesPersonTableAdapter adapter = new SalesPersonTableAdapter();
                        _salesPeople = adapter.GetByStation(StationId);
                    }

                    return _salesPeople;
                }

            }

            public SalesPersonDataTable ActiveSalesPeople
            {
                get
                {
                    if (_activeSalesPeople == null)
                    {
                        SalesPersonTableAdapter adapter = new SalesPersonTableAdapter();
                        _activeSalesPeople = adapter.GetActiveByStation(StationId);
                    }

                    return _activeSalesPeople;
                }

            }




            public AdvertiserDataTable Advertisers
            {
                get
                {
                    if (_advertisers == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _advertisers = advertiserAdapter.GetByStation(StationId);
                    }
                    return _advertisers;
                }

            }


            public AdvertiserDataTable ActiveAdvertisers
            {
                get
                {
                    if (_activeAdvertisers == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _activeAdvertisers = advertiserAdapter.GetActiveByStation(StationId);
                    }
                    return _activeAdvertisers;
                }
            }
            public AdvertiserDataTable InactiveAdvertisers
            {
                get
                {
                    if (_inactiveAdvertisers == null)
                    {
                        AdvertiserTableAdapter advertiserAdapter = new AdvertiserTableAdapter();
                        _inactiveAdvertisers = advertiserAdapter.GetInactiveByStation(StationId);
                    }
                    return _inactiveAdvertisers;
                }
            }

            public TimeZoneRow StationTimeZone
            {
                get
                {
                    if (_timeZone == null)
                    {
                        TimeZoneTableAdapter timeZoneAdapter = new TimeZoneTableAdapter();
                        _timeZone = timeZoneAdapter.GetTimeZone(TimeZoneId)[0];
                    }
                    return _timeZone;
                }
            }

            public TimeZoneInfo StationTimeZoneInfo
            {
                get
                {
                    if (_timeZoneInfo == null && StationTimeZone != null)
                    {
                        try
                        {
                            _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(StationTimeZone.Name);
                        }
                        catch { }
                    }
                    return _timeZoneInfo;
                }
            }

            public StationContentRow Content
            {
                get
                {
                    if (_content == null)
                    {
                        StationContentTableAdapter adapter = new StationContentTableAdapter();
                        StationContentDataTable contentTable = adapter.GetStationContent(StationId);

                        _content = contentTable[0];

                    }

                    return _content;
                }

            }

            public AdminDataTable Contacts
            {
                get
                {
                    if (_contacts == null)
                    {
                        AdminTableAdapter adminAdapter = new AdminTableAdapter();
                        _contacts = adminAdapter.GetContacts(StationId);
                    }
                    return _contacts;
                }
            }

            public String ShortName
            {
                get
                {
                    String shortName = Name;
                    if (Name.Length > 30)
                    {
                        shortName = Name.Substring(0, 27).Trim() + "...";
                    }
                    return shortName;
                }
            }

            public SiteType StationSiteType
            {
                get { return (SiteType)SiteTypeId; }
            }

            public String SiteNamePlainText
            {
                get
                {
                    String siteNamePlainText = String.Empty;
                    if (!IsSiteNameNull() && SiteName != String.Empty)
                    {
                        siteNamePlainText = Regex.Replace(SiteName, "<[^>]+>", "");
                    }
                    else
                    {
                        siteNamePlainText = Name + " DollarSaver";
                    }

                    return siteNamePlainText;
                }
            }

            public String StationDirUrl
            {
                get { return "station/" + StationId + "/"; }
            }

            public String ImageDirUrl
            {
                get { return StationDirUrl + "images/"; }
            }
        }

        partial class StateRow
        {

            public String Summary
            {
                get
                {
                    return Name + " (" + StateCode + ")";
                }
            }

        }
    }

    public enum AdminRole
    {
        Root = 1,
        Admin = 2,
        Manager = 3,
        SalesRep = 4
    }

    public enum DeliveryType
    {
        Print = 1,
        Ship = 2,
        PickUp = 3
    }

    public enum OrderStatus
    {
        New = 1,
        Complete = 2,
        Returned = 3,
        Processing = 4
    }

    public enum PageHitType
    {
        HomePage = 1,
        CategoryPage = 2,
        AdvertiserPage = 3
    }

    public enum PaymentMethod
    {
        Visa = 1,
        MasterCard = 2,
        Amex = 3,
        Discover = 4,
        PayPal = 5
    }

    public enum SiteType
    {
        Standard = 1,
        DealOfTheWeek = 2
    }

    public enum StationType
    {
        Radio = 1,
        Televsion = 2,
        Other = 3
    }
}

namespace DollarSaver.Core.Data.DollarSaverDBTableAdapters {
    
    
    public partial class CategoryTableAdapter {
    }
}
