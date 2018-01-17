using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using com.paypal.sdk.profiles;
using com.paypal.sdk.services;

namespace DollarSaver.Web {
    public class PayPalAPI {

        
        private const string DEV_API_USERNAME = "matthe_1196197227_biz_api1.gmail.com";
        private const string DEV_API_PASSWORD = "1196197244";
        private const string DEV_API_SIGNATURE = "ANMq461H2Z8WSzo--.DJ.M8G61KqAm-.Kf62rRpLdeJBgIflgwdMOjjo";
        private const string DEV_ENVIRONMENT = "sandbox";


        private const string API_USERNAME = "BRIAN_api1.REVENUEWITHOUTRATINGS.COM";
        private const string API_PASSWORD = "F8WUJJC57RTGL6BS";
        private const string API_SIGNATURE = "AWzY-JRE1CNgrvN1G-hEY0oIMGpEAOzQN1mr9rBZtYNFPsSnfBClXvg.";
        private const string ENVIRONMENT = "live";
        

        public PayPalAPI() {
        }

        public static NVPCallerServices PayPalAPIInitialize(bool isDev) {
            	


            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();

            if (isDev) {
                profile.APIUsername = DEV_API_USERNAME;
                profile.APIPassword = DEV_API_PASSWORD;
                profile.Environment = DEV_ENVIRONMENT;
                profile.Subject = String.Empty;
                profile.APISignature = DEV_API_SIGNATURE;
            } else {
                profile.APIUsername = API_USERNAME;
                profile.APIPassword = API_PASSWORD;
                profile.Environment = ENVIRONMENT;
                profile.Subject = String.Empty;
                profile.APISignature = API_SIGNATURE;
            }


            NVPCallerServices caller = new NVPCallerServices();
            caller.APIProfile = profile;

            return caller;

        }
    }
}
