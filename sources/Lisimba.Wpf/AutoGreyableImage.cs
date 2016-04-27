using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DustInTheWind.Lisimba.Wpf
{
    public class AutoGreyableImage : Image
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoGreyableImage"/> class.
        /// </summary>
        static AutoGreyableImage()
        {
            // Override the metadata of the IsEnabled property.
            IsEnabledProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(true, OnAutoGreyScaleImageIsEnabledPropertyChanged));
        }

        /// <summary>
        /// Called when [auto grey scale image is enabled property changed].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            AutoGreyableImage autoGreyScaleImg = source as AutoGreyableImage;
            bool isEnable = Convert.ToBoolean(args.NewValue);

            if (autoGreyScaleImg != null)
            {
                if (!isEnable)
                {
                    // Get the source bitmap
                    BitmapImage bitmapImage = new BitmapImage(new Uri(autoGreyScaleImg.Source.ToString()));

                    // Convert it to Gray
                    autoGreyScaleImg.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);

                    // Create Opacity Mask for greyscale image as FormatConvertedBitmap does not keep transparency info
                    autoGreyScaleImg.OpacityMask = new ImageBrush(bitmapImage);

                    autoGreyScaleImg.Opacity = 0.5;
                }
                else
                {
                    // Set the Source property to the original value.
                    autoGreyScaleImg.Source = ((FormatConvertedBitmap)autoGreyScaleImg.Source).Source;

                    // Reset the Opcity Mask
                    autoGreyScaleImg.OpacityMask = null;

                    autoGreyScaleImg.Opacity = 1;
                }
            }
        }
    }
}
