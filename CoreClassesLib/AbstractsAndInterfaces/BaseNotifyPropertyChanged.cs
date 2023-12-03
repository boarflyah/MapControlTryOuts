using System.ComponentModel;

namespace CoreClassesLib.AbstractsAndInterfaces

{
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged(string property, object oldValue = null, object newValue = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            OnPropertyChanged(property, oldValue, newValue);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        protected virtual void OnPropertyChanged(string property, object oldValue = null, object newValue = null) { }
    }
}
