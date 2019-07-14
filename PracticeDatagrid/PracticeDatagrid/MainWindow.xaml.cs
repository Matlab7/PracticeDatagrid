using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeDatagrid
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>+
    /// 



    public partial class MainWindow : Window
    {
        private ObservableCollection<EmployeeDetail> mEmployees;
        private Variables variables;

        public MainWindow()
        {
            InitializeComponent();

            mEmployees = new ObservableCollection<EmployeeDetail>();
            mEmployees.Add(new EmployeeDetail() { ID = 1, EmployeeName = "John", PixelPos = -1, Year = 2012 });
            mEmployees.Add(new EmployeeDetail() { ID = 2, EmployeeName = "James", PixelPos = -1, Year = 2013 });
            mEmployees.Add(new EmployeeDetail() { ID = 3, EmployeeName = "Ricky", PixelPos = -1, Year = 2011 });
            
            variables = new Variables();

            datagrid.ItemsSource = mEmployees;
            txtBlockFolderPath.DataContext = variables;
        }

        void OnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            var employee = dataGridRow.DataContext as EmployeeDetail;

            if (checkBox.IsChecked == true)
            {
                MessageBox_SaveImage(checkBox, employee, "save image, " + employee.ID + "nm");
                //SubWindow subWindow = new SubWindow();
                //subWindow.Show();

                // check if all checkboxes are checked
                // then make the auto-acquisition button available 
                bool isAutoAcqAvailable = true;
                foreach (var item in mEmployees)
                {
                    if (item.IsSelected == false)
                    {
                        isAutoAcqAvailable = false;
                        break;
                    }
                }
                if (isAutoAcqAvailable)
                    btnSaveImage.IsEnabled = true;

            }
            e.Handled = true;
        }
        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            btnSaveImage.IsEnabled = false;
        }

        private void MessageBox_SaveImage(CheckBox checkBox, EmployeeDetail employee, string str)
        {
            if (MessageBox.Show(str, "Save Image", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // save image and update pixel value
                employee.PixelPos = 123;
            }
            else
            {
                // do nothing
                checkBox.IsChecked = false;
            }
        }

        private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in mEmployees)
            {
                item.IsSelected = true;
            }
        }

        private void BtnUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in mEmployees)
            {
                item.IsSelected = false;
            }
        }

        private void BtnSaveImage_Click(object sender, RoutedEventArgs e)
        {
            // do some image saving procedure
            //
            // ...
            //
        }

        private void BtnSelectDir_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            variables.StrImageFolderPath = dialog.SelectedPath;
        }
    }


    public class VisualTreeHelpers
    {
        /// <summary>
        /// Returns the first ancester of specified type
        /// </summary>
        public static T FindAncestor<T>(DependencyObject current)
        where T : DependencyObject
        {
            current = VisualTreeHelper.GetParent(current);

            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            };
            return null;
        }

        /// <summary>
        /// Returns a specific ancester of an object
        /// </summary>
        public static T FindAncestor<T>(DependencyObject current, T lookupItem)
        where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T && current == lookupItem)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            };
            return null;
        }

        /// <summary>
        /// Finds an ancestor object by name and type
        /// </summary>
        public static T FindAncestor<T>(DependencyObject current, string parentName)
        where T : DependencyObject
        {
            while (current != null)
            {
                if (!string.IsNullOrEmpty(parentName))
                {
                    var frameworkElement = current as FrameworkElement;
                    if (current is T && frameworkElement != null && frameworkElement.Name == parentName)
                    {
                        return (T)current;
                    }
                }
                else if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            };

            return null;

        }

        /// <summary>
        /// Looks for a child control within a parent by name
        /// </summary>
        public static T FindChild<T>(DependencyObject parent, string childName)
        where T : DependencyObject
        {
            // Confirm parent and childName are valid.
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                    else
                    {
                        // recursively drill down the tree
                        foundChild = FindChild<T>(child, childName);

                        // If the child is found, break so we do not overwrite the found child.
                        if (foundChild != null) break;
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

        /// <summary>
        /// Looks for a child control within a parent by type
        /// </summary>
        public static T FindChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            // Confirm parent is valid.
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
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
