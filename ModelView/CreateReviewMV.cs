using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace WPF_Serial.ModelView
{
    public class CreateReviewMV
    {
        private Model.CreateReview cmodel;
        public ObservableCollection<ReviewMV> ListCR { get; } = new ObservableCollection<ReviewMV>();
        public void CopyReview() // Метод для копіювання об'єктів ReviewMV з моделі в колекцію ListCR
        {
            ListCR.CollectionChanged -= SynchModel; // Відключення обробника подій колекції
            ListCR.Clear(); // Очищення колекції
            foreach (Model.Review review in cmodel) // Додавання нових об'єктів ReviewMV в колекцію
                ListCR.Add(new ReviewMV(review));
            ListCR.CollectionChanged += SynchModel; // Підключення обробника подій колекції
        }
        private string filepath = "review.xml"; // Файл для збереження даних
        public CreateReviewMV()
        { 
            if (System.IO.File.Exists(filepath)) cmodel = Model.FileXml.Read(filepath); // Завантаження моделі з файлу
            else cmodel = new Model.CreateReview(); // Створення нової моделі, якщо файл не існує

            // Test
            cmodel.CreatedReview(new Model.Review("First", DateTime.Now, DateTime.Now.AddDays(3), Model.PriorityModer.NotBad));
            cmodel.CreatedReview(new Model.Review("Two", DateTime.Now, DateTime.Now.AddDays(2), Model.PriorityModer.Good));
            cmodel.CreatedReview(new Model.Review("Third", DateTime.Now, DateTime.Now.AddDays(5), Model.PriorityModer.Bad));

            CopyReview();
        }
        private void SynchModel(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action) // Обробник подій змін в колекції ListCR
            {
                case NotifyCollectionChangedAction.Add:
                    ReviewMV newreview = (ReviewMV)e.NewItems[0]; // Додавання нового елемента до колекції
                    if (newreview != null) cmodel.CreatedReview(newreview.GetModel());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ReviewMV removereview = (ReviewMV)e.OldItems[0];
                    if (removereview != null) cmodel.DeleteReview(removereview.GetModel());
                    break;
            }
        }
        public ICommand Save // Команда для збереження даних
        {
            get
            {
                return new RelayCommand(argument =>
                {
                    Model.FileXml.Save(cmodel, filepath);
                });
            }
        }
        private ICommand deleteReview; // Команда для видалення рецензії
        public ICommand DeleteReview
        {
            get
            {
                if (deleteReview == null) deleteReview = new RelayCommand(
                    o =>
                    {
                        int indexreview = (int)o;
                        ReviewMV reviewMW = ListCR[indexreview];
                        ListCR.Remove(reviewMW);
                    },
                    o =>
                    {
                        if (o == null) return false;
                        int indexreview = (int)o;
                        return indexreview >= 0;
                    });
                return deleteReview;
            }
        }
        private ICommand addReview; // Команда для створення рецензії
        public ICommand AddReview
        {
            get
            {
                if (addReview == null) addReview = new RelayCommand(
                    o =>
                    {
                        ReviewMV reviewM = o as ReviewMV;
                        if (reviewM != null) ListCR.Add(reviewM);
                    },
                    o =>
                    {
                        return (o as ReviewMV) != null;
                    });
                return addReview;
            }
        }
    }   
}
