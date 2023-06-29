using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WPF_Serial.Model
{
    public static class FileXml
    {
        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;
        public static void Save(this CreateReview reviews, string filepath) // Зберігає колекцію відгуків в файл XML
        {
            try
            {
                XDocument xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), // Створення XML-документа з указаною структурою та даними
                    new XComment("DateSave: " + DateTime.Now.ToString(formatProvider)),
                    new XElement("CreateReview", from Review review in reviews
                                                 select
                                                 new XElement("CreateReview",
                                                 new XElement("Description", review.Descr),
                                                 new XElement("DateCreate", review.DateCreate.ToString(formatProvider)),
                                                 new XElement("DateUpdate", review.DateUpdate.ToString(formatProvider)),
                                                 new XElement("Priority", ((byte)(review.Priority)).ToString()),
                                                 new XElement("Completed", review.Completed.ToString(formatProvider)))));
                xml.Save(filepath);
            }
            catch(Exception ex) 
            {
                throw new Exception("Error save", ex);
            }
        }
        public static CreateReview Read(string filepath) // Зчитує колекцію відгуків із файлу XML
        {
            try
            {
                XDocument xml = XDocument.Load(filepath);
                IEnumerable<Review> reviews =
                    from review in xml.Root.Descendants("Review")
                    select new Review(
                        review.Element("Description").Value,
                        DateTime.Parse(review.Element("DateCreate").Value, formatProvider),
                        DateTime.Parse(review.Element("DateUpdate").Value, formatProvider),
                        (PriorityModer)byte.Parse(review.Element("Priority").Value, formatProvider),
                        bool.Parse(review.Element("Completed").Value));
                CreateReview review1 = new CreateReview();
                foreach (Review review in reviews) review1.CreatedReview(review);
                return review1;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error read", ex);
            }
        }
    }
}
