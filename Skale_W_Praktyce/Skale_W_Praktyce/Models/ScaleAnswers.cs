using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Skale_W_Praktyce.Models
{
    internal class ScaleAnswers : INotifyPropertyChanged
    {

        public ScaleAnswers()
        {
            //ScaleViewName = typeof(Scale);
        }

        #region Fields
        private string questionAnswer;
        private int questionAnswerPoints;
        #endregion

        #region Properties
        public string QuestionAnswer
        {
            get { return questionAnswer; }
            set
            {
                if (questionAnswer != value)
                {
                    questionAnswer = value;
                    RaisePropertyChanged("QuestionAnswer");
                }
            }
        }
        public int QuestionAnswerPoints
        {
            get { return questionAnswerPoints; }
            set
            {
                if (questionAnswerPoints != value)
                {
                    questionAnswerPoints = value;
                    RaisePropertyChanged("QuestionAnswerPoints");
                }
            }
        }
        #endregion

        public class ScaleAnswersQuestion : ObservableCollection<ScaleAnswers>
        {
            private string questionName;
            public string QuestionName
            {
                get { return questionName; }
                set
                {
                    if (questionName != value)
                    {
                        questionName = value;
                    }
                }
            }


        }



        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion

    }

}
