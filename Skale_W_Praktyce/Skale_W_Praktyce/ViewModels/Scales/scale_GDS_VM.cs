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
    internal class Scale_GDS_VM : INotifyPropertyChanged
    {
        #region Constructor
        public Scale_GDS_VM()
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
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Myśląc o całym swoim życiu, czy jest Pan(i) z niego zadowolony(a)?" };
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que1.Add(new ScaleAnswers() { QuestionID = 0, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que2 = new ScaleAnswersQuestion() { QuestionName = "Czy zmniejszyła się liczba Pana(i) aktywności i zainteresowań?" };
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que2.Add(new ScaleAnswers() { QuestionID = 1, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Czy ma Pan(i) uczucie, że życie jest puste?" };
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que3.Add(new ScaleAnswers() { QuestionID = 2, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que4 = new ScaleAnswersQuestion() { QuestionName = "Czy często czuje się Pan(i) znudzony(a)?" };
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que4.Add(new ScaleAnswers() { QuestionID = 3, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que5 = new ScaleAnswersQuestion() { QuestionName = "Czy myśli Pan(i) z nadzieją o przyszłości?" };
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que5.Add(new ScaleAnswers() { QuestionID = 4, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que6 = new ScaleAnswersQuestion() { QuestionName = "Czy miewa Pan(i) natrętne myśli, których nie może się Pan(i) pozbyć?" };
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que6.Add(new ScaleAnswers() { QuestionID = 5, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que7 = new ScaleAnswersQuestion() { QuestionName = "Czy jest Pan(i) w dobrym nastroju przez większość czasu?" };
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que7.Add(new ScaleAnswers() { QuestionID = 6, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que8 = new ScaleAnswersQuestion() { QuestionName = "Czy obawia się Pan(i), że może się zdarzyć Panu(i) coś złego?" };
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que8.Add(new ScaleAnswers() { QuestionID = 7, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que9 = new ScaleAnswersQuestion() { QuestionName = "Czy przez większość czasu czuje się Pan(i) szczęśliwy(a)?" };
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que9.Add(new ScaleAnswers() { QuestionID = 8, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que10 = new ScaleAnswersQuestion() { QuestionName = "Czy często czuje się Pan(i) bezradny(a)?" };
            que10.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que10.Add(new ScaleAnswers() { QuestionID = 9, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que11 = new ScaleAnswersQuestion() { QuestionName = "Czy często jest Pan(i) niespokojny(a)?" };
            que11.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que11.Add(new ScaleAnswers() { QuestionID = 10, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que12 = new ScaleAnswersQuestion() { QuestionName = "Czy zamiast wyjść wieczorem z domu, woli Pan(i) w nim pozostać?" };
            que12.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que12.Add(new ScaleAnswers() { QuestionID = 11, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que13 = new ScaleAnswersQuestion() { QuestionName = "Czy często martwi się Pan(i) o przyszłość?" };
            que13.Add(new ScaleAnswers() { QuestionID = 12, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que13.Add(new ScaleAnswers() { QuestionID = 12, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que14 = new ScaleAnswersQuestion() { QuestionName = "Czy czuje Pan(i), że ma więcej kłopotów z pamięcią niż inni ludzie?" };
            que14.Add(new ScaleAnswers() { QuestionID = 13, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que14.Add(new ScaleAnswers() { QuestionID = 13, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que15 = new ScaleAnswersQuestion() { QuestionName = "Czy myśli Pan(i), że wspaniale jest żyć?" };
            que15.Add(new ScaleAnswers() { QuestionID = 14, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que15.Add(new ScaleAnswers() { QuestionID = 14, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que16 = new ScaleAnswersQuestion() { QuestionName = "Czy często czuje się Pan(i) przygnębiony(a) i smutny(a)?" };
            que16.Add(new ScaleAnswers() { QuestionID = 15, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que16.Add(new ScaleAnswers() { QuestionID = 15, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que17 = new ScaleAnswersQuestion() { QuestionName = "Czy obecnie czuje się Pan(i) gorszy(a) od innych ludzi?" };
            que17.Add(new ScaleAnswers() { QuestionID = 16, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que17.Add(new ScaleAnswers() { QuestionID = 16, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que18 = new ScaleAnswersQuestion() { QuestionName = "Czy martwi się Pan(i) tym, co zdarzyło się w przeszłości?" };
            que18.Add(new ScaleAnswers() { QuestionID = 17, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que18.Add(new ScaleAnswers() { QuestionID = 17, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que19 = new ScaleAnswersQuestion() { QuestionName = "Czy uważa Pan(i), że życie jest ciekawe?" };
            que19.Add(new ScaleAnswers() { QuestionID = 18, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que19.Add(new ScaleAnswers() { QuestionID = 18, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que20 = new ScaleAnswersQuestion() { QuestionName = "Czy trudno jest Panu(i) realizować nowe pomysły?" };
            que20.Add(new ScaleAnswers() { QuestionID = 19, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que20.Add(new ScaleAnswers() { QuestionID = 19, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que21 = new ScaleAnswersQuestion() { QuestionName = "Czy czuje się Pan(i) pełny(a) energii?" };
            que21.Add(new ScaleAnswers() { QuestionID = 20, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que21.Add(new ScaleAnswers() { QuestionID = 20, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que22 = new ScaleAnswersQuestion() { QuestionName = "Czy uważa Pan(i), że sytuacja jest beznadziejna?" };
            que22.Add(new ScaleAnswers() { QuestionID = 21, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que22.Add(new ScaleAnswers() { QuestionID = 21, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que23 = new ScaleAnswersQuestion() { QuestionName = "Czy myśli Pan(i), że ludzie są lepsi niż Pan(i)?" };
            que23.Add(new ScaleAnswers() { QuestionID = 22, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que23.Add(new ScaleAnswers() { QuestionID = 22, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que24 = new ScaleAnswersQuestion() { QuestionName = "Czy drobne rzeczy często wyprowadzają Pana(ią) z równowagi?" };
            que24.Add(new ScaleAnswers() { QuestionID = 23, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que24.Add(new ScaleAnswers() { QuestionID = 23, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que25 = new ScaleAnswersQuestion() { QuestionName = "Czy często chce się Panu(i) płakać?" };
            que25.Add(new ScaleAnswers() { QuestionID = 24, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que25.Add(new ScaleAnswers() { QuestionID = 24, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que26 = new ScaleAnswersQuestion() { QuestionName = "Czy ma Pan(i) kłopoty z koncentracją uwagi?" };
            que26.Add(new ScaleAnswers() { QuestionID = 25, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que26.Add(new ScaleAnswers() { QuestionID = 25, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que27 = new ScaleAnswersQuestion() { QuestionName = "Czy rano budzi się Pan(i) w dobrym nastroju?" };
            que27.Add(new ScaleAnswers() { QuestionID = 26, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que27.Add(new ScaleAnswers() { QuestionID = 26, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que28 = new ScaleAnswersQuestion() { QuestionName = "Czy ostatnio unika Pan(i) spotkań towarzyskich?" };
            que28.Add(new ScaleAnswers() { QuestionID = 27, QuestionAnswer = "NIE", QuestionAnswerPoints = 0 });
            que28.Add(new ScaleAnswers() { QuestionID = 27, QuestionAnswer = "TAK", QuestionAnswerPoints = 1 });

            var que29 = new ScaleAnswersQuestion() { QuestionName = "Czy łatwo podejmuje Pan(i) decyzje?" };
            que29.Add(new ScaleAnswers() { QuestionID = 28, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que29.Add(new ScaleAnswers() { QuestionID = 28, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });

            var que30 = new ScaleAnswersQuestion() { QuestionName = "Czy zdolność Pan(i) myślenia jest taka sama jak dawniej?" };
            que30.Add(new ScaleAnswers() { QuestionID = 29, QuestionAnswer = "NIE", QuestionAnswerPoints = 1 });
            que30.Add(new ScaleAnswers() { QuestionID = 29, QuestionAnswer = "TAK", QuestionAnswerPoints = 0 });



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
            ScaleQuestions.Add(que20);
            ScaleQuestions.Add(que21);
            ScaleQuestions.Add(que22);
            ScaleQuestions.Add(que23);
            ScaleQuestions.Add(que24);
            ScaleQuestions.Add(que25);
            ScaleQuestions.Add(que26);
            ScaleQuestions.Add(que27);
            ScaleQuestions.Add(que28);
            ScaleQuestions.Add(que29);
            ScaleQuestions.Add(que30);
        }
        private void CheckStatus()
        {
            if (Score >= 20)
            {
                Diagnosis = "Głęboka depresja";
            }
            else if (Score >= 10)
            {
                Diagnosis = "Depresja łagodna";
            }
            else if (Score < 10)
            {
                Diagnosis = "Brak depresji";
            }
        }
        private async Task InfoMethod()
        {
            // POPUP
            await Application.Current.MainPage.DisplayAlert("Info",
               "Geriatryczna skala oceny depresji (Geriatric Depression Scale – GDS) to jedna z częściej stosowanych skal do przesiewowej samooceny depresji w wieku podeszłym. Składa się z 30 pytań, na które należy udzielić odpowiedzi tak lub nie. " +
               "Jeśli osoba badana ma kłopot ze zrozumieniem pytań (np. z powodu zaburzeń funkcji intelektualnych), dopuszczalna jest pomoc w czytaniu i wypełnianiu skali. " +
               "Wynik skali nie jest równoznaczny z rozpoznaniem (diagnozą) depresji, ale tak jak w przypadku wielu kwestionariuszy, jest raczej wskazówką diagnostyczną. W przypadku uzyskania podwyższonego wyniku wskazana jest konsultacja u psychiatry lub psychologa. " +
               "Skalę wypełnia się zwykle mając na uwadze samopoczucie osoby badanej w 2 ostatnich tygodniach." +
                "\nŹródło:\nhttps://www.centrumdobrejterapii.pl/materialy/geriatryczna-skala-oceny-depresji/", "OK");

        }
        private async Task SetBookmarkImage()
        {
            List<Bookmark> bookmarks = await App.Database.GetBookmarksAsync();
            //Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "Glasgow");

            if (bookmarks.Any(x => x.UserID == Settings.CurrentUserID && x.ScaleName.Contains("GDS")))
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

            if (userBookmarks.Any(x => x.ScaleName.Contains("GDS")))
            {
                Bookmark x = await App.Database.GetBookmarkAsyncUserIDAndName(Settings.CurrentUserID, "GDS");
                await App.Database.DeleteBookmarkAsync(x);

                BookmarkImgSrc = "bookmark.png";
                isBookmarked = false;
                await BookmarkNotificationTask(isBookmarked);
            }
            else
            {
                await App.Database.SaveBookmarkAsync(new Bookmark { ScaleName = "GDS", UserID = Settings.CurrentUserID });

                BookmarkImgSrc = "bookmark_saved.png";
                isBookmarked = true;
                await BookmarkNotificationTask(isBookmarked);
            }
        }
        #endregion
    }
}
