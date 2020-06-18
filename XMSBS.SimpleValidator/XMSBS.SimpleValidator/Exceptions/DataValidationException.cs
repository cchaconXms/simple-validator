using System;

namespace XMSBS.SimpleValidator.Exceptions
{
    [Serializable]
    public class DataValidationException : Exception
    {
        private static string mensajeBase = "Uno o más parámetros son incorrectos";

        public string[] Messages { get; set; }
        public string FormatedMessages { get; set; }

        public DataValidationException() : base(mensajeBase)
        {
        }

        public DataValidationException(string[] messages) : base(mensajeBase)
        {
            this.Messages = messages;
            transformMessages();
        }

        public DataValidationException(string message)
            : base(message)
        {
            this.FormatedMessages = message;
        }

        public DataValidationException(string message, Exception inner)
            : base(message, inner)
        {
            this.FormatedMessages = message;
        }

        private void transformMessages()
        {

            this.FormatedMessages = "";

            foreach (var item in Messages)
            {
                FormatedMessages += item + Environment.NewLine;
            }


        }
    }
}
