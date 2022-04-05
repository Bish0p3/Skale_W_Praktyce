using Skale_W_Praktyce.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Skale_W_Praktyce.Models.ScaleAnswers;

namespace Skale_W_Praktyce.ViewModels
{
    internal class Scale_MMSE_VM : INotifyPropertyChanged
    {
        #region Constructor
        public Scale_MMSE_VM()
        {
            Init();

            SetBookmarkImage();

            BookmarkScaleCommand = new Command(async () => await BookmarkScaleMethodAsync());

            InfoCommand = new Command(async () => await InfoMethod());

            AnswerTappedCommand = new Command(HandleSelectedAnswer);
        }
        #endregion

        #region Fields
        private ObservableCollection<ScaleAnswersQuestion> scaleQuestions;
        private ScaleAnswers selectedAnswer;
        private string diagnosis = "..";
        private int score;
        private string bookmarkImgSrc;
        private bool isBookmarked = false;
        private string bookmarkNotificationText;
        private bool bookmarkNotificationVisibility = false;
        #endregion

        #region Properties
        public string BookmarkNotificationText
        {
            get { return bookmarkNotificationText; }
            set
            {
                bookmarkNotificationText = value;
                OnPropertyChanged("BookmarkNotificationText");
            }
        }
        public bool BookmarkNotificationVisibility
        {
            get { return bookmarkNotificationVisibility; }
            set
            {
                bookmarkNotificationVisibility = value;
                OnPropertyChanged("BookmarkNotificationVisibility");
            }
        }
        public string BookmarkImgSrc
        {
            get => bookmarkImgSrc;
            set
            {
                if (bookmarkImgSrc != value)
                {
                    bookmarkImgSrc = value;
                    OnPropertyChanged("BookmarkImgSrc");
                }
            }
        }
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

        public ICommand BookmarkScaleCommand { get; set; }
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
        private async Task BookmarkNotificationTask(bool IsEnabled)
        {
            if (IsEnabled)
            {
                BookmarkNotificationText = "Dodano do ulubionych";
                BookmarkNotificationVisibility = true;
                await Task.Delay(2000);
                BookmarkNotificationVisibility = false;
            }
            else
            {
                BookmarkNotificationText = "Usunięto z ulubionych";
                BookmarkNotificationVisibility = true;
                await Task.Delay(2000);
                BookmarkNotificationVisibility = false;
            }
        }
        #endregion

        #region Helpers
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Unique
        private void Init()
        {
            ScaleQuestions = new ObservableCollection<ScaleAnswersQuestion>();
            var que1 = new ScaleAnswersQuestion() { QuestionName = "1. ORIENTACJA W CZASIE I W MIEJSCU" };

            var queorientacja = new ScaleAnswersQuestion() { QuestionName = "Orientacja w czasie" };

            var que2 = new ScaleAnswersQuestion() { QuestionName = "Jaki jest teraz rok?" };
            que2.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Jaka jest teraz pora roku?" };
            que3.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Jaki jest teraz miesiąc?" };
            que4.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que4.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Jaka jest dzisiejsza data (którego dzisiaj mamy)?" };
            que5.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que5.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que6 = new ScaleAnswersQuestion() { QuestionName = "Jaki jest dzisiaj dzień tygodnia?" };
            que6.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que6.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que7 = new ScaleAnswersQuestion() { QuestionName = "Orientacja w miejscu" };

            var que8 = new ScaleAnswersQuestion() { QuestionName = "W jakim kraju się znajdujemy?" };
            que8.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que8.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que9 = new ScaleAnswersQuestion() { QuestionName = "W jakim województwie się znajdujemy?" };
            que9.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que9.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que10 = new ScaleAnswersQuestion() { QuestionName = "W jakim mieście się teraz znajdujemy?" };
            que10.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que10.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que11 = new ScaleAnswersQuestion() { QuestionName = "Jak nazywa się miejsce, w którym się teraz znajdujemy?" };
            que11.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que11.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que12 = new ScaleAnswersQuestion() { QuestionName = "Na którym piętrze się obecnie znajdujemy?" };
            que12.Add(new ScaleAnswers() { QuestionID = 12, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que12.Add(new ScaleAnswers() { QuestionID = 12, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que13 = new ScaleAnswersQuestion() { QuestionName = "2. ZAPAMIĘTYWANIE" };

            var quezapamietywanie = new ScaleAnswersQuestion() { QuestionName = "Wypowiadamy trzy słowa, wolno i wyrażnie (jedno słowo na sekunde), i prosimy o ich zapamiętanie." };

            var que14 = new ScaleAnswersQuestion() { QuestionName = "BYK" };
            que14.Add(new ScaleAnswers() { QuestionID = 15, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que14.Add(new ScaleAnswers() { QuestionID = 15, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que15 = new ScaleAnswersQuestion() { QuestionName = "MUR" };
            que15.Add(new ScaleAnswers() { QuestionID = 16, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que15.Add(new ScaleAnswers() { QuestionID = 16, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que16 = new ScaleAnswersQuestion() { QuestionName = "LAS" };
            que16.Add(new ScaleAnswers() { QuestionID = 17, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que16.Add(new ScaleAnswers() { QuestionID = 17, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que17 = new ScaleAnswersQuestion() { QuestionName = "3. UWAGA I LICZENIE" };

            var queliczenie = new ScaleAnswersQuestion() { QuestionName = "Pacjent ma za zadanie odejmować kolejno od 100 po 7, do liczby 65." };

            var que18 = new ScaleAnswersQuestion() { QuestionName = "93" };
            que18.Add(new ScaleAnswers() { QuestionID = 20, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que18.Add(new ScaleAnswers() { QuestionID = 20, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que19 = new ScaleAnswersQuestion() { QuestionName = "86" };
            que19.Add(new ScaleAnswers() { QuestionID = 21, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que19.Add(new ScaleAnswers() { QuestionID = 21, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que20 = new ScaleAnswersQuestion() { QuestionName = "79" };
            que20.Add(new ScaleAnswers() { QuestionID = 22, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que20.Add(new ScaleAnswers() { QuestionID = 22, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que21 = new ScaleAnswersQuestion() { QuestionName = "72" };
            que21.Add(new ScaleAnswers() { QuestionID = 23, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que21.Add(new ScaleAnswers() { QuestionID = 23, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que22 = new ScaleAnswersQuestion() { QuestionName = "65" };
            que22.Add(new ScaleAnswers() { QuestionID = 24, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que22.Add(new ScaleAnswers() { QuestionID = 24, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que23 = new ScaleAnswersQuestion() { QuestionName = "4. PRZYPOMINANIE:" };

            var queprzypominanie = new ScaleAnswersQuestion() { QuestionName = "Prosimy pacjenta o wymienienie trzech słów, które miał za zadanie zapamiętać." };

            var que24 = new ScaleAnswersQuestion() { QuestionName = "BYK" };
            que24.Add(new ScaleAnswers() { QuestionID = 27, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que24.Add(new ScaleAnswers() { QuestionID = 27, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que25 = new ScaleAnswersQuestion() { QuestionName = "MUR" };
            que25.Add(new ScaleAnswers() { QuestionID = 28, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que25.Add(new ScaleAnswers() { QuestionID = 28, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que26 = new ScaleAnswersQuestion() { QuestionName = "LAS" };
            que26.Add(new ScaleAnswers() { QuestionID = 29, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que26.Add(new ScaleAnswers() { QuestionID = 29, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que27 = new ScaleAnswersQuestion() { QuestionName = "5. FUNKCJE JĘZYKOWE:" };

            var que28 = new ScaleAnswersQuestion() { QuestionName = "Nazywanie: Prosimy o nazwanie dwóch przedmiotów, które kolejno pokazujemy badanemu (ołówek, zegarek)" };

            var que29 = new ScaleAnswersQuestion() { QuestionName = "Jak się nazywa pierwszy przedmiot?" };
            que29.Add(new ScaleAnswers() { QuestionID = 32, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que29.Add(new ScaleAnswers() { QuestionID = 32, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que30 = new ScaleAnswersQuestion() { QuestionName = "Jak się nazywa drugi przedmiot?" };
            que30.Add(new ScaleAnswers() { QuestionID = 33, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que30.Add(new ScaleAnswers() { QuestionID = 33, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que31 = new ScaleAnswersQuestion() { QuestionName = "Powtarzanie: Prosimy o powtórzenie zdania: 'Ani tak, ani nie, ani ale'" };
            que31.Add(new ScaleAnswers() { QuestionID = 34, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que31.Add(new ScaleAnswers() { QuestionID = 34, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que33 = new ScaleAnswersQuestion() { QuestionName = "Wykonywanie poleceń" };

            var que34 = new ScaleAnswersQuestion() { QuestionName = "proszę wziąć kartkę do lewej/prawej ręki" };
            que34.Add(new ScaleAnswers() { QuestionID = 36, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que34.Add(new ScaleAnswers() { QuestionID = 36, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que35 = new ScaleAnswersQuestion() { QuestionName = "złożyć ją oburącz na połowę" };
            que35.Add(new ScaleAnswers() { QuestionID = 37, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que35.Add(new ScaleAnswers() { QuestionID = 37, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que36 = new ScaleAnswersQuestion() { QuestionName = "i położyć ją na kolana" };
            que36.Add(new ScaleAnswers() { QuestionID = 38, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que36.Add(new ScaleAnswers() { QuestionID = 38, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que37 = new ScaleAnswersQuestion() { QuestionName = "Pokazujemy badanemu tekst polecenia zamieszczony na okładce: „proszę zamknąć oczy" };
            que37.Add(new ScaleAnswers() { QuestionID = 39, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que37.Add(new ScaleAnswers() { QuestionID = 39, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que38 = new ScaleAnswersQuestion() { QuestionName = "Pisanie: Dajemy osobie badanej czystą kartkę papieru i prosimy o napisanie dowolnego zdania." };
            que38.Add(new ScaleAnswers() { QuestionID = 40, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que38.Add(new ScaleAnswers() { QuestionID = 40, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });

            var que39 = new ScaleAnswersQuestion() { QuestionName = "6. PRAKSJA KONSTRUKCYJNA" };

            var que40 = new ScaleAnswersQuestion() { QuestionName = "Prosimy pacjenta o przerysowanie rysunku tak dokładnie, jak tylko jest to możliwe (Obrazek: patrz źródło)" };
            que40.Add(new ScaleAnswers() { QuestionID = 42, QuestionAnswer = "poprawnie", QuestionAnswerPoints = 1 });
            que40.Add(new ScaleAnswers() { QuestionID = 42, QuestionAnswer = "nie poprawnie", QuestionAnswerPoints = 0 });


            #region Add questions
            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(queorientacja);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);
            ScaleQuestions.Add(que4);
            ScaleQuestions.Add(que5);
            ScaleQuestions.Add(que6);
            ScaleQuestions.Add(que7);
            ScaleQuestions.Add(que8);
            ScaleQuestions.Add(que9);
            ScaleQuestions.Add(que10);
            ScaleQuestions.Add(que11);
            ScaleQuestions.Add(que12);
            ScaleQuestions.Add(que13);
            ScaleQuestions.Add(quezapamietywanie);
            ScaleQuestions.Add(que14);
            ScaleQuestions.Add(que15);
            ScaleQuestions.Add(que16);
            ScaleQuestions.Add(que17);
            ScaleQuestions.Add(queliczenie);
            ScaleQuestions.Add(que18);
            ScaleQuestions.Add(que19);
            ScaleQuestions.Add(que20);
            ScaleQuestions.Add(que21);
            ScaleQuestions.Add(que22);
            ScaleQuestions.Add(que23);
            ScaleQuestions.Add(queprzypominanie);
            ScaleQuestions.Add(que24);
            ScaleQuestions.Add(que25);
            ScaleQuestions.Add(que26);
            ScaleQuestions.Add(que27);
            ScaleQuestions.Add(que28);
            ScaleQuestions.Add(que29);
            ScaleQuestions.Add(que30);
            ScaleQuestions.Add(que31);
            ScaleQuestions.Add(que33);
            ScaleQuestions.Add(que34);
            ScaleQuestions.Add(que35);
            ScaleQuestions.Add(que36);
            ScaleQuestions.Add(que37);
            ScaleQuestions.Add(que38);
            ScaleQuestions.Add(que39);
            ScaleQuestions.Add(que40);

            #endregion

        }
        private void CheckStatus()
        {
            if (Score >= 27)
            {
                Diagnosis = "Wynik prawidłowy";
            }
            else if (Score >= 24 && Score <= 26)
            {
                Diagnosis = "Zaburzenia poznawcze bez otępienia";
            }
            else if (Score >= 19 && Score <= 23)
            {
                Diagnosis = "Otępienie lekkiego stopnia";
            }
            else if (Score >= 11 && Score <= 18)
            {
                Diagnosis = "Otępienie średniego stopnia";
            }
            else if (Score <= 10)
            {
                Diagnosis = "Otępienie głębokie";
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
                "INTERPRETACJA KLINICZNA: Skala MMSE jest prostym narzędziem przesiewowym. Uzyskanie przez osobębadaną wyniku poniżej wartości odcięcia stanowi konieczność podjęcia dalszychbadań diagnostycznych w celu potwierdzenia bądź wykluczenia otępienia. Nawynik MMSE, oprócz stanu psychicznego badanego ma wpływ: sposób posługiwania się skalą przez badającego, wiek i wykształcenie osoby badanej. MAKSYMALNY WYNIK = 30 PUNKTÓW Wg kryteriów diagnostycznych DSM IV oraz ICD-10 wynik niższy niż 24 punkty w Skali MMSE sugeruje obecność zespołu otępiennego. Ze względu na znaczący wpływ wieku I wykształcenia na uzyskany wynik proponuje się, by wyniki odpowiadające wartościom poniżej 27 punktów traktować Jako podstawę do przeprowadzenia dalszego szczegółowego badania klinicznego mającego na celu potwierdzenie bądź wykluczenie zespołu otępiennego. Podwyższenie punktu odcięcia (27/26) zwiększa czułość Skali MMSE w wykrywaniu wczesnych objawów otępienia u osób z wysokim wykształceniem, U osób z niskim wykształceniem, zwłaszcza w najstarszych grupach wiekowych, wskazane jest obniżenie punktu odcięcia do poziomu 22/23." +
                "\nŹródło:\nhttps://mlodzilekarzerodzinni.pl/wp-content/uploads/2020/01/MMSE.pdf", "OK");
        }
        private async Task SetBookmarkImage()
        {
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            if (bookmarks.Any(x => x.ScaleName.Contains("MMSE")))
            {
                BookmarkImgSrc = "bookmark_saved.png";

            }
            else
            {
                BookmarkImgSrc = "bookmark.png";
            }

        }
        private async Task BookmarkScaleMethodAsync()
        {
            // BAZA
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();

            if (bookmarks.Any(x => x.ScaleName.Contains("MMSE")))
            {
                //userData.Favorites.Remove("GLASGOW");
                Bookmark x = await App.Database.GetBookmarkAsyncName("MMSE");
                await App.Database.DeleteBookmarkAsync(x);

                BookmarkImgSrc = "bookmark.png";
                isBookmarked = false;
                BookmarkNotificationTask(isBookmarked);
            }
            else
            {
                await App.Database.SaveBookmarkAsync(new Bookmark { ScaleName = "MMSE", UserID = 0 });

                BookmarkImgSrc = "bookmark_saved.png";
                isBookmarked = true;
                BookmarkNotificationTask(isBookmarked);
            }
        }
        #endregion
    }
}
