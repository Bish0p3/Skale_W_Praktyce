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
            BasicCategoryTapCommand = new Command(async () => await BasicCategoryTapMethod());
            NerveSystemCategoryTapCommand = new Command(async () => await NerveSystemCategoryTapMethod());
            CirculatorySystemCategoryTapCommand = new Command(async () => await CirculatorySystemCategoryTapMethod());
            LungsSystemCategoryTapCommand = new Command(async () => await LungsSystemCategoryTapMethod());
            GeriatryCategoryTapCommand = new Command(async () => await GeriatryCategoryTapMethod());
            PsychologyCategoryTapCommand = new Command(async () => await PsychologyCategoryTapMethod());
            #endregion

            ScalesListCategory = new ObservableCollection<Scale>();
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


            #region GLASGOW - questions and answers
            ScaleQuestions = new ObservableCollection<ScaleAnswersQuestion>();
            var que1 = new ScaleAnswersQuestion() { QuestionName = "Otwieranie oczu" };
            que1.Add(new ScaleAnswers() { QuestionAnswer = "spontaniczne", QuestionAnswerPoints = 1 });
            que1.Add(new ScaleAnswers() { QuestionAnswer = "na polecenie", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionAnswer = "na bodźce bólowe", QuestionAnswerPoints = 2 });
            que1.Add(new ScaleAnswers() { QuestionAnswer = "nie otwiera oczu", QuestionAnswerPoints = 2 });


            var que2 = new ScaleAnswersQuestion() { QuestionName = "Kontakt słowny" };
            que2.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź logiczna, pacjent zorientowany co do miejsca, czasu i własnej osoby", QuestionAnswerPoints = 3 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź splątana, pacjent zdezorientowany", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź nieadekwatna, nie na temat lub krzyk", QuestionAnswerPoints = 3 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "niezrozumiałe dźwięki, pojękiwanie", QuestionAnswerPoints = 1 });
            que2.Add(new ScaleAnswers() { QuestionAnswer = "bez reakcji", QuestionAnswerPoints = 3 });

            var que3 = new ScaleAnswersQuestion() { QuestionName = "Kontakt jakis" };
            que3.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź logiczna, pacjent zorientowany co do miejsca, czasu i własnej osoby", QuestionAnswerPoints = 3 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź splątana, pacjent zdezorientowany", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "odpowiedź nieadekwatna, nie na temat lub krzyk", QuestionAnswerPoints = 3 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "niezrozumiałe dźwięki, pojękiwanie", QuestionAnswerPoints = 1 });
            que3.Add(new ScaleAnswers() { QuestionAnswer = "bez reakcji", QuestionAnswerPoints = 3 });


            ScaleQuestions.Add(que1);
            ScaleQuestions.Add(que2);
            ScaleQuestions.Add(que3);



            #endregion


        }




        #region Fields
        private ObservableCollection<Scale> scalesList;
        private ObservableCollection<Scale> scalesListCategory;
        public ObservableCollection<ScaleAnswersQuestion> scaleQuestions;

        private string gLASGOWScaleAnswer;
        #endregion

        #region Properties
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
        public string GLASGOWScaleAnswer
        {
            get { return gLASGOWScaleAnswer; }
            set
            {
                if(gLASGOWScaleAnswer != value)
                {
                    gLASGOWScaleAnswer = value;
                    OnPropertyChanged("GLASGOWScaleAnswer");
                }
            }
        }
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }

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
