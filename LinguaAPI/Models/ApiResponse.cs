
using System.Text.Json.Serialization;

namespace LinguaAPI.Models
{
    public enum StatusEnum
    {
        OK = 1,
        WARNING = 2,
        ERROR = 3
    }

    /// <summary>
    /// Base klasa za vraćanje odgovora (response) iz kontrolera.
    /// </summary>
    /// <typeparam name="T">Generički tip podatka koje vraća response.</typeparam>
    public class ApiResponse<T>
    {
        public StatusEnum Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public bool ShowPopup { get; set; }

        public ApiResponse(StatusEnum status, string message, T data, bool showPopup)
        {
            Status = status;
            Message = message;
            Data = data;
            ShowPopup = showPopup;
        }
    }


    #region OK RESPONSES
    /// <summary>
    /// Base klasa za vraćanje OK odgovora (response) iz kontrolera.
    /// </summary>
    /// <typeparam name="T">Generički tip podatka koje vraća response.</typeparam>
    public class OkResponse<T> : ApiResponse<T>
    {
        public OkResponse(string message, T data) : base(StatusEnum.OK, message, data, false)
        {

        }
    }


    /// <summary>
    /// Base klasa za vraćanje OK odgovora (response) iz kontrolera. Automatski prikazuje popup na frontendu.
    /// </summary>
    /// <typeparam name="T">Generički tip podatka koje vraća response.</typeparam>
    public class OkResponseWithDialog<T> : ApiResponse<T>
    {
        public OkResponseWithDialog(string message, T data) : base(StatusEnum.OK, message, data, true)
        {
        }
    }
    #endregion

    #region WARNING RESPONSES
    /// <summary>
    /// Base klasa za vraćanje Warning odgovora (response) iz kontrolera.
    /// </summary>
    /// <typeparam name="T">Generički tip podatka koje vraća response.</typeparam>
    public class WarningResponse<T> : ApiResponse<T>
    {
        public WarningResponse(string message, T data) : base(StatusEnum.WARNING, message, data, false)
        {

        }
    }

    /// <summary>
    /// Base klasa za vraćanje Warning odgovora (response) iz kontrolera.  Automatski prikazuje popup na frontendu.
    /// </summary>
    /// <typeparam name="T">Generički tip podatka koje vraća response.</typeparam>
    public class WarningResponseWithDialog<T> : ApiResponse<T>
    {
        public WarningResponseWithDialog(string message, T data) : base(StatusEnum.WARNING, message, data, true)
        {

        }
    }
    #endregion

    #region ERROR RESPONSES
    /// <summary>
    /// Base klasa za vraćanje Error odgovora (response) iz kontrolera.
    /// </summary>
    /// <typeparam name="T">Generički tip podatka koje vraća response.</typeparam>
    public class ErrorResponse<T> : ApiResponse<T>
    {
        public ErrorResponse(string message, T data) : base(StatusEnum.ERROR, message, data, false)
        {

        }
    }

    /// <summary>
    /// Base klasa za vraćanje Error odgovora (response) iz kontrolera. Automatski prikazuje popup na frontendu.
    /// </summary>
    /// <typeparam name="T">Generički tip podatka koje vraća response.</typeparam>
    public class ErrorResponseWithDialog<T> : ApiResponse<T>
    {
        public ErrorResponseWithDialog(string message, T data) : base(StatusEnum.ERROR, message, data, true)
        {

        }
    }
    #endregion
}
