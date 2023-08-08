using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Model
{
	public partial class DosyaEk
	{
		[Relation("aspnet_Users.AdSoyad")]
		public string KullaniciAdSoyad
		{
			get
			{
				return this.aspnet_Users.AdSoyad;
			}
		}

		[Relation("aspnet_Users.UserName")]
		public string UserName
		{
			get
			{
				return this.aspnet_Users.UserName;
			}
		}
	}
}
