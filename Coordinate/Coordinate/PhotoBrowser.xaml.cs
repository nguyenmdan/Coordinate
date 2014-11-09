using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace Coordinate
{
    public partial class PhotoBrowser : PhoneApplicationPage
    {
        bool[] validImages;
        public PhotoBrowser()
        {
           
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                validImages = new bool[msg.Length];
                MessageBoxResult debug = MessageBox.Show(msg);
                char[] paramPass = msg.ToCharArray();
                for (int i = 0; i < msg.Length; i++)
                {
                    if (paramPass[i].Equals("1"))
                    {
                        validImages[i] = true;
                    }
                    else
                    {
                        validImages[i] = false;
                    }
                }
                int j = 0;
                while (!validImages[j] && j < validImages.Length-1)
                {
                    j++;
                }
                BitmapImage image = new BitmapImage(new Uri("Assets/images/0.jpg", UriKind.RelativeOrAbsolute));
                ImageBox.Source = image;
            }


        }

        private void buttonPrevious_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}