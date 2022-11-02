
namespace Homemade.BLL
{
    public class Swal
    {
        public Swal(bool isConfirm = true)
        {
            IsConfirm = isConfirm;
        }

        public Swal(SwalActions action , bool isConfirm = true)
        {
            IsConfirm = isConfirm;
            SwalActions = action;
        }

        public Swal(SwalActions action , bool showOkButton = true , bool showCancelButton = true , bool isConfirm = true)
        {
            ShowCancelButton = showCancelButton;
            ShowOkButton = showOkButton;
            IsConfirm = isConfirm;
            SwalActions = action;
        }

        /// <summary>
        /// Swal 
        /// </summary>
        /// <param name="action">action add m delete , ......</param>
        /// <param name="callBack">call back javascript function default id confirmSwal()</param>
        public Swal(SwalActions action , string callBack , bool showOkButton = true , bool showCancelButton = true , bool isConfirm = true)
        {
            ShowCancelButton = showCancelButton;
            ShowOkButton = showOkButton;
            IsConfirm = isConfirm;
            SwalActions = action;
            CallBack = callBack;
        }

        public SwalActions SwalActions { get; set; } = SwalActions.Add;

        /// <summary>
        /// Confirm Funcrion Of Swal
        ///  confirmSwal() 
        /// </summary>
        public string CallBack { get; set; } = "confirmSwal()";

        public string Text { get; set; } = "";

        public bool ShowOkButton { get; set; } = true;

        public bool ShowCancelButton { get; set; } = true;

        public bool CloseOnConfirm { get; set; } = false;

        public bool CloseOnCancel { get; set; } = false;

        public string ConfirmButtonClass { get; set; } = "";

        public bool IsConfirm { get; set; }

        public string Title { get; set; } = "";

        public int Timer { get; set; } = 500;
    }
}
