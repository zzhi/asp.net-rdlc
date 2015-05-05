using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;


namespace ReportTest
{
    public partial class EnergyProfile : System.Web.UI.Page
    {
        
        private string _unitArea = "off";

        private int _strDayType = 10;
        private string _strMeterguids;
        private string _strSpaceGuids;
        private string _calculatorType;
        private string _categoryGuid;
        private string _yAlix;
        protected string _dateFrom = string.Empty;
        protected string _dateTo = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            var bll = new MySqlBll.EnergyProfileBll();


            if (!Page.IsPostBack)
            {



                #region 参数
                _strSpaceGuids = Request.QueryString["spaceguids"];
                    _strDayType =
                        (int)(Utility.EnDayType)Enum.Parse(typeof(Utility.EnDayType), Request.QueryString["period"]);

                  _dateFrom = DateTime.Parse(Request.QueryString["dateFrom"]).ToString("yyyy-MM-dd HH:mm");
                  _dateTo = DateTime.Parse(Request.QueryString["dateTo"]).ToString("yyyy-MM-dd HH:mm");

                if (_strDayType ==(int)Utility.EnDayType.day)
                {
                    _dateTo = DateTime.Parse(Request.QueryString["dateTo"]).ToString("yyyy-MM-dd 23:00");

                }
                if (_strDayType == (int)Utility.EnDayType.month)
                {
                    _dateTo = DateTime.Parse(Request.QueryString["dateTo"]).AddMonths(1).AddHours(-1).ToString("yyyy-MM-dd HH:mm");
                }
                _categoryGuid = Request.QueryString["categoryguid"];//"dbe6b99e-3f89-45d2-9c3c-e9e7b75d7cab";//电
      
             
                _unitArea = Request.QueryString["unitArea"];//是否按单位面积
                _calculatorType = string.IsNullOrEmpty(Request.QueryString["calculatorType"]) ? "0" : Request.QueryString["calculatorType"];
                //计算类型,能耗还是费用
                #endregion



                //声明数据表
                DataTable dtTotalEnergy = new ReportDataSet.TotalEnergyDataTable();

                #region 查询数据(案例)
                if(!string.IsNullOrEmpty(_strSpaceGuids))
                {
                    _strSpaceGuids = Utility.GuidsString(_strSpaceGuids);

                    dtTotalEnergy = bll.TotalEnergy(_strSpaceGuids, "'test'", _categoryGuid, _dateFrom, _dateTo, _unitArea, _calculatorType,
                        _strDayType);
                }
                #endregion

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/EnergyProfileRDLC.rdlc");
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.LocalReport.DisplayName = "能耗报告";


                var yAlixRp = new ReportParameter("YAlix", "能耗值");
                ReportViewer1.LocalReport.SetParameters(new[]
                {
                    yAlixRp
                });

                //TotalEnergyData 名称和RDLC文件中DataSets中数据集名要相同
                var tolEnergyProfile = new ReportDataSource("TotalEnergyData", 
                dtTotalEnergy);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(tolEnergyProfile);

             
            }
        }

       
    }
}