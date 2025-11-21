using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class TemplateReportResult
    {
        public TemplateReportResult() { }
        public TemplateReportResult(string uPDATE_STATUS, bool isMau_CuaCuon, bool isMau_CuaXep, bool isMau_CuaNhom, bool isMau_Nano, bool isMau_Tran36, bool isMau_Panel, bool isMau_Tranh, bool isMau_ThanTre, string lstCuaCuon, string lstCuaXep, string lstCuaNhom, string lstNhuaNaNo, string lstTran36, string lstPanel, string lstTranh, string lstThanTre, string lstIDID_All)
        {
            UPDATE_STATUS = uPDATE_STATUS;
            IsMau_CuaCuon = isMau_CuaCuon;
            IsMau_CuaXep = isMau_CuaXep;
            IsMau_CuaNhom = isMau_CuaNhom;
            IsMau_Nano = isMau_Nano;
            IsMau_Tran36 = isMau_Tran36;
            IsMau_Panel = isMau_Panel;
            IsMau_Tranh = isMau_Tranh;
            IsMau_ThanTre = isMau_ThanTre;
            LstCuaCuon = lstCuaCuon;
            LstCuaXep = lstCuaXep;
            LstCuaNhom = lstCuaNhom;
            LstNhuaNaNo = lstNhuaNaNo;
            LstTran36 = lstTran36;
            LstPanel = lstPanel;
            LstTranh = lstTranh;
            LstThanTre = lstThanTre;
            LstIDID_All = lstIDID_All;
        }

        public string UPDATE_STATUS { get; set; }
        public bool? IsMau_CuaCuon { get; set; }
        public bool? IsMau_CuaXep { get; set; }
        public bool? IsMau_CuaNhom { get; set; }
        public bool? IsMau_Nano { get; set; }
        public bool? IsMau_Tran36 { get; set; }
        public bool? IsMau_Panel { get; set; }
        public bool? IsMau_Tranh { get; set; }
        public bool? IsMau_ThanTre { get; set; }
        public string LstCuaCuon { get; set; }
        public string LstCuaXep { get; set; }
        public string LstCuaNhom { get; set; }
        public string LstNhuaNaNo { get; set; }
        public string LstTran36 { get; set; }
        public string LstPanel { get; set; }
        public string LstTranh { get; set; }
        public string LstThanTre { get; set; }
        public string LstIDID_All { get; set; }
    }
}
