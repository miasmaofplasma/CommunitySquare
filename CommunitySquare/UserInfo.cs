using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml.Serialization;

namespace CommunitySquare
{
    [XmlRoot("UserInfo")]
    [Serializable]
    public class UserInfo
    {
        #region IXmlSerializable Members
        public string ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        #endregion

    }
}