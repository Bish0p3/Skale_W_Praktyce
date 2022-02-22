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
using System.Linq;

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
                        ScaleViewName = typeof(scale_TINETTI)},
                    new Scale(){
                        ScaleName = "Skala Norton",
                        ScaleDesc = "Służy do oceny ryzyka postawnia odlezyn u pacjenta przewlekle chorego.",
                        ScaleTags = "Ogólne, Geriatria",
                        ScaleViewName = typeof(scale_NORTON)},
                    new Scale(){
                        ScaleName = "Skala Baxtera",
                        ScaleDesc = "WYSZUKAJ NA NECIE BO NIE MA I DOPISZ DO DOKUMENTU",
                        ScaleTags = "Ogólne",
                        ScaleViewName = typeof(MainPage_Flyout)},
                    new Scale(){
                        ScaleName = "APGAR",
                        ScaleDesc = "Skala używana w medycynie w celu określenia stanu noworodka zaraz po porodzie.",
                        ScaleTags = "Układ krążenia, Układ nerwowy, Układ oddechowy",
                        ScaleViewName = typeof(scale_APGAR)},
                    new Scale(){
                        ScaleName = "Geriatryczna Skala Depresji",
                        ScaleDesc = "Geriatryczna skala oceny depresji (Geriatric Depression Scale – GDS)",
                        ScaleTags = "Psychiatria, Geriatria",
                        ScaleViewName = typeof(scale_GDS)},
                    new Scale(){
                        ScaleName = "Skala CRUSADE",
                        ScaleDesc = "Skala określająca ryzyko krwawienia.",
                        ScaleTags = "Układ krążenia, Układ nerwowy",
                        ScaleViewName = typeof(scale_CRUSADE)},
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

        }


        #region Fields
        private ObservableCollection<Scale> scalesList;
        private ObservableCollection<Scale> favoriteScalesList;
        private ObservableCollection<Scale> scalesListCategory;
        private ImageSource bookmarkImgSrc;

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
        public ObservableCollection<Scale> FavoriteScalesList
        {
            get { return favoriteScalesList; }
            set
            {
                if (favoriteScalesList != value)
                {
                    favoriteScalesList = value;
                    OnPropertyChanged("FavoriteScalesList");
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
        public ImageSource BookmarkImgSrc
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
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }

        #region ScalesList
        public ICommand BookmarkScaleCommand { get; set; }

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
