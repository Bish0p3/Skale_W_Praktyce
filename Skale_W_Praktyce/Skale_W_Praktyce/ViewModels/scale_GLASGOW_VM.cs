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
    internal class scale_GLASGOW_VM : INotifyPropertyChanged
    {
        public scale_GLASGOW_VM()
        {
            #region questions and answers
            ScaleQuestions = new ObservableCollection<ScaleAnswersQuestion>();
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Otwieranie oczu:" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "spontaniczne", QuestionAnswerPoints = 4 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "na polecenie", QuestionAnswerPoints = 3 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "na bodźce bólowe", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "nie otwiera oczu", QuestionAnswerPoints = 1 });


            var que2 = new ScaleAnswersQuestion() { QuestionName = "Kontakt słowny:" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "odpowiedź logiczna, pacjent zorientowany co do miejsca, czasu i własnej osoby", QuestionAnswerPoints = 5 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "odpowiedź splątana, pacjent zdezorientowany", QuestionAnswerPoints = 4 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "odpowiedź nieadekwatna, nie na temat lub krzyk", QuestionAnswerPoints = 3 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "niezrozumiałe dźwięki, pojękiwanie", QuestionAnswerPoints = 2 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "bez reakcji", QuestionAnswerPoints = 1 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Reakcja ruchowa:" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "Spełnianie ruchowych poleceń słownych, migowych", QuestionAnswerPoints = 6 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "ruchy celowe, pacjent lokalizuje bodziec bólowy", QuestionAnswerPoints = 5 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "reakcja obronna na ból, wycofanie, próba usunięcia bodźca bólowego", QuestionAnswerPoints = 4 });
            que3.Add(new ScaleAnswers()
            {
                QuestionID = 2,
                QuestionAnswer = "patologiczna reakcja zgięciowa, odkorowanie (przywiedzenie ramion, zgięcie w stawach łokciowych" +
                "i ręki, przeprost w stawach kończyn dolnych)",
                QuestionAnswerPoints = 3
            });
            que3.Add(new ScaleAnswers()
            {
                QuestionID = 2,
                QuestionAnswer = "patologiczna reakcja wyprostna, odmóżdżenie (odwiedzenie i obrót ramion do wewnątrz, wyprost w stawach" +
                "łokciowych, nawrócenie przedramion i zgięcie stawów ręki, przeprost w stawach kończyn dolnych, odwrócenie stopy)",
                QuestionAnswerPoints = 2
            });
            que3.Add(new ScaleAnswers()
            { QuestionID = 2, QuestionAnswer = "brak reakcji", QuestionAnswerPoints = 1 });


            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);

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
            if (Score >= 13)
            {
                Diagnosis = "łagodne zaburzenia świadomości";
            }
            else if (Score >= 9 && Score <= 12)
            {
                Diagnosis = "umiarkowane zaburzenia świadomości";
            }
            else if (Score >= 6 && Score <= 8)
            {
                Diagnosis = "brak przytomności";
            }
            else if (Score == 5)
            {
                Diagnosis = "odkorowanie";
            }
            else if (Score == 4)
            {
                Diagnosis = "odmóżdżenie";
            }
            else if (Score == 3)
            {
                Diagnosis = "śmierć mózgu";
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
                "Skala Glasgow to skala pozwalająca określić poziom przytomności pacjenta, " +
                "stosowana w medycynie, szczególnie w przypadku pacjentów po urazie głowy. " +
                "Stosuje się ją zarówno w medycynie ratunkowej, " +
                "jak i do oceny zmian w poziomie świadomości pacjenta w trakcie leczenia." +
                "\nŹródło:\nhttps://www.medonet.pl/zdrowie,skala-glasgow---swiadomosc--ocena--przytomnosc,artykul,1731057.html", "OK");

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
