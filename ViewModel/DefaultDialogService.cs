using Microsoft.Win32;
using System;

namespace Sanator
{
    internal class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool SaveFileDialog()
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
                FilePath = dialog.FileName;
            else
                FilePath = null;
            return FilePath != null;
        }

        public void ShowMessage(string v)
        {
            System.Windows.MessageBox.Show(v);
        }

        public bool ShowMessageOKCancel(string v)
        {
            return System.Windows.MessageBox.Show(v, "Запрос", System.Windows.MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK;
        }
    }
}