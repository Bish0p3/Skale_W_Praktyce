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
    internal class Scale_CRUSADE_VM : INotifyPropertyChanged
    {
        #region Constructor
        public Scale_CRUSADE_VM()
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

            var que6 = new ScaleAnswersQuestion() { QuestionName = "Wcześniejsza choroba naczyniowa (choroba tętnic obwodowych lub udar mózgu):" };
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "Nie", QuestionAnswerPoints = 0 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "Tak", QuestionAnswerPoints = 6 });

            var que7 = new ScaleAnswersQuestion() { QuestionName = "Cukrzyca:" };
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "Nie", QuestionAnswerPoints = 0 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "Tak", QuestionAnswerPoints = 6 });

            var que8 = new ScaleAnswersQuestion() { QuestionName = "Skurczowe ciśnienie tętnicze (mmHg):" };
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "Nie", QuestionAnswerPoints = 0 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "Tak", QuestionAnswerPoints = 7 });

            var que9 = new ScaleAnswersQuestion() { QuestionName = "Skurczowe ciśnienie tętnicze (mmHg):" };
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "<=90", QuestionAnswerPoints = 10 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "91-100", QuestionAnswerPoints = 8 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "101-120", QuestionAnswerPoints = 5 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "121-180", QuestionAnswerPoints = 1 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "181-200", QuestionAnswerPoints = 3 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = ">=201", QuestionAnswerPoints = 5 });

            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);
            ScaleQuestions.Add(que4);
            ScaleQuestions.Add(que5);
            ScaleQuestions.Add(que6);
            ScaleQuestions.Add(que7);
            ScaleQuestions.Add(que8);
            ScaleQuestions.Add(que9);
        }
        private void CheckStatus()
        {
            if (Score >= 51)
            {
                Diagnosis = "Ryzyko krwawienia (%): 19,5; Klasa ryzyka: Bardzo duże";
            }
            else if (Score >= 41 && Score <= 50)
            {
                Diagnosis = "Ryzyko krwawienia (%): 11,9; Klasa ryzyka: Duże";
            }
            else if (Score >= 31 && Score <= 40)
            {
                Diagnosis = "Ryzyko krwawienia (%): 8,6; Klasa ryzyka: Pośrednie";
            }
            else if (Score == 21 && Score <= 30)
            {
                Diagnosis = "Ryzyko krwawienia (%): 5,5; Klasa ryzyka: Małe";
            }
            else if (Score > 0)
            {
                Diagnosis = "Ryzyko krwawienia (%): 3,1; Klasa ryzyka: Bardzo małe";
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
                "Skala określająca ryzyko krwawienia; Interpretacja – ryzyko krwawienia w zależności od łącznej liczby punktów" +
                "\nŹródło:\nNa podstawie: Subherwal SW, Richard BG, et al. 'Baseline Risk of Major Bleeding in Non–ST - segment Elevation Myocardial Infarction: The CRUSADE Bleeding Score' Circulation. 2009; 119: 1873-1882; Wytyczne dotyczące postępowania u chorych z migotaniem przedsionków.. Kardiologia Polska 2010; 68, supl. VII: 487–566.", "OK");

        }
        private async Task SetBookmarkImage()
        {
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            if (bookmarks.Any(x => x.ScaleName.Contains("Crusade")))
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

            if (bookmarks.Any(x => x.ScaleName.Contains("Crusade")))
            {
                //userData.Favorites.Remove("GLASGOW");
                Bookmark x = await App.Database.GetBookmarkAsyncName("Crusade");
                await App.Database.DeleteBookmarkAsync(x);

                BookmarkImgSrc = "bookmark.png";
                isBookmarked = false;
                BookmarkNotificationTask(isBookmarked);
            }
            else
            {
                await App.Database.SaveBookmarkAsync(new Bookmark { ScaleName = "Crusade", UserID = 0 });

                BookmarkImgSrc = "bookmark_saved.png";
                isBookmarked = true;
                BookmarkNotificationTask(isBookmarked);
            }
        }
        #endregion

    }
}
