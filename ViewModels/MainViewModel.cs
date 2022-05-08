using System.Windows.Input;
using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Models;
using Prism.Commands;
using Prism.Events;

namespace GeometriaObliczeniowa.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {

        #region Fields
        private readonly IEventAggregator eventAggregator;
        private SegmentsViewModel segmentsViewModel;
        private bool isSweeperAvailable;
        private string buttonText;
        #endregion

        #region Properties
        public SegmentsViewModel SegmentsViewModel
        {
            get => this.segmentsViewModel;
            set => SetProperty(ref this.segmentsViewModel, value);
        }

        public bool IsSweeperAvailable
        {
            get => this.isSweeperAvailable;
            set => SetProperty(ref this.isSweeperAvailable, value);
        }

        public string ButtonText
        {
            get { return buttonText; }
            set { SetProperty(ref buttonText, value); }
        }
        #endregion

        #region Commands
        public ICommand RunSweeperCommand { get; set; }
        #endregion

        #region Constructors
        public MainViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.InitializeProperties();
            this.InitializeEvents();
            this.InitializeCommands();
        }
        #endregion

        #region Methods
        public override void InitializeProperties()
        {
            base.InitializingProperties = true;

            this.SegmentsViewModel = new SegmentsViewModel(eventAggregator, new SegmentsDTO());
            this.IsSweeperAvailable = true;
            this.ButtonText = "Run";

            base.InitializingProperties = false;
        }

        public override void InitializeEvents()
        {
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Subscribe(OnSweeperStopped);
        }

        private void OnSweeperStopped(bool obj)
        {
            if (!obj)
            {
                this.IsSweeperAvailable = true;
                this.ButtonText = "Run";
            }
        }

        public override void Dispose()
        {
        }
        private void InitializeCommands()
        {
            this.RunSweeperCommand = new DelegateCommand(Execute, CanExecute);
        }

        private bool CanExecute()
        {
            return this.IsSweeperAvailable;
        }

        private void Execute()
        {
            this.IsSweeperAvailable = false;
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Publish(true);
            this.ButtonText = "Running";
        }
        #endregion
    }
}
