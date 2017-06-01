using System;

namespace BaselineSolution.Framework.Response
{
    /// <summary>
    /// Bevat de error informatie
    /// Indien het om een validatie error ging zal de property 'Property' het veld bevatten waarop de validatie gefaald is 
    /// De property 'Message' bevat de error
    /// De property 'ExtraInfo' kan vanalles bevatten 
    /// </summary>
    public class Message
    {
        public Message()
        {
            Type = MessageType.Error;
        }

        public string MessageText { get; set; }

        public MessageType Type { get; set; }

        public override string ToString()
        {
            return $"Property \t Message \t {MessageText}";
        }

        public Exception Exception { get; set; }

    }
}
