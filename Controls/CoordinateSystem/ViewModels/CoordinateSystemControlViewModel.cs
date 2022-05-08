using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Controls.CoordinateSystem.Models;
using GeometriaObliczeniowa.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.ViewModels
{
    public class CoordinateSystemControlViewModel : ViewModelBase
    {

        #region Fields
        private readonly IEventAggregator eventAggregator;
        private CoordinateSystemElements coordinateSystemElements;
        private ObservableCollection<SegmentsViewModel> segmentsViewModel;
        private bool isSweeperRunning;
        #endregion

        #region Properties
        public ObservableCollection<SegmentsViewModel> SegmentsViewModel
        {
            get { return this.segmentsViewModel; }
            set { SetProperty(ref this.segmentsViewModel, value); }
        }

        public CoordinateSystemElements CoordinateSystemElements
        {
            get { return this.coordinateSystemElements; }
            set { SetProperty(ref this.coordinateSystemElements, value); }
        }

        public bool IsSweeperRunning
        {
            get { return isSweeperRunning; }
            set { SetProperty(ref isSweeperRunning, value); }
        }
        #endregion

        #region Commands
        public ICommand DrawSegmentsCommand { get; private set; }
        #endregion

        #region Constructors
        public CoordinateSystemControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.InitializeProperties();
            this.InitializeEvents();
        }
        #endregion

        #region Methods
        public override void InitializeProperties()
        {
            this.CoordinateSystemElements = new CoordinateSystemElements();
            this.SegmentsViewModel = new ObservableCollection<SegmentsViewModel>();
            this.IsSweeperRunning = false;
        }

        public override void InitializeEvents()
        {
            this.PropertyChanged += base.OnPropertyChanged;
            this.HandlePropertyChangedMethod += this.OnHandlePropertyChanged;
            this.eventAggregator.GetEvent<ViewModelSendEvent>().Subscribe(OnViewModelReceived);
            this.eventAggregator.GetEvent<IsSweeperRunnigEvent>().Subscribe(RunSweeper);
        }


        public override void Dispose()
        {
            throw new NotImplementedException();
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
            }
        }

        private void RunSweeper(bool shouldSweeperRun)
        {
            if (shouldSweeperRun)
            {
                this.IsSweeperRunning = true;
            }
        }
        #endregion
    }
}
