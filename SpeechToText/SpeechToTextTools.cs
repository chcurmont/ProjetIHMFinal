using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.SpeechRecognition;
using ProjetIHM;

namespace SpeechToText
{
    public class SpeechToTextTools
    {
        private const string _subscriptionKey = "2d4d2895e4794ed29a189c66229aab7e";
        public string SubscriptionKey
        {
            get
            {
                return _subscriptionKey;
            }
        }

        private MicrophoneRecognitionClientWithIntent _micClient;
        public MicrophoneRecognitionClientWithIntent MicClient
        {
            get
            {
                return _micClient;
            }
            set
            {
                _micClient = value;
            }
        }

        public SpeechToTextTools()
        {
            MicClient=SpeechRecognitionServiceFactory.CreateMicrophoneClientWithIntent("fr-FR",SubscriptionKey,)
        }
    }
}
