using System.Collections.Generic;
using System.ComponentModel;
using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Models;

namespace GeometriaObliczeniowa.View.MainView
{
    public sealed class MainViewModel : ViewModelBase
    {
        #region Fields
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
        public MainViewModel()
        {
            InitializeProperties();
            InitializeEvents();
        }
        #endregion

        #region Methods
        public override void InitializeProperties()
        {
            base.InitializingProperties = true;

            this.SegmentsViewModel = new SegmentsViewModel(new SegmentsDTO());
            this.IsRunning = false;
            this.ButtonText = "Run";

            base.InitializingProperties = false;
        }

        public override void InitializeEvents()
        {
            this.PropertyChanged += base.OnPropertyChanged;
            //base.HandlePropertyChangedMethod += this.OnHandlePropertyChanged;
            this.SegmentsViewModel.ParentUpdateHandler += this.OnUpdateRequested;
        }


        public override void Dispose()
        {
            //if (this.SegmentsViewModel != null)
            //{
            //    this.SegmentsViewModel.ParentUpdateHandler -= this.OnUpdateRequested;
            //    this.SegmentsViewModel.Dispose();
            //}
            //this.PropertyChanged -= base.OnPropertyChanged;
        }

        public void ChangeButtonName()
        {
            if (!this.IsRunning)
            {
                this.ButtonText = "Stop";
                this.IsRunning = true;
            }
            else
            {
                this.ButtonText = "Run";
                this.IsRunning = false;
            }
        }

        private List<string> OnHandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> changedProperties = new List<string>();
            changedProperties.Add(e.PropertyName);

            return changedProperties;
        }

        private void OnUpdateRequested(object sender, ParentUpdateEventArgs args)
        {
            base.InvokeParentUpdate(sender, args);
        }
        #endregion
    }
}
