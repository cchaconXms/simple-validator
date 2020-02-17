using System;

namespace XMSBS.SimpleValidator.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        private static string mensajeBase = "Una o más condiciones no se han cumplido";

        public string[] Messages { get; set; }
        public string FormatedMessages { get; set; }

        public BusinessException() : base(mensajeBase)
        {
        }

        public BusinessException(string[] messages) : base(mensajeBase)
        {
            this.Messages = messages;
            transformMessages();
        }

        public BusinessException(string message)
            : base(message)
        {
            this.FormatedMessages = message;
        }

        public BusinessException(string message, Exception inner)
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
