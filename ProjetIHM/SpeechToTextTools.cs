using Microsoft.ProjectOxford.SpeechRecognition;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIHM
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

        private List<string> _recognizeText;
        public List<string> RecognizeText
        {
            get
            {
                return _recognizeText;
            }
            set
            {
                _recognizeText = value;
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

        private bool _error;
        public bool Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
            }
        }

        private bool _isMicroUse;
        public bool IsMicroUse
        {
            get
            {
                return _isMicroUse;
            }
            set
            {
                _isMicroUse = value;
            }
        }

        public SpeechToTextTools()
        {
#pragma warning disable CS0612 // Le type ou le membre est obsolète
            MicClient = SpeechRecognitionServiceFactory.CreateMicrophoneClientWithIntent("fr-FR", SubscriptionKey, ConfigurationManager.AppSettings["LuisAppID"], ConfigurationManager.AppSettings["luisSubscriptionID"]);
#pragma warning restore CS0612 // Le type ou le membre est obsolète
            MicClient.OnIntent += onIntentHandler;
            MicClient.OnResponseReceived += OnMicShortPhraseReceiveHandler;
            MicClient.OnPartialResponseReceived += OnPartialReponseReceiveHandler;
            MicClient.OnConversationError += OnConversationErrorHandler;
            Error = false;
            IsMicroUse = false;
            RecognizeText = new List<string>();
        }

        public void start()
        {
            IsMicroUse = true;
            MicClient.StartMicAndRecognition();
        }

        private void onIntentHandler(object sender, SpeechIntentEventArgs e)
        {
            Console.WriteLine("{0}", e.Payload);
        }

        private void OnConversationErrorHandler(object sender,SpeechErrorEventArgs e)
        {
            RecognizeText.Add(e.SpeechErrorText);
            RecognizeText.Add(e.SpeechErrorCode.ToString());
            Error = true;
        }

        private void OnMicShortPhraseReceiveHandler(object sender,SpeechResponseEventArgs e)
        {
            MicClient.EndMicAndRecognition();
            IsMicroUse = false;
            for(int i = 0; i < e.PhraseResponse.Results.Length; i++)
            {
                RecognizeText.Add(e.PhraseResponse.Results[i].DisplayText);
            }
        }

        private void OnPartialReponseReceiveHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            RecognizeText.Add(e.PartialResult);
        }
    }
}
