using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Hamburger.UI.Model
{
    public class PartyImageModel
    {
        public List<Image> Images { get; set; }

        public PartyImageModel(string partyName)
        {
            Images = new List<Image>();
            Image image1 = new Image();
            image1.Source = new BitmapImage(new Uri("ms-appx:///Assets/"+ partyName + "small.bmp"));
            Image image2 = new Image();
            image2.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + partyName + "medium.bmp"));
            Image image3 = new Image();
            image3.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + partyName + "large.bmp"));
            Image image4 = new Image();
            image4.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + partyName + "list.bmp"));
            Images.Add(image1);
            Images.Add(image2);
            Images.Add(image3);
            Images.Add(image4);
        }
    }
}
