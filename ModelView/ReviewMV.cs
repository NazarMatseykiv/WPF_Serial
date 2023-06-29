using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WPF_Serial.Model;

namespace WPF_Serial.ModelView
{
    public class ReviewMV : ObservedObject
    {
        private Model.Review modelr;

        #region Властивості
        public string Descr
        {
            get
            {
                return modelr.Descr;
            }
        }
        public Model.PriorityModer Priority
        {
            get { return modelr.Priority; }
        }
        public DateTime DateCreate
        {
            get
            {
                return modelr.DateCreate;
            }
        }
        public DateTime DateUpdate
        {
            get
            {
                return modelr.DateUpdate;
            }
        }
        public bool Completed
        {
            get { return modelr.Completed; }
        }
        public bool ReviewCompTerm // Резеценція створена чи не створена
        {
            get
            {
                return !Completed && (DateTime.Now > DateUpdate);
            }
        }
        #endregion
        public ReviewMV(Model.Review model) 
        {
            this.modelr = model;
        }
        public ReviewMV(string Descr, DateTime DateCreate, DateTime DateUpdate, Model.PriorityModer priority, bool Completed)
        {
            modelr = new Model.Review(Descr, DateCreate, DateUpdate, priority, Completed); 
        }
        public Model.Review GetModel()
        {
            return modelr;
        }
        #region Команди
        // Пов'язання команд з елементами керування в представленні та забезпечення логіки для зміни значень відповідних властивостей моделі при виконанні команд.
        private ICommand MarkAsCompleted;
        public ICommand markascompleted // Виконується, коли елемент керування (наприклад, кнопка) пов'язаний з цією командою, активується
        {
            get
            {
                if (MarkAsCompleted == null) MarkAsCompleted = new RelayCommand(
                    o => 
                    {
                    modelr.Completed = true;
                        onPropertyChanged(nameof(Completed), nameof(ReviewCompTerm));
                },
                    o =>
                    {
                        return !modelr.Completed;
                    });
                return MarkAsCompleted;
            }
        }
        private ICommand MarkAsNotCompleted;
        public ICommand markasnotcompleted // Виконується, коли елемент керування пов'язаний з цією командою, активується.
        {
            get
            {
                if (MarkAsNotCompleted == null) MarkAsNotCompleted = new RelayCommand(
                    o =>
                    {
                        modelr.Completed = false;
                        onPropertyChanged(nameof(Completed), nameof(ReviewCompTerm));
                    },
                    o =>
                    {
                        return modelr.Completed;
                    });
                return MarkAsNotCompleted;
            }
        }
        #endregion 
        public override string ToString()
        {
            return modelr.ToString();
        }
    }
}
