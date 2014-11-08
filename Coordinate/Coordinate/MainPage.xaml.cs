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

namespace Coordinate
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }



        private void textBoxRadius_Click(object sender, RoutedEventArgs e)
        {
            textBoxRadius.SelectAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if(validateRadiusInput())
            {
                GetGPSCoordinates();
            }
            
            
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

                String latitude = geoposition.Coordinate.Latitude.ToString("0.00");
                String longitude = geoposition.Coordinate.Longitude.ToString("0.00");

                MessageBoxResult coordinates = MessageBox.Show("Latitude: " + latitude + "\r\n" + "Longitude: " + longitude);
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

        private String getValidCoordinates(String latitude, String longitude, int radius)
        {

            //String fileLines = System.IO.File.ReadAllLines();
            return null;
        }

        private Boolean isInRadius(String latitude, String longitude, int radius)
        {
            Boolean valid = false;
            Double latitudeDouble;
            Double.TryParse(latitude, out latitudeDouble);
            Double longitudeDouble;
            Double.TryParse(longitude, out longitudeDouble);
            Double distance = Math.Sqrt((latitudeDouble * latitudeDouble) + (longitudeDouble * longitudeDouble));
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