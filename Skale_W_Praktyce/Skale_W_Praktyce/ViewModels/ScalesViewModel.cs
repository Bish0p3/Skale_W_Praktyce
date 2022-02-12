using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.Views;
using Skale_W_Praktyce.Views.Flyout;
using Skale_W_Praktyce.Views.Scales;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Skale_W_Praktyce.Models.ScaleAnswers;

namespace Skale_W_Praktyce.ViewModels
{
    class ScalesViewModel : INotifyPropertyChanged
    {
        public ScalesViewModel(INavigation navigation)
        {
            Navigation = navigation;


            #region ScalesCategories
            // Kategorie tap commands
            BasicCategoryTapCommand = new Command(async () => await BasicCategoryTapMethod());
            NerveSystemCategoryTapCommand = new Command(async () => await NerveSystemCategoryTapMethod());
            CirculatorySystemCategoryTapCommand = new Command(async () => await CirculatorySystemCategoryTapMethod());
            LungsSystemCategoryTapCommand = new Command(async () => await LungsSystemCategoryTapMethod());
            GeriatryCategoryTapCommand = new Command(async () => await GeriatryCategoryTapMethod());
            PsychologyCategoryTapCommand = new Command(async () => await PsychologyCategoryTapMethod());

            // lista po wybraniu kategorii
            ScalesListCategory = new ObservableCollection<Scale>();

            #endregion

            #region Scales List
            // Lista skal
            ScalesList = new ObservableCollection<Scale>
                {
                    new Scale(){
                        ScaleName = "Skala śpiączki Glasgow (dla dorosłych)",
                        ScaleDesc = "GLASGOW Meningococcal Septicemia Prognostic Score (GMSPS).",
                        ScaleTags = "Układ nerwowy",
                        ScaleViewName = typeof(scale_GLASGOW)},
                    new Scale(){
                        ScaleName = "Skala TINETTI",
                        ScaleDesc = "Służy do oceny równowagi i chodu.",
                        ScaleTags = "Geriatria",
                        ScaleViewName = typeof(PatientsListPage)},
                    new Scale(){
                        ScaleName = "Skala Norton",
                        ScaleDesc = "Służy do oceny ryzyka postawnia odlezyn u pacjenta przewlekle chorego.",
                        ScaleTags = "Ogólne",
                        ScaleViewName = typeof(MainPage_Flyout)},
                    new Scale(){
                        ScaleName = "Skala Baxtera",
                        ScaleDesc = "WYSZUKAJ NA NECIE BO NIE MA I DOPISZ DO DOKUMENTU",
                        ScaleTags = "Ogólne",
                        ScaleViewName = typeof(MainPage_Flyout)},
                    new Scale(){
                        ScaleName = "APGAR",
                        ScaleDesc = "Skala używana w medycynie w celu określenia stanu noworodka zaraz po porodzie.",
                        ScaleTags = "Układ krążenia, Układ nerwowy, Układ oddechowy",
                        ScaleViewName = typeof(MainPage_Flyout)},
                    new Scale(){
                        ScaleName = "BMI",
                        ScaleDesc = "Wskaźnik masy ciała (ang. Body Mass Index)",
                        ScaleTags = "Układ nerwowy",
                        ScaleViewName = typeof(PatientsListPage)},
                    new Scale(){
                        ScaleName = "DOZ MIANIY",
                        ScaleDesc = "LOREM IPSUM essasito essasito essasito essasito",
                        ScaleTags = "Układ nerwowy",
                        ScaleViewName = typeof(MainPage_Flyout)},
                    new Scale(){
                        ScaleName = "Trauma and Injury Severity Score",
                        ScaleDesc = "TRISS determine the propability of survival of a patient from the iss and rts score.",
                        ScaleTags = "Układ nerwowy",
                        ScaleViewName = typeof(MainPage_Flyout)},
                    new Scale(){
                        ScaleName = "Mini-Mental State Examination (MMSE)",
                        ScaleDesc = "Krótkie narzędzie przesiewowe do oceny otępień.",
                        ScaleTags = "Geriatria, Psychiatria, Układ nerwowy",
                        ScaleViewName = typeof(MainPage_Flyout)},
                    new Scale(){
                        ScaleName = "Test AUDIT (The Alcohol Use Disorders Identification Test",
                        ScaleDesc = "Badanie identyfikujące osoby pijące w sposób ryzykowny i szkodliwy dla zdrowia, używane w terapii uzależnień alkoholowych",
                        ScaleTags = "Ogólne, Psychiatria",
                        ScaleViewName = typeof(MainPage_Flyout)},
                };
            #endregion

            #region GLASGOW - questions and answers
            ScaleQuestions = new ObservableCollection<ScaleAnswersQuestion>();
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Otwieranie oczu:" };
            que1.Add(new ScaleAnswers() { QuestionAnswer = "spontaniczne", QuestionAnswerPoints = 4 });
            que1.Add(new ScaleAnswers() { QuestionAnswer = "na polecenie", QuestionAnswerPoints = 3 });
            que1.Add(new ScaleAnswers() { QuestionAnswer = "na bodźce bólowe", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionAnswer = "nie otwiera oczu", QuestionAnswerPoints = 1 });


            var que2 = new ScaleAnswersQuestion() { QuestionName = "Kontakt słowny:" };
            que2.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź logiczna, pacjent zorientowany co do miejsca, czasu i własnej osoby", QuestionAnswerPoints = 5 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź splątana, pacjent zdezorientowany", QuestionAnswerPoints = 4 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź nieadekwatna, nie na temat lub krzyk", QuestionAnswerPoints = 3 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "niezrozumiałe dźwięki, pojękiwanie", QuestionAnswerPoints = 2 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "bez reakcji", QuestionAnswerPoints = 1 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Reakcja ruchowa:" };
            que3.Add(new ScaleAnswers() { QuestionAnswer = "Spełnianie ruchowych poleceń słownych, migowych", QuestionAnswerPoints = 6 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "ruchy celowe, pacjent lokalizuje bodziec bólowy", QuestionAnswerPoints = 5 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "reakcja obronna na ból, wycofanie, próba usunięcia bodźca bólowego", QuestionAnswerPoints = 4 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "patologiczna reakcja zgięciowa, odkorowanie (przywiedzenie ramion, zgięcie w stawach łokciowych" +
                "i ręki, przeprost w stawach kończyn dolnych)", QuestionAnswerPoints = 3 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "patologiczna reakcja wyprostna, odmóżdżenie (odwiedzenie i obrót ramion do wewnątrz, wyprost w stawach" +
                "łokciowych, nawrócenie przedramion i zgięcie stawów ręki, przeprost w stawach kończyn dolnych, odwrócenie stopy)", QuestionAnswerPoints = 2 });
            que3.Add(new ScaleAnswers()
            { QuestionAnswer = "brak reakcji", QuestionAnswerPoints = 1 });


            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);



            #endregion

            AnswerTappedCommand = new Command(HandleSelectedAnswer);
        }


        #region Fields
        private ObservableCollection<Scale> scalesList;
        private ObservableCollection<Scale> scalesListCategory;
        public ObservableCollection<ScaleAnswersQuestion> scaleQuestions;
        private ScaleAnswers selectedAnswer;

        #region GLASGOW
        private string diagnoza_Glasgow = "Diagnoza";
        private int wynik_Glasgow;
        #endregion

        #endregion

        #region Properties

        #region GLASGOW
        public int Wynik_Glasgow
        {
            get { return wynik_Glasgow; }
            set
            {
                if (wynik_Glasgow != value)
                {
                    wynik_Glasgow = value;
                    OnPropertyChanged("Wynik_Glasgow");
                }
            }
        }
        public string Diagnoza_Glasgow
        {
            get { return diagnoza_Glasgow; }
            set
            {
                if (diagnoza_Glasgow != value)
                {
                    diagnoza_Glasgow = value;
                    OnPropertyChanged("Diagnoza_Glasgow");
                }
            }
        }

        #endregion

        public ObservableCollection<Scale> ScalesList
        {
            get { return scalesList; }
            set
            {
                if (scalesList != value)
                {
                    scalesList = value;
                    OnPropertyChanged("ScalesList");
                }
            }
        }
        public ObservableCollection<Scale> ScalesListCategory
        {
            get { return scalesListCategory; }
            set
            {
                if (scalesListCategory != value)
                {
                    scalesListCategory = value;
                    OnPropertyChanged("ScalesListCategory");
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
        public INavigation Navigation { get; set; }
        public ICommand AnswerTappedCommand { get; set; }

        #region ScalesList


        #endregion

        #region ScalesCategories
        public ICommand BasicCategoryTapCommand { get; }
        public ICommand NerveSystemCategoryTapCommand { get; }
        public ICommand CirculatorySystemCategoryTapCommand { get; }
        public ICommand LungsSystemCategoryTapCommand { get; }
        public ICommand GeriatryCategoryTapCommand { get; }
        public ICommand PsychologyCategoryTapCommand { get; }



        #endregion

        #endregion

        #region Methods

        #region ScalesCategories
        public async Task BasicCategoryTapMethod()
        {
            for (int i = 0; i < ScalesList.Count; i++)
            {
                if (ScalesList[i].ScaleTags == "Ogólne")
                {
                    ScalesListCategory.Add(ScalesList[i]);
                }
            }
            await Navigation.PushAsync(new ScalesListCategories());
        }
        public async Task NerveSystemCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListPage_Flyout());
        }
        public async Task CirculatorySystemCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListPage_Flyout());
        }
        public async Task LungsSystemCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListPage_Flyout());
        }
        public async Task GeriatryCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListPage_Flyout());
        }
        public async Task PsychologyCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListPage_Flyout());
        }
        public void ScaleFavButtonMethod()
        {

        }

        #endregion

        #region GLASGOW
        private void HandleSelectedAnswer()
        {
            if (SelectedAnswer.IsSelected == false)
            {
                Wynik_Glasgow += SelectedAnswer.QuestionAnswerPoints;
                SelectedAnswer.AnswerSelectedColor = Color.FromHex("#F07167");
                SelectedAnswer.IsSelected = true;
                CheckStatus();
            }
            else
            {
                Wynik_Glasgow -= SelectedAnswer.QuestionAnswerPoints;
                SelectedAnswer.AnswerSelectedColor = Color.Transparent;
                SelectedAnswer.IsSelected = false;
                CheckStatus();
            }
        }
        private void CheckStatus()
        {
            if (Wynik_Glasgow >= 13)
            {
                Diagnoza_Glasgow = "łagodne zaburzenia świadomości";
            }
            else if (Wynik_Glasgow >= 9 && Wynik_Glasgow <=12 )
            {
                Diagnoza_Glasgow = "umiarkowane zaburzenia świadomości";
            }
            else if (Wynik_Glasgow >= 6 && Wynik_Glasgow <= 8)
            {
                Diagnoza_Glasgow = "brak przytomności";
            }
            else if (Wynik_Glasgow == 5)
            {
                Diagnoza_Glasgow = "odkorowanie";
            }
            else if (Wynik_Glasgow == 4)
            {
                Diagnoza_Glasgow = "odmóżdżenie";
            }
            else if (Wynik_Glasgow == 3)
            {
                Diagnoza_Glasgow = "śmierć mózgu";
            }
        }
        #endregion

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
