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
    internal class Scale_ALDRET_VM : INotifyPropertyChanged
    {
        #region Constructor
        public Scale_ALDRET_VM()
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
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Poruszanie kończynami:" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "wszystkimi", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "porusza dwiema", QuestionAnswerPoints = 1 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "nie porusza", QuestionAnswerPoints = 0 });


            var que2 = new ScaleAnswersQuestion() { QuestionName = "Oddech:" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "głęboki, wydolny kaszel", QuestionAnswerPoints = 2 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "duszność, oddech płytki", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "bezdech", QuestionAnswerPoints = 0 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Krążenie:" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "RR ± 20 mm Hg od wartości wyjściowej", QuestionAnswerPoints = 2 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "RR ± 20-50 mm Hg od wartości wyjściowej", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "RR < ± 50 mm Hg od wartości wyjściowej", QuestionAnswerPoints = 0 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Stan przytomności:" };
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "przytomny", QuestionAnswerPoints = 2 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "budzi się na polecenie", QuestionAnswerPoints = 1 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "bez reakcji", QuestionAnswerPoints = 0 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Kolor skóry:" };
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "normalny", QuestionAnswerPoints = 0 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "blady, plamy, zażółcenie", QuestionAnswerPoints = 1 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "sinica", QuestionAnswerPoints = 0 });



            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);
            ScaleQuestions.Add(que4);
            ScaleQuestions.Add(que5);
        }
        private void CheckStatus()
        {
            if (Score >= 9)
            {
                Diagnosis = "Noworodek w dobrym stanie";
            }
            else if (Score >= 7)
            {
                Diagnosis = "Zmęczenie porodem";
            }
            else if (Score >= 4)
            {
                Diagnosis = "Zamartwica średniego stopnia (sina)";
            }
            else if (Score < 3)
            {
                Diagnosis = "Zamartwica ciężka (blada)";
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
               "Skala Apgar to skala używana w medycynie w celu określenia stanu noworodka zaraz po porodzie: w 1., 5.i 15.minucie życia. \n" +
               "Wprowadziła ją w 1953 Virginia Apgar, absolwentka Mount Holyoke College(Massachusetts, USA), akronim powstał zaś 10 lat później.Dziecko minimalnie może dostać 0, a maksymalnie 10 punktów." +
                "\nŹródło:\nhttps://pl.wikipedia.org/wiki/Skala_Apgar", "OK");
        }
        private async Task SetBookmarkImage()
        {
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            //Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "Glasgow");

            if (bookmarks.Any(x => x.UserID == Settings.CurrentUserID && x.ScaleName.Contains("Aldret")))
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

            if (userBookmarks.Any(x => x.ScaleName.Contains("Aldret")))
            {
                Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "Aldret");
                await App.Database.DeleteBookmarkAsync(x);

                BookmarkImgSrc = "bookmark.png";
                isBookmarked = false;
                await BookmarkNotificationTask(isBookmarked);
            }
            else
            {
                await App.Database.SaveBookmarkAsync(new Bookmark { ScaleName = "Aldret", UserID = Settings.CurrentUserID });

                BookmarkImgSrc = "bookmark_saved.png";
                isBookmarked = true;
                await BookmarkNotificationTask(isBookmarked);
            }
        }
        #endregion

    }
}
