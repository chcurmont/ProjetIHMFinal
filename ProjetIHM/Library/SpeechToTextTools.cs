using Microsoft.ProjectOxford.SpeechRecognition;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
    public class SpeechToTextTools
    {
        private string _primarySubscriptionKey = ConfigurationManager.AppSettings["primaryKey"];
        public string PrimarySubscriptionKey
        {
            get
            {
                return _primarySubscriptionKey;
            }
        }

        private string _secondarySubscriptionKey = ConfigurationManager.AppSettings["secondaryKey"];
        public string SecondarySubscriptionKey
        {
            get
            {
                return _secondarySubscriptionKey;
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

        private MicrophoneRecognitionClient _micClient;
        public MicrophoneRecognitionClient MicClient
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
            Error = false;
            IsMicroUse = false;
            RecognizeText = new List<string>();
        }

        public void start()
        {
            IsMicroUse = true;
            MicClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(SpeechRecognitionMode.ShortPhrase, "fr-FR", PrimarySubscriptionKey, SecondarySubscriptionKey);
            MicClient.OnIntent += onIntentHandler;
            MicClient.OnResponseReceived += OnMicShortPhraseReceiveHandler;
            MicClient.OnPartialResponseReceived += OnPartialReponseReceiveHandler;
            MicClient.OnConversationError += OnConversationErrorHandler;
            MicClient.StartMicAndRecognition();
        }

        private void onIntentHandler(object sender, SpeechIntentEventArgs e)
        {
            RecognizeText.Add(e.Payload);
            MicClient.EndMicAndRecognition();
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
