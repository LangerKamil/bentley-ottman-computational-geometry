using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GeometriaObliczeniowa.Common.BaseClasses;
using GeometriaObliczeniowa.Common.Events;
using GeometriaObliczeniowa.Common.Extensions;
using GeometriaObliczeniowa.Models;
using Prism.Mvvm;

namespace GeometriaObliczeniowa.View.MainView
{
    public sealed class SegmentsViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<SegmentViewModel> segments;
        private readonly SegmentsDTO segmentsDTO;
        #endregion

        #region Properties
        public ObservableCollection<SegmentViewModel> Segments
        {
            get { return this.segments; }
            set { SetProperty(ref this.segments, value); }
        }
        #endregion

        #region Constructors
        public SegmentsViewModel(SegmentsDTO segmentsDTO)
        {
            this.segmentsDTO = segmentsDTO;
            this.InitializeProperties();
            this.InitializeEvents();
        }
        #endregion

        #region Methods
        public override void InitializeProperties()
        {
            base.InitializingProperties = true;
            this.Segments = new ObservableCollection<SegmentViewModel>()
            {
                new SegmentViewModel(new SegmentDTO()),
                new SegmentViewModel(new SegmentDTO())
            };
            base.InitializingProperties = false;
        }

        public override void InitializeEvents()
        {
            this.PropertyChanged += base.OnPropertyChanged;
            this.HandlePropertyChangedMethod += this.OnHandlePropertyChanged;
            this.Segments.ForEach(c => c.ParentUpdateHandler += this.OnUpdateRequested);
        }

        public void UpdateBaseModel(SegmentsDTO value = null)
        {
            if (value != null)
            {
                this.segmentsDTO.UpdateBaseModel(value);
                this.UpdateViewModel();
            }
            else
            {
                MapDTOtoVM(this.Segments, this.segmentsDTO);
            }
        }

        public void UpdateViewModel()
        {
            base.InitializingProperties = true;

            MapVMtoDTO(this.Segments, this.segmentsDTO);

            base.InitializingProperties = false;
        }

        private List<string> OnHandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> changedProperties = new List<string>();

            changedProperties.Add(e.PropertyName);
            this.UpdateBaseModel();
            return changedProperties;
        }

        public override void Dispose()
        {
        }

        private void OnUpdateRequested(object sender, ParentUpdateEventArgs args)
        {
            this.UpdateBaseModel();
            base.InvokeParentUpdate(sender, args);
        }

        public void MapDTOtoVM(ObservableCollection<SegmentViewModel> vmCollection, SegmentsDTO segmentsDTO)
        {
            segmentsDTO.Segments.ForEach(c =>
                vmCollection.ForEach(x => x.StartingPoint = c.StartingPoint));
            segmentsDTO.Segments.ForEach(c =>
                vmCollection.ForEach(x => x.EndingPoint = c.EndingPoint));
        }

        public void MapVMtoDTO(ObservableCollection<SegmentViewModel> vmCollection, SegmentsDTO segmentsDTO)
        {
            vmCollection.ForEach(c =>
                segmentsDTO.Segments.ForEach(x => x.StartingPoint = c.StartingPoint));
            vmCollection.ForEach(c =>
                segmentsDTO.Segments.ForEach(x => x.EndingPoint = c.EndingPoint));
        }
        #endregion
    }
}
