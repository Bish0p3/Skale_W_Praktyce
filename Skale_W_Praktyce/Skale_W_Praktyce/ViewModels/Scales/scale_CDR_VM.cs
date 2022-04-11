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
    internal class Scale_CDR_VM : INotifyPropertyChanged
    {
        #region Constructor
        public Scale_CDR_VM()
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
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Pamięć:" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "brak utraty pamięci lub lekkie, niespójne zapominanie", QuestionAnswerPoints = 0 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "konsekwentne lekkie zapominanie; częściowe przypominanie sobie wydarzeń; łagodne zapominanie", QuestionAnswerPoints = 1 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "umiarkowana utrata pamięci; bardziej wyraźna w przypadku ostatnich wydarzeń; wada przeszkadza w wykonywaniu codziennych czynności", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "poważna utrata pamięci; zachowany jest tylko dobrze wyuczony materiał; nowy materiał jest szybko tracony", QuestionAnswerPoints = 3 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "poważna utrata pamięci; pozostają tylko fragmenty", QuestionAnswerPoints = 4 });

            var que2 = new ScaleAnswersQuestion() { QuestionName = "Orientacja:" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "pełna orientacja", QuestionAnswerPoints = 0 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "w pełni zorientowany, z wyjątkiem niewielkich trudności z relacjami czasowymi", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "umiarkowane trudności z relacjami czasowymi; orientuje się w miejscu podczas egzaminu; może mieć dezorientację geograficzną w innych miejscach", QuestionAnswerPoints = 2 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "poważne trudności z relacjami czasowymi; zwykle dezorientacja w czasie, często w miejscu", QuestionAnswerPoints = 3 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "orientacja tylko do osoby", QuestionAnswerPoints = 4 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Rozwiązywanie problemów:" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "dobrze rozwiązuje codzienne problemy; dobra ocena w odniesieniu do wcześniejszych osiągnięć", QuestionAnswerPoints = 0 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "niewielkie upośledzenie w rozwiązywaniu problemów, podobieństw, różnic", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "umiarkowane trudności w radzeniu sobie z problemami, podobieństwami, różnicami; ocena społeczna zazwyczaj zachowana", QuestionAnswerPoints = 2 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "poważnie upośledzony w radzeniu sobie z problemami, podobieństwami, różnicami; ocena społeczna zazwyczaj upośledzona", QuestionAnswerPoints = 3 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "nie potrafi oceniać i rozwiązywać problemów", QuestionAnswerPoints = 4 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Socjalizacja:" };
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "samodzielne funkcjonowanie na zwykłym poziomie w pracy, zakupach, sprawach biznesowych i finansowych, wolontariacie i grupach społecznych", QuestionAnswerPoints = 0 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "niewielkie upośledzenie tych aktywności", QuestionAnswerPoints = 1 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "nie jest w stanie samodzielnie wykonywać tych czynności, ale może nadal brać udział w niektórych z nich; przy pobieżnej ocenie wygląda normalnie", QuestionAnswerPoints = 2 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "nie udaje, że funkcjonuje samodzielnie poza domem; wydaje się na tyle sprawny, że można go zabierać na zajęcia poza domem rodzinnym", QuestionAnswerPoints = 3 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "nie udaje, że funkcjonuje samodzielnie poza domem; sprawia wrażenie chorego, gdy jest zabierany na zajęcia poza domem rodzinnym", QuestionAnswerPoints = 4 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Dom i zainteresowania:" };
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "życie w domu, hobby, zainteresowania intelektualne dobrze utrzymane", QuestionAnswerPoints = 0 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "życie domowe, hobby, zainteresowania intelektualne nieznacznie upośledzone", QuestionAnswerPoints = 1 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "łagodne, ale zdecydowane upośledzenie funkcjonowania w domu; rezygnacja z trudniejszych prac domowych; rezygnacja z bardziej skomplikowanych hobby i zainteresowań", QuestionAnswerPoints = 2 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "zachowane tylko proste czynności; bardzo ograniczone zainteresowania, słabo podtrzymywane", QuestionAnswerPoints = 3 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "brak istotnych funkcji w domu", QuestionAnswerPoints = 4 });

            var que6 = new ScaleAnswersQuestion() { QuestionName = "Samoopieka:" };
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "w pełni zdolny do samoopieki", QuestionAnswerPoints = 0 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "w pełni zdolny do samoopieki", QuestionAnswerPoints = 1 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "potrzebuje podpowiedzi", QuestionAnswerPoints = 2 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "wymaga pomocy przy ubieraniu się, higienie, przechowywaniu rzeczy osobistych", QuestionAnswerPoints = 3 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "wymaga dużej pomocy przy pielęgnacji ciała; częste problemy z trzymaniem moczu", QuestionAnswerPoints = 4 });


            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);
            ScaleQuestions.Add(que4);
            ScaleQuestions.Add(que5);
        }
        private void CheckStatus()
        {
            if (Score >= 21)
            {
                Diagnosis = "3 - Wysokie zaburzenia poznawcze i niemożność w samodzielnym funkcjonowaniu";
            }
            else if (Score >= 6 && Score <= 20)
            {
                Diagnosis = "1 - Średnie zaburzenia poznawcze i problemy w funkcjonowaniu";
            }
            else if (Score >= 5)
            {
                Diagnosis = "0.5 - Łagodne zaburzenia poznawcze";
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
                "5-punktowa skala służąca do charakterystyki sześciu domen funkcjonowania poznawczego i funkcjonalnego w odniesieniu do choroby Alzheimera i pokrewnych demencji" +
                "\nŹródło:\nhttps://fizjoterapeuty.pl/testy-funkcjonalne/skala-tinetti.html", "OK");
        }
        private async Task SetBookmarkImage()
        {
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            //Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "Glasgow");

            if (bookmarks.Any(x => x.UserID == Settings.CurrentUserID && x.ScaleName.Contains("CDR")))
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

            if (userBookmarks.Any(x => x.ScaleName.Contains("CDR")))
            {
                Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "CDR");
                await App.Database.DeleteBookmarkAsync(x);

                BookmarkImgSrc = "bookmark.png";
                isBookmarked = false;
                await BookmarkNotificationTask(isBookmarked);
            }
            else
            {
                await App.Database.SaveBookmarkAsync(new Bookmark { ScaleName = "CDR", UserID = Settings.CurrentUserID });

                BookmarkImgSrc = "bookmark_saved.png";
                isBookmarked = true;
                await BookmarkNotificationTask(isBookmarked);
            }
        }
        #endregion
    }
}
