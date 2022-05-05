using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace GeometriaObliczeniowa.View.MainView
{
    public sealed class SegmentViewModel : ViewModelBase
    {
        #region Fields
        private readonly SegmentDTO segmentDTO;
        private Point startingPoint;
        private Point endingPoint;
        #endregion

        #region Properties
        public Point StartingPoint
        {
            get { return this.startingPoint; }
            set { SetProperty(ref this.startingPoint, value); }
        }

        public Point EndingPoint
        {
            get { return endingPoint; }
            set { SetProperty(ref endingPoint, value); }
        }
        #endregion

        #region Constructors
        public SegmentViewModel(SegmentDTO segmentDTO)
        {
            this.segmentDTO = segmentDTO;
            this.InitializeProperties();
            this.InitializeEvents();
        }
        #endregion

        #region Methods
        public override void InitializeProperties()
        {
            base.InitializingProperties = true;
            this.StartingPoint = new Point(0, 0);
            this.EndingPoint = new Point(0, 0);
            base.InitializingProperties = false;
        }

        public override void InitializeEvents()
        {
            this.PropertyChanged += base.OnPropertyChanged;
            base.HandlePropertyChangedMethod += this.OnHandlePropertyChanged;
        }

        public override void Dispose()
        {
            this.PropertyChanged -= base.OnPropertyChanged;
        }

        public void UpdateBaseModel(SegmentDTO value = null)
        {
            if (value != null)
            {
                this.segmentDTO.UpdateBaseModel(value);
                this.UpdateViewModel();
            }
            else
            {
                this.segmentDTO.StartingPoint = this.StartingPoint;
                this.segmentDTO.EndingPoint = this.EndingPoint;
            }
        }

        public void UpdateViewModel()
        {
            base.InitializingProperties = true;
            this.StartingPoint = this.segmentDTO.StartingPoint;
            this.EndingPoint = this.segmentDTO.EndingPoint;
            base.InitializingProperties = false;
        }

        private List<string> OnHandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> changedProperties = new List<string>();

            changedProperties.Add(e.PropertyName);
            this.UpdateBaseModel();
            return changedProperties;
        }

        #endregion
    }
}
