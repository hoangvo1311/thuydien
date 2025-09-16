using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class Settings
    {
        public static string TNMT_Url
        {
            get
            {
                var value = ConfigurationManager.AppSettings["TNMT_Url"];
                if (value != null)
                {
                    return value.ToString();
                }
                return "https://gisportal.monre.gov.vn:8443/input/TNN_REST_INPUT?DATA=";
            }
        }

        public static string Bitexco_Url
        {
            get
            {
                var value = ConfigurationManager.AppSettings["Bitexco_Url"];
                if (value != null)
                {
                    return value.ToString();
                }
                return "http://qlkt.bitexcopower.vn:8080/api/sensor_data";
            }
        }

        public static string TNMT_Username
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_Username"].ToString();
            }
        }

        public static string TNMT_Password
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_Password"].ToString();
            }
        }

        public static string TNMT_FTP_Password
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_FTP_Password"].ToString();
            }
        }

        public static string TNMT_FTP_Username
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_FTP_Username"].ToString();
            }
        }

        public static string TNMT_FTP_IP
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_FTP_IP"].ToString();
            }
        }

        public static string TNMT_FTP_Password_2
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_FTP_Password_2"].ToString();
            }
        }

        public static string TNMT_FTP_Username_2
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_FTP_Username_2"].ToString();
            }
        }

        public static string TNMT_FTP_IP_2
        {
            get
            {
                return ConfigurationManager.AppSettings["TNMT_FTP_IP_2"].ToString();
            }
        }



        public static string Bitexco_PlcId
        {
            get
            {
                return ConfigurationManager.AppSettings["Bitexco_PlcId"] != null
                    ? ConfigurationManager.AppSettings["Bitexco_PlcId"].ToString()
                    : string.Empty;
            }
        }

        

        public static string Bitexco_Password
        {
            get
            {
                return ConfigurationManager.AppSettings["Bitexco_Password"] != null 
                    ? ConfigurationManager.AppSettings["Bitexco_Password"].ToString()
                    : string.Empty;
            }
        }


        public static string PlantID
        {
            get
            {
                return ConfigurationManager.AppSettings["PlantID"].ToString();
            }
        }

        public static string KyHieuCongTrinh
        {
            get
            {
                return ConfigurationManager.AppSettings["KyHieuCongTrinh"].ToString();
            }
        }

        public static int TNMT_SendingInterval
        {
            get
            {
                int intervalTime = 0;
                var value = ConfigurationManager.AppSettings["TNMT_SendingInterval (minutes)"];
                if (value != null && Int32.TryParse(value.ToString(), out intervalTime))
                {
                    return intervalTime * 60000;
                }
                return Constants.DefaultTNMTSendingInterval;
            }
        }

        public static int Web_SyncInterval
        {
            get
            {
                double intervalTime = 0;
                var value = ConfigurationManager.AppSettings["Web_SyncInterval (seconds)"];
                if (value != null && Double.TryParse(value.ToString(), out intervalTime))
                {
                    return (int)(intervalTime * 1000);
                }
                return Constants.DefaultTNMTSendingInterval;
            }
        }


        public static int Bitexco_SendingInterval
        {
            get
            {
                int intervalTime = 0;
                var value = ConfigurationManager.AppSettings["Bitexco_SendingInterval (minutes)"];
                if (value != null && Int32.TryParse(value.ToString(), out intervalTime))
                {
                    return intervalTime * 60000;
                }
                return Constants.DefaultBitexcoSendingInterval;
            }
        }
    }
}
