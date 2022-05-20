

namespace Sanator
{
    public interface IDialogService
    {
        void ShowMessage(string v);
        bool ShowMessageOKCancel(string v);
        string FilePath { get; set; }

        bool SaveFileDialog();
    }
}