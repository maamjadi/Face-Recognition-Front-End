using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamvvm;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using DLToolkit.Forms.Controls;
using System.Collections.Generic;

namespace FaceRecognitionFrontEnd
{
    public class MainPageModel : BasePageModel
	{
        public MainPageModel() 
        {
            ReloadData();
        }

        public void ReloadData()
        {
            var list = new ObservableCollection<ItemModel>();

            string[] subjects = {
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
                "Client Side Technologies",
            };

            int[] percentages = {
                57,
                23,
                24,
                65,
                75,
                34,
                23,
                10,
                90,
            };

            for (int i = 0; i < subjects.Length; i++)
            {
                var item = new ItemModel()
                {
                    SubjectName = subjects[i],
                    Percentage = string.Format("{0}%", percentages[i]),
                };

                list.Add(item);
            }

            list.Add(addCreateNewSubjectBtn());

            Items = list;
        }

        private ItemModel addCreateNewSubjectBtn() {
            var item = new ItemModel()
            {
                Add = "+",
            };
            return item;
        }

        public ObservableCollection<ItemModel> Items
        {
            get { return GetField<ObservableCollection<ItemModel>>(); }
            set { SetField(value); }
        }

        public class ItemModel : BaseModel
        {
            string subjectName;
            public string SubjectName
            {
                get { return subjectName; }
                set { SetField(ref subjectName, value); }
            }

            string percentage;
            public string Percentage
            {
                get { return percentage; }
                set { SetField(ref percentage, value); }
            }

            string add;
            public string Add
            {
                get { return add; }
                set { SetField(ref add, value); }
            }
        }
    }
}
