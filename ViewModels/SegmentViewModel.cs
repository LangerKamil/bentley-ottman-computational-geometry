using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Models;

namespace GeometriaObliczeniowa.ViewModels
{
    public sealed class SegmentViewModel : ViewModelBase
    {
        #region Fields
        private readonly SegmentDTO segmentDTO;
        private double startingPointX;
        private double startingPointY;
        private double endingPointX;
        private double endingPointY;
        #endregion

        #region Properties
        public int ID { get; set; }

        public double StartingPointX
        {
            get { return this.startingPointX; }
            set { SetProperty(ref this.startingPointX, value); }
        }

        public double StartingPointY
        {
            get { return this.startingPointY; }
            set { SetProperty(ref this.startingPointY, value); }
        }

        public double EndingPointX
        {
            get { return this.endingPointX; }
            set { SetProperty(ref this.endingPointX, value); }
        }

        public double EndingPointY
        {
            get { return this.endingPointY; }
            set { SetProperty(ref this.endingPointY, value); }
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
            this.StartingPointX = 0;
            this.StartingPointY = 0;
            this.EndingPointX = 0;
            this.EndingPointY = 0;
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
                this.segmentDTO.StartingPoint = new Point(this.StartingPointX, this.StartingPointY);
                this.segmentDTO.EndingPoint = new Point(this.EndingPointX, this.EndingPointY);
            }
        }

        public void UpdateViewModel()
        {
            base.InitializingProperties = true;
            this.StartingPointX = this.segmentDTO.StartingPoint.X;
            this.StartingPointY = this.segmentDTO.StartingPoint.Y;
            this.EndingPointX = this.segmentDTO.EndingPoint.X;
            this.EndingPointY = this.segmentDTO.EndingPoint.Y;
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
