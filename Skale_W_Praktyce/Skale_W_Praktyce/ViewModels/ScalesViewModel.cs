using Skale_W_Praktyce.Models;
using Skale_W_Praktyce.Views;
using Skale_W_Praktyce.Views.Scales;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Skale_W_Praktyce.ViewModels
{
    class ScalesViewModel : INotifyPropertyChanged
    {

        #region Constructor
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

            #endregion

            #region Scales List
            // Lista skal
            ScalesListInit();
            #endregion

        }
        #endregion

        #region Fields
        private ObservableCollection<Scale> scalesList;
        private ObservableCollection<Scale> favoriteScalesList;
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
        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command<string>((text) =>
        {
            if (text.Length >= 1)
            {
                ScalesList.Clear();
                ScalesListInit();
                var suggestions = ScalesList.Where(c => c.ScaleName.ToLower().Contains(text.ToLower())).ToList();
                ScalesList.Clear();
                foreach (var scale in suggestions)
                    ScalesList.Add(scale);

            }
            else
            {
                ScalesListInit();
            }

        }));
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
            await Navigation.PushAsync(new ScalesListCategories("Ogólne"));
        }
        public async Task NerveSystemCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListCategories("Układ nerwowy"));
        }
        public async Task CirculatorySystemCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListCategories("Układ krążenia"));
        }
        public async Task LungsSystemCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListCategories("Układ oddechowy"));
        }
        public async Task GeriatryCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListCategories("Geriatria"));
        }
        public async Task PsychologyCategoryTapMethod()
        {
            await Navigation.PushAsync(new ScalesListCategories("Psychologia"));
        }
        #endregion

        public void ScalesListInit()
        {
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
                        ScaleTags = "Geriatria, Układ nerwowy",
                        ScaleViewName = typeof(scale_TINETTI)},
                    new Scale(){
                        ScaleName = "Skala Norton",
                        ScaleDesc = "Służy do oceny ryzyka postawnia odlezyn u pacjenta przewlekle chorego.",
                        ScaleTags = "Ogólne, Geriatria",
                        ScaleViewName = typeof(scale_NORTON)},
                    new Scale(){
                        ScaleName = "Skala Aldreta",
                        ScaleDesc = "Skala kwalifikująca chorego po zabiegu chirurgicznym do przeniesienia go z sali budzeń na oddział zabiegowy.",
                        ScaleTags = "Układ krążenia, Układ nerwowy, Układ oddechowy",
                        ScaleViewName = typeof(Scale_ALDRET)},
                    new Scale(){
                        ScaleName = "APGAR",
                        ScaleDesc = "Skala używana w medycynie w celu określenia stanu noworodka zaraz po porodzie.",
                        ScaleTags = "Układ krążenia, Układ nerwowy, Układ oddechowy",
                        ScaleViewName = typeof(scale_APGAR)},
                    new Scale(){
                        ScaleName = "Geriatryczna Skala Depresji (GDS)",
                        ScaleDesc = "Geriatryczna skala oceny depresji (Geriatric Depression Scale – GDS)",
                        ScaleTags = "Psychologia, Geriatria",
                        ScaleViewName = typeof(scale_GDS)},
                    new Scale(){
                        ScaleName = "Skala CRUSADE",
                        ScaleDesc = "Skala określająca ryzyko krwawienia.",
                        ScaleTags = "Układ krążenia, Układ nerwowy",
                        ScaleViewName = typeof(scale_CRUSADE)},
                    new Scale(){
                        ScaleName = "Skala CDR",
                        ScaleDesc = "Badanie służące do Klinicznej Oceny Stopnia Otępienia (CDR)",
                        ScaleTags = "Ogólne, Układ nerwowy, Geriatria",
                        ScaleViewName = typeof(scale_CDR)},
                    new Scale(){
                        ScaleName = "Mini-Mental State Examination (MMSE)",
                        ScaleDesc = "Krótkie narzędzie przesiewowe do oceny otępień.",
                        ScaleTags = "Ogólne, Geriatria, Psychologia, Układ nerwowy",
                        ScaleViewName = typeof(Scale_MMSE)},
                    new Scale(){
                        ScaleName = "Test AUDIT (The Alcohol Use Disorders Identification Test)",
                        ScaleDesc = "Badanie identyfikujące osoby pijące w sposób ryzykowny i szkodliwy dla zdrowia, używane w terapii uzależnień alkoholowych",
                        ScaleTags = "Ogólne, Psychologia",
                        ScaleViewName = typeof(scale_AUDIT)},
                };

        }
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
