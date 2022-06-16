using GeometriaObliczeniowa.Controls.CoordinateSystem.ViewModels;
using System;
using System.Windows.Controls;

namespace GeometriaObliczeniowa.Controls.CoordinateSystem.Views
{
    public partial class CoordinateSystemControl : UserControl
    {
        #region Constructors
        public CoordinateSystemControl()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods   
        private void Timeline_OnCompleted(object sender, EventArgs e)
        {
            CoordinateSystemControlViewModel vm = this.DataContext as CoordinateSystemControlViewModel;
            vm.OnSweeperCompletedCommand.Execute(true);
        }
        #endregion

    }
}
