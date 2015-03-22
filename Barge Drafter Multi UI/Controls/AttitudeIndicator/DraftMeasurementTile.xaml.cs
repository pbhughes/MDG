using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDG.Visuals
{
    /// <summary>
    /// Interaction logic for DraftMeasurementTile.xaml
    /// </summary>
    public partial class DraftMeasurementTile : UserControl
    {

        public DraftMeasurementTile()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AboveWaterReadingProperty =
            DependencyProperty.Register("AboveWaterReading", typeof (double), typeof (DraftMeasurementTile), new PropertyMetadata(0D,OnBelowReadingChanged));

        public double AboveWaterReading
        {
            get { return (double) GetValue(AboveWaterReadingProperty); }
            set { SetValue(AboveWaterReadingProperty, value); }
        }


        public static readonly DependencyProperty BelowWaterReadingProperty =
            DependencyProperty.Register("BelowWaterReading", typeof(double), typeof(DraftMeasurementTile), new PropertyMetadata(0D, OnAboveReadingChanged));

        public double BelowWaterReading
        {
            get { return (double) GetValue(BelowWaterReadingProperty); }
            set { SetValue(BelowWaterReadingProperty, value); }
        }

        private static void OnAboveReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var xd = (DraftMeasurementTile)d;
            var dNewvalue = (double)e.NewValue;
            xd.draftReading.AboveWaterReading = dNewvalue;

        }

        private static void OnBelowReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var xd = (DraftMeasurementTile)d;
            var dNewvalue = (double)e.NewValue;
            xd.draftReading.BelowWaterReading = dNewvalue;

        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof (string), typeof (DraftMeasurementTile),
                                        new PropertyMetadata("A Title", OnTitleChanged));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var xd = (DraftMeasurementTile)d;
            var dNewvalue = (string)e.NewValue;
            xd.mahTile.Title = dNewvalue;

        }

        public static readonly DependencyProperty BargeDepthProperty =
            DependencyProperty.Register("BargeDepth", typeof(double), typeof(DraftMeasurementTile), new PropertyMetadata(12D, OnBargeDepthChanged));

        public double BargeDepth
        {
            get { return (int) GetValue(BargeDepthProperty); }
            set { SetValue(BargeDepthProperty, value); }
        }
        private static void OnBargeDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var xd = (DraftMeasurementTile)d;
            var dNewvalue = (double)e.NewValue;
            xd.draftReading.BargeDepth = dNewvalue;

        }
    }
}
