using System;
using Xamarin.Forms;

namespace ElegantTabs.Abstraction
{
    public static class Transforms
    {
        public static BindableProperty HideTitleProperty = BindableProperty.CreateAttached("HideTitle", typeof(bool), typeof(Transforms), default(bool), BindingMode.OneWay);
        public static BindableProperty KeepIconColourIntactProperty = BindableProperty.CreateAttached("KeepIconColourIntact", typeof(bool), typeof(Transforms), default(bool), BindingMode.OneWay);
        public static BindableProperty DisableLoadProperty = BindableProperty.CreateAttached("DisableLoad", typeof(bool), typeof(Transforms), default(bool), BindingMode.OneWay);
        public static BindableProperty SelectedIconProperty = BindableProperty.CreateAttached("SelectedIcon", typeof(string), typeof(Transforms), default(string), BindingMode.OneWay);
        public static event EventHandler<ElegentTabsEventArgs> TabIconClicked;

        public static bool GetHideTitle(BindableObject view)
        {
            return (bool)view.GetValue(HideTitleProperty);
        }

        public static void SetHideTitle(BindableObject view, bool value)
        {
            view.SetValue(HideTitleProperty, value);
        }

        public static bool GetKeepIconColourIntact(BindableObject view)
        {
            return (bool)view.GetValue(KeepIconColourIntactProperty);
        }

        public static void SetKeepIconColourIntact(BindableObject view, bool value)
        {
            view.SetValue(KeepIconColourIntactProperty, value);
        }

        public static bool GetDisableLoad(BindableObject view)
        {
            return (bool)view.GetValue(DisableLoadProperty);
        }

        public static void SetDisableLoad(BindableObject view, bool value)
        {
            view.SetValue(DisableLoadProperty, value);
        }

        public static string GetSelectedIcon(BindableObject view)
        {
            return (string)view.GetValue(SelectedIconProperty);
        }

        public static void SetSelectedIcon(BindableObject view, string value)
        {
            view.SetValue(SelectedIconProperty, value);
        }

        /// <summary>
        /// Internal use only. Attempts to get the badged child of a tabbed page (either navigation page or content page)
        /// </summary>
        /// <param name="parentTabbedPage">Tabbed page</param>
        /// <param name="tabIndex">Index</param>
        /// <returns>Page</returns>
        public static Page GetChildPageWithTransform(this TabbedPage parentTabbedPage, int tabIndex)
        {
            var element = parentTabbedPage.Children[tabIndex];
            return GetPageWithTransform(element);
        }

        public static Page GetPageWithTransform(this Page element)
        {
            if (GetHideTitle(element) != (bool)HideTitleProperty.DefaultValue || GetKeepIconColourIntact(element) != (bool)KeepIconColourIntactProperty.DefaultValue)
            {
                return element;
            }

            if (element is NavigationPage navigationPage)
            {
                //if the child page is a navigation page get its root page
                return navigationPage.RootPage;
            }

            return element;
        }

        public static void TabIconClickedFunction(int index, object sender)
        {
            TabIconClicked?.Invoke(sender, new ElegentTabsEventArgs() { selectedIndex = index });
        }
    }
}
