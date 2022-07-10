using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LogDogGUI
{

    public class NavButton : ListBoxItem
    {
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }



        // to hold information about how to find a specific page within the application
        public Uri NavLink
        {
            get { return (Uri)GetValue(NavLinkProperty); }
            set { SetValue(NavLinkProperty, value); }
        }
        public static readonly DependencyProperty NavLinkProperty =
            DependencyProperty.Register("NavLink", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));


        // hold information about icon
        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(NavButton), new PropertyMetadata(null));




    } // class
} // namespace
