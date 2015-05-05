
using System.Linq;


namespace ReportTest
{
    public class Utility
    {
        /// <summary>
        /// QueryString To Guids
        /// </summary>
        /// <param name="strGuids"></param>
        /// <returns></returns>
        public static string GuidsString(string strGuids)
        {
            string str = string.Empty;
            var guids = strGuids.Split(',');
            for (int i = 0; i < guids.Count(); i++)
            {
                str += "'" + guids[i] + "',";
            }
            str = str.TrimEnd(new[] { ',' });
            return str;
        }

        public enum EnDayType
        {
            year=4,
            month=7,
            day=10,
            hour=13
        }




      
    
    }
}