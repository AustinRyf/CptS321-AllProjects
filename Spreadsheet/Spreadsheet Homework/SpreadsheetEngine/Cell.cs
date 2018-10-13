using System;
using System.ComponentModel;

namespace CptS321
{
    public abstract class Cell : INotifyPropertyChanged
    {
        protected int columnIndex;
        protected int rowIndex;
        protected string text = String.Empty;
        protected string value = String.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        //Notifies if the text of a cell has been changed
        protected void OnPropertyChanged(string hText)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(hText));
            }
        }

        //Cell constructor
        public Cell(int newRow, int newColumn)
        {
            columnIndex = newColumn;
            rowIndex = newRow;
        }

        //Cell column getter/setter
        public int ColumnIndex
        {
            get { return columnIndex; }
            set
            {
                this.columnIndex = value;
            }
        }

        //Cell row getter/setter
        public int RowIndex
        {
            get { return rowIndex; }
            set
            {
                this.rowIndex = value;
            }
        }

        //Cell text getter/setter
        public string Text
        {
            get { return text; }
            protected internal set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        //Cell value getter/setter
        public string Value
        {
            get { return value; }
            protected internal set
            {
                if (value == this.Value)
                {
                    return;
                }
                else
                {
                    this.value = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }
    }
}
