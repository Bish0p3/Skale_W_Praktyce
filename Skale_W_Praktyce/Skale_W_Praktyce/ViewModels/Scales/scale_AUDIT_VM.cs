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
    internal class Scale_AUDIT_VM : INotifyPropertyChanged
    {
        #region Constructor
        public Scale_AUDIT_VM()
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
        int poziom_ryzyka = 0;
        bool ryzyko = false;
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
                        if (SelectedAnswer.QuestionID < 3 && SelectedAnswer.QuestionAnswerPoints < 2)
                        {
                            if (poziom_ryzyka > 0)
                            {
                                poziom_ryzyka = poziom_ryzyka - 1;
                            }
                        }
                    }
                }
                if (SelectedAnswer.QuestionID < 3 && SelectedAnswer.QuestionAnswerPoints > 2)
                {
                    poziom_ryzyka = poziom_ryzyka + 1;
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
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Jak często pijesz napoje alkoholowe?" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "nigdy", QuestionAnswerPoints = 0 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "raz w miesiącu", QuestionAnswerPoints = 1 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "2-4 razy w miesiącu", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "2-3 razy w tygodniu", QuestionAnswerPoints = 3 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "≥4 razy w tygodniu", QuestionAnswerPoints = 4 });

            var que2 = new ScaleAnswersQuestion() { QuestionName = "Ile standardowych porcji zawierających alkohol wypijasz w dniu, w którym pijesz? (porcja - 250ml piwa, 100ml wina, 30ml wódki)" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "1-2 porcje", QuestionAnswerPoints = 0 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "3-4 porcje", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "5-6 porcji", QuestionAnswerPoints = 2 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "7-9 porcji", QuestionAnswerPoints = 3 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "≥10 porcji", QuestionAnswerPoints = 4 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Jak często wypijasz ≥6 porcji alkoholu podczas jednego dnia?" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "nigdy", QuestionAnswerPoints = 0 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "rzadziej niż raz w miesiącu", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "około raz w miesiącu", QuestionAnswerPoints = 2 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "około raz w tygodniu w tygodniu", QuestionAnswerPoints = 3 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "codziennie lub prawie codziennie", QuestionAnswerPoints = 4 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Jak często w ostatnim roku nie mogłeś/aś przerwać picia po jego rozpoczęciu?" };
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "nigdy ", QuestionAnswerPoints = 0 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "rzadziej niż raz w miesiącu", QuestionAnswerPoints = 1 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "około raz w miesiącu", QuestionAnswerPoints = 2 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "około raz w tygodniu", QuestionAnswerPoints = 3 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "codziennie lub prawie codziennie", QuestionAnswerPoints = 4 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Jak często w ciągu ostatniego roku z powodu picia alkoholu zrobiłeś/aś coś niewłaściwego, niegodnego z przyjętymi w Twoim środowisku normami postępowania?" };
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "nigdy", QuestionAnswerPoints = 0 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "rzadziej niż raz w miesiącu", QuestionAnswerPoints = 1 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "około raz w miesiącu", QuestionAnswerPoints = 2 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "około raz w tygodniu", QuestionAnswerPoints = 3 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "codziennie lub prawie codziennie", QuestionAnswerPoints = 4 });

            var que6 = new ScaleAnswersQuestion() { QuestionName = "Jak często w ostatnim roku potrzebowałeś/aś napić się alkoholu rano następnego dnia po “dużym piciu”, aby móc dojść do siebie?" };
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "nigdy", QuestionAnswerPoints = 0 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "rzadziej niż raz w miesiącu", QuestionAnswerPoints = 1 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "około raz w miesiącu", QuestionAnswerPoints = 2 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "około raz w tygodniu", QuestionAnswerPoints = 3 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "codziennie lub prawie codziennie", QuestionAnswerPoints = 4 });

            var que7 = new ScaleAnswersQuestion() { QuestionName = "Jak często w ostatnim roku miałeś/aś poczucie winy lub wyrzuty sumienia po piciu alkoholu?" };
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "nigdy", QuestionAnswerPoints = 0 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "rzadziej niż raz w miesiącu", QuestionAnswerPoints = 1 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "około raz w miesiącu", QuestionAnswerPoints = 2 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "około raz w tygodniu", QuestionAnswerPoints = 3 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "codziennie lub prawie codziennie", QuestionAnswerPoints = 4 });

            var que8 = new ScaleAnswersQuestion() { QuestionName = "Jak często w ostatnim roku nie mogłeś/aś przypomnieć sobie, co zdarzyło się poprzedniego dnia lub nocy, z powodu picia?" };
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "nigdy", QuestionAnswerPoints = 0 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "rzadziej niż raz w miesiącu", QuestionAnswerPoints = 1 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "około raz w miesiącu", QuestionAnswerPoints = 2 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "około raz w tygodniu", QuestionAnswerPoints = 3 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "codziennie lub prawie codziennie", QuestionAnswerPoints = 4 });

            var que9 = new ScaleAnswersQuestion() { QuestionName = "Czy kiedykolwiek doznałeś/aś lub ktoś inny doznał jakiegoś urazu fizycznego w wyniku Twojego picia?" };
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "nie", QuestionAnswerPoints = 0 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "tak, ale nie w ostatnim roku", QuestionAnswerPoints = 2 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "tak, w ostatnim roku", QuestionAnswerPoints = 4 });

            var que10 = new ScaleAnswersQuestion() { QuestionName = "Czy ktoś z rodziny albo lekarz lub inny pracownik opieki zdrowotnej interesował się Twoim piciem albo sugerował jego ograniczenie?" };
            que10.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "nie", QuestionAnswerPoints = 0 });
            que10.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "tak, ale nie w ostatnim roku", QuestionAnswerPoints = 3 });
            que10.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "tak, w ostatnim roku", QuestionAnswerPoints = 4 });



            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);
            ScaleQuestions.Add(que4);
            ScaleQuestions.Add(que5);
            ScaleQuestions.Add(que6);
            ScaleQuestions.Add(que7);
            ScaleQuestions.Add(que8);
            ScaleQuestions.Add(que9);
            ScaleQuestions.Add(que10);

        }
        private void CheckStatus()
        {
            if (Score >= 8)
            {
                Diagnosis = "wskazane pogłębione badanie diagnostyczne u terapeuty uzależnień";
            }
            if (Score < 8)
            {
                Diagnosis = Diagnosis.Replace("wskazane pogłębione badanie diagnostyczne u terapeuty uzależnień", "..");
            }


            if (poziom_ryzyka > 3 && ryzyko == false)
            {
                Diagnosis = Diagnosis + " UWAGA: prawdopodobnie pijesz w sposób ryzykowny, dowiedz się więcej na ten temat";
                ryzyko = true;
            }
            if (poziom_ryzyka < 3 && ryzyko == true)
            {
                Diagnosis = Diagnosis.Replace(" UWAGA: prawdopodobnie pijesz w sposób ryzykowny, dowiedz się więcej na ten temat", "");
                ryzyko = false;
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
                "Interpretacja wyników: ≥8 punktów – wskazane pogłębione badanie diagnostyczne u terapeuty uzależnień, podwyższone wyniki w trzech pierwszych pytaniach(przy niskich wynikach w pozostałych) – prawdopodobnie pijesz w sposób ryzykowny, dowiedz się więcej na ten temat, podwyższone wyniki w skalach 4–6 – prawdopodobnie jesteś uzależniony od alkoholu, konieczne jest leczenie odwykowe. (Test AUDIT został opracowany przez ekspertów Światowej Organizacji Zdrowia, a w Polsce propagowany jest przez Państwową Agencję Rozwiązywania Problemów Alkoholowych)." +
                "\nŹródło:\nhttps://www.medonet.pl/zdrowie,skala-norton---czego-dotyczy-i-jak-oraz-kiedy-sie-ja-stosuje,artykul,1732049.html", "OK");


        }
        private async Task SetBookmarkImage()
        {
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            if (bookmarks.Any(x => x.ScaleName.Contains("AUDIT")))
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

            if (bookmarks.Any(x => x.ScaleName.Contains("AUDIT")))
            {
                //userData.Favorites.Remove("GLASGOW");
                Bookmark x = await App.Database.GetBookmarkAsyncName("AUDIT");
                await App.Database.DeleteBookmarkAsync(x);

                BookmarkImgSrc = "bookmark.png";
                isBookmarked = false;
                BookmarkNotificationTask(isBookmarked);
            }
            else
            {
                await App.Database.SaveBookmarkAsync(new Bookmark { ScaleName = "AUDIT", UserID = 0 });

                BookmarkImgSrc = "bookmark_saved.png";
                isBookmarked = true;
                BookmarkNotificationTask(isBookmarked);
            }
        }
        #endregion
    }
}
