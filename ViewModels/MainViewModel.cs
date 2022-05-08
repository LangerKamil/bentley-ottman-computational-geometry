using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Models;
using Prism.Events;

namespace GeometriaObliczeniowa.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {

        #region Fields
        private readonly IEventAggregator eventAggregator;
        private SegmentsViewModel segmentsViewModel;
        private bool isRunning;
        #endregion

        #region Properties
        public SegmentsViewModel SegmentsViewModel
        {
            get => this.segmentsViewModel;
            set => SetProperty(ref this.segmentsViewModel, value);
        }

        public bool IsRunning
        {
            get => this.isRunning;
            set => SetProperty(ref this.isRunning, value);
        }

        public string ButtonText { get; set; }
        #endregion

        #region Constructors
        public MainViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            InitializeProperties();
            InitializeEvents();
        }
        #endregion

        #region Methods
        public override void InitializeProperties()
        {
            base.InitializingProperties = true;

            this.SegmentsViewModel = new SegmentsViewModel(eventAggregator, new SegmentsDTO());
            this.IsRunning = false;
            this.ButtonText = "Run";

            base.InitializingProperties = false;
        }

        public override void InitializeEvents()
        {
        }

        public override void Dispose()
        {
        }
        #endregion
    }
}
