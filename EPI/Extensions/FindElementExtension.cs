using System.Windows;
using System.Windows.Media;

namespace EPI.Extensions
{
    public static class FindElementExtension
    {
        public static T FindParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            return FindParent<T>(child, null);
        }
        public static T FindParent<T>(this DependencyObject child, string parentName)
        where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent == null)
            {
                return null;
            }
            var frameworkElement = (FrameworkElement)parent;
            if ((parentName == null || frameworkElement.Name == parentName) && frameworkElement is T)
            {
                return (T)parent;
            }
            else
            {
                return FindParent<T>(parent, parentName);
            }
        }
        public static T FindChild<T>(this DependencyObject parent)
            where T : DependencyObject
        {
            return FindChild<T>(parent, null);
        }

        public static T FindChild<T>(this DependencyObject parent, string childName)
            where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                T childType = child as T;

                if (childType == null)
                {
                    // 재귀 메서드로 자식을 찾는다.
                    foundChild = FindChild<T>(child, childName);
                    // 만약 자식을 찾았다면, 덮어쓰지 않고 멈춘다. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = (FrameworkElement)child;
                    // 만약 자식의 이름이 검색용으로 설정된 경우
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // 자식의 이름이 요청한 이름인 경우 종료 한다.
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
    }
}
