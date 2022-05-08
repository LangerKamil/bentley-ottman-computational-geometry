using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Controls.CoordinateSystem.Models;
using GeometriaObliczeniowa.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using GeometriaObliczeniowa.Models;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.ViewModels
{
    public class CoordinateSystemControlViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;

        #region Fields
        private ObservableCollection<CoordinateSystemElementsViewModel> canvasElements;
        private bool isSweepRunning;
        #endregion

        #region Properties
        public ObservableCollection<CoordinateSystemElementsViewModel> CanvasElements
        {
            get { return this.canvasElements; }
            set { SetProperty(ref this.canvasElements, value); }
        }

        public bool IsSweepRunning
        {
            get { return isSweepRunning; }
            set { SetProperty(ref isSweepRunning, value); }
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
            this.CanvasElements = new ObservableCollection<CoordinateSystemElementsViewModel>();
            this.IsSweepRunning = false;
        }

        public override void InitializeEvents()
        {
            this.PropertyChanged += base.OnPropertyChanged;
            this.HandlePropertyChangedMethod += this.OnHandlePropertyChanged;
            this.eventAggregator.GetEvent<ViewModelSendEvent>().Subscribe(OnViewModelReceived);
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
                this.CanvasElements.Clear();
                this.CanvasElements.Add(new CoordinateSystemElementsViewModel(segmentsViewModel.Segments));

            }
        }

        #endregion
    }
}
