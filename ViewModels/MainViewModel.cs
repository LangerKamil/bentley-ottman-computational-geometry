using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Common.Extensions;
using GeometriaObliczeniowa.Engines;
using GeometriaObliczeniowa.Engines.Interface;
using GeometriaObliczeniowa.Models;
using Prism.Commands;
using Prism.Events;

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

        private void OnSweeperStopped(bool obj)
        {
            if (!obj)
            {
                this.Intersection = this.intersectionEngine.Intersection(GenerateEngineInput());
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
            this.Intersection = String.Empty;
            this.IsSweeperAvailable = false;
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Publish(true);
            this.ButtonText = "Running";

        }

        private IntersectionEngineInput GenerateEngineInput()
        {
            List<Point> lineCoords = new List<Point>();
            this.SegmentsViewModel.Segments.ForEach(x =>
            {
                lineCoords.Add(new Point(x.StartingPointX, x.StartingPointY));
                lineCoords.Add(new Point(x.EndingPointX, x.EndingPointY));
            });
            return new IntersectionEngineInput(lineCoords);
        }
        #endregion
    }
}
