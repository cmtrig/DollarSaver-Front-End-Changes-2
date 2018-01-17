using System;
using System.Data;
using System.Configuration;

using System.IO;

using DollarSaver.Core.Data;
using DollarSaver.Core.Data.DollarSaverDBTableAdapters;

namespace DollarSaver.Core.Common {

    /// <summary>
    /// Summary description for Globals
    /// </summary>
    public class Globals {

        public static String ConvertToNull(string val) {
            if (val == String.Empty) {
                return null;
            }
            return val;
        }

        public static int? ConvertToNull(int val) {
            if (val == 0) {
                return null;
            }
            return val;
        }

        public static DateTime? ConvertToNull(DateTime val) {
            if (val == new DateTime()) {
                return null;
            }
            return val;
        }

        public static bool ConvertToBool(String val) {
            if (val != null) {
                val = val.ToUpper();
                if (val == "T" || val == "TRUE" || val == "1" || val == "Y" || val == "YES") {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public static void CheckDirectory(String directoryPath) {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            if (!directory.Exists) {
                directory.Create();
            }
        }
    }
}