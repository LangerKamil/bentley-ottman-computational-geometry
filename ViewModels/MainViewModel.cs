using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Engines.Interface;
using GeometriaObliczeniowa.Engines.Models;
using GeometriaObliczeniowa.Models;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace GeometriaObliczeniowa.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {

        #region Fields
        private readonly IEventAggregator eventAggregator;
        private readonly IIntersectionEngine intersectionEngine;
        private SegmentsViewModel segmentsViewModel;
        private bool isSweeperAvailable;
        private string buttonText;
        private string intersection;
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
            get { return this.buttonText; }
            set { SetProperty(ref this.buttonText, value); }
        }

        public string Intersection
        {
            get { return this.intersection; }
            set { SetProperty(ref this.intersection, value); }
        }
        #endregion

        #region Commands
        public ICommand RunSweeperCommand { get; set; }
        #endregion

        #region Constructors
        public MainViewModel(IEventAggregator eventAggregator,
            IIntersectionEngine intersectionEngine)
        {
            this.eventAggregator = eventAggregator;
            this.intersectionEngine = intersectionEngine;
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

        private void OnSweeperStopped(bool isRunning)
        {
            if (!isRunning)
            {
                IntersectionEngineOutput engineOutput = this.intersectionEngine.FindIntersection(
                    new IntersectionEngineInput(this.SegmentsViewModel.Segments));
                this.Intersection = engineOutput.GetOutput();
                this.eventAggregator.GetEvent<EngineOutputSendEvent>().Publish(engineOutput.GetCoorinates());
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
            this.Intersection = "";
            this.IsSweeperAvailable = false;
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Publish(true);
            this.ButtonText = "Running";

        }
        #endregion
    }
}
