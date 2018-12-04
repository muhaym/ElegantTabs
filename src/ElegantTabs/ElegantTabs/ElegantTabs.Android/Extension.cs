using Android.Runtime;
using Android.Views;

namespace ElegantTabs.Droid
{
    [Preserve(AllMembers =true)]
    public static class Extension
    {
        public static T FindChildOfType<T>(this ViewGroup parent) where T : View
        {
            if (parent == null)
                return null;

            if (parent.ChildCount == 0)
                return null;

            for (var i = 0; i < parent.ChildCount; i++)
            {
                var child = parent.GetChildAt(i);


                if (child is T typedChild)
                {
                    return typedChild;
                }

                if (!(child is ViewGroup))
                    continue;


                var result = FindChildOfType<T>(child as ViewGroup);
                if (result != null)
                    return result;
            }
            return null;
        }
    }

}