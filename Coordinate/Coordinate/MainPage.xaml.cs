using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Coordinate.Resources;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using System.IO;
using Windows.Storage;
using System.Text;

namespace Coordinate
{
    public partial class MainPage : PhoneApplicationPage
    {
        String[] flickrData;
        bool[] validRecords;
        String userLatitude;
        String userLongitude;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataToArray();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void DataToArray()
        {
            String rawData = "1428800419,34.020225,-118.284816,https://www.flickr.com/photos/12796946@N00/1428800419;3873160929,34.019514,-118.289966,https://www.flickr.com/photos/24518663@N08/3873160929;7351820378,34.021701,-118.285031,https://www.flickr.com/photos/53400673@N08/7351820378;7544624022,34.018358,-118.286225,https://www.flickr.com/photos/53400673@N08/7544624022;6761317911,34.021008,-118.282349,https://www.flickr.com/photos/53400673@N08/6761317911;14499353789,34.023533,-118.281404,https://www.flickr.com/photos/77799978@N00/14499353789;5701894637,34.02187,-118.286576,https://www.flickr.com/photos/94588149@N00/5701894637;4706842471,34.023925,-118.284988,https://www.flickr.com/photos/39525342@N04/4706842471;8579229384,34.020385,-118.285642,https://www.flickr.com/photos/12549675@N05/8579229384;6743391127,34.023409,-118.281226,https://www.flickr.com/photos/62837186@N06/6743391127;14135684129,34.022769,-118.281512,https://www.flickr.com/photos/92605333@N00/14135684129;9737751966,34.023532,-118.28582,https://www.flickr.com/photos/12549675@N05/9737751966;474622677,34.020688,-118.285503,https://www.flickr.com/photos/12796946@N00/474622677;7173320387,34.021328,-118.28266,https://www.flickr.com/photos/53400673@N08/7173320387;14551559503,34.021192,-118.284002,https://www.flickr.com/photos/92605333@N00/14551559503;7146197881,34.021227,-118.284112,https://www.flickr.com/photos/73417912@N00/7146197881;6743388089,34.023364,-118.281269,https://www.flickr.com/photos/62837186@N06/6743388089;8388114409,34.02051,-118.287906,https://www.flickr.com/photos/64065167@N04/8388114409;8273230974,34.020519,-118.285425,https://www.flickr.com/photos/54117860@N08/8273230974;1241422312,34.019585,-118.284988,https://www.flickr.com/photos/12796946@N00/1241422312;5888995200,34.021857,-118.280214,https://www.flickr.com/photos/34052289@N06/5888995200;2487180002,34.021079,-118.286361,https://www.flickr.com/photos/12796946@N00/2487180002;8454028418,34.021239,-118.280611,https://www.flickr.com/photos/39527581@N07/8454028418;1322823803,34.020794,-118.286705,https://www.flickr.com/photos/12796946@N00/1322823803;5107197497,34.021115,-118.288464,https://www.flickr.com/photos/12549675@N05/5107197497;1626326028,34.020368,-118.28516,https://www.flickr.com/photos/12796946@N00/1626326028;490935530,34.022039,-118.282027,https://www.flickr.com/photos/80658667@N00/490935530;1444160390,34.02179,-118.28722,https://www.flickr.com/photos/12796946@N00/1444160390;9316513497,34.019959,-118.286329,https://www.flickr.com/photos/92605333@N00/9316513497;14540765665,34.018874,-118.289108,https://www.flickr.com/photos/92605333@N00/14540765665;7000111506,34.021227,-118.284112,https://www.flickr.com/photos/73417912@N00/7000111506;9736315525,34.019905,-118.289977,https://www.flickr.com/photos/12549675@N05/9736315525;8675644089,34.021221,-118.28679,https://www.flickr.com/photos/54776562@N08/8675644089;8578118495,34.019184,-118.282562,https://www.flickr.com/photos/12549675@N05/8578118495;160661492,34.022502,-118.287391,https://www.flickr.com/photos/22243808@N00/160661492;8579227920,34.02131,-118.286372,https://www.flickr.com/photos/12549675@N05/8579227920;14637748857,34.020047,-118.283765,https://www.flickr.com/photos/8918304@N05/14637748857;8579223776,34.019994,-118.285793,https://www.flickr.com/photos/12549675@N05/8579223776;14035441873,34.021262,-118.283874,https://www.flickr.com/photos/92605333@N00/14035441873;8729050141,34.021364,-118.287048,https://www.flickr.com/photos/54776562@N08/8729050141;8671072961,34.021417,-118.287531,https://www.flickr.com/photos/64232630@N00/8671072961;7402255238,34.018482,-118.286597,https://www.flickr.com/photos/53400673@N08/7402255238;14362038109,34.022564,-118.286887,https://www.flickr.com/photos/92605333@N00/14362038109;6777384147,34.023587,-118.28144,https://www.flickr.com/photos/62837186@N06/6777384147;51597920,34.020901,-118.2834,https://www.flickr.com/photos/78467561@N00/51597920;7188853337,34.021026,-118.283475,https://www.flickr.com/photos/53400673@N08/7188853337;8579238144,34.019478,-118.283733,https://www.flickr.com/photos/12549675@N05/8579238144;6010870088,34.019549,-118.285889,https://www.flickr.com/photos/12549675@N05/6010870088;12040886736,34.020225,-118.285278,https://www.flickr.com/photos/51283825@N06/12040886736;4771587983,34.023596,-118.286202,https://www.flickr.com/photos/51421101@N02/4771587983;3036299539,34.023909,-118.280907,https://www.flickr.com/photos/7459168@N04/3036299539;8466151390,34.018803,-118.288807,https://www.flickr.com/photos/38243431@N03/8466151390;8450206895,34.021239,-118.280611,https://www.flickr.com/photos/39527581@N07/8450206895;5399958229,34.021043,-118.286533,https://www.flickr.com/photos/46728064@N06/5399958229;11931341443,34.021125,-118.284721,https://www.flickr.com/photos/12549675@N05/11931341443;8729049155,34.021435,-118.28679,https://www.flickr.com/photos/54776562@N08/8729049155;2489125792,34.020937,-118.285803,https://www.flickr.com/photos/67341227@N00/2489125792;8579225906,34.020848,-118.28575,https://www.flickr.com/photos/12549675@N05/8579225906;6010808676,34.020225,-118.285396,https://www.flickr.com/photos/12549675@N05/6010808676;6010250873,34.019184,-118.282562,https://www.flickr.com/photos/12549675@N05/6010250873;6771484005,34.023409,-118.28144,https://www.flickr.com/photos/62837186@N06/6771484005;6771481757,34.023524,-118.281397,https://www.flickr.com/photos/62837186@N06/6771481757;5107825704,34.021115,-118.288464,https://www.flickr.com/photos/12549675@N05/5107825704;6010799932,34.020368,-118.28458,https://www.flickr.com/photos/12549675@N05/6010799932;2692720244,34.020465,-118.284441,https://www.flickr.com/photos/45923298@N00/2692720244;2692717318,34.019185,-118.287777,https://www.flickr.com/photos/45923298@N00/2692717318;7553286682,34.020617,-118.285031,https://www.flickr.com/photos/8896423@N04/7553286682;9736248671,34.01971,-118.290213,https://www.flickr.com/photos/12549675@N05/9736248671;8579231662,34.021125,-118.284721,https://www.flickr.com/photos/12549675@N05/8579231662;7553280606,34.020617,-118.285031,https://www.flickr.com/photos/8896423@N04/7553280606;5759198778,34.02051,-118.285363,https://www.flickr.com/photos/51619679@N00/5759198778;8501573662,34.018349,-118.286114,https://www.flickr.com/photos/45923298@N00/8501573662;14824291415,34.022102,-118.283282,https://www.flickr.com/photos/8918304@N05/14824291415;7130662937,34.018416,-118.280611,https://www.flickr.com/photos/23820645@N05/7130662937;5284235865,34.020012,-118.289816,https://www.flickr.com/photos/24518663@N08/5284235865;6010872700,34.022533,-118.28678,https://www.flickr.com/photos/12549675@N05/6010872700;8729038841,34.021506,-118.28722,https://www.flickr.com/photos/54776562@N08/8729038841;8730167484,34.021719,-118.286962,https://www.flickr.com/photos/54776562@N08/8730167484;35460703,34.025347,-118.286533,https://www.flickr.com/photos/20738560@N00/35460703;3774120260,34.022666,-118.282834,https://www.flickr.com/photos/51035707567@N01/3774120260;2663903657,34.02083,-118.284569,https://www.flickr.com/photos/9902549@N08/2663903657;6991201144,34.01834,-118.288893,https://www.flickr.com/photos/30993133@N04/6991201144;8123948860,34.018287,-118.290878,https://www.flickr.com/photos/39017545@N02/8123948860;7984406589,34.023053,-118.288614,https://www.flickr.com/photos/60466607@N06/7984406589;7625279386,34.024138,-118.287466,https://www.flickr.com/photos/74494819@N05/7625279386;9004295962,34.021,-118.284,https://www.flickr.com/photos/77339424@N00/9004295962;5045338234,34.021897,-118.285675,https://www.flickr.com/photos/45534482@N03/5045338234;6166593875,34.020363,-118.283954,https://www.flickr.com/photos/45206671@N00/6166593875;3637598821,34.022502,-118.281089,https://www.flickr.com/photos/28423600@N02/3637598821;13083729324,34.020858,-118.287438,https://www.flickr.com/photos/97570635@N07/13083729324;11931080955,34.022351,-118.286672,https://www.flickr.com/photos/12549675@N05/11931080955;201223327,34.021924,-118.284065,https://www.flickr.com/photos/61737427@N00/201223327;5414131407,34.019821,-118.282424,https://www.flickr.com/photos/59031889@N06/5414131407;7992943248,34.022644,-118.281433,https://www.flickr.com/photos/31998658@N06/7992943248;11693043933,34.019,-118.286834,https://www.flickr.com/photos/25990689@N05/11693043933;5573633294,34.020617,-118.285406,https://www.flickr.com/photos/42513008@N07/5573633294;2664727170,34.021808,-118.283185,https://www.flickr.com/photos/9902549@N08/2664727170;8730157384,34.021933,-118.287048,https://www.flickr.com/photos/54776562@N08/8730157384;9650627955,34.0235,-118.2865,https://www.flickr.com/photos/39234410@N02/9650627955;5122694188,34.022075,-118.287048,https://www.flickr.com/photos/45534482@N03/5122694188;201223328,34.019403,-118.282005,https://www.flickr.com/photos/61737427@N00/201223328;2692721726,34.02195,-118.289741,https://www.flickr.com/photos/45923298@N00/2692721726;8578132763,34.018785,-118.284376,https://www.flickr.com/photos/12549675@N05/8578132763;3419902293,34.02195,-118.287112,https://www.flickr.com/photos/99323105@N00/3419902293;8344553012,34.022725,-118.28587,https://www.flickr.com/photos/43413770@N06/8344553012;9422825588,34.021728,-118.285868,https://www.flickr.com/photos/29929498@N00/9422825588;4433692180,34.021826,-118.286876,https://www.flickr.com/photos/45561728@N06/4433692180;11745348174,34.020305,-118.284234,https://www.flickr.com/photos/45206671@N00/11745348174;";
            flickrData = rawData.Split(new Char[]{';'});
        }




        private void textBoxRadius_Click(object sender, RoutedEventArgs e)
        {
            textBoxRadius.SelectAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (validateRadiusInput())
            {
                GetGPSCoordinates();
                
            }

        }

        private void nextPage()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < validRecords.Length; i++)
            {
                if (validRecords[i] == true)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
            }
                NavigationService.Navigate(new Uri("/PhotoBrowser.xaml?msg=" + sb.ToString(), UriKind.Relative));
        }

        private Boolean validateRadiusInput()
        {
            Boolean isValid = false;
            int radius;
            if (Int32.TryParse(textBoxRadius.Text, out radius))
            {
                if (radius > 0)
                {
                    isValid = true;
                }
                else
                {
                    MessageBoxResult invalid = MessageBox.Show("Invalid input for radius");
                }
            }
            else
            {
                MessageBoxResult invalid = MessageBox.Show("Invalid input for radius");
            }
            return isValid;
        }

        //TODO: Do math for GPS radius calculations
        private async void GetGPSCoordinates()
        {
            if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            {
                // The user has opted out of Location.
                return;
            }

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                userLatitude = geoposition.Coordinate.Latitude.ToString("0.00");
                userLongitude = geoposition.Coordinate.Longitude.ToString("0.00");

                getValidCoordinates(Int32.Parse(textBoxRadius.Text));
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    MessageBoxResult error = MessageBox.Show("Location access on this phone is not allowed.");
                }
                //else
                {
                    // something else happened acquring the location
                }
            }
        }

        private async void getValidCoordinates(int radius)
        {
            String[] elementData = new String[4];
            validRecords = new bool[flickrData.Length];
            for (int i = 0; i < flickrData.Length; i++)
            {
                if (isInRadius(elementData[1], elementData[2], radius))
                {
                    validRecords[i] = true;
                }
                else
                {
                    validRecords[i] = false;
                }
            }
            nextPage();
        }

        private Boolean isInRadius(String latitude, String longitude, int radius)
        {
            Boolean valid = false;
            Double latitudeDouble;
            Double.TryParse(latitude, out latitudeDouble);
            Double longitudeDouble;
            Double.TryParse(longitude, out longitudeDouble);
            Double distance = Math.Sqrt(Math.Pow((Double.Parse(userLatitude) - latitudeDouble), 2) + Math.Pow((Double.Parse(userLongitude) - longitudeDouble),2));
            if (distance <= radius)
            {
                valid = true;
            }
            return valid;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                // User has opted in or out of Location
                return;
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}