using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDatagrid
{
    public class MainWindowViewModel
    {
    }

    public static class _Variables
    {
        public static string strImageFolderPath;
    }

    public class Variables : INotifyPropertyChanged
    {
        private string strImageFolderPath;
        public string StrImageFolderPath
        {
            get { return strImageFolderPath; }
            set
            {
                if (value != strImageFolderPath)
                {
                    strImageFolderPath = value;
                    _Variables.strImageFolderPath = value;      // to share easily with other class objects
                    OnPropertyChanged("StrImageFolderPath");
                }
            }
        }

        
        


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class EmployeeDetail : INotifyPropertyChanged
    {
        private int mID;
        private string mEmployeeName;
        private bool mIsSelected;
        private int pixelPos, mYear;

        public int ID
        {
            get { return mID; }
            set
            {
                mID = value;
                OnPropertyChanged("ID");
            }
        }

        public int PixelPos
        {
            get { return pixelPos; }
            set
            {
                pixelPos = value;
                OnPropertyChanged("PixelPos");
            }
        }

        public int Year
        {
            get { return mYear; }
            set
            {
                mYear = value;
                OnPropertyChanged("Year");
            }
        }



        public string EmployeeName
        {
            get { return mEmployeeName; }
            set
            {
                mEmployeeName = value;
                OnPropertyChanged("EmployeeName");
            }
        }

        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                mIsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
