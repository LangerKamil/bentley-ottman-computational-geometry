using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Controls.CoordinateSystem.Models;
using GeometriaObliczeniowa.Engines.Models;
using GeometriaObliczeniowa.ViewModels;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GeometriaObliczeniowa.Common.Resources;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.ViewModels
{
    public sealed class CoordinateSystemControlViewModel : ViewModelBase
    {

        #region Fields
        private readonly IEventAggregator eventAggregator;
        private CoordinateSystemElements coordinateSystemElements;
        private ObservableCollection<SegmentsViewModel> segmentsViewModel;
        private bool isSweeperRunning;
        private Point intersection;
        private Visibility isIntersectionPointVisable;
        private Visibility isCommonSegmentVisable;
        private Line commonSegment;

        #endregion

        #region Properties
        public ObservableCollection<SegmentsViewModel> SegmentsViewModel
        {
            get => this.segmentsViewModel;
            set => SetProperty(ref this.segmentsViewModel, value);
        }

        public CoordinateSystemElements CoordinateSystemElements
        {
            get => this.coordinateSystemElements;
            set => SetProperty(ref this.coordinateSystemElements, value);
        }

        public bool IsSweeperRunning
        {
            get => isSweeperRunning;
            set => SetProperty(ref isSweeperRunning, value);
        }

        public Point Intersection
        {
            get => this.intersection;
            set => SetProperty(ref this.intersection, value);
        }

        public Visibility IsIntersectionPointVisable
        {
            get => this.isIntersectionPointVisable;
            set => SetProperty(ref this.isIntersectionPointVisable, value);
        }

        public Line CommonSegment
        {
            get => this.commonSegment;
            set => SetProperty(ref this.commonSegment, value);
        }

        public Visibility IsCommonSegmentVisable
        {
            get => this.isCommonSegmentVisable;
            set => SetProperty(ref this.isCommonSegmentVisable, value);
        }
        #endregion

        #region Commands
        public ICommand OnSweeperCompletedCommand { get; set; }
        #endregion

        #region Constructors
        public CoordinateSystemControlViewModel(IEventAggregator eventAggregator)
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
            this.CoordinateSystemElements = new CoordinateSystemElements();
            this.SegmentsViewModel = new ObservableCollection<SegmentsViewModel>();
            this.IsSweeperRunning = false;
            this.Intersection = new Point(0, 0);
            this.HideIntersections();
        }

        public override void InitializeEvents()
        {
            this.PropertyChanged += base.OnPropertyChanged;
            this.HandlePropertyChangedMethod += this.OnHandlePropertyChanged;
            this.eventAggregator.GetEvent<ViewModelSendEvent>().Subscribe(OnViewModelReceived);
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Subscribe(RunSweeper);
            this.eventAggregator.GetEvent<EngineOutputSendEvent>().Subscribe(OnEngineOutputReceived);
        }

        public override void Dispose()
        {
        }

        public void StopSweeper()
        {
            this.IsSweeperRunning = false;
        }

        private void OnEngineOutputReceived(IntersectionEngineOutput output)
        {
            if (output.GetCommonPart() == null)
            {
                this.Intersection = output.GetCoorinates();
                this.IsIntersectionPointVisable = output.GetOutput() == Strings.No ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                this.CommonSegment = output.GetCommonPart();
                this.IsCommonSegmentVisable = Visibility.Visible;
            }
        }

        private void InitializeCommands()
        {
            this.OnSweeperCompletedCommand = new DelegateCommand(Execute, CanExecute);
        }

        private bool CanExecute()
        {
            return this.IsSweeperRunning;
        }

        private void Execute()
        {
            this.StopSweeper();
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Publish(false);
        }

        private List<string> OnHandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> changedProperties = new List<string>();

            changedProperties.Add(e.PropertyName);
            return changedProperties;
        }

        private void OnViewModelReceived(ViewModelBase viewModel)
        {
            if (viewModel != null && viewModel is SegmentsViewModel)
            {
                SegmentsViewModel segmentsViewModel = (SegmentsViewModel)viewModel;
                this.SegmentsViewModel.Clear();
                this.SegmentsViewModel.Add(segmentsViewModel);
                this.HideIntersections();
            }
        }

        private void RunSweeper(bool shouldSweeperRun)
        {
            if (shouldSweeperRun)
            {
                this.HideIntersections();
                this.CommonSegment = null;
                this.IsSweeperRunning = true;
            }
        }

        private void HideIntersections()
        {
            this.IsIntersectionPointVisable = Visibility.Hidden;
            this.IsCommonSegmentVisable = Visibility.Hidden;
        }
        #endregion
    }
}
