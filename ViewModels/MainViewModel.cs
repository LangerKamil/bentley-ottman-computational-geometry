using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Engines.Interface;
using GeometriaObliczeniowa.Engines.Models;
using GeometriaObliczeniowa.Models;
using Prism.Commands;
using Prism.Events;
using System;
using System.Windows;
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
        private string coordinates;
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

        public string Coordinates
        {
            get { return this.coordinates; }
            set { SetProperty(ref this.coordinates, value); }
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
            this.SegmentsViewModel.ParentUpdateHandler += this.OnUpdateRequested;
        }

        private void OnUpdateRequested(object sender, ParentUpdateEventArgs e)
        {
            this.Intersection = "";
            this.Coordinates = "";
        }

        private void OnSweeperStopped(bool isRunning)
        {
            if (!isRunning)
            {
                IntersectionEngineOutput engineOutput = this.intersectionEngine.FindIntersection(
                    new IntersectionEngineInput(this.SegmentsViewModel.Segments));
                Point result = engineOutput.GetCoorinates();
                var line = engineOutput.GetCommonPart();
                this.Intersection = engineOutput.GetOutput();

                if (line != null)
                {
                    this.Coordinates =
                        $"X: {Math.Round(line.Left.X)},Y: {Math.Round(line.Left.Y)} | X: {Math.Round(line.Right.X)}, Y: {Math.Round(line.Right.Y)}";
                }
                else
                {
                    this.Coordinates = this.Intersection == "TAK" ? $"X: {Math.Round(result.X)} Y: {Math.Round(result.Y)}" : "-";
                }

                this.eventAggregator.GetEvent<EngineOutputSendEvent>().Publish(engineOutput);
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
            this.Coordinates = "";
            this.IsSweeperAvailable = false;
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Publish(true);
            this.ButtonText = "Running";
        }
        #endregion
    }
}
