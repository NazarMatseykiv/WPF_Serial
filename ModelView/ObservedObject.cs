using System.ComponentModel;


namespace WPF_Serial.ModelView
{
    public abstract class ObservedObject : INotifyPropertyChanged // Базовий абстрактний клас для об’єктів, які підтримують повідомлення про зміну властивості
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(params string[] propertyName)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyNames in propertyName) 
                {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyNames));
                }
            }
        }
    }
}
