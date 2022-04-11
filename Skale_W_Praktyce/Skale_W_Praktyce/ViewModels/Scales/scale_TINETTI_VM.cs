using Skale_W_Praktyce.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Skale_W_Praktyce.Models.ScaleAnswers;

namespace Skale_W_Praktyce.ViewModels
{
    internal class Scale_TINETTI_VM : INotifyPropertyChanged
    {
        #region Constructor
        public Scale_TINETTI_VM()
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
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Równowaga podczas siedzenia:" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "pochyla się lub ześlizguje z krzesła", QuestionAnswerPoints = 0 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "zachowuje równowagę", QuestionAnswerPoints = 1 });

            var que2 = new ScaleAnswersQuestion() { QuestionName = "Wstawanie z miejsca:" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "niezdolny", QuestionAnswerPoints = 0 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "wstaje pomagając sobie rękami", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "wstaje bez pomocy rąk", QuestionAnswerPoints = 2 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Próby wstawania z miejsca:" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "niezdolny bez pomocy", QuestionAnswerPoints = 0 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "wstaje, ale potrzebuje kilku prób", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "wstaje od razu", QuestionAnswerPoints = 2 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Równowaga bezpośrednio po wstaniu z miejsca przez pierwsze 5 sekund:" };
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "stoi niepewnie", QuestionAnswerPoints = 0 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "stoi pewnie, ale podpiera się", QuestionAnswerPoints = 1 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "stoi pewnie, ale bez podparcia", QuestionAnswerPoints = 2 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Równowaga podczas stania:" };
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "stoi niepewnie", QuestionAnswerPoints = 0 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "stoi pewnie ale na szerokiej podstawie lub podpierając się", QuestionAnswerPoints = 1 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "stoi bez podparcia, ze złączonymi stopami", QuestionAnswerPoints = 2 });

            var que6 = new ScaleAnswersQuestion() { QuestionName = "Próba trącania - trzykrotne trącanie dłonią w klatkę piersiową na wysokości mostka:" };
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "zaczyna się przewracać", QuestionAnswerPoints = 0 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "zatacza się, ale utrzymuje pozycję", QuestionAnswerPoints = 1 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "stoi pewnie", QuestionAnswerPoints = 2 });

            var que7 = new ScaleAnswersQuestion() { QuestionName = "Próba trącania przy zamkniętych oczach badanego:" };
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "stoi niepewnie", QuestionAnswerPoints = 0 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "stoi pewnie", QuestionAnswerPoints = 1 });

            var que8 = new ScaleAnswersQuestion() { QuestionName = "Obracanie się o 360 stopni:" };
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "ruch przerywany", QuestionAnswerPoints = 0 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "ruch ciągły", QuestionAnswerPoints = 1 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "zatacza się, chwyta przedmiotów", QuestionAnswerPoints = 2 });

            var que9 = new ScaleAnswersQuestion() { QuestionName = "Siadanie:" };
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "niepewne", QuestionAnswerPoints = 0 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "pomaga sobie rękami", QuestionAnswerPoints = 1 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "pewny ruch", QuestionAnswerPoints = 2 });

            var que10 = new ScaleAnswersQuestion() { QuestionName = "Zapoczątkowanie chodu bezpośrednio po wydaniu polecenia:" };
            que10.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "wahanie lub kilkakrotne próby ruszenia z miejsca", QuestionAnswerPoints = 0 });
            que10.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "start bez wahania", QuestionAnswerPoints = 1 });

            var que11 = new ScaleAnswersQuestion() { QuestionName = "Długość kroku prawej stopy:" };
            que11.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "nie przekracza miejsca stania lewej stopy", QuestionAnswerPoints = 0 });
            que11.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "przekracza", QuestionAnswerPoints = 1 });

            var que12 = new ScaleAnswersQuestion() { QuestionName = "Wysokość kroku prawej stopy:" };
            que12.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "prawa stopa nie odrywa się całkowicie od podłogi", QuestionAnswerPoints = 0 });
            que12.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "odrywa się całkowicie", QuestionAnswerPoints = 1 });

            var que13 = new ScaleAnswersQuestion() { QuestionName = "Długość kroku lewej stopy:" };
            que13.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "nie przekracza miejsca stania prawej stopy", QuestionAnswerPoints = 0 });
            que13.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "przekracza", QuestionAnswerPoints = 1 });

            var que14 = new ScaleAnswersQuestion() { QuestionName = "Wysokość kroku lewej stopy:" };
            que14.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "lewa stopa nie odrywa się całkowicie od podłogi", QuestionAnswerPoints = 0 });
            que14.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "odrywa się całkowicie", QuestionAnswerPoints = 1 });

            var que15 = new ScaleAnswersQuestion() { QuestionName = "Symetria kroku:" };
            que15.Add(new ScaleAnswers() { QuestionID = 12, QuestionAnswer = "długość kroku obu stóp nie jest jednakowa", QuestionAnswerPoints = 0 });
            que15.Add(new ScaleAnswers() { QuestionID = 12, QuestionAnswer = "długość kroku obu stóp wydaje się równa", QuestionAnswerPoints = 1 });

            var que16 = new ScaleAnswersQuestion() { QuestionName = "Ciągłość chodu:" };
            que16.Add(new ScaleAnswers() { QuestionID = 13, QuestionAnswer = "zatrzymywanie się pomiędzy poszczególnymi krokami, brak ciągłości chodu", QuestionAnswerPoints = 0 });
            que16.Add(new ScaleAnswers() { QuestionID = 13, QuestionAnswer = "chód wydaje się ciągły", QuestionAnswerPoints = 1 });

            var que17 = new ScaleAnswersQuestion() { QuestionName = "Ścieżka chodu oceniana na odcinku około 3 metrów:" };
            que17.Add(new ScaleAnswers() { QuestionID = 14, QuestionAnswer = "wyraźne odchylenie od toru", QuestionAnswerPoints = 0 });
            que17.Add(new ScaleAnswers() { QuestionID = 14, QuestionAnswer = "niewielkie odchylenie lub korzystanie z przyrządów pomocniczych", QuestionAnswerPoints = 1 });
            que17.Add(new ScaleAnswers() { QuestionID = 14, QuestionAnswer = "prosta ścieżka, bez pomocy", QuestionAnswerPoints = 2 });

            var que18 = new ScaleAnswersQuestion() { QuestionName = "Tułów:" };
            que18.Add(new ScaleAnswers() { QuestionID = 15, QuestionAnswer = "wyraźne kołysanie lub korzystanie z przyrządów pomocniczych", QuestionAnswerPoints = 0 });
            que18.Add(new ScaleAnswers() { QuestionID = 15, QuestionAnswer = "brak kołysania, ale zginane są kolana lub plecy", QuestionAnswerPoints = 1 });
            que18.Add(new ScaleAnswers() { QuestionID = 15, QuestionAnswer = "brak zginania kolan, pleców, nie korzysta z przyrządów pomocniczych", QuestionAnswerPoints = 2 });

            var que19 = new ScaleAnswersQuestion() { QuestionName = "Pozycja podczas chodzenia:" };
            que19.Add(new ScaleAnswers() { QuestionID = 16, QuestionAnswer = "rozstawione pięty", QuestionAnswerPoints = 0 });
            que19.Add(new ScaleAnswers() { QuestionID = 16, QuestionAnswer = "pięty prawie się stykają w trakcie chodzenia", QuestionAnswerPoints = 1 });




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
            ScaleQuestions.Add(que11);
            ScaleQuestions.Add(que12);
            ScaleQuestions.Add(que13);
            ScaleQuestions.Add(que14);
            ScaleQuestions.Add(que15);
            ScaleQuestions.Add(que16);
            ScaleQuestions.Add(que17);
            ScaleQuestions.Add(que18);
            ScaleQuestions.Add(que19);
        }
        private void CheckStatus()
        {
            if (Score >= 25)
            {
                Diagnosis = "Ryzyko upadku jest niskie.";
            }
            else if (Score >= 19 && Score <= 24)
            {
                Diagnosis = "Występuje skłonność do upadków";
            }
            else if (Score <= 18)
            {
                Diagnosis = "Wysokie zagrożenie upadkiem";
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
                "Skala Tinetti jest powszechnie stosowana przy całościowej ocenie geriatrycznej, jednakże znajduje zastosowanie bardzo często na oddziałach neurologicznych czy ortopedycznych. Służy do oceny ryzyka upadków. \nPacjent może uzyskać w części dotyczącej równowagi maksymalnie 16 punktów, a w części dotyczącej chodu – 12 punktów. Łącznie daje to sumę 28 punktów. Wynik poniżej 26 punktów oznacza istnienie problemu. Natomiast uzyskanie mniej niż 19 punktów oznacza, że pacjent ma 5 - krotnie wyższe ryzyko upadku niż osoba, która uzyskała 28 punktów. Ponadto uzyskanie przynajmniej jednego „0” lub dwóch „1” (gdzie nie jest to najwyższa liczba punktów do zdobycia) stanowi wskazanie do konsultacji z fizjoterapeutą.." +
                "\nŹródło:\nhttps://fizjoterapeuty.pl/testy-funkcjonalne/skala-tinetti.html", "OK");
        }
        private async Task SetBookmarkImage()
        {
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            //Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "Glasgow");

            if (bookmarks.Any(x => x.UserID == Settings.CurrentUserID && x.ScaleName.Contains("TINETTI")))
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
            List<Bookmark> userBookmarks = new List<Bookmark>();
            foreach (Bookmark bookmark in bookmarks)
            {
                if (bookmark.UserID == Settings.CurrentUserID)
                {
                    userBookmarks.Add(bookmark);
                }
            }

            if (userBookmarks.Any(x => x.ScaleName.Contains("TINETTI")))
            {
                Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "TINETTI");
                await App.Database.DeleteBookmarkAsync(x);

                BookmarkImgSrc = "bookmark.png";
                isBookmarked = false;
                await BookmarkNotificationTask(isBookmarked);
            }
            else
            {
                await App.Database.SaveBookmarkAsync(new Bookmark { ScaleName = "TINETTI", UserID = Settings.CurrentUserID });

                BookmarkImgSrc = "bookmark_saved.png";
                isBookmarked = true;
                await BookmarkNotificationTask(isBookmarked);
            }
        }
        #endregion
    }
}
