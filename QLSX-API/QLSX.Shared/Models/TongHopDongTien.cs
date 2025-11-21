using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
	public class TongHopDongTien
	{

		public int STT { get; set; }
		public string SCT { get; set; }

		public decimal SOTIENTHU { get; set; }
		public decimal SOTIENCHI { get; set; }
		public decimal SODUDAUKY { get; set; }
		public decimal SODUCUOIKY { get; set; }
		public decimal PHAITHU_DAU { get; set; }
		public decimal PHAITHU_PS1 { get; set; }
		public decimal PHAITHU_PS2 { get; set; }
		public decimal PHAITHU_CUOI { get; set; }
		public decimal PHAITRA_DAU { get; set; }
		public decimal PHAITRA_PS1 { get; set; }
		public decimal PHAITRA_PS2 { get; set; }
		public decimal PHAITRA_CUOI { get; set; }

		public decimal TONG_DAU { get; set; }
		public decimal TONG_PS1 { get; set; }
		public decimal TONG_PS2 { get; set; }
		public decimal TONG_CUOI { get; set; }

		public decimal TONG_DAU_NOT1 { get; set; }
		public decimal TONG_PS1_NOT1 { get; set; }
		public decimal TONG_PS2_NOT1 { get; set; }
		public decimal TONG_CUOI_NOT1 { get; set; }


		public decimal TONG_DAU_NOT2 { get; set; }
		public decimal TONG_PS1_NOT2 { get; set; }
		public decimal TONG_PS2_NOT2 { get; set; }
		public decimal TONG_CUOI_NOT2 { get; set; }


		public int ID { get; set; }

		public string MDONVISUDUNG { get; set; }
		public string TENDONVISUDUNG { get; set; }
		public int LOAITIEN { get; set; }
		public string TENLOAITIEN { get; set; }
		public string THOIGIAN { get; set; }
	}
}
