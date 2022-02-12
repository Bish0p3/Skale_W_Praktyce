using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

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
        private bool isSelected = false;
        private Color answerSelectedColor;
        #endregion

        #region Properties
        public Color AnswerSelectedColor
        {
            get { return answerSelectedColor; }
            set
            {
                if (answerSelectedColor != value)
                {
                    answerSelectedColor = value;
                    RaisePropertyChanged("AnswerSelectedColor");
                }
            }
        }
        public string QuestionAnswer
        {
            get => questionAnswer;
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
            get => questionAnswerPoints;
            set
            {
                if (questionAnswerPoints != value)
                {
                    questionAnswerPoints = value;
                    RaisePropertyChanged("QuestionAnswerPoints");
                }
            }
        }
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    RaisePropertyChanged("IsSelected");
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
