using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Serial.Model
{
    public class CreateReview : IEnumerable<Review> // Перехід до елемента
    {
        private List<Review> reviews = new List<Review>();

        // CRUD
        public void CreatedReview(Review review)
        {
            reviews.Add(review);
        }
        public bool DeleteReview(Review review)
        {
            return reviews.Remove(review);
        }

        
        public int CountReview
        {
            get 
            { 
                return reviews.Count; 
            }
        }
        public Review this[int index] // Індекс
        {
            get
            {
                return reviews[index];
            }
        }
        public IEnumerator<Review> GetEnumerator()
        {
            return reviews.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

    }
}
