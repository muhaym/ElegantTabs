using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms.Internals;
using static Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page;
using PageUIStatusBarAnimation = Xamarin.Forms.PlatformConfiguration.iOSSpecific.UIStatusBarAnimation;
using ElegantTabs.Abstraction;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using Xamarin.Forms;

namespace ElegantTabs.iOS
{

    [Preserve(AllMembers = true)]
    public class ElegantTabRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            ShouldSelectViewController = (tabController, controller) =>
            {
                var index = tabController.ViewControllers?.IndexOf(controller);
                if (index.HasValue)
                {
                    var page = Tabbed.GetChildPageWithTransform(index.Value);
                    Transforms.TabIconClickedFunction(index.Value, this);
                    if (!Transforms.GetDisableLoad(page))
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            };
           
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            for (int i = 0; i < TabBar.Items.Length; i++)
            {
                var page = Tabbed.GetChildPageWithTransform(i);
                if (Transforms.GetHideTitle(page))
                {
                    TabBar.Items[i].SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.Clear }, UIControlState.Selected);
                    TabBar.Items[i].SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.Clear }, UIControlState.Normal);
                    TabBar.Items[i].ImageInsets = new UIEdgeInsets(6, 0, -6, 0);
                }
            }
        }
        
        async protected override Task<Tuple<UIImage, UIImage>> GetIcon(Page page)
        {
            var pageIcon = page.Icon?.File;
            if (!string.IsNullOrWhiteSpace(pageIcon))
            {
                var icon = UIImage.FromBundle(pageIcon);
                if (Transforms.GetKeepIconColourIntact(page))
                {

                    icon = icon.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                    var tuple = new Tuple<UIImage, UIImage>(icon, (UIImage)null);
                    return tuple;
                }
                else
                {
                    var selectedImage = Transforms.GetSelectedIcon(page);
                    if (string.IsNullOrWhiteSpace(selectedImage))
                    {
                        //means there is no selected image, so give default tinting scheme
                        var tuple = new Tuple<UIImage, UIImage>(icon, (UIImage)null);
                        return tuple;
                    }
                    else
                    {
                        //means there is selected image, so no tining instead give both image with original rendering mode
                        icon = icon.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                        var selectedIcon = UIImage.FromBundle(selectedImage)?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                        var tuple = new Tuple<UIImage, UIImage>(icon, selectedIcon);
                        return tuple;
                    }
                }
            }
            return null;
        }
    }

    [Preserve(AllMembers =true)]
    public static class ImageSourceExtensions
    {
        static ImageLoaderSourceHandler s_imageLoaderSourceHandler;

        static ImageSourceExtensions()
        {
            s_imageLoaderSourceHandler = new ImageLoaderSourceHandler();
        }
        public static Task<UIImage> ToUIImage(this ImageSource imageSource)
        {
            return s_imageLoaderSourceHandler.LoadImageAsync(imageSource);
        }
    }

}