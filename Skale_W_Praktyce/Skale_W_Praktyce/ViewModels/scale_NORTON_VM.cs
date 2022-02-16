using Skale_W_Praktyce.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Skale_W_Praktyce.Models.ScaleAnswers;

namespace Skale_W_Praktyce.ViewModels
{
    internal class scale_NORTON_VM : INotifyPropertyChanged
    {
        public scale_NORTON_VM()
        {
            #region questions and answers
            ScaleQuestions = new ObservableCollection<ScaleAnswersQuestion>();
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Stan fizyczny pacjenta:" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "bardzo ciężki", QuestionAnswerPoints = 1 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "średni", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "dość dobry", QuestionAnswerPoints = 3 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "dobry", QuestionAnswerPoints = 4 });

            var que2 = new ScaleAnswersQuestion() { QuestionName = "Stan świadomości:" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "stupor lub śpiączka", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "zaburzona", QuestionAnswerPoints = 2 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "apatia", QuestionAnswerPoints = 3 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "pełna przytomność i świadomość", QuestionAnswerPoints = 4 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Aktywność lokomocyjna:" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "stale pozostaje w łóżku", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "porusza się tylko na wózku inwalidzkim", QuestionAnswerPoints = 2 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "chodzi z pomocą", QuestionAnswerPoints = 3 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "chodzi samodzielnie", QuestionAnswerPoints = 4 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Stopień samodzielności zmian pozycji ciała:" };
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "całkowita niesprawność", QuestionAnswerPoints = 1 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "bardzo ograniczona", QuestionAnswerPoints = 2 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "lekko ograniczona", QuestionAnswerPoints = 3 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "pełna", QuestionAnswerPoints = 4 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Czynność zwieraczy odbytu i cewki moczowej:" };
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "całkowite nieotrzymanie stolca", QuestionAnswerPoints = 1 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "zazwyczaj nieotrzymanie moczu", QuestionAnswerPoints = 2 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "sporadyczne moczenie się", QuestionAnswerPoints = 3 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "pełna sprawność zwieraczy", QuestionAnswerPoints = 4 });



            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);
            ScaleQuestions.Add(que4);
            ScaleQuestions.Add(que5);

            InfoCommand = new Command(async () => await InfoMethod());

            #endregion

            AnswerTappedCommand = new Command(HandleSelectedAnswer);

        }

        public ObservableCollection<ScaleAnswersQuestion> scaleQuestions;

        #region Fields

        private ScaleAnswers selectedAnswer;
        private string diagnosis = "..";
        private int score;

        #endregion

        #region Properties
        public int Score
        {
            get => score;
            set
            {
                if (score != value)
                {
                    score = value;
                    OnPropertyChanged("Score");
                }
            }
        }
        public string Diagnosis
        {
            get => diagnosis;
            set
            {
                if (diagnosis != value)
                {
                    diagnosis = value;
                    OnPropertyChanged("Diagnosis");
                }
            }
        }

        public ObservableCollection<ScaleAnswersQuestion> ScaleQuestions
        {
            get { return scaleQuestions; }
            set
            {
                if (scaleQuestions != value)
                {
                    scaleQuestions = value;
                    OnPropertyChanged("ScaleQuestions");
                }
            }
        }


        public ScaleAnswers SelectedAnswer
        {
            get { return selectedAnswer; }
            set
            {
                if (selectedAnswer != value)
                {
                    selectedAnswer = value;
                }
            }
        }

        #endregion

        #region Commands
        public ICommand AnswerTappedCommand { get; set; }
        public ICommand InfoCommand { get; }

        #endregion

        #region Methods
        private void HandleSelectedAnswer()
        {
            if (SelectedAnswer.IsSelected == false)
            {
                foreach (var answer in ScaleQuestions[SelectedAnswer.QuestionID])
                {
                    if (answer.IsSelected == true)
                    {
                        answer.AnswerSelectedColor = Color.Transparent;
                        answer.IsSelected = false;
                        Score -= answer.QuestionAnswerPoints;
                    }
                }
                Score += SelectedAnswer.QuestionAnswerPoints;
                SelectedAnswer.AnswerSelectedColor = Color.FromHex("#F07167");
                SelectedAnswer.IsSelected = true;
                CheckStatus();
            }
            else
            {
                Score -= SelectedAnswer.QuestionAnswerPoints;
                SelectedAnswer.AnswerSelectedColor = Color.Transparent;
                SelectedAnswer.IsSelected = false;
                CheckStatus();
            }
        }
        private void CheckStatus()
        {
            if (Score >= 15)
            {
                Diagnosis = "Małe ryzyko wystąpienia odleżyn";
            }
            else if (Score < 15)
            {
                Diagnosis = "Duże ryzyko wystąpienia odleżyn";
            }

        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
                "Skala Norton może być stosowana w każdym przypadku pacjenta po zabiegu lub w okresie choroby. Szczególnie przydatna jest w zakładach opieki medycznej oraz w domach seniora, do oceny ryzyka rozwoju odleżyn u danej osoby. Zgodnie z wynikiem uzyskanym w skali Norton stosuje się środki zaradcze (materace, częste zmiany pozycji, smarowanie skóry chorego odpowiednimi preparatami) i monitoruje stan pacjenta." +
                "\nŹródło:\nhttps://www.medonet.pl/zdrowie,skala-norton---czego-dotyczy-i-jak-oraz-kiedy-sie-ja-stosuje,artykul,1732049.html", "OK");

        }
        #endregion



        #region Helpers
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
