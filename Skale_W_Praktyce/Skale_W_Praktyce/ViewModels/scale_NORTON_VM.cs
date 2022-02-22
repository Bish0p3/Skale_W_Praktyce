﻿using Skale_W_Praktyce.Models;
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
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Wyjściowy hematokryt (%):" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "<31", QuestionAnswerPoints = 9 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "31-33.9", QuestionAnswerPoints = 7 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "34-36.9", QuestionAnswerPoints = 3 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "37-39.9", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = ">=40", QuestionAnswerPoints = 0 });


            var que2 = new ScaleAnswersQuestion() { QuestionName = "Klirens kreatyniny (ml/min):" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "<15", QuestionAnswerPoints = 39 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = ">15-30", QuestionAnswerPoints = 35 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = ">30-60", QuestionAnswerPoints = 28 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = ">60-90", QuestionAnswerPoints = 17 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = ">90-120", QuestionAnswerPoints = 7 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = ">=120", QuestionAnswerPoints = 0 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Częstotliwość rytmu serca/min:" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "<70", QuestionAnswerPoints = 0 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "71-80", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "81-90", QuestionAnswerPoints = 3 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "91-100", QuestionAnswerPoints = 6 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "101-110", QuestionAnswerPoints = 8 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "111-120", QuestionAnswerPoints = 10 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = ">121", QuestionAnswerPoints = 11 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Płeć:" };
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "Kobieta", QuestionAnswerPoints = 8 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "Mężczyzna", QuestionAnswerPoints = 0 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Objawy przedmiotowe niewydolności serca przy przyjęciu:" };
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "Nie", QuestionAnswerPoints = 0 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "Tak", QuestionAnswerPoints = 7 });

            var que6 = new ScaleAnswersQuestion() { QuestionName = "Objawy przedmiotowe niewydolności serca przy przyjęciu:" };
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "Nie", QuestionAnswerPoints = 0 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "Tak", QuestionAnswerPoints = 7 });

            var que7 = new ScaleAnswersQuestion() { QuestionName = "Objawy przedmiotowe niewydolności serca przy przyjęciu:" };
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "Nie", QuestionAnswerPoints = 0 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "Tak", QuestionAnswerPoints = 7 });

            var que8 = new ScaleAnswersQuestion() { QuestionName = "Objawy przedmiotowe niewydolności serca przy przyjęciu:" };
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "Nie", QuestionAnswerPoints = 0 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "Tak", QuestionAnswerPoints = 7 });



            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);
            ScaleQuestions.Add(que4);
            ScaleQuestions.Add(que5);
            ScaleQuestions.Add(que6);
            ScaleQuestions.Add(que7);
            ScaleQuestions.Add(que8);


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
