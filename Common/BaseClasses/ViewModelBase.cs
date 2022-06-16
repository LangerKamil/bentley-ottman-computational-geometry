using GeometriaObliczeniowa.Common.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GeometriaObliczeniowa.Common.BaseClasses
{
    public abstract class ViewModelBase : BindableBase
    {
        #region Properties
        protected HandlePropertyChanged HandlePropertyChangedMethod { get; set; }

        protected bool InitializingProperties { get; set; }
        #endregion

        #region Delegates
        protected delegate List<string> HandlePropertyChanged(object sender, PropertyChangedEventArgs e);
        #endregion

        #region Events
        public event EventHandler<ParentUpdateEventArgs> ParentUpdateHandler;
        #endregion

        #region Methods
        public abstract void InitializeProperties();

        public abstract void InitializeEvents();

        public abstract void Dispose();

        protected virtual void InvokeParentUpdate(object sender, ParentUpdateEventArgs eventArgs = null)
        {
            if (this.ParentUpdateHandler != null)
            {
                this.ParentUpdateHandler.Invoke(sender, eventArgs);
            }
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!this.InitializingProperties)
            {
                List<string> changedProperties = new List<string>();

                try
                {
                    this.InitializingProperties = true;

                    if (this.HandlePropertyChangedMethod != null)
                    {
                        changedProperties = this.HandlePropertyChangedMethod(sender, e);
                    }

                    if (changedProperties.Count > 0)
                    {
                        this.InvokeParentUpdate(this, new ParentUpdateEventArgs(changedProperties));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    this.InitializingProperties = false;
                }
            }
        }
        #endregion
    }
}
