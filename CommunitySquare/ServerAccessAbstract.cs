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
using Microsoft.WindowsAzure.MobileServices;

namespace CommunitySquare
{
    public abstract class ServerAccessAbstract
    {
        protected MobileServiceClient MobileService = new MobileServiceClient("https://comsquare.azurewebsites.net");
    }

    
}